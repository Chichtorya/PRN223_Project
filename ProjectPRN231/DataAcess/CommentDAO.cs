using ProjectPRN231.Models;

namespace ProjectPRN231.DataAcess
{
    public class CommentDAO
    {
        public CommentDAO() { }
        public List<Comment> GetCommentsByUserId(int userId)
        {
            using (var context = new toDoContext())
            {
                return context.Comments
                    .Where(c => c.UserId == userId)
                    .ToList();
            }
        }
        public List<Comment> GetCommentsByTask(int taskId)
        {
            using (var context = new toDoContext())
            {
                return context.Comments
                    .Where(c => c.TaskId == taskId)
                    .ToList();
            }
        }
        public List<Comment> GetCommentsByPlantId(int plantId)
        {
            using (var context = new toDoContext())
            {
                return context.Comments
                    .Where(c => c.Task.PlantId == plantId)
                    .ToList();
            }
        }
        public void UpdateComment(Comment updatedComment)
        {
            using (var context = new toDoContext())
            {
                var existingComment = context.Comments.FirstOrDefault(c => c.Id == updatedComment.Id);
                if (existingComment != null)
                {
                    existingComment.Content = updatedComment.Content;
                    existingComment.CreatedAt = updatedComment.CreatedAt;
                    // Cập nhật các thuộc tính khác nếu cần
                    context.SaveChanges();
                }
            }
        }
        public void AddComment(Comment newComment)
        {
            using (var context = new toDoContext())
            {
                context.Comments.Add(newComment);
                context.SaveChanges();
            }
        }
        public void DeleteComment(int commentId)
        {
            using (var context = new toDoContext())
            {
                var commentToDelete = context.Comments.FirstOrDefault(c => c.Id == commentId);
                if (commentToDelete != null)
                {
                    context.Comments.Remove(commentToDelete);
                    context.SaveChanges();
                }
            }
        }
        public List<Comment> GetCommentsWithNullTaskId()
        {
            using (var context = new toDoContext())
            {
                return context.Comments
                    .Where(c => c.TaskId == null)
                    .ToList();
            }
        }
        public List<Comment> GetCommentsByUserIdAndTaskId(int userId, int taskId)
        {
            using (var context = new toDoContext())
            {
                return context.Comments
                    .Where(c => c.UserId == userId && c.TaskId == taskId)
                    .ToList();
            }
        }

    }
}
