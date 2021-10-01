using System.Reactive;
using BlazorChat.UI.Shared.Features.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BlazorChat.UI.Shared.Features.Counter
{
    public class CounterViewModel : RoutableViewModel
    {
        [Reactive] public int CurrentCount { get; set; }
        
        public ReactiveCommand<Unit, Unit> IncrementCommand { get; private set; }

        public CounterViewModel(IScreen hostScreen) : base(hostScreen)
        {
            IncrementCommand = ReactiveCommand.Create(Increment);
        }
        
        private void Increment() => CurrentCount++;
        public override string? UrlPathSegment => "counter";
    }
}