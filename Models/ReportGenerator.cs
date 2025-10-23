using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using IbhayiPharmacy.Models.PharmacistVM;

public static class ReportGenerator
{
    public static byte[] GeneratePrescriptionReport(PrescriptionReportVM report)
    {
        using var ms = new MemoryStream();
        var doc = new Document(PageSize.A4, 40, 40, 80, 60);
        var writer = PdfWriter.GetInstance(doc, ms);
        writer.PageEvent = new PdfHeaderFooter(report);

        doc.Open();

        var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
        var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
        var textFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
        var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);

        // Dynamic title based on grouping
        string reportTitle = report.GroupBy == "Doctor"
            ? "DISPENSED PRESCRIPTIONS BY DOCTOR"
            : "DISPENSED PRESCRIPTIONS BY MEDICATION";

        // Title and Date Range
        var title = new Paragraph(reportTitle, titleFont)
        {
            Alignment = Element.ALIGN_CENTER,
            SpacingAfter = 10f
        };
        doc.Add(title);

        var dateRange = new Paragraph($"{report.StartDate:dd/MM/yyyy} – {report.EndDate:dd/MM/yyyy}", textFont)
        {
            Alignment = Element.ALIGN_CENTER,
            SpacingAfter = 20f
        };
        doc.Add(dateRange);

        foreach (var group in report.Groups)
        {
            // Group Header
            var groupHeader = new Paragraph($"{report.GroupBy.ToUpper()}: {group.GroupName}", headerFont)
            {
                SpacingAfter = 10f
            };
            doc.Add(groupHeader);

            // Create table with dynamic columns based on grouping
            int columnCount = report.GroupBy == "Medication" ? 5 : 4;
            PdfPTable table = new PdfPTable(columnCount);
            table.WidthPercentage = 100;

            if (report.GroupBy == "Medication")
            {
                table.SetWidths(new float[] { 20, 35, 10, 10, 25 }); // Date, Medication, Qty, Repeats, Doctor
            }
            else
            {
                table.SetWidths(new float[] { 20, 45, 15, 20 }); // Date, Medication, Qty, Repeats
            }

            // Table headers
            table.AddCell(new PdfPCell(new Phrase("Date", boldFont)) { Padding = 5 });
            table.AddCell(new PdfPCell(new Phrase("Medication", boldFont)) { Padding = 5 });
            table.AddCell(new PdfPCell(new Phrase("Qty", boldFont)) { Padding = 5 });
            table.AddCell(new PdfPCell(new Phrase("Repeats", boldFont)) { Padding = 5 });

            if (report.GroupBy == "Medication")
            {
                table.AddCell(new PdfPCell(new Phrase("Doctor", boldFont)) { Padding = 5 });
            }

            // Table rows
            foreach (var record in group.Records)
            {
                table.AddCell(new PdfPCell(new Phrase(record.Date.ToString("yyyy-MM-dd"), textFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(record.Medication, textFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(record.Quantity.ToString(), textFont)) { Padding = 5 });
                table.AddCell(new PdfPCell(new Phrase(record.Repeats.ToString(), textFont)) { Padding = 5 });

                if (report.GroupBy == "Medication")
                {
                    table.AddCell(new PdfPCell(new Phrase(record.DoctorName, textFont)) { Padding = 5 });
                }
            }

            doc.Add(table);

            // Subtotal
            var subtotal = new Paragraph($"Sub-total: {group.Subtotal} items", boldFont)
            {
                SpacingBefore = 10f,
                SpacingAfter = 20f
            };
            doc.Add(subtotal);
        }

        // Grand Total
        if (report.Groups.Count > 0)
        {
            var grandTotal = new Paragraph($"GRAND TOTAL: {report.GrandTotal} items", titleFont)
            {
                SpacingBefore = 20f,
                Alignment = Element.ALIGN_RIGHT
            };
            doc.Add(grandTotal);
        }
        else
        {
            var noData = new Paragraph("No data found for the selected period.", textFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 20f
            };
            doc.Add(noData);
        }

        doc.Close();
        return ms.ToArray();
    }
}

public class PdfHeaderFooter : PdfPageEventHelper
{
    private readonly PrescriptionReportVM _report;

    public PdfHeaderFooter(PrescriptionReportVM report)
    {
        _report = report;
    }

    public override void OnEndPage(PdfWriter writer, Document document)
    {
        var footerFont = FontFactory.GetFont(FontFactory.HELVETICA, 8);

        var footer = new PdfPTable(1);
        footer.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

        var cell = new PdfPCell(new Phrase($"Generated on: {DateTime.Now:dd/MM/yyyy HH:mm} | Page {writer.PageNumber}", footerFont));
        cell.Border = 0;
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.Padding = 5;

        footer.AddCell(cell);
        footer.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin, writer.DirectContent);
    }
}