

namespace BankTask_API.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string AccountId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool IsSuccessful { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    }
}