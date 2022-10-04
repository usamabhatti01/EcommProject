using EcommProject.Model;
using EcommProject.NewFolder;
using EcommProject.Query;
using EcommProject.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace EcommProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            var record = await categoryRepository.GetAllRecordAsync();
            return Ok(record);
        }
        [HttpGet("/filter")]
        public async Task<IActionResult> CategoryFilter([FromQuery] FilterQuery filter)
        {
            /*string query = @"SELECT * FROM Categories";
            DataTable dt = new DataTable();
            string sqlDataSource = "Server=DESKTOP-2I9MOJ6;Database=EcommerStore;Integrated Security=True";
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                if (filter.HaveFilter)
                {
                    string filterSQL = "";
                    if (!string.IsNullOrEmpty(filter.Category_Name))
                        filterSQL += " Category_Name = ('" + filter.Category_Name + "')";
                    if (filter.Product_Id!=null)
                    {
                        if (!string.IsNullOrEmpty(filterSQL))
                            filterSQL += " and";
                        filterSQL += " ProductId = ('" + filter.Product_Id + "')";
                    }
                    query += $" where{filterSQL}";
                }
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dt.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }
            Console.WriteLine(query);
            return new JsonResult(dt);*/


            var response = await categoryRepository.CategoryFiltering(filter);
            return Ok(response);
        }


        [HttpGet("/sorting")]
        public async Task<IActionResult> CategorySorting([FromQuery] SortingQuery sorting)
        {
           /*  string query = @"SELECT * FROM Categories";
             DataTable dt = new DataTable();
             string sqlDataSource = "Server=DESKTOP-2I9MOJ6;Database=EcommerStore;Integrated Security=True";
             SqlDataReader sqlDataReader;
             using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
             {
                 sqlConnection.Open();
                 if (sorting.HaveFilter)
                 {
                     string filterSQL = "";
                     if (!string.IsNullOrEmpty(sorting.sort_type))
                         filterSQL += sorting.sort_type + " ";
                        {
                        if (!string.IsNullOrEmpty(sorting.sort_by))
                        {
                            filterSQL += sorting.sort_by + "";
                        }

                        query += $" order by {filterSQL}";
                    }
                  
                 }
                 using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                 {
                     sqlDataReader = sqlCommand.ExecuteReader();
                     dt.Load(sqlDataReader);
                     sqlDataReader.Close();
                     sqlConnection.Close();
                 }
             }
             Console.WriteLine(query);
             return new JsonResult(dt);
*/
            var response = await categoryRepository.CategorySorting(sorting);
            return Ok(response);
        }



        [HttpGet("/pagination")]
        public async Task<IActionResult> CategoryPagination([FromQuery] PaginationQuery pagination)
        {
            /* string query = @"SELECT * FROM Categories";
             DataTable dt = new DataTable();
             string sqlDataSource = "Server=DESKTOP-2I9MOJ6;Database=EcommerStore;Integrated Security=True";
             SqlDataReader sqlDataReader;
             using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
             {
                 sqlConnection.Open();
                 {
                     query += $" ORDER BY Id OFFSET {pagination.PageSize} * ({pagination.PageNumber} - 1) ROWS FETCH NEXT {pagination.PageSize} ROWS ONLY";
                 }
                 using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                 {
                     sqlDataReader = sqlCommand.ExecuteReader();
                     dt.Load(sqlDataReader);
                     sqlDataReader.Close();
                     sqlConnection.Close();
                 }
             }
             return new JsonResult(dt);*/

            var response = await categoryRepository.CategoryPagination(pagination);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooksbyId([FromRoute] String id)
        {
            var record = await categoryRepository.GetProductByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryModel categoryModel)
        {
            if (string.IsNullOrEmpty(categoryModel.Category_Name))
            {
                return BadRequest(error: new { error = "Name Field Empty" });
            }

            var id = await categoryRepository.AddProductAsync(categoryModel);
            return Ok(id);
        }

        [HttpPut]
        public async Task<String> Update([FromBody] CategoryModel categoryModel)
        {


            return await categoryRepository.UpdateProductAsync(categoryModel); ;
        }


        [HttpPatch]
        public async Task<String> UpdateBookPatchAsync(int id, [FromBody] JsonPatchDocument categoryModel)
        {


            return await categoryRepository.UpdateBookPatchAsync(id, categoryModel); ;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooksbyId([FromRoute] int id)
        {
            var record = await categoryRepository.DeleteBookByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }


        [HttpGet("/FPS")]
        public async Task<IActionResult> CategoryPagFILSOR([FromQuery] FPSQuery fpsquery)
        {
            /*string query = @"SELECT * FROM Categories";
            DataTable dt = new DataTable();
            string sqlDataSource = "Server=DESKTOP-2I9MOJ6;Database=EcommerStore;Integrated Security=True";
            SqlDataReader sqlDataReader;
            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                {
                    if (fpsquery.HaveFilter)
                    {
                        string WhereQuery = "";
                        string OrderBy = "";

                        if (!string.IsNullOrEmpty(fpsquery.Filter_Category_Name))
                            WhereQuery += " Category_Name = ('" + fpsquery.Filter_Category_Name + "')";

                        if (!string.IsNullOrEmpty(WhereQuery))
                        query += $" where{WhereQuery}";

                        if (fpsquery.Sort_Type != null)
                        {
                            if (!string.IsNullOrEmpty(OrderBy))
                            OrderBy +=  fpsquery.Sort_Type + " ";
                        

                            if (fpsquery.Sort_By != null)
                            {
                                if (!string.IsNullOrEmpty(OrderBy))
                                    OrderBy += " ,";
                                OrderBy += "Id " + fpsquery.Sort_By + " ";
                            }
                        }
                        if (!string.IsNullOrEmpty(OrderBy))
                            query += $" order by {OrderBy} ";
                        if (string.IsNullOrEmpty(OrderBy))
                            query += $" order by Id ";

                        query += $"OFFSET {fpsquery.PageSize} * ({fpsquery.PageNumber} - 1) ROWS FETCH NEXT {fpsquery.PageSize} ROWS ONLY";

                    }

                }
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlDataReader = sqlCommand.ExecuteReader();
                    dt.Load(sqlDataReader);
                    sqlDataReader.Close();
                    sqlConnection.Close();
                }
            }
            return new JsonResult(dt);
*/

            var response = await categoryRepository.CategoryFPS(fpsquery);
            return Ok(response);
        }
    }
}