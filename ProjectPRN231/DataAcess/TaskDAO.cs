using ProjectPRN231.Models;

namespace ProjectPRN231.DataAcess
{
    public class TaskDAO
    {
        public TaskDAO() { }
        public List<Models.Task> GetTasksByUserId(int userId)
        {
            using (var context = new toDoContext())
            {
                return context.Tasks
                    .Where(t => t.UserId == userId)
                    .ToList();
            }
        }
        public List<Models.Task> GetTasksByUserIdAndMilestoneId(int userId, int milestoneId)
        {
            using (var context = new toDoContext())
            {
                return context.Tasks
                    .Where(t => t.UserId == userId && t.MilestoneId == milestoneId)
                    .ToList();
            }
        }


        public void AddTask(Models.Task newTask)
        {
            using (var context = new toDoContext())
            {
                context.Tasks.Add(newTask);
                context.SaveChanges();
            }
        }

        public void DeleteTask(int taskId)
        {
            using (var context = new toDoContext())
            {
                var taskToDelete = context.Tasks.FirstOrDefault(t => t.Id == taskId);
                if (taskToDelete != null)
                {
                    context.Tasks.Remove(taskToDelete);
                    context.SaveChanges();
                }
            }
        }

        public void UpdateTask(Models.Task updatedTask)
        {
            using (var context = new toDoContext())
            {
                var existingTask = context.Tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
                if (existingTask != null)
                {
                    existingTask.Name = updatedTask.Name;
                    existingTask.Title = updatedTask.Title;
                    existingTask.Description = updatedTask.Description;
                    // Update other properties as needed
                    context.SaveChanges();
                }
            }
        }


        public List<Models.Task> GetTasksWithNullPlantId(int userid)
        {
            using (var context = new toDoContext())
            {
                return context.Tasks.Where(t => t.PlantId == null && t.UserId == userid).ToList();
            }
        }

        public Models.Task GetTaskById(int taskId)
        {
            using (var context = new toDoContext())
            {
                return context.Tasks.FirstOrDefault(t => t.Id == taskId);
            }
        }

    

        public List<Models.Task> GetTasksByPlantId(int plantId)
        {
            using (var context = new toDoContext())
            {
                return context.Tasks.Where(t => t.PlantId == plantId).ToList();
            }
        }

        public List<Models.Task> GetTasksWithNullTaskParent(int userid)
        {
            using (var context = new toDoContext())
            {
                return context.Tasks.Where(t => t.TaskParentId == null && t.UserId == userid).ToList();
            }
        }

        public List<Models.Task> GetTasksByTaskParentId(int taskParentId)
        {
            using (var context = new toDoContext())
            {
                return context.Tasks.Where(t => t.TaskParentId == taskParentId).ToList();
            }
        }

        public List<Models.Task> GetTasksByTag(int tag,int userid)
        {
            using (var context = new toDoContext())
            {
                return context.Tasks
                    .Where(t => t.TaskTags.Any(tt => tt.TagId == tag && t.UserId == userid))
                    .ToList();
            }
        }



    }
}
