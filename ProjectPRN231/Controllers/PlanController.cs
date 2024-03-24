using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPRN231.DTO;
using ProjectPRN231.Models;
using System.Globalization;
using System.Numerics;
using System.Threading.Tasks;

namespace ProjectPRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PlanController : Controller
    {
        readonly toDo2Context _context;

        public PlanController(toDo2Context context)
        {
            _context = context;
        }
        [Authorize(Roles = "1,2,3")]
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<PlanDto>>> GetPlantsByUserId(int userId)
        {
            try
            {
                //var Plants = await _context.Plants
                //    .Where(p => p.UserId == userId & p.HoatDong==true & p.Dflag == false)
                //    .ToListAsync();

                //if (Plants == null)
                //{
                //    return NotFound();
                //}
                //            var Plants = await _context.Plants
                //.Include(p => p.Milestone)
                //.Include(p => p.PlantTags) 
                //.ThenInclude(pt => pt.Tag) 
                //.Where(p => p.UserId == userId && p.HoatDong == true && p.Dflag == false)
                //.ToListAsync();

                //            // Chuyển đổi từ Plant sang PlanDto và lấy tên của Tag
                //            var planDtos = Plants.Select(p => new PlanDto
                //            {
                //                Id = p.Id,
                //                Name = p.Name,
                //                Description = p.Description,
                //                UserId = p.UserId,
                //                MilestoneName = p.Milestone.Type,
                //                tagName = p.PlantTags.FirstOrDefault()?.Tag?.Name, 
                //                StartDate = p.Milestone?.PlannedStartDate,
                //                EndDate = p.Milestone?.PlannedEndDate,
                //            }).ToList();
                var query = _context.Plants
                .Join(_context.Milestones,
                      p => p.MilestoneId,
                      m => m.Id,
                      (p, m) => new { Plant = p, Milestone = m })
                .Join(_context.PlantTags,
                      pm => pm.Plant.Id,
                      pt => pt.PlantId,
                      (pm, pt) => new { pm.Plant, pm.Milestone, PlantTag = pt })
                .Join(_context.Tags,
                      pmp => pmp.PlantTag.TagId,
                      t => t.Id,
                      (pmp, t) => new { pmp.Plant, pmp.Milestone, pmp.PlantTag, Tag = t })
                .Where(x => x.Plant.UserId == userId && x.Plant.HoatDong == true && x.Plant.Dflag == false)
                .Select(x => new PlanDto
                {
                    Id = x.Plant.Id,
                    Name = x.Plant.Name,
                    Description = x.Plant.Description,
                    UserId = x.Plant.UserId,
                    MilestoneName = x.Milestone.Type,
                    tagName = x.Tag.Name,
                    StartDate = x.Milestone.PlannedStartDate,
                    EndDate = x.Milestone.PlannedEndDate,
                })
                .ToList();


                return query;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);

            }
        }
        [Authorize(Roles = "1,2,3")]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<PlanDto>> GetPlantsUpdate(int id)
        {
            try
            {
                //var Plants = await _context.Plants
                //    .Where(p => p.UserId == userId & p.HoatDong==true & p.Dflag == false)
                //    .ToListAsync();

                //if (Plants == null)
                //{
                //    return NotFound();
                //}
                //            var Plants = await _context.Plants
                //.Include(p => p.Milestone)
                //.Include(p => p.PlantTags) 
                //.ThenInclude(pt => pt.Tag) 
                //.Where(p => p.UserId == userId && p.HoatDong == true && p.Dflag == false)
                //.ToListAsync();

                //            // Chuyển đổi từ Plant sang PlanDto và lấy tên của Tag
                //            var planDtos = Plants.Select(p => new PlanDto
                //            {
                //                Id = p.Id,
                //                Name = p.Name,
                //                Description = p.Description,
                //                UserId = p.UserId,
                //                MilestoneName = p.Milestone.Type,
                //                tagName = p.PlantTags.FirstOrDefault()?.Tag?.Name, 
                //                StartDate = p.Milestone?.PlannedStartDate,
                //                EndDate = p.Milestone?.PlannedEndDate,
                //            }).ToList();
                var query = _context.Plants
                .Join(_context.Milestones,
                      p => p.MilestoneId,
                      m => m.Id,
                      (p, m) => new { Plant = p, Milestone = m })
                .Join(_context.PlantTags,
                      pm => pm.Plant.Id,
                      pt => pt.PlantId,
                      (pm, pt) => new { pm.Plant, pm.Milestone, PlantTag = pt })
                .Join(_context.Tags,
                      pmp => pmp.PlantTag.TagId,
                      t => t.Id,
                      (pmp, t) => new { pmp.Plant, pmp.Milestone, pmp.PlantTag, Tag = t })
                .Where(x => x.Plant.Id == id && x.Plant.HoatDong == true && x.Plant.Dflag == false)
                .Select(x => new PlanDto
                {
                    Id = x.Plant.Id,
                    Name = x.Plant.Name,
                    Description = x.Plant.Description,
                    UserId = x.Plant.UserId,
                    MilestoneName = x.Milestone.Type,
                    tagName = x.Tag.Name,
                    StartDate = x.Milestone.PlannedStartDate,
                    EndDate = x.Milestone.PlannedEndDate,
                    tag = x.Tag.Id,
                    MilestoneId = x.Milestone.Id,
                    customMilestoneCheckbox = x.Milestone.UserId == 0 ? "on" : "off"
                })
                .FirstOrDefault();


                return query;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);

            }
        }
        [Authorize(Roles = "3")]

        [HttpPost()]
        public async Task<ActionResult<Plant>> AddPlan(PlanDto planDto)
        {
            try
            {
                if (planDto.customMilestoneCheckbox == "on")
                {
                    var milestone = new Milestone
                    {
                        Type = planDto.Name + " Milestone",
                        UserId = planDto.UserId,
                        PlannedStartDate = planDto.StartDate,
                        PlannedEndDate = planDto.EndDate,
                        HoatDong = true,
                        Dflag = false,

                    };

                    _context.Milestones.Add(milestone);
                    await _context.SaveChangesAsync();
                    var plan = new Plant
                    {
                        Name = planDto.Name,
                        Description = planDto.Description,
                        UserId = planDto.UserId,
                        MilestoneId = milestone.Id,
                        HoatDong = true,
                        Dflag = false,
                    };
                    _context.Plants.Add(plan);
                    await _context.SaveChangesAsync();
                    if (planDto.tag != null)
                    {
                        var tag = new PlantTag
                        {
                            TagId = planDto.tag,
                            PlantId = plan.Id
                        };
                        _context.PlantTags.Add(tag);
                        await _context.SaveChangesAsync();

                    }


                }
                else
                {
                    var plan = new Plant
                    {
                        Name = planDto.Name,
                        Description = planDto.Description,
                        UserId = planDto.UserId,
                        MilestoneId = planDto.MilestoneId == null ? 0 : planDto.MilestoneId,
                        HoatDong = true,
                        Dflag = false,
                    };
                    _context.Plants.Add(plan);
                    await _context.SaveChangesAsync();
                    if (planDto.tag != null)
                    {
                        var tag = new PlantTag
                        {
                            TagId = planDto.tag,
                            PlantId = plan.Id
                        };
                        _context.PlantTags.Add(tag);
                        await _context.SaveChangesAsync();

                    }


                }


                return Ok("Create Plan Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "3")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlan(int id, [FromForm] PlanDto planDto)
        {
            try
            {
                var existingPlan = await _context.Plants
                    .Include(p => p.Milestone)
                    .Include(p => p.PlantTags)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (existingPlan == null)
                {
                    return NotFound("Plan not found");
                }

                existingPlan.Name = planDto.Name;
                existingPlan.Description = planDto.Description;
                existingPlan.UserId = planDto.UserId;
                existingPlan.PlantTags.FirstOrDefault().TagId = planDto.tag;
                // Check if the custom milestone checkbox is checked
                if (planDto.customMilestoneCheckbox == "on")
                {
                    // Update the milestone associated with the plan
                    if (existingPlan.Milestone != null)
                    {
                        existingPlan.Milestone.PlannedStartDate = planDto.StartDate;
                        existingPlan.Milestone.PlannedEndDate = planDto.EndDate;
                        // Update the milestone in the database
                        _context.Milestones.Update(existingPlan.Milestone);
                    }
                    else
                    {
                        // Create a new milestone if it doesn't exist
                        var milestone = new Milestone
                        {
                            Type = planDto.Name + " Milestone",
                            UserId = planDto.UserId,
                            PlannedStartDate = planDto.StartDate,
                            PlannedEndDate = planDto.EndDate,
                            HoatDong = true,
                            Dflag = false
                        };
                        // Associate the milestone with the plan
                        existingPlan.Milestone = milestone;
                        // Add the new milestone to the database
                        _context.Milestones.Add(milestone);
                    }
                }
                else
                {
                    existingPlan.MilestoneId = planDto.MilestoneId;

                }

                // Update the plan in the database
                _context.Plants.Update(existingPlan);
                await _context.SaveChangesAsync();

                return Ok("Update Plan Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(int id)
        {
            try
            {
                var plan = await _context.Plants.FindAsync( id);
                if (plan == null)
                {
                    return NotFound("Plan not found");
                }
                plan.HoatDong = false;
                plan.Dflag = true;
               

                plan.LastUpdate = DateTime.Today;
                _context.Plants.Update(plan);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "3")]
        [HttpGet("compelete/{id}")]
        public async Task<IActionResult> Complete(int id)
        {
            try
            {
                var plan = await _context.Plants.FindAsync(id);
                if (plan == null)
                {
                    return NotFound("Plan not found");
                }
                plan.HoatDong = false;
                plan.Dflag = false;
               

                plan.LastUpdate= DateTime.Today;
                _context.Plants.Update(plan);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Export")]
        public async Task<IActionResult> Export(ExportDto ex)
        {
            try
            {

                // Lấy tổng số plan và tổng số task của mỗi người dùng
                var userPlans = await _context.Plants.Where(p => p.UserId == ex.userId).CountAsync();
                var userTasks = await _context.Tasks.Where(t => t.Plant.UserId == ex.userId).CountAsync();

                // Lấy số lượng task được tạo trong khoảng thời gian từ 'from' đến 'to' cho mỗi người dùng
                var tasksCreatedInRange = await _context.Tasks
                    .Include(x => x.Milestone)
                    .Where(t => t.Plant.UserId == ex.userId && t.Milestone.PlannedStartDate >= ex.from && t.Milestone.PlannedEndDate <= ex.to)
                    .CountAsync();

                // Lấy số lượng plan được tạo trong khoảng thời gian từ 'from' đến 'to' cho mỗi người dùng
                var plansCreatedInRange = await _context.Plants
                                        .Include(x => x.Milestone)
                    .Where(p => p.UserId == ex.userId && p.Milestone.PlannedStartDate >= ex.from && p.Milestone.PlannedEndDate <= ex.to)
                    .CountAsync();

                // Lấy số lượng plan và task hoàn thành trong khoảng thời gian từ 'from' đến 'to' cho mỗi người dùng
                var completedPlans = await _context.Plants
                    .Where(p => p.UserId == ex.userId && p.HoatDong == false && p.Dflag == false && p.LastUpdate >= ex.from && p.LastUpdate <= ex.to)
                    .CountAsync();
                var completedTasks = await _context.Tasks
                    .Where(t => t.Plant.UserId == ex.userId && t.HoatDong == false && t.Dflag == false && t.LastUpdate >= ex.from &&  t.LastUpdate <= ex.to)
                    .CountAsync();

                // Lấy số lượng plan và task bị xóa trong khoảng thời gian từ 'from' đến 'to' cho mỗi người dùng
                var deletedPlans = await _context.Plants
                    .Where(p => p.UserId == ex.userId && !p.HoatDong == true && p.Dflag == true &&  p.LastUpdate >= ex.from &&  p.LastUpdate <= ex.to)
                    .CountAsync();
                var deletedTasks = await _context.Tasks
                    .Where(t => t.Plant.UserId == ex.userId && !t.HoatDong == true && t.Dflag == true &&  t.LastUpdate >= ex.from &&  t.LastUpdate <= ex.to)
                    .CountAsync();

                // Lấy số lượng plan và task vượt quá hạn chót trong khoảng thời gian từ 'from' đến 'to' cho mỗi người dùng
                var overduePlans = await _context.Plants
                                     .Include(x => x.Milestone)
                    .Where(p => p.UserId == ex.userId && p.HoatDong == true && p.Dflag == false && p.Milestone.PlannedEndDate < DateTime.Now && p.Milestone.PlannedEndDate >= ex.from && p.Milestone.PlannedEndDate <= ex.to)
                    .CountAsync();
                var overdueTasks = await _context.Tasks
                                        .Include(x => x.Milestone)
                    .Where(t => t.Plant.UserId == ex.userId && t.HoatDong == true && t.Dflag == false && t.Milestone.PlannedEndDate < DateTime.Now && t.Milestone.PlannedEndDate >= ex.from && t.Milestone.PlannedEndDate <= ex.to)
                    .CountAsync();

                // Trả về các thông tin về export
                var exportInfo = new
                {
                    TotalPlans = userPlans,
                    TotalTasks = userTasks,
                    TasksCreatedInRange = tasksCreatedInRange,
                    PlansCreatedInRange = plansCreatedInRange,
                    CompletedPlans = completedPlans,
                    CompletedTasks = completedTasks,
                    DeletedPlans = deletedPlans,
                    DeletedTasks = deletedTasks,
                    OverduePlans = overduePlans,
                    OverdueTasks = overdueTasks
                };

                return Ok(exportInfo);
            }
            catch (Exception exs)
            {
                return BadRequest(exs);
            }
        }
    }

}
