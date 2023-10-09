namespace Skillfactory.Module32.MVCStartWebApp.Repositories
{
	public interface IRequestsRepository
	{
		Task AddRequest(Request request);
		Task<Request[]> GetAllRequests();
	}
}