using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKProject.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string FileName { get; set; }

        public MainWindowViewModel()
        {
            FileName = "File Not Loaded";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnOpenFileClick()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            var res = fileDialog.ShowDialog();

            if (res == true)
            {
                FileName = fileDialog.FileName;
                OnPropertyChanged("FileName");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
