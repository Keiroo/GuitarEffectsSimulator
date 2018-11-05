using NWaves.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKProject.Model
{
    class Chorus : Effect
    {
        public DiscreteSignal Process(DiscreteSignal input, int chorusMiliseconds, float chorusGain1, float chorusGain2, float volume, bool bypass)
        {
            DiscreteSignal output;

            if (!bypass)
            {
                int miliseconds;

                // Default values if zeros
                if (chorusMiliseconds != 0) miliseconds = chorusMiliseconds;
                else miliseconds = 1;

                // Process delay
                var processed = ProcessChorus(input, miliseconds, chorusGain1, chorusGain2);

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

        private DiscreteSignal ProcessChorus(DiscreteSignal signal, int miliseconds, float gain1, float gain2)
        {
            var input = signal.Samples;
            var output = new float[input.Length];

            var random = new Random();


            var samples1 = (int)((signal.SamplingRate / 1000) * miliseconds);
            var samples2 = (int)((signal.SamplingRate / 1000) * (2 * miliseconds));

            int delay1, delay2;

            for (var i = 0; i < signal.Length; i++)
            {
                // Prevent from diving by zero
                if (i > 0)
                {
                    delay1 = D(samples1, i, 1f, signal.SamplingRate);
                    delay2 = D(samples2, i, 1f, signal.SamplingRate);
                }
                else
                {
                    delay1 = 1;
                    delay2 = 1;
                }

                // Prevent from going out of array
                if (i - delay1 < 0)
                {
                    output[i] = input[i];
                }
                else
                {
                    output[i] = input[i] + gain1 * input[i - delay1];
                }
                if (i - delay2 >= 0)
                {
                    output[i] += input[i] + gain2 * input[i - delay2];
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
