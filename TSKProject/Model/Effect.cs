﻿using NWaves.Audio;
using NWaves.Signals;

namespace TSKProject.Model
{
    class Effect
    {
        public DiscreteSignal WaveToSignal(WaveFile waveFile)
        {
            return waveFile[Channels.Average];
        }

        protected DiscreteSignal ProcessVolume(DiscreteSignal signal, float volume)
        {
            var input = signal.Samples;
            var output = new float[input.Length];

            // Check if volume is between 0.0 and 1.0
            if (volume < 0.0f) volume = 0.0f;
            if (volume > 1.0f) volume = 1.0f;

            for (var i = 0; i < signal.Length; i++)
            {
                output[i] = volume * input[i];
            }

            return new DiscreteSignal(signal.SamplingRate, output);
        }

        protected DiscreteSignal ProcessClipping(DiscreteSignal signal)
        {
            var input = signal.Samples;
            var output = new float[input.Length];

            for (int i = 0; i < signal.Length; i++)
            {
                if (input[i] >= 1.0f)
                {
                    output[i] = 0.99f;
                }
                else if (input[i] <= -1.0f)
                {
                    output[i] = -0.99f;
                }
                else
                {
                    output[i] = input[i];
                }
            }

            return new DiscreteSignal(signal.SamplingRate, output);
        }
    }
}
