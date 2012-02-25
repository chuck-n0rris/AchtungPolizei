namespace AchtungPolizei.Plugins.TextToSpeech
{
    using System;
    using System.Collections.Generic;
    using System.Speech.Synthesis;
    using System.Threading.Tasks;
    
    using AchtungPolizei.Plugins;

    public class TextToSpeechPlugin : IOutputPlugin
    {
        private readonly Dictionary<BuildStatus, string> statusToText = new Dictionary<BuildStatus, string>
            {
                { BuildStatus.Broken, "Build has been broken."},
                { BuildStatus.StillBroken, "Build is still broken."},
                { BuildStatus.Fixed, "Build has been fixed."}
            };

        private readonly SpeechSynthesizer synthesizer;

        private readonly Guid id = new Guid("603e7da9-4cb1-4ac7-b84e-7ce12b3cbee3");

        private const string name = "Text to Speech";

        private bool disposed;

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

        public Task Start(BuildState state, BuildStatus status)
        {
            return new Task(Speak, statusToText[status]);
        }

        private void Speak(object text)
        {
            var builder = new PromptBuilder();
            builder.AppendText((string)text);
            synthesizer.Speak(builder);
        }
    }
}
