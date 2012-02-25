using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AchtungPolizei.Plugins;
using System.Linq;

namespace AchtungPolizei.Core
{

    public class ExecutionStage
    {
        public BuildState BuildState { get; set; }
        public BuildStatus BuildStatus { get; set; }
        public IOutputPlugin OutputPlugin { get; set; }

        public ExecutionStage(BuildState buildState, BuildStatus buildStatus, IOutputPlugin outputPlugin)
        {
            BuildState = buildState;
            BuildStatus = buildStatus;
            OutputPlugin = outputPlugin;
        }

        public Task Run()
        {
            return OutputPlugin.Start(this.BuildState, this.BuildStatus);
        }
    }

    public class ExecutionPlan
    {
        public List<ExecutionStage> ExecutionStages { get; set; }

        public Task Run()
        {
            return Task
                .Factory
                .ContinueWhenAll(ExecutionStages
                    .Select(it => it.Run())
                    .ToArray(), (results) => {

                        });
        }
    }

    public struct ProcessRequest
    {
        public Project Project { get; set; }

        public ProjectState ProjectState { get; set; }
    }

    public struct ProjectState
    {
        public BuildState BuildState { get; set; }

        public BuildStatus BuildStatus { get; set; }
    }

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
                    var executionPlan = new ExecutionPlan() {ExecutionStages = new List<ExecutionStage>()};

                    executionPlan.Run().Wait();
                }
            }
        }
    }
}