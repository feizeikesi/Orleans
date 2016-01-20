using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orleans.Demo.Interfaces.EmployeesAndManagers
{
    public interface IManager : IGrainWithGuidKey
    {
        Task<IEmployee> AsEmployee();
        Task<List<IEmployee>> GetDirectReports();
        Task AddDirectReport(IEmployee employee);
    }
}