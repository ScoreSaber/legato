#nullable enable

using SongDetailsCache;
using SongDetailsCache.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace SongDataCore {
    public static class Plugin {
        public static BeatStar.BeatStarDatabase Songs { get; } = new BeatStar.BeatStarDatabase();
    }
}

namespace SongDataCore.BeatStar {
    public sealed class BeatStarDatabase {
        private readonly Task<SongDetails> _details;

        public Action OnDataFinishedProcessing;
        public BeatStarDataFile Data { get; private set; }

        internal BeatStarDatabase() {
            _details = SongDetails.Init();
            SongDetailsReadyNotifier.Start(this);
        }

        public bool IsDataAvailable() => Data?.Songs != null;

        internal void Complete() {
            Data = new BeatStarDataFile(_details.Result);
            OnDataFinishedProcessing?.Invoke();
        }

        internal bool IsComplete => _details.IsCompleted;
        internal bool IsSuccessful => _details.Status == TaskStatus.RanToCompletion;
    }

    public sealed class BeatStarDataFile {
        public Dictionary<string, BeatStarSong> Songs { get; }

        internal BeatStarDataFile(SongDetails details) {
            Songs = details.songs.ToDictionary(
                song => song.hash,
                Convert,
                StringComparer.OrdinalIgnoreCase);
        }

        private static BeatStarSong Convert(Song song) => new BeatStarSong {
            key = song.key,
            diffs = song.difficulties.Select(Convert).ToList(),
            bpm = song.bpm,
            downloadCount = (int)song.downloadCount,
            upVotes = (int)song.upvotes,
            downVotes = (int)song.downvotes,
            heat = song.rating,
            rating = song.rating
        };

        private static BeatStarSongDifficultyStats Convert(SongDifficulty difficulty) => new BeatStarSongDifficultyStats {
            diff = difficulty.difficulty == MapDifficulty.ExpertPlus ? "Expert+" : difficulty.difficulty.ToString(),
            star = difficulty.stars,
            pp = difficulty.song.rankedStates.HasFlag(RankedStates.ScoresaberRanked)
                ? difficulty.stars * 43.146
                : 0,
            type = Characteristic(difficulty.characteristic),
            njs = (int)difficulty.njs,
            bmb = (int)difficulty.bombs,
            nts = (int)difficulty.notes,
            obs = (int)difficulty.obstacles
        };

        private static int Characteristic(MapCharacteristic characteristic) => characteristic switch {
            MapCharacteristic.Standard => 1,
            MapCharacteristic.OneSaber => 2,
            MapCharacteristic.NoArrows => 3,
            MapCharacteristic.Lightshow => 4,
            MapCharacteristic.NinetyDegree => 5,
            MapCharacteristic.ThreeSixtyDegree => 6,
            MapCharacteristic.Lawless => 7,
            _ => 0
        };
    }

    public sealed class BeatStarSong {
        public string key { get; set; }
        public List<BeatStarSongDifficultyStats> diffs { get; set; }
        public float bpm { get; set; }
        public int downloadCount { get; set; }
        public int upVotes { get; set; }
        public int downVotes { get; set; }
        public float heat { get; set; }
        public float rating { get; set; }
    }

    public sealed class BeatStarSongDifficultyStats {
        public string diff { get; set; }
        public long scores { get; set; }
        public double star { get; set; }
        public double pp { get; set; }
        public int type { get; set; }
        public int len { get; set; }
        public int njs { get; set; }
        public int bmb { get; set; }
        public int nts { get; set; }
        public int obs { get; set; }
    }

    internal sealed class SongDetailsReadyNotifier : MonoBehaviour {
        private BeatStarDatabase _database;

        internal static void Start(BeatStarDatabase database) {
            var gameObject = new GameObject("Legato_SongDetailsReady");
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
            gameObject.AddComponent<SongDetailsReadyNotifier>()._database = database;
        }

        private void Update() {
            if (!_database.IsComplete) {
                return;
            }
            if (_database.IsSuccessful) {
                _database.Complete();
            }
            Destroy(gameObject);
        }
    }
}
