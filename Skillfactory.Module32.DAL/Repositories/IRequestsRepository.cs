namespace Skillfactory.Module32.DAL;
public interface IRequestsRepository
{
	Task AddRequest(Request request);
	Task<Request[]> GetAllRequests();
}