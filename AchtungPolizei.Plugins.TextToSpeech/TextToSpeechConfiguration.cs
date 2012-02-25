namespace AchtungPolizei.Plugins.TextToSpeech
{
    public class TextToSpeechConfiguration : ConfigurationBase
    {
        private const string BuildBrokenPhraseParam = "BuildBrokenPhrase";

        private const string BuildStillBrokenPhraseParam = "BuildStillBrokenPhrase";

        private const string BuildFixedBrokenPhraseParam = "BuildFixedPhrase";

        public string BuildBrokenPhrase
        {
            get { return Parameters[BuildBrokenPhraseParam]; }
            set { Parameters[BuildBrokenPhraseParam] = value; }
        }

        public string BuildStillBrokenPhrase
        {
            get { return Parameters[BuildStillBrokenPhraseParam]; }
            set { Parameters[BuildStillBrokenPhraseParam] = value; }
        }

        public string BuildFixedPhrase
        {
            get { return Parameters[BuildFixedBrokenPhraseParam]; }
            set { Parameters[BuildFixedBrokenPhraseParam] = value; }
        }
    }
}