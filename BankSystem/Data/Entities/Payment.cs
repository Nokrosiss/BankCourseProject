namespace Web_Application.Data.Entities;

public class Payment
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public int CreditId { get; set; }
    
    public decimal Amount { get; set; }

    public DateTime Date { get; set; }
}