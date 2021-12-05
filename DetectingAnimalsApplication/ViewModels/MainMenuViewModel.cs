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
    public class MainMenuViewModel : ViewModelBase
    {
        /// <summary>
        /// Ссылка на окно, которому принадлежит ViewModel.
        /// </summary>
        private readonly Window _currentWindow;

        /// <summary>
        /// Конструктор класса MainMenuViewModel.
        /// </summary>
        /// <param name="currentWindow">Ссылка на окно, которому принадлежит ViewModel.</param>
        public MainMenuViewModel(Window currentWindow)
        {
            // Инициализация полей
            _currentWindow = currentWindow;
            // Инициализация команд
            NavigateDetectingPhoto = new RelayCommand(NavigateToDetectingPhotoWindow);
            NavigateDetectingVideo = new RelayCommand(NavigateToDetectingVideoWindow);
            NavigateInformation = new RelayCommand(NavigateToInformationWindow);
            ExitCommand = new RelayCommand(Close);
        }
        /// <summary>
        /// Заголовок страницы.
        /// </summary>
        public override string Title => "Главная";

        #region Команды
        /// <summary>
        /// Команда, переводящая пользователя на окно DetectingPhotoWindow.
        /// </summary>
        public RelayCommand NavigateDetectingPhoto { get;}
        /// <summary>
        /// Команда, переводящая пользователя на окно DetectingVideoWindow.
        /// </summary>
        public RelayCommand NavigateDetectingVideo { get;}
        /// <summary>
        /// Команда, переводящая пользователя на окно InformaatioWindow.
        /// </summary>
        public RelayCommand NavigateInformation { get;}
        /// <summary>
        /// Команда, закрывающая окно.
        /// </summary>
        public RelayCommand ExitCommand { get; }
        #endregion


        #region Методы
        /// <summary>
        /// Метод, переводящий пользователя на окно DetectingPhotoWindow.
        /// </summary>
        private void NavigateToDetectingPhotoWindow()
        {
            DetectingPhotoWindow window = new();
            window.Show();
            Close();
        }
        /// <summary>
        /// Метод, переводящий пользователя на окно DetectingVideoWindow.
        /// </summary>
        private void NavigateToDetectingVideoWindow()
        {
            DetectingVideoWindow window = new();
            window.Show();
            Close();
        }
        /// <summary>
        /// Метод, переводящий пользователя на окно InformationWindow.
        /// </summary>
        private void NavigateToInformationWindow()
        {
            InformationWindow window = new();
            window.Show();
            Close();
        }

        /// <summary>
        /// Метод, закрывающий окно.
        /// </summary>
        private void Close()
        {
            _currentWindow.Close();
        }

        #endregion       
    }
}
