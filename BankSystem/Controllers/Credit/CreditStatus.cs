namespace Web_Application.Controllers.Credit;

public partial class CreditController
{
    public static Dictionary<CreditStatus, string> StatusMap { get; } = GenerateStatusToStringMap();
    
    private static Dictionary<CreditStatus, string> GenerateStatusToStringMap()
    {
        var map = new Dictionary<CreditStatus, string>
        {
            { CreditStatus.Issued, "Issued" },
            { CreditStatus.Processing, "Processing" },
            { CreditStatus.Accepted, "Accepted" },
            { CreditStatus.Rejected, "Rejected" },
            { CreditStatus.Closed, "Closed" },
        };

        return map;
    }
}

public enum CreditStatus
{
    Issued = 0,
    Processing = 1,
    Accepted = 2,
    Rejected = 3,
    Closed = 4
}