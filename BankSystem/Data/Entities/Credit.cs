namespace Web_Application.Data.Entities
{
    public class Credit
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        
        public decimal StartingAmount { get; set; }
        public decimal Amount { get; set; }
        
        public string Status { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
