using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoList.Models;
using TodoList.Models.Context;
using TodoList.Utilities;

namespace TodoList.Services
{
    public class TodoService : BaseService, ITodoService
    {

        private readonly UserManager<TodoUser> _userManager;

        public TodoService(TodoContext context, UserManager<TodoUser> userManager) : base(context)
        {
            _userManager = userManager;
        }


        public async Task AddTodoTaskAsync(TodoTask todoTask)
        {
            await Insert<TodoTask>(todoTask);
            await Save();
        }
        public async Task<string> GetDayName(int id)
        {
            return GetEnumText.GetText<WeekDay>(id);


        }
        public async Task<TodoTask> GetTaskById(int id)
        {
            return await GetById<TodoTask>(id);

        }
        public async Task<TodoList.Models.TodoList> GetTodoListById(int id)
        {
            return await GetById<TodoList.Models.TodoList>(id);

        }
        public async Task DeleteTaskById(int id)
        {
            var task = await GetTaskById(id);
            await Delete<TodoTask>(task);
            await Save();

        }
        public async Task AddTodoListAsync(TodoList.Models.TodoList todoList)
        {

            await Insert<TodoList.Models.TodoList>(todoList);
            await Save();
        }
        public async Task<List<TodoList.Models.TodoList>> GetAllTasks(string userId)
        {
            var task = await Table<TodoList.Models.TodoList>().Where(t => t.UserId == userId).Include(c => c.TodoTasks).ToListAsync();
            return task;
        }
        public async Task DoneTaskById(int id)
        {
            var todo = await GetTaskById(id); ;
            todo.IsDone = true;
            await Update<TodoTask>(todo);
            await Save();
        }
        public List<TodoTask> GetTodos()
        {
            return Table<TodoTask>().ToList();
        }

        public async Task UpdateTask(TodoTask todo)
        {
            await Update<TodoTask>(todo);
            await Save();

        }

        public async Task UpdateGoal(Models.TodoList todo)
        {
            await Update<Models.TodoList>(todo);
            await Save();
        }

        public async Task DeleteTodoListById(int id)
        {
            var todolist = await GetTodoListById(id);
            await Delete<TodoList.Models.TodoList>(todolist);
            await Save();
        }

        public async Task EditTodoList(Models.TodoList todoList)
        {
            await Update<Models.TodoList>(todoList);
            await Save();
        }
    }
}
