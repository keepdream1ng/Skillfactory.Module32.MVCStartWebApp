namespace Skillfactory.Module32.DAL;
public interface IBlogRepository
{
	Task AddUser(User user);
	Task<User[]> GetUsers();
}