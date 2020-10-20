using GeneralStoreApi.Models;
using GeneralStoreApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreApi.Controllers
{
    [RoutePrefix("api/transaction")]
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();
        //Post api/Transaction
        [HttpPost]

        public async Task<IHttpActionResult> Create([FromBody] Transaction transactionToCreate)
        {
            Transaction createdTransaction = _ctx.Transactions.Add(transactionToCreate);
            await _ctx.SaveChangesAsync();
            return Ok(createdTransaction);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAllTransactions()
        {
            List<Transaction> transactions = await _ctx.Transactions.ToListAsync();
            return Ok(transactions);
        }
        //Read Single
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetTransactionByID([FromUri] int id)
        {
            Transaction requestedTransaction = await _ctx.Transactions.FindAsync(id);
            if (requestedTransaction == null)
            {
                return NotFound();
            }
            return Ok(requestedTransaction);
        }
        //Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateTransaction([FromUri] int id, [FromBody] Transaction updatedTransaction)
        {
            Transaction requestedTransaction = await _ctx.Transactions.FindAsync(id);
            if (requestedTransaction == null)
            {
                return NotFound();
            }
            if(updatedTransaction.ProductId != 0)
            {
                requestedTransaction.ProductId = updatedTransaction.ProductId;
            }
            if (updatedTransaction.CustomerId != 0)
            {
                requestedTransaction.CustomerId = updatedTransaction.CustomerId;
            }

            if (updatedTransaction.DateOfTransaction != default(DateTime))
            {
                requestedTransaction.DateOfTransaction = updatedTransaction.DateOfTransaction;
            }
            try
            {
                await _ctx.SaveChangesAsync();
                return Ok(requestedTransaction);
            }
            catch (Exception e)
            {
                return (BadRequest(e.Message));
            }
        }
        //Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteTransaction([FromUri] int id)
        {
            Transaction requestedTransaction = await _ctx.Transactions.FindAsync(id);
            if (requestedTransaction == null)
            {
                return NotFound();
            }
            try
            {
                _ctx.Transactions.Remove(requestedTransaction);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return (BadRequest(e.Message));
            }
        }
    }
}
