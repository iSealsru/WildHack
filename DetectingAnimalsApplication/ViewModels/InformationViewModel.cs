using Avalonia.Controls;
using DetectingAnimalsApplication.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectingAnimalsApplication.ViewModels
{
    /// <summary>
    /// ViewModel обеспечиющая работу окна InformationWindow.
    /// </summary>
    public class InformationViewModel : ViewModelBase
    {
        /// <summary>
        /// Содержит ссылку на окно, к которому привязана ViewModel.
        /// </summary>
        private readonly Window _currentWindow;
        /// <summary>
        /// Конструктор класса InformationViewModel.
        /// </summary>
        /// <param name="currentWindow">Ссылка на окно, к которому привязана ViewModel.</param>
        public InformationViewModel(Window currentWindow)
        {
            // Инициализация полей.
            _currentWindow = currentWindow;
            // Инициализация команд.
            BackCommand = new RelayCommand(GoBack);
        }
        /// <summary>
        /// Команда возвращающяя пользователя на предыдущее окно.
        /// </summary>
        public RelayCommand BackCommand { get; }
        /// <summary>
        /// Заголовок страницы.
        /// </summary>
        public override string Title => "Информация";
        /// <summary>
        /// Метод возвращающий пользователя на предыдущее окно.
        /// </summary>
        private void GoBack()
        {
            MainMenuWindow window = new();
            window.Show();
            _currentWindow.Close();
        }
    }
}
