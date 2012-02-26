using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Altalerta.Core
{
    public class SoundPlayer : IOutputPlugin
    {
        private readonly IAudioPlayer player;

        public SoundPlayer(IAudioPlayer player)
        {
            this.player = player;
        }

        public string BrokenSound
        {
            get { return Configuration["BrokenSound"]; }
        }

        public string FixedSound
        {
            get { return Configuration["FixedSound"]; }
        }

        public int Interval
        {
            get { return int.Parse(Configuration["Interval"]); }
        }

        #region IOutputPlugin Members

        public void Dispose()
        {
        }

        public string Name
        {
            get { return "Sound Player"; }
        }

        public IDictionary<string, string> Configuration { get; set; }

        public Task Notify(BuildState state, BuildInfo info)
        {
            return Task.Factory.StartNew(
                () =>
                    {
                        player.Play(state == BuildState.Broken ? BrokenSound : FixedSound);
                        Thread.Sleep(Interval);
                        player.Stop();
                    });
        }

        #endregion
    }
}