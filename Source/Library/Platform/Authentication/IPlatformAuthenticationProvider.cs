#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace Legato.Platform.Authentication {
    public interface IPlatformAuthenticationProvider {
        Task<string> GetAuthToken();
        Task<string> GetCrossPlatformAccessToken(CancellationToken cancellationToken);
    }
}
