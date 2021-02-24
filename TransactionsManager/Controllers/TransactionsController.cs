using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionsManager.DAL.Models;
using TransactionsManager.Services;
using TransactionsManger.DAL.Services;

namespace TransactionsManager.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IExcelHelper _excelHelper;
        private readonly ICSVHelper _icsvHelper;
        public TransactionsController(ITransactionService transactionService, IExcelHelper excelHelper, ICSVHelper csvHelper)
        {
            _transactionService = transactionService;
            _excelHelper = excelHelper;
            _icsvHelper = csvHelper;
        }

        [HttpGet]
        public async Task<List<Transaction>> Get([FromQuery] TransactionFilter filter)
        {
            var transaction = await _transactionService.Get(filter);
            return transaction;
        }

        [HttpGet]
        [Route("export")]
        public async Task<FileContentResult> Export([FromQuery] TransactionFilter filter)
        {
            // we don't want to filter export data by client name
            filter.ClientName = "";
            var transactions = await _transactionService.Get(filter);
            var content = _excelHelper.GetExcelFileContent(transactions);

            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "transactions.xlsx");
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _transactionService.Delete(id);
            return Ok();
        }

        [HttpPut]
        [Route("{id}/{status}")]
        public async Task<ActionResult> Update(int id, string status)
        {
            if (!Enum.IsDefined(typeof(Status), status)) 
                return BadRequest("Wrong or incorrect status");
            await _transactionService.Update(id, status);
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Transaction> Get(int id)
        {
            var transaction = await _transactionService.GetById(id);
            return transaction;
        }

        [HttpPost("single-file")]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            var transactions = _icsvHelper.GetTransactions(file);

            await _transactionService.Merge(transactions);
            return Ok();
        }
    }
}
