using NWaves.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKProject.Model
{
    class Chrous : Effect
    {
        public DiscreteSignal Process(DiscreteSignal input, int chorusMiliseconds, float chorusGain1, float chorusGain2, float volume, bool bypass)
        {
            DiscreteSignal output;

            if (!bypass)
            {
                int miliseconds;

                // Default values if zeros
                if (chorusMiliseconds != 0) miliseconds = chorusMiliseconds;
                else miliseconds = 20;

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

            var samples = (signal.SamplingRate / 1000) * miliseconds;

            for (var i = 0; i < signal.Length; i++)
            {
                int delay1, delay2;

                // Prevent from diving by zero
                if (i > 0) delay1 = D(samples, i, signal.SamplingRate);
                else delay1 = 1;
                if (i > 0) delay2 = D(samples, i, signal.SamplingRate);
                else delay2 = 1;

                // Prevent from going out of array
                if (i - delay1 < 0)
                {
                    output[i] = input[i];
                }
                else
                {
                    output[i] = input[i] + gain1 * input[i - delay1];
                }

                // Add second delay
                if (i - delay2 < 0)
                {
                    output[i] += input[i];
                }
                else
                {
                    output[i] += input[i] + gain2 * input[i - delay2];
                }
            }

            return null;
        }

        private int D(int d, int n, int samplingRate)
        {
            var rand = new Random();
            var randValue = d * (0.5f + (float)((rand.NextDouble() * 2f) - 1f));
            var res = (int)(randValue * samplingRate);
            return res;
        }
    }
}
