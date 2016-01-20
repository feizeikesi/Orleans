namespace Orleans.Demo.Interfaces
{
    public interface IChat:IGrainObserver
    {
        void ReceiveMessage(string message);
    }
}