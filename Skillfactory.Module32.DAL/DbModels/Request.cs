using System.ComponentModel.DataAnnotations.Schema;

namespace Skillfactory.Module32.DAL;

[Table("Requests")]
public class Request
{
	public Guid Id { get; set; }
	public DateTime Date { get; set; }
	public string Url { get; set; }
}
