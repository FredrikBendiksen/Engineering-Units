using Engineering_Units;
using Engineering_Units.Models;
using Microsoft.AspNetCore.Mvc;

namespace Engineering_Units_API.Controllers;

[ApiController]
public class APIController : ControllerBase
{
    IEngineeringUnits engineeringUnits;

    public APIController(MemoryLists memory)
    {
        engineeringUnits = Factoring.GetEngineeringUnits(memory);
    }

    [HttpGet("Convert")]
    public IActionResult Convert(decimal value, string fromUOM, string toUOM)
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
    public IActionResult CreateAlias(string uomName, string newAlias)
    {
        string? result = engineeringUnits.CreateAlias(uomName, newAlias);
        result ??= "Success";
        return new JsonResult(result);
    }

    [HttpGet("AliasesForUOMName/{uomName}")]
    public IActionResult GetAliasesForUOMName(string uomName)
    {
        List<string> result = engineeringUnits.GetAliasesForUOMName(uomName);
        return new JsonResult(result);
    }

    [HttpGet("UnitDimensions")]
    public IActionResult GetUnitDimensions()
    {
        var result = engineeringUnits.GetUnitDimensions().Select(x => new {x.symbol, x.definition, x.baseUnit}).ToList();
        return new JsonResult(result);
    }

    [HttpGet("UOMsForUnitDimension/{unitDimension}")]
    public IActionResult GetUOMsForUnitDimension(string unitDimension)
    {
        var result = engineeringUnits.GetUOMsForUnitDimension(unitDimension).Select(x => new {x.name, x.annotation}).ToList();
        return new JsonResult(result);
    }

    [HttpGet("QuantityClasses")]
    public IActionResult GetAllQuantityClasses()
    {
        List<string> result = engineeringUnits.GetAllQuantityClasses();
        return new JsonResult(result);
    }

    [HttpGet("UOMsForQuantityClass/{quantityClass}")]
    public IActionResult GetUOMsForQuantityClass(string quantityClass)
    {
        var result = engineeringUnits.GetUOMsForQuantityClass(quantityClass).Select(x => new { x.name, x.annotation }).ToList();
        return new JsonResult(result);
    }

    [HttpPost("CreateSubQuantityClass")]
    public IActionResult CreateSubQuantityClass(string name, string[] uomNames)
    {
        string? result = engineeringUnits.CreateSubQuantityClass(name, uomNames.ToList());
        result ??= "Success";
        return new JsonResult(result);
    }

    [HttpPost("CreateUOM")]
    public IActionResult CreateUOM(string name, string annotation, string baseUOM,
        decimal conversionParameterA, decimal conversionParameterB, decimal conversionParameterC, decimal conversionParameterD, string[] quantityClasses)
    {
        string? result = engineeringUnits.CreateUOM(name, annotation, quantityClasses.ToList(), baseUOM, conversionParameterA, conversionParameterB, conversionParameterC, conversionParameterD);
        result ??= "Success";
        return new JsonResult(result);
    }

}