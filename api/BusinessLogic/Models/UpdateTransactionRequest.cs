using System.ComponentModel.DataAnnotations;

public class UpdateTransactionRequest
{
    [Required]
    [RegularExpression("^(income|expense)$")]
    public string Type { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public double Amount { get; set; }

    [RegularExpression("^[A-Z]{3}$")]
    public string? Currency { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Category { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Description { get; set; }

    [Required]
    public DateTime Date { get; set; }
}