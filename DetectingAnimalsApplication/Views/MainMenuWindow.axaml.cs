using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DetectingAnimalsApplication.ViewModels;

namespace DetectingAnimalsApplication.Views
{
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new MainMenuViewModel(this);
        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
