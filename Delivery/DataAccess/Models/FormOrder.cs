namespace Delivery.DataAccess.Models;

public class FormOrder
{
    public int Id { get; set; }
    public string SendersCity { get; set; } = "";
    public string SendersAddress { get; set; } = "";
    public string RecipientsCity { get; set; } = "";
    public string RecipientsAddress { get; set; } = "";
    public int? Weight { get; set; } = null;
    public DateTime ReceiptTime { get; set; } = DateTime.Now;

}