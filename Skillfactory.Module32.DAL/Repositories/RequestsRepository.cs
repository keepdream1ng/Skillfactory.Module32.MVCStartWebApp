using Microsoft.EntityFrameworkCore;

namespace Skillfactory.Module32.DAL;

public class RequestsRepository : IRequestsRepository
{
	private readonly BlogContext _context;

	public RequestsRepository(BlogContext context)
	{
		_context = context;
	}

	public async Task AddRequest(Request request)
	{
		var entry = _context.Entry(request);
		if (entry.State == EntityState.Detached)
			await _context.Requests.AddAsync(request);

		await _context.SaveChangesAsync();
	}

	public async Task<Request[]> GetAllRequests()
	{
		return await _context.Requests.ToArrayAsync();
	}
}
