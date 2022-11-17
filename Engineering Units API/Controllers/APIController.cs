using Microsoft.AspNetCore.Mvc;

namespace Engineering_Units_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        public APIController()
        {
        }

        [HttpGet(Name = "UnitDimensions")]
        public JsonResult UnitDimensions()
        {
            return new JsonResult(new EmptyResult());
        }
    }
}