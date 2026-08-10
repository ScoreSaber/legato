#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace Legato.Platform.Users {
    public interface IPlatformUserProvider {
        Task<UserInfo> GetUserInfo(CancellationToken cancellationToken);
    }
}
