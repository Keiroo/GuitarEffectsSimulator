using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NWaves;
using NWaves.Audio;
using NWaves.Effects;
using NWaves.Filters.Base;
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

            // Process delay
            var delayed = ApplyDelay(signal, delaySamples, delayGain);

            // Apply volume control
            // TODO

            // Apply clipping
            // TODO

            return delayed;
        }

        private DiscreteSignal ApplyDelay(DiscreteSignal signal, int samples, float gain)
        {
            var input = signal.Samples;
            var output = new float[input.Length];

            // Copy first samples that delay doesn't affect
            for (var i = 0; i < samples; i++)
            {
                output[i] = input[i];
            }

            // Apply delay and gain to signal
            for (var i = samples; i < signal.Length; i++)
            {
                output[i] = input[i] + gain * input[i - samples];
            }

            return new DiscreteSignal(signal.SamplingRate, output);
        }
    }
}
