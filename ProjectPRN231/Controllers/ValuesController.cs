using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectPRN231.Models;

namespace ProjectPRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ValuesController : ControllerBase
    {
        readonly toDo2Context db;
        public ValuesController(toDo2Context toDo)
        {
            db = toDo;
        }
        [HttpGet("st")]
        public IActionResult Get()
        {
            //var list = db.Users.ToList();
            return Ok("dafsfnasjfn");
        }
    }
}
