using System.Threading.Tasks;
using Orleans.Demo.Interfaces;

namespace Orleans.Demo.Implements
{
    public class HelloGrain:Grain,IHello

    {
        private ObserverSubscriptionManager<IChat> _subManager;

        public override Task OnActivateAsync()
        {
            _subManager=new ObserverSubscriptionManager<IChat>();
            return base.OnActivateAsync();
        }

        public async Task Subscribe(IChat observer)
        {
            _subManager.Subscribe(observer);
        }

        public async Task UnSubscribe(IChat observer)
        {
            _subManager.Unsubscribe(observer);
        }

        public Task<string> SayHello(string msg)
        {
            return Task.FromResult(string.Format("You said {0}, I say: Hello!", msg));
        }
    }
}