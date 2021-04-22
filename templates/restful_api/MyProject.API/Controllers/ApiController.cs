using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.API.Configurations;

[assembly: ApiConventionType(typeof(ApiConventions))] // Adding api conventions to all controllers

namespace MyProject.API.Controllers
{
    /// <summary>
    ///     Base API controller. (Can) enforces authentication on all controllers and setup route definition.
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route("api")]
    public abstract class ApiController : ControllerBase
    {
    }
}
