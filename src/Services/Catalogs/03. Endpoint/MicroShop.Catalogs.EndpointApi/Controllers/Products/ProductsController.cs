using MicroShop.Catalogs.Core.ApplicationServices.Products;
using MicroShop.Catalogs.Core.Contracts.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroShop.Catalogs.EndpointApi.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AddProductServiceTest _addProductServiceTest;

        public ProductsController(AddProductServiceTest addProductServiceTest)
        {
            _addProductServiceTest = addProductServiceTest;
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            await _addProductServiceTest.Handle();
            return Ok();
        }
    }
}
