using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DetectingAnimalsApplication.Views
{
    public partial class InformationWindow : Window
    {
        public InformationWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new ViewModels.InformationViewModel(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
