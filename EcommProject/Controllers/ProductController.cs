using EcommProject.Model;
using EcommProject.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EcommProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        ///asdsadsadsasdfffa
        public async Task<IActionResult> GetALL()
        {
            var record = await productRepository.GetAllRecordAsync();
            return Ok(record);
        }
     

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooksbyId([FromRoute] int id)
        {
            var record = await productRepository.GetProductByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductModel ecommModel)
        {
            /*if (string.IsNullOrEmpty(ecommModel.ProductName))
            {
                return BadRequest(error: new { error = "Name Field Empty" });
            }
*/
            var id = await productRepository.AddProductAsync(ecommModel);
            return Ok(id);
        }

        [HttpPut]
        public async Task<String> Update([FromBody] ProductModel ecommModel)
        {


            return await productRepository.UpdateProductAsync(ecommModel); ;
        }


        [HttpPatch]
        public async Task<String> UpdateBookPatchAsync(int id, [FromBody] JsonPatchDocument ecommModel)
        {


            return await productRepository.UpdateBookPatchAsync(id, ecommModel); ;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooksbyId([FromRoute] int id)
        {
            var record = await productRepository.DeleteBookByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }
    }

}
