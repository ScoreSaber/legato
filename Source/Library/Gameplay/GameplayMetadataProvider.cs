#nullable enable

using Legato.Gameplay.Controllers;
using UnityEngine;
using Zenject;
#if !BEAT_SABER_1_29_0 && !BEAT_SABER_1_37_1 && !BEAT_SABER_1_38_0
using BeatSaber.GameSettings;
#endif

namespace Legato.Gameplay {
    internal class GameplayMetadataProvider {
#if BEAT_SABER_1_38_0
        private readonly SettingsManager _settingsManager;

        public GameplayMetadataProvider([InjectOptional] SettingsManager settingsManager) {
            _settingsManager = settingsManager;
        }
#elif !BEAT_SABER_1_29_0 && !BEAT_SABER_1_37_1
        private readonly VariableMovementDataProvider _movementDataProvider;
        private readonly ControllerProfilesModel _controllerProfilesModel;

        public GameplayMetadataProvider([InjectOptional] VariableMovementDataProvider movementDataProvider, [InjectOptional] ControllerProfilesModel controllerProfilesModel) {
            _movementDataProvider = movementDataProvider;
            _controllerProfilesModel = controllerProfilesModel;
        }
#endif

        internal float JumpDistance(BeatmapObjectSpawnController.InitData initData) {
#if !BEAT_SABER_1_29_0 && !BEAT_SABER_1_37_1 && !BEAT_SABER_1_38_0
            if (_movementDataProvider != null && _movementDataProvider.jumpDistance > 0f) {
                return _movementDataProvider.jumpDistance;
            }
#endif

            if (initData.noteJumpValueType == BeatmapObjectSpawnMovementData.NoteJumpValueType.JumpDuration) {
                return initData.noteJumpMovementSpeed * initData.noteJumpValue * 2f;
            }

            if (initData.beatsPerMinute <= 0f) {
                return 0f;
            }

            float halfJumpDuration = 4f;
            float beatDuration = 60f / initData.beatsPerMinute;
            while (initData.noteJumpMovementSpeed * beatDuration * halfJumpDuration > 17.999f) {
                halfJumpDuration /= 2f;
            }

            halfJumpDuration = Mathf.Max(0.25f, halfJumpDuration + initData.noteJumpValue);
            return initData.noteJumpMovementSpeed * beatDuration * halfJumpDuration * 2f;
        }

        internal int ArcVisibility(PlayerSpecificSettings settings) {
#if BEAT_SABER_1_29_0
            return (int)settings.arcsVisible;
#else
            return (int)settings.arcVisibility;
#endif
        }

        internal ControllerPoseSet? ControllerPoses() {
#if BEAT_SABER_1_29_0
            MainSettingsModelSO[] settings = UnityEngine.Resources.FindObjectsOfTypeAll<MainSettingsModelSO>();
            return settings.Length == 0
                ? (ControllerPoseSet?)null
                : new ControllerPoseSet(shared: new ControllerPose(settings[0].controllerPosition.value, settings[0].controllerRotation.value));
#elif BEAT_SABER_1_37_1
            return null;
#elif BEAT_SABER_1_38_0
            if (_settingsManager == null) {
                return null;
            }

            var controller = _settingsManager.settings.controller;
            return new ControllerPoseSet(
                shared: new ControllerPose(
                    new Vector3(controller.position.x, controller.position.y, controller.position.z),
                    new Vector3(controller.rotation.x, controller.rotation.y, controller.rotation.z)));
#else
            if (_controllerProfilesModel == null) {
                return null;
            }

            var profile = _controllerProfilesModel.selectedProfile;
            return new ControllerPoseSet(
                left: new ControllerPose(profile.leftController.position, profile.leftController.rotation),
                right: new ControllerPose(profile.rightController.position, profile.rightController.rotation));
#endif
        }
    }
}
