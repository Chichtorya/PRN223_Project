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
        readonly toDoContext db;
        public ValuesController(toDoContext toDo) {
            db = toDo;
        }
        [HttpGet("st")]
        public IActionResult  Get()
        {
            //var list = db.Users.ToList();
            return Ok("dafsfnasjfn");
        }
    }
}
