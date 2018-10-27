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
        public DiscreteSignal Process(DiscreteSignal input, int flangerMiliseconds, float flangerGain, float volume, float flangerSpeed, bool bypass)
        {
            DiscreteSignal output;

            if (!bypass)
            {
                int miliseconds;
                float speed;

                // Default values if zeros
                if (flangerMiliseconds != 0) miliseconds = flangerMiliseconds;
                else miliseconds = 1;
                if (flangerSpeed != 0) speed = flangerSpeed;
                else speed = 1f;

                // Process delay
                var processed = ProcessFlanger(input, miliseconds, flangerGain, speed);

                // Apply volume control
                var volumeProcessed = ProcessVolume(processed, volume);

                var clippingProcessed = ProcessClipping(volumeProcessed);

                output = clippingProcessed;
            }
            else
            {
                output = input;
            }


            return output;
        }

        private DiscreteSignal ProcessFlanger(DiscreteSignal signal, int miliseconds, float gain, float speed)
        {
            var input = signal.Samples;
            var output = new float[input.Length];

            var samples = (signal.SamplingRate / 1000) * miliseconds;

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
