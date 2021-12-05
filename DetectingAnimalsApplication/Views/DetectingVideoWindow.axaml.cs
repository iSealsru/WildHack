using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DetectingAnimalsApplication.ViewModels;

namespace DetectingAnimalsApplication.Views
{
    public partial class DetectingVideoWindow : Window
    {
        public DetectingVideoWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new DetectingVideoViewModel(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
