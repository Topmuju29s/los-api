using Ios_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ios_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ios_apiController : ControllerBase
    {
        private readonly losContext _context;
        public Ios_apiController(losContext context)
        {
            _context = context;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var Stock_result = _context.Stock.Select(p => p).ToList();
                var Product_result = _context.Product.Select(p => p).ToList();
                return Ok(new { Stock_result, Product_result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("availableStock")]
        public IActionResult AvailableStock()
        {
            //Product testADd = new Product();
            //testADd.imageUrl = "12345";
            //testADd.name = "topmuju";
            //testADd.price = 500;
            //_context.Product.Add(testADd);
            //_context.SaveChangesAsync();
            try
            {
                var item = _context.Stock.Where(av => av.amount > 0).ToList();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Stockbyid/{id}")]
        public IActionResult Stockbyid(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return StatusCode(400);
                //var Stock_result = _context.Stock.Where(av => av.productId == Convert.ToInt64(id)).ToList();
                //var Product_result = _context.Product.Where(avp => avp.id == Convert.ToInt64(id)).ToList();
                var pageObject = (from st in _context.Stock
                                  join pr in _context.Product on st.productId equals pr.id
                                  where st.productId == Convert.ToInt64(id) && st.amount > 0
                                  select new { pr.id, pr.name,pr.price,pr.imageUrl, st.productId,st.amount }
                                  ).ToList();
                return Ok(pageObject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
