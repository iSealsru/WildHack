using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DetectingAnimalsApplication.ViewModels;

namespace DetectingAnimalsApplication.Views
{
    public partial class ErrorMessageWindow : Window
    {
        public ErrorMessageWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public ErrorMessageWindow(string title, string message)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new MessageViewModel(title, message, this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
