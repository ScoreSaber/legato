#nullable enable

#if BEAT_SABER_1_42_0
using Legato.Platform.Authentication;
using Legato.Platform.Friends;
using Legato.Platform.Users;
using OculusStudios.Platform.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Legato.Platform {
    public class GamePlatformAdapter : IPlatformUserProvider, IPlatformAuthenticationProvider, IPlatformFriendsProvider {
        private readonly IPlatform _platform;

        public GamePlatformAdapter(IPlatform platform) {
            _platform = platform;
        }

        public Task<UserInfo> GetUserInfo(CancellationToken _) {
            UserInfo.Platform platform;
            switch (_platform.key) {
                case "steam":
                    platform = UserInfo.Platform.Steam;
                    break;
                case "oculus":
                case "oculus-mock":
                    platform = UserInfo.Platform.Oculus;
                    break;
                default:
                    platform = UserInfo.Platform.Test;
                    break;
            }
            return Task.FromResult(new UserInfo(platform, _platform.user.userId.ToString(), _platform.user.displayName));
        }

        public Task<string> GetAuthToken() => _platform.user.GetAccessTokenAsync();

        public Task<string> GetCrossPlatformAccessToken(CancellationToken _) => _platform.user.GetXPlatformAccessTokenAsync(false);

        public Task<IReadOnlyList<string>> GetFriendUserIds() => Task.FromResult(_platform.key == "steam" ? SteamFriendUserIds() : (IReadOnlyList<string>)new string[0]);

        private static IReadOnlyList<string> SteamFriendUserIds() {
            int friendCount = Steamworks.SteamFriends.GetFriendCount(Steamworks.EFriendFlags.k_EFriendFlagAll);
            var ids = new List<string>(friendCount);
            for (int i = 0; i < friendCount; i++) {
                ids.Add(Steamworks.SteamFriends.GetFriendByIndex(i, Steamworks.EFriendFlags.k_EFriendFlagImmediate).m_SteamID.ToString());
            }
            return ids;
        }
    }
}
#endif
