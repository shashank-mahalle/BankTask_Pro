
using BankTask_API.DTOs;

namespace BankTask_API.Services;

public interface ITransactionService
{
    
    List<TransactionDto>    GetAll();
    TransactionDto?         GetById(int id);
    TransactionDto          Create(CreateTransactionDto dto);
    bool                    Delete(int id);
}