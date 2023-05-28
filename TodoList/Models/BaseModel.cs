using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
	public abstract class BaseModel
	{
		public int Id { get; set; }
	}
}
