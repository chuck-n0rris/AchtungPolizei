using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AchtungPolizei.Core.Helpers;
using AchtungPolizei.Plugins;

namespace AchtungPolizei.Core
{
    public class OutputQue
    {
        private readonly BlockingCollection<ProcessRequest> que = new BlockingCollection<ProcessRequest>();

        private bool shutdown = false;

        public void Add(ProcessRequest request)
        {
            que.TryAdd(request);
        }

        public void Start()
        {
            shutdown = false;
            Task.Factory.StartNew(Consumer);
        }

        public void Stop()
        {
            shutdown = true;
        }

        private void Consumer()
        {
            while (!shutdown)
            {
                ProcessRequest request;

                if (que.TryTake(out request, TimeSpan.FromMinutes(1)))
                {
                    var executionPlan = BuildExecutionPlan(request);

                    executionPlan.Run().Wait();
                }
            }
        }

        private static ExecutionPlan BuildExecutionPlan(ProcessRequest request)
        {
            var executionPlan = new ExecutionPlan
            {
                ExecutionStages = new List<ExecutionStage>()
            };

            foreach (var pluginConfiguration in request.Project.OutputPlugins)
            {
                var plugin = PluginLocator.Current.GetInstanceById(pluginConfiguration.PluginId);

                if (plugin is IOutputPlugin)
                {
                    plugin.SetConfiguration(pluginConfiguration.Configuration);

                    executionPlan.ExecutionStages.Add(new ExecutionStage(request.ProjectState.BuildState,
                                                                         request.ProjectState.BuildStatus, 
                                                                         (IOutputPlugin) plugin));
                }
            }
            
            return executionPlan;
        }
    }
}