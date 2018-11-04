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

            var samples = (signal.SamplingRate / 1000) * miliseconds;
            var half = samples / 2;

            int delay1 = half,
                delay2 = half,
                oldDelay1 = half,
                oldDelay2 = half;

            var rand = new Random();

            for (var i = 0; i < signal.Length; i++)
            {
                // Prevent from diving by zero
                if (i > 0)
                {
                    delay1 = D(samples, i, oldDelay1, rand);
                    oldDelay1 = delay1;

                    delay2 = D(samples, i, oldDelay2, rand);
                    oldDelay2 = delay2;
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

                // Add second delay
                if (i - delay2 < 0)
                {
                    output[i] = input[i];
                }
                else
                {
                    output[i] += input[i] + gain2 * input[i - delay2];
                }
            }

            return new DiscreteSignal(signal.SamplingRate, output);
        }

        private int D(int d, int n, int oldDelay, Random random)
        {
            var max = d;
            var min = 0;
            var range = 100;

            var rand = random.Next(oldDelay - range, oldDelay + range);

            if (rand > max) rand = max;
            if (rand < min) rand = min;

            Console.WriteLine(rand);

            return rand;
        }
    }
}
