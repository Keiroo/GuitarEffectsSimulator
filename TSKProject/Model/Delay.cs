using NWaves.Audio;
using NWaves.Signals;

namespace TSKProject.Model
{
    class Delay : Effect
    {
        public DiscreteSignal Process(DiscreteSignal input, int delaySamples, float delayGain, float volume, bool bypass)
        {
            DiscreteSignal output;

            if (!bypass)
            {                
                int samples;
                float gain;

                // Default values if zeros
                if (delaySamples != 0) samples = delaySamples;
                else samples = 1;
                if (delayGain != 0) gain = delayGain;
                else gain = 0.01f;

                // Process delay
                var processed = ProcessDelay(input, samples, gain);

                // Apply volume control
                var volumeProcessed = ProcessVolume(processed, volume);

                // Apply clipping
                var clippingProcessed = ProcessClipping(volumeProcessed);

                output = clippingProcessed;
            }
            else
            {
                output = input;
            }
            

            return output;
        }

        private DiscreteSignal ProcessDelay(DiscreteSignal signal, int samples, float gain)
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
