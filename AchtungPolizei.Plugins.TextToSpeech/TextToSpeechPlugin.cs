﻿namespace AchtungPolizei.Plugins.TextToSpeech
{
    using System;
    using System.Collections.Generic;
    using System.Speech.Synthesis;
    using System.Threading.Tasks;
    
    using Plugins;

    public class TextToSpeechPlugin : IOutputPlugin
    {
        private readonly Dictionary<BuildStatus, string> defaultPhrases = new Dictionary<BuildStatus, string>
            {
                { BuildStatus.Broken, "Build has been broken."},
                { BuildStatus.StillBroken, "Build is still broken."},
                { BuildStatus.Fixed, "Build has been fixed."}
            };

        private readonly SpeechSynthesizer synthesizer;

        private readonly Guid id = new Guid("603e7da9-4cb1-4ac7-b84e-7ce12b3cbee3");

        private const string name = "Text to Speech";

        private IConfigirationControl control;

        private bool disposed;

        private TextToSpeechConfiguration configuration;

        public TextToSpeechPlugin()
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            synthesizer.Dispose();

            disposed = true;
        }

        public Guid Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public void SetConfiguration(ConfigurationBase configuration)
        {
            this.configuration = configuration as TextToSpeechConfiguration;
            InitConfiguration();
        }
        
        public IConfigirationControl GetConfigControl()
        {
            this.configuration = this.configuration ?? new TextToSpeechConfiguration();
            InitConfiguration();
            return new SettingsControl(configuration);
        }

        public IConfigirationControl GetConfigControl(ConfigurationBase config)
        {
            var textToSpeechConfig = (TextToSpeechConfiguration)config;
            return new SettingsControl(textToSpeechConfig);
        }

        private void InitConfiguration()
        {
            this.configuration.BuildBrokenPhrase = this.configuration.BuildBrokenPhrase
                ?? defaultPhrases[BuildStatus.Broken];

            this.configuration.BuildStillBrokenPhrase = this.configuration.BuildStillBrokenPhrase
                ?? defaultPhrases[BuildStatus.StillBroken];

            this.configuration.BuildFixedPhrase = this.configuration.BuildFixedPhrase
                ?? defaultPhrases[BuildStatus.Fixed];
        }

        public Task Start(BuildState state, BuildStatus status)
        {
            string phrase = GetPhrase(status);

            return Task.Factory.StartNew(() =>
            {
                var builder = new PromptBuilder();
                builder.AppendText(phrase);
                synthesizer.Speak(builder);
            });
        }

        private string GetPhrase(BuildStatus status)
        {
            string phrase = null;

            if (configuration != null)
            {
                switch (status)
                {
                    case BuildStatus.Broken:
                        phrase = configuration.BuildBrokenPhrase;
                        break;

                    case BuildStatus.StillBroken:
                        phrase = configuration.BuildStillBrokenPhrase;
                        break;

                    case BuildStatus.Fixed:
                        phrase = configuration.BuildFixedPhrase;
                        break;

                    default:
                        phrase = "Unknown status";
                        break;
                }
            }

            return string.IsNullOrEmpty(phrase)
                       ? defaultPhrases[status]
                       : phrase;
        }
    }
}
