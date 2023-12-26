namespace Web_Application.Models.Credit
{
    public class CreditViewModel
    {
        public decimal Amount { get; set; }
        
        public string Passport { get; set; }
        
        public IFormFile DocumentImage { get; set; }
        
        public bool ReasonForCredit_HomePurchase { get; set; }
        
        public bool ReasonForCredit_DebtConsolidation { get; set; }
        
        public bool ReasonForCredit_EmergencyExpense { get; set; }
        
        public bool AcceptTerms { get; set; }

    }


}
