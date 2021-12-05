using Avalonia.Controls;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectingAnimalsApplication.ViewModels
{
    /// <summary>
    /// ViewModel обеспечивающая отображение сообщений.
    /// </summary>
    public class MessageViewModel
    {
        /// <summary>
        /// Ссылка на окно, к которому привязана ViewModel.
        /// </summary>
        private readonly Window _currentWindow;
        /// <summary>
        /// Конструктор класса MessageViewModel.
        /// </summary>
        /// <param name="title">Заголовок сообщения.</param>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="currentWindow">Ссылка на окно, к которому привязана ViewModel.</param>
        public MessageViewModel(string title, string message, Window currentWindow)
        {
            // Инициализация свойств.
            Title = title;
            Message = message;
            // Инициализация полей.
            _currentWindow = currentWindow;
            // Инициализация команд.
            OkCommand = new RelayCommand(Close);
        }

        /// <summary>
        /// Заголовок страницы.
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// Команда, выполняющаяся при нажатии на кнопку "Ок"
        /// </summary>
        public RelayCommand OkCommand { get; private set; }
        /// <summary>
        /// Метод, закрывающий окно.
        /// </summary>
        private void Close()
        {
            _currentWindow.Close();
        }
    }
}
