using ProductModel = mongodb_dotnet_example.Models.Product;
using ProductService = mongodb_dotnet_example.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace mongodb_dotnet_example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<ProductModel>> Get()
        {
            var products = _productService.Get();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
public ActionResult<ProductModel> Get(string id)
{
    var product = _productService.Get(id);

    if (product == null)
    {
        return NotFound();
    }

    return Ok(product);
}

        [HttpPost]
        public ActionResult<ProductModel> Create(ProductModel product)
        {
            _productService.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, ProductModel productIn)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Update(id, productIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Delete(id);

            return NoContent();
        }
    }
}
