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
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();
        //Post api/Customer
        [HttpPost]
        public async Task<IHttpActionResult> Create ([FromBody] Customer customerToCreate)
        {
            Customer createdCustomer = _ctx.Customers.Add(customerToCreate);
            await _ctx.SaveChangesAsync();
            return Ok(createdCustomer);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomers()
        {
            List<Customer> customers = await _ctx.Customers.ToListAsync();
            return Ok(customers);
        }
        //Read Single
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetCustomerByID([FromUri] int id)
        {
            Customer requestedCustomer = await _ctx.Customers.FindAsync(id);
            if (requestedCustomer == null)
            {
                return NotFound();
            }
            return Ok(requestedCustomer);
        }
        //Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateCustomer([FromUri] int id, [FromBody] Customer updatedCustomer)
        {
            Customer requestedCustomer = await _ctx.Customers.FindAsync(id);
            if (requestedCustomer == null)
            {
                return NotFound();
            }
            if(updatedCustomer.Name != null)
            {
                requestedCustomer.Name = updatedCustomer.Name;
            }
            try
            {
                await _ctx.SaveChangesAsync();
                return Ok(requestedCustomer);
            }
            catch(Exception e)
            {
                return (BadRequest(e.Message));
            }
        }
        //Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteCustomer([FromUri] int id)
        {
            Customer requestedCustomer = await _ctx.Customers.FindAsync(id);
            if (requestedCustomer == null)
            {
                return NotFound();
            }
            try
            {
                _ctx.Customers.Remove(requestedCustomer);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            catch(Exception e)
            {
                return (BadRequest(e.Message));
            }
        }
    }
}
