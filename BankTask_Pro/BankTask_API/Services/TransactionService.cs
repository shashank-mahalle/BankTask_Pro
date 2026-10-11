// Services/TransactionService.cs
// In-memory data for now — EF Core added in Day 10

using BankTask_API.DTOs;
using BankTask_API.Models;

namespace BankTask_API.Services;

public class TransactionService : ITransactionService
{
    // Static list = in-memory database for now
    // Day 10 → replace with real SQL Server + EF Core
    private static List<Transaction> _transactions = new()
    {
        new Transaction { Id = 1, AccountId = "ACC001", Amount = 75000m,
                          Type = "CREDIT", Category = "SALARY",
                          IsSuccessful = true },

        new Transaction { Id = 2, AccountId = "ACC001", Amount = 12000m,
                          Type = "DEBIT", Category = "TRANSFER",
                          IsSuccessful = true },

        new Transaction { Id = 3, AccountId = "ACC002", Amount = 95000m,
                          Type = "CREDIT", Category = "SALARY",
                          IsSuccessful = true },

        new Transaction { Id = 4, AccountId = "ACC002", Amount = 8000m,
                          Type = "DEBIT",  Category = "SHOPPING",
                          IsSuccessful = false }
    };

    private static int _nextId = 5;

    public List<TransactionDto> GetAll()
    {
        // Map every Transaction → TransactionDto before returning
        return _transactions.Select(MapToDto).ToList();
    }

    public TransactionDto? GetById(int id)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);

        // Return null if not found — controller handles 404
        if (transaction == null) return null;

        return MapToDto(transaction);
    }

    public TransactionDto Create(CreateTransactionDto dto)
    {
        // Create new Transaction from DTO
        var transaction = new Transaction
        {
            Id           = _nextId++,
            AccountId    = dto.AccountId,
            Amount       = dto.Amount,
            Type         = dto.Type,
            Category     = dto.Category,
            IsSuccessful = true,
            CreatedAt    = DateTime.UtcNow
        };

        _transactions.Add(transaction);

        return MapToDto(transaction);
    }

    public bool Delete(int id)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);

        if (transaction == null) return false;

        _transactions.Remove(transaction);
        return true;
    }

    // Private helper — maps Transaction → TransactionDto
    // Keeps mapping logic in ONE place
    private TransactionDto MapToDto(Transaction t) => new(
        Id:              t.Id,
        AccountId:       t.AccountId.ToString() ,
        Amount:          t.Amount,
        Type:            t.Type,
        Category:        t.Category,
        status:          t.IsSuccessful ? "Success" : "Failed",
        FormattedAmount: $"₹{t.Amount:N2}",
        Date:            t.CreatedAt.ToString("dd MMM yyyy")
    );
}