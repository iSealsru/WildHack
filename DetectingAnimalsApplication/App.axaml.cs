using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DetectingAnimalsApplication.ViewModels;
using DetectingAnimalsApplication.Views;
using Yolov5Net.Scorer;
using Yolov5Net.Scorer.Models;

namespace DetectingAnimalsApplication
{
    public class App : Application
    {
        
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainMenuWindow();
                desktop.MainWindow.DataContext = new MainMenuViewModel(desktop.MainWindow);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
