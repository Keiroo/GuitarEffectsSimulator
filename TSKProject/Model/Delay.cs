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
        public IWaveProvider Process(AudioFileReader audioFile, int delaySamples, float gain)
        {
            // Generate delayed audio
            var offsetSampleProvider = new OffsetSampleProvider(audioFile)
            {
                DelayBySamples = delaySamples
            };

            // Change delayed audio gain
            var volumeSampleProvider = new VolumeSampleProvider(offsetSampleProvider);
            if (gain <= 1.0f && gain >= 0.0f)
                volumeSampleProvider.Volume = gain;
            else volumeSampleProvider.Volume = 0.5f;

            // Change both tracks volume to avoid clipping
            float volumeForMixing = 0.75f;
            var volumeSampleProviderOrg = new VolumeSampleProvider(audioFile);
            volumeSampleProviderOrg.Volume = volumeForMixing;
            volumeSampleProvider.Volume *= volumeForMixing;

            // Mix tracks
            VolumeSampleProvider[] tracks = { volumeSampleProviderOrg, volumeSampleProvider };
            var mixer = new MixingSampleProvider(tracks);

            //return mixer.ToWaveProvider();
            return mixer.ToWaveProvider();
        }
    }
}
