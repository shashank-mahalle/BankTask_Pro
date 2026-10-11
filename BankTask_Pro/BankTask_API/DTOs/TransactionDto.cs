
using System.ComponentModel.DataAnnotations;

namespace BankTask_API.DTOs;



public record class TransactionDto
(
// Response DTO — sent TO React

int Id,
string AccountId,
decimal Amount,
string Type,
string Category,
string status,
string FormattedAmount,
string Date

);

