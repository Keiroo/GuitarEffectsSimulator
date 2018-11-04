using NWaves.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NWaves.Filters.BiQuad;

namespace TSKProject.Model
{
    class Distortion : Effect
    {
        public DiscreteSignal Process(DiscreteSignal input, int gain, int distortion, float volume, bool bypass)
        {
            DiscreteSignal output;

            if (!bypass)
            {
                // Process delay
                var processed = ProcessDistortion(input, gain, distortion);

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

        private DiscreteSignal ProcessDistortion(DiscreteSignal signal, int gain, int distortion)
        {
            var input = signal.Samples;
            var output = new float[input.Length];

            
            for (var i = 0; i < signal.Length; i++)
            {
                // Process gain
                output[i] = Gain(input[i], gain);

                // Process distortion
                output[i] = Dist(output[i], distortion);
            }

            // Process lowpass filter
            var freq = 10000.0 /* Hz */ / signal.SamplingRate;
            LowPassFilter lpf = new LowPassFilter(freq);
            DiscreteSignal processed = new DiscreteSignal(signal.SamplingRate, output);
            DiscreteSignal res = lpf.ApplyTo(processed);

            return res;
        }

        private float Gain(float sample, int gain)
        {
            return ((gain / 100) * 100 + 1) * sample;
        }

        private float Dist(float sample, int d)
        {
            float a = (float)Math.Sin(((d + 1) / 101f) * (Math.PI / 2f));
            float k = (2f * a) / (1f - a);
            float res = ((1f + k) * sample) / (1f + k * Math.Abs(sample));

            return res;
        }
    }
}
