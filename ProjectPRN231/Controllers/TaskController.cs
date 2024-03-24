using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPRN231.DTO;
using ProjectPRN231.Models;

namespace ProjectPRN231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TaskController : ControllerBase
    {
        readonly toDo2Context _context;

        public TaskController(toDo2Context context)
        {
            _context = context;
        }

        [Authorize(Roles = "1,2,3")]
        [HttpGet("{plantId}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTaskByPlantId(int plantId)
        {
            try
            {
                //var Task = await _context.Task
                //    .Where(p => p.UserId == userId & p.HoatDong==true & p.Dflag == false)
                //    .ToListAsync();

                //if (Task == null)
                //{
                //    return NotFound();
                //}
                //            var Task = await _context.Task
                //.Include(p => p.Milestone)
                //.Include(p => p.TaskTags) 
                //.ThenInclude(pt => pt.Tag) 
                //.Where(p => p.UserId == userId && p.HoatDong == true && p.Dflag == false)
                //.ToListAsync();

                //            // Chuyển đổi từ Task sang TaskDto và lấy tên của Tag
                //            var TaskDtos = Task.Select(p => new TaskDto
                //            {
                //                Id = p.Id,
                //                Name = p.Name,
                //                Description = p.Description,
                //                UserId = p.UserId,
                //                MilestoneName = p.Milestone.Type,
                //                tagName = p.TaskTags.FirstOrDefault()?.Tag?.Name, 
                //                StartDate = p.Milestone?.PlannedStartDate,
                //                EndDate = p.Milestone?.PlannedEndDate,
                //            }).ToList();
                var query = _context.Tasks
                .Join(_context.Milestones,
                      p => p.MilestoneId,
                      m => m.Id,
                      (p, m) => new { Task = p, Milestone = m })
                .Join(_context.TaskTags,
                      pm => pm.Task.Id,
                      pt => pt.TaskId,
                      (pm, pt) => new { pm.Task, pm.Milestone, TaskTag = pt })
                .Join(_context.Tags,
                      pmp => pmp.TaskTag.TagId,
                      t => t.Id,
                      (pmp, t) => new { pmp.Task, pmp.Milestone, pmp.TaskTag, Tag = t })
                .Where(x => x.Task.PlantId == plantId && x.Task.HoatDong == true && x.Task.Dflag == false)
                .Select(x => new TaskDto
                {
                    Id = x.Task.Id,
                    Name = x.Task.Name,
                    Description = x.Task.Description,
                    planID = x.Task.PlantId,
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
        [Authorize(Roles = "3")]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<TaskDto>> GetTaskUpdate(int id)
        {
            try
            {
               
                var query = _context.Tasks
                .Join(_context.Milestones,
                      p => p.MilestoneId,
                      m => m.Id,
                      (p, m) => new { Task = p, Milestone = m })
                .Join(_context.TaskTags,
                      pm => pm.Task.Id,
                      pt => pt.TaskId,
                      (pm, pt) => new { pm.Task, pm.Milestone, TaskTag = pt })
                .Join(_context.Tags,
                      pmp => pmp.TaskTag.TagId,
                      t => t.Id,
                      (pmp, t) => new { pmp.Task, pmp.Milestone, pmp.TaskTag, Tag = t })
                .Where(x => x.Task.Id == id && x.Task.HoatDong == true && x.Task.Dflag == false)
                .Select(x => new TaskDto
                {
                    Id = x.Task.Id,
                    Name = x.Task.Name,
                    Description = x.Task.Description,
                    planID = x.Task.PlantId,
                    MilestoneName = x.Milestone.Type,
                    tagName = x.Tag.Name,
                    StartDate = x.Milestone.PlannedStartDate,
                    EndDate = x.Milestone.PlannedEndDate,
                    tag = x.Tag.Id,
                    MilestoneId = x.Milestone.Id,
                    customMilestoneCheckbox = x.Milestone.UserId ==0 ? "on" : "off"
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
        public async Task<ActionResult<Models.Task>> AddTask(TaskDto TaskDto)
        {
            try
            {
                if (TaskDto.customMilestoneCheckbox == "on")
                {
                    var milestone = new Milestone
                    {
                        Type = TaskDto.Name+ " Milestone",
                        UserId = TaskDto.UserId,
                        PlannedStartDate = TaskDto.StartDate,
                        PlannedEndDate = TaskDto.EndDate,
                        HoatDong = true,
                        Dflag = false,

                    };

                    _context.Milestones.Add(milestone);
                    await _context.SaveChangesAsync();
                    var plan = new Models.Task
                    {
                        Name = TaskDto.Name,
                        Description = TaskDto.Description,
                        PlantId = TaskDto.planID,
                        MilestoneId = milestone.Id,
                        HoatDong = true,
                        Dflag = false,
                    };
                    _context.Tasks.Add(plan);
                    await _context.SaveChangesAsync();
                    if (TaskDto.tag != null)
                    {
                        var tag = new TaskTag
                        {
                            TagId = TaskDto.tag,
                            TaskId = plan.Id
                        };
                        _context.TaskTags.Add(tag);
                        await _context.SaveChangesAsync();

                    }


                }
                else
                {
                    var plan = new Models.Task
                    {
                        Name = TaskDto.Name,
                        Description = TaskDto.Description,
                        PlantId = TaskDto.planID,
                        MilestoneId = TaskDto.MilestoneId == null ? 0 : TaskDto.MilestoneId,
                        HoatDong = true,
                        Dflag = false,
                    };
                    _context.Tasks.Add(plan);
                    await _context.SaveChangesAsync();
                    if (TaskDto.tag != null)
                    {
                        var tag = new TaskTag
                        {
                            TagId = TaskDto.tag,
                            TaskId = plan.Id
                        };
                        _context.TaskTags.Add(tag);
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
        public async Task<IActionResult> UpdateTask(int id, [FromForm] TaskDto TaskDto)
        {
            try
            {
                var existingTask = await _context.Tasks
                    .Include(p => p.Milestone)
                    .Include(p => p.TaskTags)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (existingTask == null)
                {
                    return NotFound("Plan not found");
                }

                existingTask.Name = TaskDto.Name;
                existingTask.Description = TaskDto.Description;
                existingTask.PlantId = TaskDto.planID;
                existingTask.TaskTags.FirstOrDefault().TagId = TaskDto.tag;
                // Check if the custom milestone checkbox is checked
                if (TaskDto.customMilestoneCheckbox == "on")
                {
                    // Update the milestone associated with the plan
                    if (existingTask.Milestone != null)
                    {
                        existingTask.Milestone.PlannedStartDate = TaskDto.StartDate;
                        existingTask.Milestone.PlannedEndDate = TaskDto.EndDate;
                        // Update the milestone in the database
                        _context.Milestones.Update(existingTask.Milestone);
                    }
                    else
                    {
                        // Create a new milestone if it doesn't exist
                        var milestone = new Milestone
                        {
                            Type = TaskDto.Name+" Milestone",
                            UserId = TaskDto.UserId,
                            PlannedStartDate = TaskDto.StartDate,
                            PlannedEndDate = TaskDto.EndDate,
                            HoatDong = true,
                            Dflag = false
                        };
                        // Associate the milestone with the plan
                        existingTask.Milestone = milestone;
                        // Add the new milestone to the database
                        _context.Milestones.Add(milestone);
                    }
                }
                else
                {
                    existingTask.MilestoneId = TaskDto.MilestoneId;

                }

                // Update the plan in the database
                _context.Tasks.Update(existingTask);
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
                var plan = await _context.Tasks.FindAsync(id);
                if (plan == null)
                {
                    return NotFound("Plan not found");
                }
                plan.HoatDong = false;
                plan.Dflag = true;
                _context.Tasks.Update(plan);
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
                var plan = await _context.Tasks.FindAsync(id);
                if (plan == null)
                {
                    return NotFound("Plan not found");
                }
                plan.HoatDong = false;
                plan.Dflag = false;
                _context.Tasks.Update(plan);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
