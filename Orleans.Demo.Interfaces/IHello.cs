using System.Threading.Tasks;

namespace Orleans.Demo.Interfaces
{
    public interface IHello:IGrainWithIntegerKey
    {
        Task<string> SayHello(string msg);

    }
}