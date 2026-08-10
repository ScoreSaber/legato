#nullable enable

using Legato.Platform.Authentication;
using Legato.Platform.Friends;
using Legato.Platform.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Legato.Platform {
    public class GamePlatformAdapter : IPlatformUserProvider, IPlatformAuthenticationProvider, IPlatformFriendsProvider {
        private readonly IPlatformUserModel _platformUserModel;

        public GamePlatformAdapter(IPlatformUserModel platformUserModel) {
            _platformUserModel = platformUserModel;
        }

        public Task<UserInfo> GetUserInfo(CancellationToken cancellationToken) => _platformUserModel.GetUserInfo(cancellationToken);

        public async Task<string> GetAuthToken() => (await _platformUserModel.GetUserAuthToken()).token;

        public async Task<string> GetCrossPlatformAccessToken(CancellationToken cancellationToken) =>
            (await _platformUserModel.RequestXPlatformAccessToken(cancellationToken)).token;

        public Task<IReadOnlyList<string>> GetFriendUserIds() => _platformUserModel.GetUserFriendsUserIds(false);
    }
}
