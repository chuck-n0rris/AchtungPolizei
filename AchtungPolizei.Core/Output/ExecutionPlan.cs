using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AchtungPolizei.Core
{
    public class ExecutionPlan
    {
        public List<ExecutionStage> ExecutionStages { get; set; }

        public Task Run()
        {
            if (!ExecutionStages.Any())
            {
                return Task.Factory.StartNew(() => { });
            }

            return Task.Factory.ContinueWhenAll(ExecutionStages.Select(it => it.Run()).ToArray(), (results) => { });
        }
    }
}