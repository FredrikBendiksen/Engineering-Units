using Engineering_Units;
using Microsoft.AspNetCore.Mvc;

namespace Engineering_Units_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        readonly IEngineeringUnits engineeringUnits;

        public APIController()
        {
            engineeringUnits = Factoring.GetEngineeringUnits();
        }

        [HttpGet("Convert")]
        public JsonResult Convert(decimal value, string fromUOM, string toUOM)
        {
            (decimal val, string uom, string annotation) = engineeringUnits.Convert(value, fromUOM, toUOM);
            var json = new JsonResult(new
            {
                value = val,
                uom,
                annotation
            });
            return json;
        }
    }
}