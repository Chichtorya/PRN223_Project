using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectPRN231.DTO;
using ProjectPRN231.Models;

namespace ProjectPRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MileStoneController : Controller
    {
        readonly toDo2Context _context;

        public MileStoneController(toDo2Context context)
        {
            _context = context;
        }

        [HttpPost()]
        public async Task<ActionResult<MileStoneDto>> CreateMileStone(MileStoneDto milestoneDto)
        {
            try
            {

                var newMileStone = new Milestone
                {
                    UserId = milestoneDto.UserId,
                    Type = milestoneDto.Type.IsNullOrEmpty() ?"0": milestoneDto.Type,
                    PlannedStartDate = milestoneDto.PlannedStartDate,
                    PlannedEndDate = milestoneDto.PlannedEndDate,
                    CreatedAt = DateTime.UtcNow,
                };
                _context.Milestones.Add(newMileStone);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<MileStoneDto>>> GetMileStonesByUserId(int userId)
        {
            try
            {

                var mileStones = await _context.Milestones
                           .Where(ms => ms.UserId == userId || ms.UserId == 0)
                           .ToListAsync();
                if (mileStones.IsNullOrEmpty())
                { return Ok("no mileStone"); }
                return mileStones.Select(ms => new MileStoneDto
                {
                    Id = ms.Id,
                    UserId = ms.UserId,
                    Type = ms.Type,
                    PlannedStartDate = ms.PlannedStartDate,
                    PlannedEndDate = ms.PlannedEndDate

                }).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


    }
}
