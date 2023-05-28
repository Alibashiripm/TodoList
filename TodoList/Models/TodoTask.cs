namespace TodoList.Models
{
	public class TodoTask :BaseModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
        public int WeekDaynumber { get; set; }

        public bool IsDone { get; set; }
        public int TodoListId { get; set; }
        public virtual TodoList TodoList { get; set; }
    }
}
