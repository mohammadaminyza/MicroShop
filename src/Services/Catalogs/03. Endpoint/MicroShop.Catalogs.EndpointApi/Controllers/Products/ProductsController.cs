using MicroShop.Catalogs.Core.ApplicationServices.Products.Commands.CreateProduct;
using Microsoft.AspNetCore.Mvc;

namespace MicroShop.Catalogs.EndpointApi.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {

        [HttpPost("create")]
        public async Task<IActionResult> AddProduct(CreateProductCommand productCommand)
        {
            return await Create(productCommand);
        }
    }
}
