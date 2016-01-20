using System.Threading.Tasks;

namespace Orleans.Demo.Interfaces.EmployeesAndManagers
{
    public interface IEmployee:IGrainWithGuidKey
    {
        Task<int> GetLevel();
        Task Promote(int newLevel);

        Task<IManager> GetManager();
        Task SetManager(IManager manager);

        Task Greeting(IEmployee from, string message);
    }
}
