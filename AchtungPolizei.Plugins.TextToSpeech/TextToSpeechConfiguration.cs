using System;
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
            get
            {
                try
                {
                    return parameters[BuildBrokenPhraseParam];    
                }
                catch (KeyNotFoundException)
                {
                    return null;
                }
            }
            set { parameters[BuildBrokenPhraseParam] = value; }
        }

        public string BuildStillBrokenPhrase
        {
            get
            {
                try
                {
                    return parameters[BuildStillBrokenPhraseParam];
                }
                catch (KeyNotFoundException)
                {
                    return null;
                }
                
            }
            set { parameters[BuildStillBrokenPhraseParam] = value; }
        }

        public string BuildFixedPhrase
        {
            get
            {
                try
                {
                    return parameters[BuildFixedBrokenPhraseParam];
                }
                catch (KeyNotFoundException)
                {
                    return null;
                }
            }
            set { parameters[BuildFixedBrokenPhraseParam] = value; }
        }
    }
}