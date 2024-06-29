using API.Errors;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ErrorsController : BaseController
{
    private readonly StoreDbContext _context;

    public ErrorsController(StoreDbContext context)
    {
        _context = context;
    }

    [HttpGet("notfound")]
    public ActionResult NotFoundMethod()
    {
        var category = _context.Categories.Find(200);
        if (category is null)
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound));

        return Ok(category);
    }


    [HttpGet("server-error")]
    public ActionResult ServerErrorMethod()
    {
        var category = _context.Categories.Find(200);
       
        return Ok(category.ToString());
    }


    [HttpGet("bad-request")]
    public ActionResult BadRequestMethod()
    {
        

        return BadRequest( new ApiResponse(StatusCodes.Status400BadRequest));
    }

    [HttpGet("bad-request/{id}")]
    public ActionResult BadRequestMethod(int id)
    {


        return BadRequest();
    }

}


 