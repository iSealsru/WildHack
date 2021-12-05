using Avalonia.Media.Imaging;
using DetectingAnimalsApplication.ViewModels;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INotifyPropertyChanged = System.ComponentModel.INotifyPropertyChanged;

namespace DetectingAnimalsApplication.Models
{
    public class FileModel 
    {
        private readonly byte[]? _bytes;
        private BindingList<FileModel> _fileModels;
        public delegate void CollectionChangedHandler(BindingList<FileModel> list);
        public event CollectionChangedHandler CollectionChanged;

        public FileModel(string name, string path, byte[]? photo = null, BindingList<FileModel> fileModels = null)
        {
            Name = name;
            Path = path;
            _bytes = photo;
            _fileModels = fileModels;
            RemoveCommand = new RelayCommand(RemoveFile);
            AddCommand = new RelayCommand<BindingList<FileModel>>(AddFile);
        }

        public Bitmap? Photo 
        {
            get
            {
                try
                {
                    if(_bytes != null)
                    {
                        using (MemoryStream ms = new MemoryStream(_bytes))
                        {
                            return new Bitmap(ms);
                        }
                    }
                }
                catch (Exception)
                {

                }
                return null;
            }
        }
        public string Name { get; }
        public string Path { get; }
        public RelayCommand RemoveCommand { get; }
        public RelayCommand<BindingList<FileModel>> AddCommand { get; }
        public void RemoveFile()
        {
            _fileModels.Remove(this);
            CollectionChanged?.Invoke(_fileModels);
        }
        public void AddFile(BindingList<FileModel> collection)
        {
            _fileModels = collection;
            _fileModels.Add(this);
            CollectionChanged?.Invoke(_fileModels);
        }
    }
}
