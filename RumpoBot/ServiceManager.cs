using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonLib;

namespace RumpoBot {
    public class ServiceManager {
        private readonly IEnumerable<IServiceRunner> _serviceRunners;
        private readonly CancellationTokenSource _cancellationToken;
        private readonly IList<Task> _serviceTasks = new List<Task>();

        public ServiceManager(IEnumerable<IServiceRunner> serviceRunners, CancellationTokenSource cancellationToken) {
            _serviceRunners = serviceRunners;
            _cancellationToken = cancellationToken;
        }


        public void RunServices() {
            foreach (var serviceRunner in _serviceRunners) {
                _serviceTasks.Add(serviceRunner.RunService(_cancellationToken.Token));
            }

             Task.WaitAll(_serviceTasks.ToArray());
        }


    }
}
