#nullable enable

namespace Legato.Gameplay {
    internal static class OverrideEnvironmentSettingsFactory {
        internal static OverrideEnvironmentSettings? Create(PlayerData playerData, EnvironmentsListModel environmentsListModel, string environmentName, bool useOverride) {
#if BEAT_SABER_1_29_0
            return playerData.overrideEnvironmentSettings;
#else
            if (!useOverride || string.IsNullOrEmpty(environmentName)) {
                return playerData.overrideEnvironmentSettings;
            }

            EnvironmentInfoSO? environmentInfo = environmentsListModel.GetEnvironmentInfoBySerializedName(environmentName);
            if (environmentInfo == null) {
                return playerData.overrideEnvironmentSettings;
            }

            var settings = new OverrideEnvironmentSettings();
            settings.overrideEnvironments = true;
            settings.SetEnvironmentInfoForType(environmentInfo.environmentType, environmentInfo);
            return settings;
#endif
        }
    }
}
