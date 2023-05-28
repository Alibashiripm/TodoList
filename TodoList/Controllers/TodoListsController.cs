using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TodoList.Models;
using TodoList.Models.Context;
using TodoList.Services;
using TodoList.Utilities;

namespace TodoList.Controllers
{
    [Authorize]
    public class TodoListsController : Controller
    {
        private readonly TodoContext _context;
        private readonly ITodoService todoService;
        private readonly UserManager<TodoUser> _userManager;

        public TodoListsController(TodoContext context,ITodoService todoService,UserManager<TodoUser> userManager)
        {
            _context = context;
            this.todoService = todoService;
            _userManager = userManager;
        }

        // GET: TodoLists
        public async Task<IActionResult> Index()
        {
            var id = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result.Id;
            var ListGoal = await todoService.GetAllTasks(id);
            ViewBag.taskList = ListGoal;
            return View(ListGoal);
        }

        // GET: TodoLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TodoLists == null)
            {
                return NotFound();
            }

            var todoList = await _context.TodoLists
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoList == null)
            {
                return NotFound();
            }

            return View(todoList);
        }

        // GET: TodoLists/Create
        public IActionResult Create()
        {
             return View();
        }
         
        // POST: TodoLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoList.Models.TodoList todoList)
        {
           
                TodoUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);



                todoList.UserId = user.Id;
                await todoService.AddTodoListAsync(todoList);

            return RedirectToAction(nameof(Index));
          
      
        }

        // GET: TodoLists/Edit/5
        [Route("EditTodolist/{id}")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TodoLists == null)
            {
                return NotFound();
            }

            var todoList = await _context.TodoLists.FindAsync(id);
            if (todoList == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.TodoUsers, "Id", "Id", todoList.UserId);
            return View(todoList);
        }

        // POST: TodoLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EditTodolist/{id}")]

        public async Task<IActionResult> Edit(int id, [Bind("Goal,UserId,TaskId,Id")] TodoList.Models.TodoList todoList)
        {
            if (id != todoList.Id)
            {
                return NotFound();
            }
          await  todoService.EditTodoList(todoList);
            return Redirect("/TodoLists");

        }

        // GET: TodoLists/Delete/5
        [Route("DeleteTodolist/{id}")]
        public async Task<IActionResult> Delete(int  id)
        {
          await  todoService.DeleteTodoListById(id);

            return Redirect("/TodoLists");
        }

    }
}
