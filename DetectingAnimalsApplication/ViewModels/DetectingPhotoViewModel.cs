using Avalonia.Controls;
using DetectingAnimalsApplication.Models;
using DetectingAnimalsApplication.Views;
using GalaSoft.MvvmLight.Command;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace DetectingAnimalsApplication.ViewModels
{
    /// <summary>
    /// ViewModel обеспечиющая работу окна DetectingPhotoWindow
    /// </summary>
    public class DetectingPhotoViewModel : ViewModelBase
    {
        #region Поля
        /// <summary>
        /// Поле, содержащее ссылку на окно к которуму привязана ViewModel.
        /// </summary>
        private readonly Window _currentWindow;
        /// <summary>
        /// Поле, хранящее список всех загженных фотографий.
        /// </summary>
        private BindingList<FileModel> _photoList = new();
        /// <summary>
        /// Поле, хранящее прогресс работы нейронной сети.
        /// </summary>
        private int _predictValue;
        /// <summary>
        /// BackgroundWorker, предназначенный для вывода работы нейроной сети в отдельный поток.
        /// </summary>
        private readonly BackgroundWorker _worker = new();
        /// <summary>
        /// Путь, по которому требуется сохранить результат работы нейронной сети.
        /// </summary>
        private string? _absolutePath = "";
        #endregion

        /// <summary>
        /// Конструктор класса DetectingPhotoViewModel.
        /// </summary>
        /// <param name="currentWindow">Ссылка на окно к которуму привязана ViewModel.</param>
        public DetectingPhotoViewModel(Window currentWindow)
        {   // Присваивание полей.
            _currentWindow = currentWindow;
            _worker.WorkerReportsProgress = true;
            // Инициализация комманд.
            SelectPhotoCommand = new RelayCommand(LoadPhoto);
            RemoveSelectedCommand = new RelayCommand(RemoveSelectedPhoto);
            PredictCommand = new RelayCommand(Predict);
            BackCommand = new RelayCommand(GoBack);
            // Подписка на события.
            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            _worker.ProgressChanged += Worker_ProgressChanged;
            PhotosList.ListChanged += PhotosList_ListChanged;
        }

        #region Команды
        /// <summary>
        /// Команда, выполняющаяся при выделении фото.
        /// </summary>
        public RelayCommand SelectPhotoCommand { get; }
        /// <summary>
        /// Команда, выполняющаяся при удалении выделеных фотографий.
        /// </summary>
        public RelayCommand RemoveSelectedCommand { get; }
        /// <summary>
        /// Команда, запускающая работу нейронной сети.
        /// </summary>
        public RelayCommand PredictCommand { get; }
        /// <summary>
        /// Команда, выполняющая навигацию назад.
        /// </summary>
        public RelayCommand BackCommand { get; }
        #endregion

        #region Свойства
        /// <summary>
        /// Заголовок страницы.
        /// </summary>
        public override string Title => "Страница отбора фотографий";
        /// <summary>
        /// Свойство, содержащее список всех фотографий
        /// </summary>
        public BindingList<FileModel> PhotosList
        {
            get => _photoList;
            set => _ = this.RaiseAndSetIfChanged(ref _photoList, value);
        }
        /// <summary>
        /// Значение прогресса работы нейронной сети.
        /// </summary>
        public int PredictValue
        {
            get { return _predictValue; }
            set { this.RaiseAndSetIfChanged(ref _predictValue, value); }
        }
        /// <summary>
        /// Коллекция выдленных фотографий.
        /// </summary>
        public ObservableCollection<FileModel> SelectedPhotos { get; private set; } = new ObservableCollection<FileModel>();
        #endregion

        #region Обработчики событий
        /// <summary>
        /// Обработчик события BindingList.ListChanged
        /// </summary>
        private void PhotosList_ListChanged(object sender, ListChangedEventArgs e)
        {
            PhotosList = new BindingList<FileModel>(sender as BindingList<FileModel>);
        }
        /// <summary>
        /// Обработчик события BackGroudWorker.DoWork.
        /// </summary>
        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < PhotosList.Count; i++)
            {
                NNModel.Predict(PhotosList[i].Path, _absolutePath);
                _worker.ReportProgress((int)(((double)i + 1) / ((double)PhotosList.Count) * 100));
            }
        }
        /// <summary>
        /// Обработчик события завершения работы BackGroundWorker.
        /// </summary>
        private async void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            InfoMessageWindow message = new("Сообщение", $"Успешный отбор фотографий.");
            await message.ShowDialog(_currentWindow);
        }
        /// <summary>
        /// Обработчик события изменения прогресса BackgroundWorker.
        /// </summary>
        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            PredictValue = e.ProgressPercentage;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Метод, удаляющий выбранное фото.
        /// </summary>
        private void RemoveSelectedPhoto()
        {
            List<FileModel> photos = new(SelectedPhotos);
            foreach (var photo in photos)
            {
                PhotosList.Remove(photo);            
            }
        }
        /// <summary>
        /// Метод, переходящий на предыдущее окно.
        /// </summary>
        private void GoBack()
        {
            MainMenuWindow window = new();
            window.Show();
            _currentWindow.Close();
        }
        /// <summary>
        /// Метод, заружающий фотографии.
        /// </summary>
        private async void LoadPhoto()
        {
            OpenFileDialog dialog = new() {AllowMultiple= true };
            var filtres = dialog.Filters;
            filtres.Add(new FileDialogFilter() { Extensions = { "JPG" , "PNG", "BMP"}, Name = "Файлы изображений" });
            var selectedFiles = await dialog.ShowAsync(_currentWindow);
            if(selectedFiles != null)
            {
                foreach (string? selectedFile in selectedFiles)
                {
                    try
                    {
                        var name = selectedFile.Split('\\')[^1];

                        using (MemoryStream ms = new(File.ReadAllBytes(selectedFile)))
                        {
                            var a = new Bitmap(ms);
                        }
                        FileModel photoModel = new(name, selectedFile, File.ReadAllBytes(selectedFile));
                        photoModel.AddFile(PhotosList);

                    }
                    catch (Exception)
                    {
                        ErrorMessageWindow message = new("Ошибка", $"При загрузке файла {selectedFile} произошла ошибка.");
                        await message.ShowDialog(_currentWindow);
                    }
                }
            }
        }
        /// <summary>
        /// Метод запускающий нейронную сеть.
        /// </summary>
        private async void Predict()
        {
            if ((PhotosList == null || PhotosList.Count == 0))
                return;
            if (!_worker.IsBusy)
            {
                
                OpenFolderDialog openFolderDialog = new();
                _absolutePath = await openFolderDialog.ShowAsync(_currentWindow);
                _worker.RunWorkerAsync();
            }
        }
        #endregion
    }
}
