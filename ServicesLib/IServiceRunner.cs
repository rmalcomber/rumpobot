using System.Threading;
using System.Threading.Tasks;

namespace CommonLib
{
    public interface IServiceRunner
    {
        Task RunService(CancellationToken cancellationToken);
    }
}
