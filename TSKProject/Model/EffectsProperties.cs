namespace TSKProject.Model
{
    public struct EffectsProperties
    {
        public readonly int DelaySamples;
        public readonly float DelayGain;
        public readonly float DelayVolume;
        public readonly bool DelayBypass;
        public readonly int FlangerSamples;
        public readonly float FlangerGain;
        public readonly float FlangerVolume;
        public readonly float FlangerSpeed;
        public readonly bool FlangerBypass;

        public EffectsProperties(int delaySamples, float delayGain, float delayVolume, bool delayBypass,
                                int flangerSamples, float flangerGain, float flangerVolume, float flangerSpeed, bool flangerBypass)
        {
            DelaySamples = delaySamples;
            DelayGain = delayGain;
            DelayVolume = delayVolume;
            DelayBypass = delayBypass;
            FlangerSamples = flangerSamples;
            FlangerGain = flangerGain;
            FlangerVolume = flangerVolume;
            FlangerSpeed = flangerSpeed;
            FlangerBypass = flangerBypass;
        }
    }
}
