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
        public DiscreteSignal Process(WaveFile waveFile, int delaySamples, float gain)
        {
            var signal = waveFile[Channels.Left];
            //var delay = new DelayEffect(delaySamples / waveFile.WaveFmt.SamplingRate, 0.5f);
            var delay = new DelayEffect(delaySamples / (float)waveFile.WaveFmt.SamplingRate, gain);
            var processed = delay.ApplyTo(signal);


            return processed;
        }
    }
}
