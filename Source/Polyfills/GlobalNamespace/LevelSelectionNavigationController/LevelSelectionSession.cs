#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Legato {
    // Own the private controller seam used when a mod replaces the visible pack.
    internal sealed class LevelSelectionSession {
        private const BindingFlags InstanceFields = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private readonly LevelSelectionNavigationController _selection;
        private readonly LevelCollectionNavigationController _collection;
        private readonly LevelFilteringNavigationController _filtering;

        internal LevelSelectionSession(LevelSelectionNavigationController selection) {
            _selection = selection;
            _collection = Field<LevelCollectionNavigationController>(selection, "_levelCollectionNavigationController");
            _filtering = Field<LevelFilteringNavigationController>(selection, "_levelFilteringNavigationController");
        }

        internal IAnnotatedBeatmapLevelCollection ApplyFilteredPack(
            IAnnotatedBeatmapLevelCollection sourcePack,
            IReadOnlyList<IPreviewBeatmapLevel> levels,
            string packID,
            string packName,
            string shortPackName,
            Sprite fallbackCoverImage) {
            var source = BeatmapLevelPackAdapter.From(sourcePack);
            string? selectedLevelID = _selection.GetBeatmapKey().levelId;
            var pack = BeatmapLevelPackAdapter.CreateFiltered(
                packID,
                packName,
                shortPackName,
                source.coverImage ?? fallbackCoverImage,
                source.smallCoverImage ?? fallbackCoverImage,
                levels);

            _collection.SetPackData(
                pack,
                true,
                !Field<bool>(_selection, "_hidePracticeButton"),
                Field<string>(_selection, "_actionButtonText"),
                Field<GameObject>(_filtering, "_currentNoDataInfoPrefab"),
                Field<BeatmapDifficultyMask>(_selection, "_allowedBeatmapDifficultyMask"),
                Field<BeatmapCharacteristicSO[]>(_selection, "_notAllowedCharacteristics"));

            BeatmapLevelAdapter? selected = pack.beatmapLevels.FirstOrDefault(level => level.levelID == selectedLevelID);
            if (selected != null) {
                _collection.SelectLevel(selected.source);
            }
            return pack;
        }

        private static T Field<T>(object instance, string name) =>
            (T)(instance.GetType().GetField(name, InstanceFields)?.GetValue(instance)
                ?? throw new MissingFieldException(instance.GetType().FullName, name));
    }

    internal static class LevelSelectionSessionExtensions {
        internal static LevelSelectionSession OpenSession(this LevelSelectionNavigationController controller) => new LevelSelectionSession(controller);
    }
}
