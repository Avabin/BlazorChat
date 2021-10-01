using BlazorChat.UI.Shared;
using BlazorChat.UI.Shared.Features.Counter;
using ReactiveUI.Blazor;

namespace BlazorChat.UI.WebClient.Features.Counter
{
    public partial class Counter
    {
        public Counter() : this(ServiceLocator.Get<CounterViewModel>())
        {
            
        }
        public Counter(CounterViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}