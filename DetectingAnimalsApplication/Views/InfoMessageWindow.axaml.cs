using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DetectingAnimalsApplication.ViewModels;

namespace DetectingAnimalsApplication.Views
{
    public partial class InfoMessageWindow : Window
    {
        public InfoMessageWindow(string title, string message)
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new MessageViewModel(title, message, this);
        }
        public InfoMessageWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
