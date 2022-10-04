using EcommProject.Model;
using EcommProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace EcommProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class subCategoryController : ControllerBase
    {
        private readonly ISubCategoryRepository subCategoryRepository;

        public subCategoryController(ISubCategoryRepository subCategoryRepository)
        {
            this.subCategoryRepository = subCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var record = await subCategoryRepository.GetAllRecordAsync();
            return Ok(record);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooksbyId([FromRoute] int id)
        {
            var record = await subCategoryRepository.GetProductByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] subCategoryModel subCategoryModel)
        {
            if (string.IsNullOrEmpty(subCategoryModel.subCategory_Name))
            {
                return BadRequest(error: new { error = "Name Field Empty" });
            }

            var id = await subCategoryRepository.AddProductAsync(subCategoryModel);
            return Ok(id);
        }

        [HttpPut]
        public async Task<object> Update([FromBody] subCategoryModel subCategoryModel)
        {


            return await subCategoryRepository.UpdateProductAsync(subCategoryModel); ;
        }


        [HttpPatch]
        public async Task<String> UpdateBookPatchAsync(int id, [FromBody] JsonPatchDocument subCategoryModel)
        {


            return await subCategoryRepository.UpdateBookPatchAsync(id, subCategoryModel); ;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooksbyId([FromRoute] int id)
        {
            var record = await subCategoryRepository.DeleteBookByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }
    }
}
