#nullable enable

using SongCore;
using System.Collections;
using System.Reflection;

namespace Legato {
    internal static class BeatmapLevelAdapterExtensions {
        internal static bool HasRequirements(this BeatmapLevelAdapter level) {
#if BEAT_SABER_1_29_0
            return false;
#else
            MethodInfo? getSongData = typeof(Collections).GetMethod(
                "GetCustomLevelSongData",
                BindingFlags.Public | BindingFlags.Static,
                null,
                new[] { typeof(string) },
                null);
            object? songData = getSongData?.Invoke(null, new object[] { level.levelID });
            IEnumerable? difficulties = Member(songData, "_difficulties") as IEnumerable;
            if (difficulties == null) {
                return false;
            }

            foreach (object difficulty in difficulties) {
                object? additional = Member(difficulty, "additionalDifficultyData");
                if (Member(additional, "_requirements") is ICollection requirements && requirements.Count > 0) {
                    return true;
                }
            }
            return false;
#endif
        }

        private static object? Member(object? instance, string name) {
            if (instance == null) {
                return null;
            }
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return instance.GetType().GetField(name, flags)?.GetValue(instance)
                ?? instance.GetType().GetProperty(name, flags)?.GetValue(instance);
        }
    }
}
