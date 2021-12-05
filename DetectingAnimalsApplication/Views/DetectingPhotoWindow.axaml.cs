using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DetectingAnimalsApplication.ViewModels;

namespace DetectingAnimalsApplication.Views
{
    public partial class DetectingPhotoWindow : Window
    {
        public DetectingPhotoWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new DetectingPhotoViewModel(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
