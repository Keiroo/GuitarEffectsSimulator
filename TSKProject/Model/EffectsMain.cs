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
                outputDevice.Init(audioFile);
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
    }
}
