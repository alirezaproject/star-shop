using Application.Interfaces.Services.Categories;
using Application.Requests.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryTypeController : ControllerBase
    {
        private readonly ICategoryTypeService _categoryTypeService;

        public CategoryTypeController(ICategoryTypeService categoryTypeService)
        {
            _categoryTypeService = categoryTypeService;
        }

        [HttpGet("{parentId:int}/{page:int}/{pageSize:int}")]
        public async Task<IActionResult> Get(int? parentId,int page,int pageSize)
        {
            return Ok(await _categoryTypeService.GetList(parentId,page,pageSize));
        }
    }
}
