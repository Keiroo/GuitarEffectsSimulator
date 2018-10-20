using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKProject.Model
{
    class EffectsMain
    {
        public EffectsMain()
        {
            delay = new Delay();
        }

        public void LoadFile(string fileName)
        {
            audioFile = new AudioFileReader(fileName);
        }

        public void Play()
        {
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += OnPlaybackStopped;
            }
            if (audioFile != null)
            {
                // Temp delay processing test
                IWaveProvider processed = delay.Process(audioFile, 1000, 0.1f);

                outputDevice.Init(processed);
                outputDevice.Play();
            }
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            outputDevice.Dispose();
            outputDevice = null;
        }

        private AudioFileReader audioFile;
        private WaveOutEvent outputDevice;
        private Delay delay;
    }
}
