using Microsoft.CodeAnalysis.Differencing;
using TodoList.Models;

namespace TodoList.Services
{
	public interface ITodoService
	{
		Task AddTodoTaskAsync(TodoTask todo);
		Task<string> GetDayName(int id);

        Task AddTodoListAsync(TodoList.Models.TodoList todoList);
		Task<List<TodoList.Models.TodoList>> GetAllTasks(string userId);
        List<TodoTask> GetTodos();
		Task<TodoTask> GetTaskById(int id);
		Task UpdateTask(TodoTask todo);
		Task UpdateGoal(TodoList.Models.TodoList todo);
		Task DeleteTaskById(int id);
		Task DoneTaskById(int id);
		Task DeleteTodoListById(int id);
		Task EditTodoList(TodoList.Models.TodoList todoList);
    }
}
