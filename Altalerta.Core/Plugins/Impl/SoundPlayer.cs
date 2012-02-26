using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Altalerta.Core.Essential;
using Altalerta.Core.Tools;

namespace Altalerta.Core.Plugins.Impl
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

        public string StillBrokenSound
        {
            get { return Configuration["StillBrokenSound"]; }
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
                        if (state == BuildState.Ok)
                        {
                            return;
                        }

                        player.Play(GetSoundName(state));
                        Thread.Sleep(Interval);
                        player.Stop();
                    });
        }

        private string GetSoundName(BuildState state)
        {
            switch (state)
            {
                case BuildState.Broken:
                    return BrokenSound;
                case BuildState.StillBroken:
                    return StillBrokenSound;
                case BuildState.Fixed:
                    return FixedSound;
                default:
                    throw new ApplicationException("Can't find suitable sound.");
            }
        }

        #endregion
    }
}