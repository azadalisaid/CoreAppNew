using CoreApp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Controllers
{
    
    [Route("api/Product")]
    [ApiController]
    //[EnableCors("AllowOrigin")]
    public class ProductController : ControllerBase
    {

        private readonly PaymentDetailContext _context;

        public ProductController(PaymentDetailContext context)
        {
            _context = context;

        }

        // GET: api/PaymentDetail
        [Route("GetProducts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {

            return await (from c in _context.Products
                          join d in _context.Categories on c.CategoryId equals d.CategoryId
                          join e in _context.SubCategories on c.SubCategoryId equals e.SubCategoryId
                          join f in _context.QuantityTypes on c.QuantityTypeId equals f.QuantityTypeId
                          
                          select c).ToListAsync();

        }

        [Route("GetProductById")]
        [HttpGet]
        public async Task<ActionResult<Product>> GetProductById(int ProductId)
        {

            return await (from c in _context.Products
                          join d in _context.Categories on c.CategoryId equals d.CategoryId
                          join e in _context.SubCategories on c.SubCategoryId equals e.SubCategoryId
                          join f in _context.QuantityTypes on c.QuantityTypeId equals f.QuantityTypeId
                          where c.ProductId==ProductId
                          select c).FirstOrDefaultAsync();

        }

        [Route("SearchProducts")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<productStockList>>> SearchProducts(ProductSearch productSearch)
        {


            return await (from c in _context.Products
                          join d in _context.Categories on c.CategoryId equals d.CategoryId
                          join e in _context.SubCategories on c.SubCategoryId equals e.SubCategoryId
                          join f in _context.QuantityTypes on c.QuantityTypeId equals f.QuantityTypeId
                          join g in _context.ProductStocks on c.ProductId equals g.ProductId into gc
                          from gcprod in gc.DefaultIfEmpty()
                          where (c.CategoryId == productSearch.CategorySearch || productSearch.CategorySearch == 0)
                          && (c.SubCategoryId == productSearch.SubCategorySearch || productSearch.SubCategorySearch == 0)
                          && (c.ProductCode.Contains(productSearch.ProductCodeSearch) || productSearch.ProductCodeSearch == "")
                          group gcprod by new { c.ProductId, c.ProductCode, c.Name,c.photo,c.Description,c.Price } into h
                          select new productStockList { ProductId = h.Key.ProductId,ProductCode=h.Key.ProductCode,
                          ProductName=h.Key.Name,Photo=h.Key.photo,
                              TotalQuantity = h.Sum(g=>g.Quntity),
                              TotalAmount = h.Sum(g => g.Amount),
                              Description=h.Key.Description,
                              Quantity=0,
                              Price=Convert.ToDecimal(h.Key.Price),
                              ActualQuantity= h.Sum(g => g.Quntity),
                              ActualAmount = h.Sum(g => g.Amount),
                          }).ToListAsync();


        }



        [Route("GetSubCategories")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategories(int CategoryId)
        {
            return await _context.SubCategories.Where(c => c.CategoryId == CategoryId).ToListAsync();
        }


        [Route("GetCategories")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }




        [Route("GetQunatityType")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuantityType>>> GetQunatityType()
        {
            return await _context.QuantityTypes.ToListAsync();
        }

        // GET: api/PaymentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetUser(int id)
        {
            var Products = await _context.Products.FindAsync(id);

            if (Products == null)
            {
                return NotFound();
            }

            return Products;
        }

        // PUT: api/PaymentDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("UpdateProduct")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct([FromForm] Product product)
        {
            //if (paymentDetail.PaymentDetailId)
            //{
            //    return BadRequest();
            //}
            if (product.PhotoFile != null)
            {
                IFormFile file = product.PhotoFile;
                string fileName = file.FileName;
                long length = file.Length;
                if (length < 0)
                    return BadRequest();
                var fileStream = file.OpenReadStream();
                byte[] bytes = new byte[length];
                fileStream.Read(bytes, 0, (int)file.Length);
                product.photo = bytes;
            }
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductDetailExists(product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // PUT: api/PaymentDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("UpdateStock")]
        [HttpPost]
        public async Task<IActionResult> UpdateStock(IList<ProductQuantity> productQuantity)
        {
            //if (paymentDetail.PaymentDetailId)
            //{
            //    return BadRequest();
            //}
            
            foreach (var prod in productQuantity)
            {
                decimal prodPrice = Convert.ToDecimal(_context.Products.FirstOrDefault(c => c.ProductId == prod.ProductId).Price);
                _context.ProductStocks.Add(new ProductStock
                {
                    Quntity = prod.quantity,
                    Date = DateTime.Now,
                    ProductId = prod.ProductId,
                    Amount = prod.quantity * prodPrice,
                    Multiply = 1,
                });
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("SearchProducts", productQuantity);
            //return NoContent();
        }

        // POST: api/PaymentDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("InsertProduct")]
        [HttpPost]
        public async Task<ActionResult<Registration>> InsertProduct([FromForm] Product product)
        {
            if (product.PhotoFile != null)
            {
                IFormFile file = product.PhotoFile;
                //// string fileName = file.FileName;
                long length = file.Length;
                if (length < 0)
                    return BadRequest();
                var fileStream = file.OpenReadStream();
                byte[] bytes = new byte[length];
                fileStream.Read(bytes, 0, (int)file.Length);
                product.photo = bytes;
            }
            //var file = Request.Form.Files[0];
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = product.ProductId }, product);
        }

        // DELETE: api/PaymentDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductDetailExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        [Route("ProductExist")]
        [HttpPost]
        public async Task<int> ProductExist(ProductExistansy productExistansy)
        {
            if(productExistansy.ProductId>0)
            {
                return await( _context.Products.CountAsync(c=>c.ProductId!=productExistansy.ProductId && c.ProductCode==productExistansy.ProductCode));
            }
            else
            {
                return await(_context.Products.CountAsync(c =>  c.ProductCode == productExistansy.ProductCode));
            }
        }

    }
}
