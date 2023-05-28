using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoList.Models;
using TodoList.Models.Context;
using TodoList.Services;

namespace TodoList.Controllers
{
    public class TodoTasksController : Controller
    {
        private readonly TodoContext _context;
        private readonly ITodoService todoService;

        public TodoTasksController(TodoContext context, ITodoService todoService)
        {
            _context = context;
            this.todoService = todoService;
        }

        // GET: TodoTasks
        public async Task<IActionResult> Index()
        {
            var todoContext = _context.TodoTasks.Include(t => t.TodoList);
            return View(await todoContext.ToListAsync());
        }

        // GET: TodoTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TodoTasks == null)
            {
                return NotFound();
            }

            var todoTask = await _context.TodoTasks
                .Include(t => t.TodoList)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoTask == null)
            {
                return NotFound();
            }

            return View(todoTask);
        }

        // GET: TodoTasks/Create
        [Route("AddTask/{id}")]
        public IActionResult Create([FromRoute] int id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: TodoTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AddTask/{todoListId}")]

        public async Task<IActionResult> Create(TodoTask todoTask, [FromRoute] int todoListId)
        {
            todoTask.TodoListId = todoListId;

            await todoService.AddTodoTaskAsync(todoTask);
            return Redirect("/");

        }

        // GET: TodoTasks/Edit/5
        [Route("EditTask/{Id}")]

        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var todoTask = await todoService.GetTaskById(id);

            if (todoTask == null)
            {
                return NotFound();
            }
            return View(todoTask);
        }

        // POST: TodoTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EditTask/{Id}")]

        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,WeekDaynumber,IsDone,TodoListId,Id")] TodoTask todoTask)
        {
            if (id != todoTask.Id)
            {
                return NotFound();
            }

           await todoService.UpdateTask(todoTask);
            return Redirect("/TodoLists");

        }
        [Route("DeleteTask/{Id}")]
        public async Task<IActionResult> Delete(int id)
        {

           await todoService.DeleteTaskById(id);
            return Redirect("/TodoLists");
        }

        [Route("DoneTask/{Id}")]
        public async Task<IActionResult> Done (int id)
        {

           await todoService.DoneTaskById(id);
            return Redirect("/TodoLists");
        }



        private bool TodoTaskExists(int id)
        {
            return (_context.TodoTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
