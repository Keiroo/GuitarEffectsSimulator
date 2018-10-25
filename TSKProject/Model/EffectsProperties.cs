namespace TSKProject.Model
{
    public struct EffectsProperties
    {
        public readonly int DelaySamples;
        public readonly float DelayGain;
        public readonly float DelayVolume;
        public readonly bool DelayBypass;

        public EffectsProperties(int delaySamples, float delayGain, float delayVolume, bool delayBypass)
        {
            DelaySamples = delaySamples;
            DelayGain = delayGain;
            DelayVolume = delayVolume;
            DelayBypass = delayBypass;
        }
    }
}
