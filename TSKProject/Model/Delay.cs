using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NWaves;
using NWaves.Audio;
using NWaves.Effects;
using NWaves.Signals;

namespace TSKProject.Model
{
    class Delay : Effect
    {
        public DiscreteSignal Process(WaveFile waveFile, int delaySamples, float delayGain)
        {
            var signal = waveFile[Channels.Left];
            //var delay = new DelayEffect(delaySamples / waveFile.WaveFmt.SamplingRate, 0.5f);
            int samples;
            float gain;

            // Default values if zeros
            if (delaySamples != 0) samples = delaySamples;
            else samples = 1;
            if (delayGain != 0) gain = delayGain;
            else gain = 0.01f;

            var delay = new DelayEffect(samples / (float)waveFile.WaveFmt.SamplingRate, gain);
            var processed = delay.ApplyTo(signal);


            return processed;
        }
    }
}
