using IbhayiPharmacy.Models.PharmacistVM;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

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
        var textFont = FontFactory.GetFont(FontFactory.HELVETICA, 11);

        doc.Add(new Paragraph("DISPENSED PRESCRIPTIONS REPORT", titleFont));
        doc.Add(new Paragraph($"{report.StartDate:d} – {report.EndDate:d}", textFont));
        doc.Add(Chunk.NEWLINE);

        foreach (var group in report.Groups)
        {
            doc.Add(new Paragraph($"{report.GroupBy.ToUpper()}: {group.GroupName}", titleFont));
            doc.Add(Chunk.NEWLINE);

            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 15, 30, 10, 10, 35 }); // Adjust column widths

            table.AddCell("Date");
            table.AddCell("Medication");
            table.AddCell("Qty");
            table.AddCell("Repeats");
            table.AddCell("Instructions / Doctor");

            foreach (var record in group.Records)
            {
                table.AddCell(record.Date.ToString("yyyy-MM-dd"));
                table.AddCell(record.Medication);
                table.AddCell(record.Quantity.ToString());
                table.AddCell(record.Repeats.ToString());
                table.AddCell(report.GroupBy == "Doctor" ? record.Instructions : record.DoctorName);
            }

            doc.Add(table);
            doc.Add(new Paragraph($"Sub-total: {group.Subtotal}", textFont));
            doc.Add(Chunk.NEWLINE);
        }

        doc.Add(new Paragraph($"GRAND TOTAL: {report.GrandTotal}", titleFont));
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
        PdfPTable footer = new PdfPTable(2);
        footer.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
        footer.AddCell(new PdfPCell(new Phrase($"Generated: {_report.GeneratedOn:g}", FontFactory.GetFont(FontFactory.HELVETICA, 9))) { Border = 0 });
        footer.AddCell(new PdfPCell(new Phrase($"Page {writer.PageNumber}", FontFactory.GetFont(FontFactory.HELVETICA, 9))) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
        footer.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin + 20, writer.DirectContent);
    }
}
