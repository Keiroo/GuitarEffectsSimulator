using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKProject.Model
{
    class Delay : Effect
    {
        public WaveStream Process(WaveFileReader audioFile, int delaySamples, float gain)
        {
            // Convert audioFile to PCM
            WaveStream source = WaveFormatConversionStream.CreatePcmStream(audioFile);

            // Generate delayed audio
            //var offsetSampleProvider = new OffsetSampleProvider(audioFile)
            //{
            //    DelayBySamples = delaySamples
            //};
            var totalTime = source.TotalTime;
            double delaySeconds = delaySamples / (double)source.WaveFormat.SampleRate;
            var offsetAudio = new WaveOffsetStream(source, TimeSpan.FromSeconds(20), TimeSpan.Zero, source.TotalTime);

            //// Change delayed audio gain
            //var volumeSampleProvider = new VolumeSampleProvider(offsetSampleProvider);
            //if (gain <= 1.0f && gain >= 0.0f)
            //    volumeSampleProvider.Volume = gain;
            //else volumeSampleProvider.Volume = 0.5f;

            ////Change both tracks volume to avoid clipping
            //float volumeForMixing = 0.75f;
            //var volumeSampleProviderOrg = new VolumeSampleProvider(audioFile);
            //volumeSampleProviderOrg.Volume = volumeForMixing;
            //volumeSampleProvider.Volume *= volumeForMixing;

            //var sampleToWave = new SampleToWaveProvider24(offsetSampleProvider)
            //{
            //    Volume = 0.75f
            //};

            // Convert tracks to WaveChannel32
            var track1 = new WaveChannel32(source, 1f, -1f);
            var track2 = new WaveChannel32(offsetAudio, 1f, 1f);

            // Change delayed audio gain
            if (gain <= 1.0f && gain >= 0.0f)
                track2.Volume = gain;
            else track2.Volume = 0.5f;

            // Change both tracks audio to avoid clipping
            var volume = 0.7f;
            track1.Volume *= volume;
            track2.Volume *= volume;


            // Mix both tracks
            var mixer = new WaveMixerStream32
            {
                AutoStop = true
            };
            mixer.AddInputStream(track1);
            mixer.AddInputStream(track2);


            WaveFileWriter.CreateWaveFile("test2.wav", mixer);


            // Mix tracks
            //VolumeSampleProvider[] tracks = { volumeSampleProviderOrg, volumeSampleProvider };
            //var mixer = new MixingSampleProvider(tracks);

            return mixer;
        }
    }
}
