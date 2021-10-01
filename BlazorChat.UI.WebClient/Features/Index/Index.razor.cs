using System.ComponentModel;
using BlazorChat.UI.Shared.Features.Index;
using ReactiveUI.Blazor;

namespace BlazorChat.UI.WebClient.Features.Index
{
    public partial class Index
    {
        public Index() : this(ServiceLocator.Get<HostScreenViewModel>())
        {
            
        }
        public Index(HostScreenViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}