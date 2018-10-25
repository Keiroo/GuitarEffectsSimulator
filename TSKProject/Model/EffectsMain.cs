﻿using NWaves.Audio;
using NWaves.Audio.Mci;
using NWaves.Signals;
using System;
using System.IO;

namespace TSKProject.Model
{
    class EffectsMain
    {
        public void LoadFile(string fileName)
        {
            this.fileName = fileName;
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                waveFile = new WaveFile(stream);
            }

            // Create effects objects
            delay = new Delay();
        }

        public async void PlayAsync(EffectsProperties properties)
        {
            if (waveFile != null)
            {
                // Process delay
                DiscreteSignal delayProcessed = delay.Process(
                    waveFile,
                    properties.DelaySamples, properties.DelayGain,
                    properties.DelayVolume, properties.DelayBypass);

                // Redirect last proccesed effect to output
                var output = delayProcessed;

                if (audioPlayer == null)
                {
                    audioPlayer = new MciAudioPlayer();
                }

                audioPlayer.Stop();                

                // Save processed file to stream
                var processedFileName = string.Format("{0}.wav", Guid.NewGuid());
                using (var stream = new FileStream(processedFileName, FileMode.Create))
                {
                    var waveFile = new WaveFile(output);
                    waveFile.SaveTo(stream);
                }

                await audioPlayer.PlayAsync(processedFileName);

                // cleanup temporary file
                File.Delete(processedFileName);
            }
        }

        public async void PlayUnprocessed()
        {
            if (waveFile != null)
            {
                if (audioPlayer == null)
                {
                    audioPlayer = new MciAudioPlayer();
                }

                audioPlayer.Stop();

                await audioPlayer.PlayAsync(fileName);
            }
        }

        public void Stop()
        {
            if (audioPlayer != null)
            {
                audioPlayer.Stop();
            }            
        }

        private WaveFile waveFile;
        private string fileName;
        private MciAudioPlayer audioPlayer;
        private Delay delay;
    }
}
