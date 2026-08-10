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

        public Task<UserInfo> GetUserInfo(CancellationToken _) => _platformUserModel.GetUserInfo();

        public async Task<string> GetAuthToken() => (await _platformUserModel.GetUserAuthToken()).token;

        public Task<string> GetCrossPlatformAccessToken(CancellationToken _) {
            var completion = new TaskCompletionSource<string>();
            Oculus.Platform.Users.GetAccessToken().OnComplete(message => completion.TrySetResult(message.IsError ? string.Empty : message.Data));
            return completion.Task;
        }

        public Task<IReadOnlyList<string>> GetFriendUserIds() => _platformUserModel.GetUserFriendsUserIds(false);
    }
}
