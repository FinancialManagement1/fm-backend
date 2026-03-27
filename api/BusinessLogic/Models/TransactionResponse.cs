using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models
{
    public class TransactionResponse
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; } = string.Empty;
        [Required]
        public double Amount { get; set; }
        [Required]
        public string Currency { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required]
        public string? Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}