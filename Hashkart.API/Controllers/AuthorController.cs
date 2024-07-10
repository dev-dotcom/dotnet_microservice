using Hashkart.API.Models;
using Hashkart.Domain.Entities;
using Hashkart.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hashkart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorServices _services;
        public AuthorController(IAuthorServices services)
        {
            _services = services;
        }   
        [HttpGet]
        public async Task<ActionResult>Get(int PageIndex, int PageSize)
        {
            try
            {
                var result = await _services.Get(PageIndex, PageSize);
                List<Author> authors = result.data as List<Author>;
                int thisPageSize = authors.Skip((PageIndex - 1) * PageSize).Take(PageSize).Count();
                int totalRecords = authors.Count();
                int TotalPages = totalRecords/PageSize;
                

                ApiResponse res = new ApiResponse(result.data,totalRecords, TotalPages, thisPageSize);
                return Ok(res);
            }
            catch (Exception e)
            {

                return StatusCode(500, new { Status = "error", Message = e.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Author model)
        {
            try
            {
                var result = await _services.Create(model);
                return Ok(result);  
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
