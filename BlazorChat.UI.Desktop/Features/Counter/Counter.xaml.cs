using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using BlazorChat.UI.Shared;
using BlazorChat.UI.Shared.Features.Counter;
using DynamicData.Binding;
using ReactiveUI;

namespace BlazorChat.UI.Desktop.Features.Counter
{
    public partial class Counter
    {
        public Counter(CounterViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            this.WhenActivated(d =>
            {
                if (ViewModel is null) return;

                ViewModel.WhenValueChanged(vm => vm.CurrentCount)
                    .BindTo(this, v => v.CurrentCountLabel.Content)
                    .DisposeWith(d);

                this.BindCommand(ViewModel,
                        vm => vm.IncrementCommand,
                        v => v.IncrementButton)
                    .DisposeWith(d);
            });
        }
    }
}