using Microsoft.AspNetCore.Identity;

namespace TodoList.Models
{
    public class TodoList:BaseModel
    {
        public string Goal { get; set; }
        public string UserId { get; set; }
        public virtual TodoUser User { get; set; }
        public virtual List<TodoTask> TodoTasks { get; set; }  
    }
}
