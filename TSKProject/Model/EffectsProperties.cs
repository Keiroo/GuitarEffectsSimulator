using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKProject.Model
{
    public struct EffectsProperties : INotifyPropertyChanged
    {
        public int DelaySamples { get; private set; }
        public float DelayGain { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetDelaySamples(int value)
        {
            DelaySamples = value;
            OnPropertyChanged("DelaySamples");
        }

        public void SetDelayGain(float value)
        {
            DelayGain = value;
            OnPropertyChanged("DelayGain");
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
