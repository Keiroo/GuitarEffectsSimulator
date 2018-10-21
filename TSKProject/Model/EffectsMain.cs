using NWaves.Audio;
using NWaves.Audio.Interfaces;
using NWaves.Audio.Mci;
using NWaves.Signals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            delay = new Delay();
        }

        public async void PlayAsync()
        {
            if (waveFile != null)
            {
                // Process delay
                DiscreteSignal processed = delay.Process(waveFile, 10000, 0.5f);
                
                if (audioPlayer == null)
                {
                    audioPlayer = new MciAudioPlayer();
                }

                audioPlayer.Stop();

                var processedFileName = string.Format("{0}.wav", Guid.NewGuid());
                using (var stream = new FileStream(processedFileName, FileMode.Create))
                {
                    var waveFile = new WaveFile(processed);
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

        private WaveFile waveFile;
        private string fileName;
        private MciAudioPlayer audioPlayer;
        private Delay delay;
    }
}
