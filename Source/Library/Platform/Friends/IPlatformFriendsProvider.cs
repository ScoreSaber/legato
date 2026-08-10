#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Legato.Platform.Friends {
    public interface IPlatformFriendsProvider {
        Task<IReadOnlyList<string>> GetFriendUserIds();
    }
}
