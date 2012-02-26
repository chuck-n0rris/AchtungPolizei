using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altalerta.Core.Plugins;
using Altalerta.Core.Tools;

namespace Altalerta.Core.Essential
{
    public class Project : IDisposable
    {
        private IInputPlugin input;

        private IEnumerable<IOutputPlugin> output;

        private bool wasBroken;

        public string Name { get; set; }

        public PluginReference Input { get; set; }

        public IEnumerable<PluginReference> Output { get; set; }

        #region IDisposable Members

        public void Dispose()
        {
            input.Dispose();
            foreach (IOutputPlugin plugin in output)
            {
                plugin.Dispose();
            }
        }

        #endregion

        public void Initialize(IPluginRepository plugins)
        {
            input = plugins.Instantiate(Input) as IInputPlugin;
            input.BuildInfoReceived += OnBuildInfoReceived;

            output = Output
                .Select(plugins.Instantiate)
                .Cast<IOutputPlugin>()
                .ToArray();
        }

        private void OnBuildInfoReceived(object sender, BuildInfoReceivedEventArgs args)
        {
            lock (this)
            {
                Task.WaitAll(
                    output
                        .Select(x => x.Notify(
                            args.Info.GetState(wasBroken),
                            args.Info))
                        .ToArray());
                wasBroken = !args.Info.IsSuccessful;
            }
        }
    }
}