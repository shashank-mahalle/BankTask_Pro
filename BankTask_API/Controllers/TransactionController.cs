// Controllers/TransactionsController.cs

using BankTask_API.DTOs;
using BankTask_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankTask_API.Controllers;

[ApiController]
[Route("api/[controller]")]  // → api/transactions
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    // DI — service injected via constructor (SOLID Day 6)
    public TransactionsController(ITransactionService service)
        => _service = service;

    // GET api/transactions
    // React: fetch('http://localhost:5000/api/transactions')
    [HttpGet]
    public ActionResult<List<TransactionDto>> GetAll()
    {
        var transactions = _service.GetAll();
        return Ok(transactions); // 200 + JSON array
    }

    // GET api/transactions/1
    // React: fetch('http://localhost:5000/api/transactions/1')
    [HttpGet("{id:int}")]
    public ActionResult<TransactionDto> GetById(int id)
    {
        var transaction = _service.GetById(id);

        if (transaction == null)
            return NotFound($"Transaction {id} not found"); // 404

        return Ok(transaction); // 200 + single object
    }

    // POST api/transactions
    // React sends JSON body → CreateTransactionDto
    [HttpPost]
    public ActionResult<TransactionDto> Create([FromBody] CreateTransactionDto dto)
    {
        // [ApiController] validates dto automatically
        // If invalid → 400 returned before reaching here

        var created = _service.Create(dto);

        // 201 Created — standard for successful POST
        return CreatedAtAction(
            nameof(GetById),
            new { id = created.Id },
            created
        );
    }

    // DELETE api/transactions/1
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _service.Delete(id);

        if (!deleted)
            return NotFound($"Transaction {id} not found"); // 404

        return NoContent(); // 204 — success, nothing to return
    }
}