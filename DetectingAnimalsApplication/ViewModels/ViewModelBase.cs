using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace DetectingAnimalsApplication.ViewModels
{
    /// <summary>
    /// Базовый класс ViewModel.
    /// </summary>
    public abstract class ViewModelBase : ReactiveObject
    {
        /// <summary>
        /// Заголовок страницы.
        /// </summary>
        public abstract string Title { get; }
    }
}
