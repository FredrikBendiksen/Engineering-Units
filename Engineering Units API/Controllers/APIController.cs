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

        [HttpPost("CreateAlias")]
        public JsonResult CreateAlias([FromBody]string uomName, string newAlias)
        {
            bool result = engineeringUnits.CreateAlias(uomName, newAlias);
            return new JsonResult(result);
        }
        /*
        public List<(char symbol, string definition, string baseUnit)> GetUnitDimensions();

        public List<(string name, string annotation)> GetUOMsForUnitDimension(string unitDimension);

        public List<string> GetAllQuantityClasses();

        public List<string> GetUOMsForQuantityClass(string quantityClass);

        public List<string> GetAliasesForUOMName(string uomName);

        public bool CreateSubQuantityClass(string name, List<string> uomNames);

        public bool CreateUOM(string name, string annotation, List<string> quantityClasses, string baseUOM,
            decimal converstionParameterA, decimal converstionParameterB, decimal converstionParameterC, decimal converstionParameterD);
        */
    }
}