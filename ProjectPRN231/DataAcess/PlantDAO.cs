using ProjectPRN231.Models;

namespace ProjectPRN231.DataAcess
{
    public class PlantDAO
    {
        public void AddPlant(Plant newPlant)
        {
            using (var context = new toDoContext())
            {
                context.Plants.Add(newPlant);
                context.SaveChanges();
            }
        }
        public Plant GetPlantById(int plantId, int userId)
        {
            using (var context = new toDoContext())
            {
                return context.Plants.FirstOrDefault(p => p.Id == plantId && p.UserId == userId);
            }
        }
        public Plant GetPlantByMilestoneId(int milestoneId, int userId)
        {
            using (var context = new toDoContext())
            {
                return context.Plants.FirstOrDefault(p => p.MilestoneId == milestoneId && p.UserId == userId);
            }
        }
        public void UpdatePlant(Plant updatedPlant)
        {
            using (var context = new toDoContext())
            {
                var existingPlant = context.Plants.FirstOrDefault(p => p.Id == updatedPlant.Id && p.UserId == updatedPlant.UserId);
                if (existingPlant != null)
                {
                    existingPlant.Name = updatedPlant.Name;
                    existingPlant.Description = updatedPlant.Description;
                    existingPlant.Title = updatedPlant.Title;
                    // Cập nhật các thuộc tính khác nếu cần
                    context.SaveChanges();
                }
            }
        }
        public void DeletePlant(int plantId, int userId)
        {
            using (var context = new toDoContext())
            {
                var plantToDelete = context.Plants.FirstOrDefault(p => p.Id == plantId && p.UserId == userId);
                if (plantToDelete != null)
                {
                    context.Plants.Remove(plantToDelete);
                    context.SaveChanges();
                }
            }
        }

    }
}
