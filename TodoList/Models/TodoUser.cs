using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class TodoUser: IdentityUser
    {
  

        public virtual List<TodoList> TodoList{ get; set; }    
    }
}
