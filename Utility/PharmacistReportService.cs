using IbhayiPharmacy.Data;
using IbhayiPharmacy.Models;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IbhayiPharmacy.Utility
{
    public class PharmacistReportService
    {
        private readonly ApplicationDbContext _context;

        public PharmacistReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Helper class for report data - NOT a database entity
        public class ReportDataItem
        {
            public DateTime DispensedDate { get; set; }
            public string MedicationName { get; set; } = string.Empty;
            public int Quantity { get; set; }
            public string Instructions { get; set; } = string.Empty;
            public string CustomerName { get; set; } = string.Empty;
            public string Schedule { get; set; } = string.Empty;
            public string GroupKey { get; set; } = string.Empty;
            public int ItemPrice { get; set; } // Added price field
            public int LineTotal { get; set; } // Added line total field
        }

        // Get dispensed medications data WITH PRICING - FIXED QUERY
        public async Task<List<ReportDataItem>> GetDispensedMedicationsAsync(DateTime startDate, DateTime endDate, int pharmacistId, string groupBy)
        {
            // Simplified query to avoid the ThenInclude issue
            var orderLines = await _context.OrderLines
                .Where(ol => ol.Status == "Dispensed" || ol.Status == "Completed")
                .Where(ol => ol.Order != null && ol.Order.OrderDate >= startDate && ol.Order.OrderDate <= endDate)
                .Where(ol => ol.Order != null && ol.Order.PharmacistID == pharmacistId)
                .Include(ol => ol.Order)
                    .ThenInclude(o => o.Customer)
                        .ThenInclude(c => c.ApplicationUser)
                .Include(ol => ol.Medications)
                .Include(ol => ol.ScriptLine)
                .ToListAsync();

            // Get prescription data separately to avoid complex ThenInclude chains
            var prescriptionIds = orderLines
                .Where(ol => ol.ScriptLine != null)
                .Select(ol => ol.ScriptLine.PrescriptionID)
                .Distinct()
                .ToList();

            var prescriptions = await _context.Prescriptions
                .Where(p => prescriptionIds.Contains(p.PrescriptionID))
                .Include(p => p.ApplicationUser)
                .ToDictionaryAsync(p => p.PrescriptionID, p => p);

            // Then transform to ReportDataItem with null checks and pricing
            var dispensedData = orderLines.Select(ol =>
            {
                var medicationName = ol.Medications?.MedicationName ?? "Unknown Medication";
                var instructions = ol.ScriptLine?.Instructions ?? "No instructions";

                // Get customer name from prescription data
                string customerName = "Unknown Customer";
                if (ol.ScriptLine != null && prescriptions.TryGetValue(ol.ScriptLine.PrescriptionID, out var prescription))
                {
                    customerName = $"{prescription.ApplicationUser?.Name} {prescription.ApplicationUser?.Surname}";
                }
                else if (ol.Order?.Customer?.ApplicationUser != null)
                {
                    // Fallback to order customer data
                    customerName = $"{ol.Order.Customer.ApplicationUser.Name} {ol.Order.Customer.ApplicationUser.Surname}";
                }

                var schedule = ol.Medications?.Schedule ?? "Unknown Schedule";
                var dispensedDate = ol.Order?.OrderDate ?? DateTime.MinValue;
                var itemPrice = ol.ItemPrice;
                var lineTotal = itemPrice * ol.Quantity;

                // Determine group key based on grouping option
                string groupKey = groupBy.ToLower() switch
                {
                    "customer" => customerName,
                    "medication" => medicationName,
                    "schedule" => schedule,
                    _ => customerName
                };

                return new ReportDataItem
                {
                    DispensedDate = dispensedDate,
                    MedicationName = medicationName,
                    Quantity = ol.Quantity,
                    Instructions = instructions,
                    CustomerName = customerName,
                    Schedule = schedule,
                    GroupKey = groupKey,
                    ItemPrice = itemPrice,
                    LineTotal = lineTotal
                };
            })
            .OrderBy(x => x.GroupKey)
            .ThenBy(x => x.DispensedDate)
            .ToList();

            return dispensedData;
        }

        // Generate PDF document WITH PRICING COLUMNS
        public byte[] GeneratePDF(List<ReportDataItem> data, string groupBy, DateTime startDate, DateTime endDate, string pharmacistName)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);

                    // Header
                    page.Header().Element(c => ComposeHeader(c, startDate, endDate, pharmacistName));

                    // Content
                    page.Content().Element(c => ComposeContent(c, data, groupBy));

                    // Footer
                    page.Footer().Element(ComposeFooter);
                });
            });

            return document.GeneratePdf();
        }

        private void ComposeHeader(IContainer container, DateTime startDate, DateTime endDate, string pharmacistName)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"PRESCRIPTIONS DISPENSED - {pharmacistName.ToUpper()}")
                        .FontSize(16).Bold();

                    column.Item().Text($"{startDate:dd/MM/yyyy} – {endDate:dd/MM/yyyy}")
                        .FontSize(12);

                    column.Item().PaddingBottom(10).LineHorizontal(1);
                });
            });
        }

        private void ComposeContent(IContainer container, List<ReportDataItem> data, string groupBy)
        {
            container.Column(column =>
            {
                if (!data.Any())
                {
                    column.Item().Text("No dispensed medications found for the selected period.")
                        .FontSize(12).Italic();
                    return;
                }

                var groupedData = data.GroupBy(x => x.GroupKey);
                var grandTotalQuantity = data.Sum(x => x.Quantity);
                var grandTotalValue = data.Sum(x => x.LineTotal);

                foreach (var group in groupedData)
                {
                    // Group header
                    column.Item().PaddingTop(10).Text($"{GetGroupTitle(groupBy)}: {group.Key}")
                        .FontSize(14).Bold();

                    // Table
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2); // Date
                            columns.RelativeColumn(2); // Medication
                            columns.RelativeColumn(1); // Qty
                            columns.RelativeColumn(2); // Unit Price
                            columns.RelativeColumn(2); // Line Total
                            columns.RelativeColumn(3); // Instructions
                        });

                        // Table header
                        table.Header(header =>
                        {
                            header.Cell().Text("Date").Bold();
                            header.Cell().Text("Medication").Bold();
                            header.Cell().Text("Qty").Bold();
                            header.Cell().Text("Unit Price").Bold();
                            header.Cell().Text("Line Total").Bold();
                            header.Cell().Text("Instructions").Bold();
                        });

                        // Table rows
                        foreach (var item in group)
                        {
                            table.Cell().Text(item.DispensedDate.ToString("dd/MM/yyyy"));
                            table.Cell().Text(item.MedicationName);
                            table.Cell().Text(item.Quantity.ToString());
                            table.Cell().Text($"R {item.ItemPrice:F2}");
                            table.Cell().Text($"R {item.LineTotal:F2}");
                            table.Cell().Text(item.Instructions);
                        }
                    });

                    // Sub-total for this group (Quantity and Value)
                    var groupQuantity = group.Sum(x => x.Quantity);
                    var groupValue = group.Sum(x => x.LineTotal);

                    column.Item().PaddingTop(5).Row(row =>
                    {
                        row.RelativeItem().Text($"Quantity: {groupQuantity}");
                        row.RelativeItem().Text($"Value: R {groupValue:F2}").AlignRight()
                        .FontSize(12).Bold();
                    });

                    column.Item().PaddingBottom(15).LineHorizontal(0.5f);
                }

                // Grand total (Quantity and Value)
                column.Item().PaddingTop(10).Row(row =>
                {
                    row.RelativeItem().Text($"TOTAL QUANTITY: {grandTotalQuantity}")
                        .FontSize(14).Bold();
                    row.RelativeItem().Text($"TOTAL VALUE: R {grandTotalValue:F2}")
                        .FontSize(14).Bold().AlignRight();
                });
            });
        }

        private void ComposeFooter(IContainer container)
        {
            container.AlignCenter().Text(text =>
            {
                text.Span("Page ");
                text.CurrentPageNumber();
                text.Span(" of ");
                text.TotalPages();
            });
        }

        private string GetGroupTitle(string groupBy)
        {
            return groupBy.ToLower() switch
            {
                "customer" => "CUSTOMER",
                "medication" => "MEDICATION",
                "schedule" => "SCHEDULE",
                _ => "CUSTOMER"
            };
        }
    }
}