#nullable enable

#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1 || BEAT_SABER_1_38_0 || BEAT_SABER_1_40_0
using System;
using Zenject;

namespace Legato {
    internal static class MenuTransitionsHelperExtensions {
        internal static void StartStandardLevel(
            this MenuTransitionsHelper menuTransitionsHelper,
            string gameMode,
            in BeatmapKey beatmapKey,
            BeatmapLevel beatmapLevel,
            OverrideEnvironmentSettings overrideEnvironmentSettings,
            ColorScheme playerOverrideColorScheme,
            bool playerOverrideLightshowColors,
            GameplayModifiers gameplayModifiers,
            PlayerSpecificSettings playerSpecificSettings,
            PracticeSettings practiceSettings,
            EnvironmentsListModel environmentsListModel,
            GameplayAdditionalInformation gameplayAdditionalInformation,
            Action beforeSceneSwitchToGameplayCallback,
            Action<DiContainer> afterSceneSwitchToGameplayCallback,
            Action<StandardLevelScenesTransitionSetupData, LevelCompletionResults> levelFinishedCallback,
            Action<StandardLevelScenesTransitionSetupData, LevelCompletionResults> levelRestartedCallback) {
#if BEAT_SABER_1_29_0
            IDifficultyBeatmap difficultyBeatmap = beatmapKey.difficultyBeatmap
                ?? throw new InvalidOperationException("The selected beatmap is unavailable");
            menuTransitionsHelper.StartStandardLevel(
                gameMode,
                difficultyBeatmap,
                difficultyBeatmap.level,
                overrideEnvironmentSettings,
                playerOverrideColorScheme,
                gameplayModifiers,
                playerSpecificSettings,
                practiceSettings,
                gameplayAdditionalInformation.backButtonText,
                gameplayAdditionalInformation.useTestNoteCutSoundEffects,
                gameplayAdditionalInformation.startPaused,
                beforeSceneSwitchToGameplayCallback,
                afterSceneSwitchToGameplayCallback,
                levelFinishedCallback,
                levelRestartedCallback == null
                    ? null
                    : new Action<LevelScenesTransitionSetupDataSO, LevelCompletionResults>((setupData, results) =>
                        levelRestartedCallback((StandardLevelScenesTransitionSetupDataSO)setupData, results)));
#elif BEAT_SABER_1_37_1
            menuTransitionsHelper.StartStandardLevel(
                gameMode,
                beatmapKey,
                beatmapLevel,
                overrideEnvironmentSettings,
                playerOverrideColorScheme,
                beatmapLevel.GetColorScheme(beatmapKey),
                gameplayModifiers,
                playerSpecificSettings,
                practiceSettings,
                environmentsListModel,
                gameplayAdditionalInformation.backButtonText,
                gameplayAdditionalInformation.useTestNoteCutSoundEffects,
                gameplayAdditionalInformation.startPaused,
                beforeSceneSwitchToGameplayCallback,
                afterSceneSwitchToGameplayCallback,
                levelFinishedCallback,
                levelRestartedCallback == null
                    ? null
                    : new Action<LevelScenesTransitionSetupDataSO, LevelCompletionResults>((setupData, results) =>
                        levelRestartedCallback((StandardLevelScenesTransitionSetupDataSO)setupData, results)));
#elif BEAT_SABER_1_38_0
            menuTransitionsHelper.StartStandardLevel(
                gameMode,
                beatmapKey,
                beatmapLevel,
                overrideEnvironmentSettings,
                playerOverrideColorScheme,
                beatmapLevel.GetColorScheme(beatmapKey),
                gameplayModifiers,
                playerSpecificSettings,
                practiceSettings,
                environmentsListModel,
                gameplayAdditionalInformation.backButtonText,
                gameplayAdditionalInformation.useTestNoteCutSoundEffects,
                gameplayAdditionalInformation.startPaused,
                beforeSceneSwitchToGameplayCallback,
                afterSceneSwitchToGameplayCallback,
                levelFinishedCallback,
                levelRestartedCallback);
#else
            menuTransitionsHelper.StartStandardLevel(
                gameMode,
                beatmapKey,
                beatmapLevel,
                overrideEnvironmentSettings,
                playerOverrideColorScheme,
                playerOverrideLightshowColors,
                beatmapLevel.GetColorScheme(beatmapKey),
                gameplayModifiers,
                playerSpecificSettings,
                practiceSettings,
                environmentsListModel,
                gameplayAdditionalInformation.backButtonText,
                gameplayAdditionalInformation.useTestNoteCutSoundEffects,
                gameplayAdditionalInformation.startPaused,
                beforeSceneSwitchToGameplayCallback,
                afterSceneSwitchToGameplayCallback,
                levelFinishedCallback,
                levelRestartedCallback);
#endif
        }
    }
}
#endif
