using Microsoft.Win32;
using System.ComponentModel;
using TSKProject.Model;

namespace TSKProject.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string FileName { get; set; }
        public EffectsProperties Properties { get; set; }

        // Delay
        public int DelaySamples { get; set; }
        public float DelayGain { get; set; }
        public float DelayVolume { get; set; }
        public bool DelayBypass { get; set; }

        // Flanger
        public int FlangerSamples { get; set; }
        public float FlangerGain { get; set; }
        public float FlangerVolume { get; set; }
        public float FlangerSpeed { get; set; }
        public bool FlangerBypass { get; set; }

        // Chorus
        public int ChorusMiliseconds { get; set; }
        public float ChorusGain1 { get; set; }
        public float ChorusGain2 { get; set; }
        public float ChorusVolume { get; set; }
        public bool ChorusBypass { get; set; }

        // Distortion
        public int DistGain { get; set; }
        public int DistDistortion { get; set; }
        public float DistVolume { get; set; }
        public bool DistBypass { get; set; }

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
                DelaySamples, DelayGain, DelayVolume, DelayBypass,
                FlangerSamples, FlangerGain, FlangerVolume, FlangerSpeed, FlangerBypass,
                ChorusMiliseconds, ChorusGain1, ChorusGain2, ChorusVolume, ChorusBypass,
                DistGain, DistDistortion, DistVolume, DistBypass);

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
