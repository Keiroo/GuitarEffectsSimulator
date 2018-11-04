namespace TSKProject.Model
{
    public struct EffectsProperties
    {
        // Delay
        public readonly int DelaySamples;
        public readonly float DelayGain;
        public readonly float DelayVolume;
        public readonly bool DelayBypass;

        // Flanger
        public readonly int FlangerSamples;
        public readonly float FlangerGain;
        public readonly float FlangerVolume;
        public readonly float FlangerSpeed;
        public readonly bool FlangerBypass;

        // Chorus
        public readonly int ChorusMiliseconds;
        public readonly float ChorusGain1;
        public readonly float ChorusGain2;
        public readonly float ChorusVolume;
        public readonly bool ChorusBypass;

        // Distortion
        public readonly int DistGain;
        public readonly int DistDistortion;
        public readonly float DistVolume;
        public readonly bool DistBypass;


        public EffectsProperties(int delaySamples, float delayGain, float delayVolume, bool delayBypass,
                                int flangerSamples, float flangerGain, float flangerVolume, float flangerSpeed, bool flangerBypass,
                                int chorusMiliseconds, float chorusGain1, float chorusGain2, float chorusVolume, bool chorusBypass,
                                int distGain, int distDistortion, float distVolume, bool distBypass
                                )
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
            ChorusMiliseconds = chorusMiliseconds;
            ChorusGain1 = chorusGain1;
            ChorusGain2 = chorusGain2;
            ChorusVolume = chorusVolume;
            ChorusBypass = chorusBypass;
            DistGain = distGain;
            DistDistortion = distDistortion;
            DistVolume = distVolume;
            DistBypass = distBypass;
        }
    }
}
