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
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();
        //Post api/Product
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] Product productToCreate)
        {
            Product createdProduct = _ctx.Products.Add(productToCreate);
            await _ctx.SaveChangesAsync();
            return Ok(createdProduct);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            List<Product> products = await _ctx.Products.ToListAsync();
            return Ok(products);
        }
        //Read Single
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetProductsByID([FromUri] int id)
        {
            Product requestedProduct = await _ctx.Products.FindAsync(id);
            if (requestedProduct == null)
            {
                return NotFound();
            }
            return Ok(requestedProduct);
        }
        //Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateProduct([FromUri] int id, [FromBody] Product updatedProduct)
        {
            Product requestedProduct = await _ctx.Products.FindAsync(id);
            if (requestedProduct == null)
            {
                return NotFound();
            }
            if (updatedProduct.Name != null)
            {
                requestedProduct.Name = updatedProduct.Name;
            }
            if (updatedProduct.Price != 0)
            {
                requestedProduct.Price = updatedProduct.Price;
            }
            try
            {
                await _ctx.SaveChangesAsync();
                return Ok(requestedProduct);
            }
            catch (Exception e)
            {
                return (BadRequest(e.Message));
            }
        }
        //Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteProduct([FromUri] int id)
        {
            Product requestedProduct = await _ctx.Products.FindAsync(id);
            if (requestedProduct == null)
            {
                return NotFound();
            }
            try
            {
                _ctx.Products.Remove(requestedProduct);
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
