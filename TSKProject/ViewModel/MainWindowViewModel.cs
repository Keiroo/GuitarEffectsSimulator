﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSKProject.Model;

namespace TSKProject.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string FileName { get; set; }
        public EffectsProperties Properties { get; set; }
        public int DelaySamples { get; set; }
        public float DelayGain { get; set; }
        public float DelayVolume { get; set; }
        public bool DelayBypass { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        

        public MainWindowViewModel()
        {
            main = new EffectsMain();
            FileName = "File Not Loaded";
        }        

        public void OnOpenFileClick()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            var res = fileDialog.ShowDialog();

            if (res == true)
            {
                FileName = fileDialog.FileName;
                main.LoadFile(FileName);
                OnPropertyChanged("FileName");
            }
        }

        public void OnPlayClick()
        {
            // Apply saved variables to sturcture
            var properties = new EffectsProperties(
                DelaySamples, DelayGain, DelayVolume, DelayBypass);

            main.PlayAsync(properties);
        }

        public void OnPlayUnprocessedClick()
        {
            main.PlayUnprocessed();
        }

        public void OnStopClick()
        {
            main.Stop();
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }       

        private EffectsMain main;
    }
}
