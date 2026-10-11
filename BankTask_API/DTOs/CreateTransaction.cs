using System.ComponentModel.DataAnnotations;

namespace BankTask_API.DTOs;





public record CreateTransactionDto
(
[Required]
string AccountId,

[Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
decimal Amount,

// [Range(typeof(decimal), "0.01", "79228162514264337593543950335",
//     ErrorMessage = "Amount must be greater than 0.")]
// decimal Amount,

[Required]
[RegularExpression("CREDIT|DEBIT", ErrorMessage = "Type must be either 'CREDIT' or 'DEBIT'.")]
string Type,

[Required]
string Category

);