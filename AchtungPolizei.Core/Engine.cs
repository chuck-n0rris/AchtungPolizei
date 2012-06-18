namespace AchtungPolizei.Core
{
    using System.Collections.Generic;

    public class Engine
    {
        private readonly IPluginsManager pluginsManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="pluginsManager">
        /// The plugins manager.
        /// </param>
        public Engine(IPluginsManager pluginsManager)
        {
            this.pluginsManager = pluginsManager;
        }

        /// <summary>
        /// Runs the engine.
        /// </summary>
        /// <param name="configurations">
        /// The configurations.
        /// </param>
        public void Run(IEnumerable<Configuration> configurations)
        {
        }

        /// <summary>
        /// Stops the engine.
        /// </summary>
        public void Stop()
        {
        }
    }
}