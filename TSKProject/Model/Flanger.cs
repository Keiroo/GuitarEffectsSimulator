using NWaves.Audio;
using NWaves.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKProject.Model
{
    class Flanger : Effect
    {
        public DiscreteSignal Process(DiscreteSignal input, int flangerSamples, float flangerGain, float volume, float flangerSpeed, bool bypass)
        {
            DiscreteSignal output;

            if (!bypass)
            {
                int samples;
                float gain, speed;

                // Default values if zeros
                if (flangerSamples != 0) samples = flangerSamples;
                else samples = 1;
                if (flangerGain != 0) gain = flangerGain;
                else gain = 0.01f;
                if (flangerSpeed != 0) speed = flangerSpeed;
                else speed = 1f;

                // Process delay
                var processed = ProcessFlanger(input, samples, gain, speed);

                // Apply volume control
                var volumeProcessed = ProcessVolume(processed, volume);

                output = volumeProcessed;
            }
            else
            {
                output = input;
            }


            return output;
        }

        private DiscreteSignal ProcessFlanger(DiscreteSignal signal, int samples, float gain, float speed)
        {
            var input = signal.Samples;
            var output = new float[input.Length];

            for (var i = 0; i < signal.Length; i++)
            {
                int delay;

                // Prevent from diving by zero
                if (i > 0) delay = D(samples, i, speed, signal.SamplingRate);
                else delay = 1;

                // Prevent from going out of array
                if (i - delay < 0)
                {
                    output[i] = input[i];
                }
                else
                {
                    output[i] = input[i] + gain * input[i - delay];
                }

                // Decrease output to avoid going over limit
                output[i] *= 0.5f;
            }

            return new DiscreteSignal(signal.SamplingRate, output);
        }

        private int D(int d, int n, float F, int samplingRate)
        {
            var cos = Math.Cos(2 * Math.PI * F * (n / (float)samplingRate));
            var coeff = (cos + 1);
            var res = (int)(d * coeff);
            return res;
        }
    }
}
