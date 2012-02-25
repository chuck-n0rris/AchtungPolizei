using System.Collections.Generic;

namespace AchtungPolizei.Plugins.TextToSpeech
{
    public class TextToSpeechConfiguration : ConfigurationBase
    {
        public TextToSpeechConfiguration()
        {
            parameters = new Dictionary<string, string>();
        }

        private const string BuildBrokenPhraseParam = "BuildBrokenPhrase";

        private const string BuildStillBrokenPhraseParam = "BuildStillBrokenPhrase";

        private const string BuildFixedBrokenPhraseParam = "BuildFixedPhrase";

        public string BuildBrokenPhrase
        {
            get { return parameters[BuildBrokenPhraseParam]; }
            set { parameters[BuildBrokenPhraseParam] = value; }
        }

        public string BuildStillBrokenPhrase
        {
            get { return parameters[BuildStillBrokenPhraseParam]; }
            set { parameters[BuildStillBrokenPhraseParam] = value; }
        }

        public string BuildFixedPhrase
        {
            get { return parameters[BuildFixedBrokenPhraseParam]; }
            set { parameters[BuildFixedBrokenPhraseParam] = value; }
        }
    }
}