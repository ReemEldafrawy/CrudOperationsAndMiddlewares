using CrudOperations.Data;
using CrudOperations.Filiters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace CrudOperations.Controllers
{
 
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDBContext _dbcontext;
        private readonly ILogger<ProductsController> logger;
        public ProductsController(ApplicationDBContext dbcontext, ILogger<ProductsController> Logger)
        {
            _dbcontext = dbcontext;
            logger = Logger;
        }

        [HttpPost]
        [Route("")]
        public ActionResult<int> CreateProduct([FromBody] Product product)
        {
            product.id = 0;
            _dbcontext.Set<Product>().Add(product);
            _dbcontext.SaveChanges();
            return Ok(product.id);
        }
        [HttpGet]
        [Route("")]
       
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var username = User.Identity.Name;
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var records=_dbcontext.Set<Product>().ToList();
            return Ok(records);
        }
        [SensitiveInformationFilter]
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public ActionResult<Product> GetIdRecord( int id)
        {
            logger.LogDebug("products debug :" + id);
            if (id<=0)
            {
                return BadRequest();
            }
            else
            {
              
                var record = _dbcontext.Set<Product>().Find(id);
                if(record!=null)
                {
                    return Ok(record);
                }
                else
                {
                    logger.LogWarning("Product {id} Error:", id);
                    return NotFound();
                }
            }
        }







        [HttpPut]
        [Route("")]
        public ActionResult updateProduct(Product product)
        {
            if (product.id <= 0)
            {
                return BadRequest();
            }
            var existingproduct = _dbcontext.Set<Product>().Find(product.id);
            if (existingproduct != null)
            {
                existingproduct.name = product.name;
                existingproduct.sku = product.sku;
                _dbcontext.Set<Product>().Update(existingproduct);
                _dbcontext.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }



        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            else
            {
                var existingproduct = _dbcontext.Set<Product>().Find(id);
                if (existingproduct != null)
                {
                    _dbcontext.Set<Product>().Remove(existingproduct);
                    _dbcontext.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}