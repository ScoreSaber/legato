# Legato library reference

This lists the small reusable APIs supplied by Legato. Transparent polyfills mirror the original game or dependency API and are omitted.

| Area | API | Purpose |
| --- | --- | --- |
| Beatmaps | `BeatmapLevelsModel.GetLoadedBeatmapLevels()` | Enumerate loaded levels |
| Beatmaps | `BeatmapLevelsModel.GetLevelByHash(hash, cancellationToken)` | Find a custom level by hash |
| Beatmaps | `BeatmapLevel.TryGetDifficultyDetails(key, out details)` | Read difficulty metadata |
| Beatmaps | `BeatmapMaxScoreCache.GetMaxScore(level, key)` | Compute and cache maximum score |
| Coroutines | `SharedCoroutineStarterAdapter.StartCoroutine(routine)` | Adapt the 1.29.0 game coroutine runner |
| Gameplay | `ColorSchemeFactory.Create(...)` | Build a color scheme with player fallbacks |
| Gameplay | `GameplayMetadataProvider.JumpDistance(initData)` | Read or reconstruct jump distance |
| Gameplay | `GameplayMetadataProvider.ArcVisibility(settings)` | Read arc visibility |
| Gameplay | `GameplayMetadataProvider.ControllerPoses()` | Read controller offsets |
| Events | `MovementDataEvents.Initializing` | Change jump speed, value type or value before movement data initializes |
| Events | `HeadsetEvents.HeadsetUnmounted` | Observe HMD unmounts |
| Events | `HeadsetUnmountedEventArgs.SuppressGameHandling` | Prevent the game from handling an HMD unmount |
| Events | `InputFocusEvents.InputFocusEvaluated` | Inspect or change the evaluated input focus state |
| Gameplay | `OverrideEnvironmentSettingsFactory.Create(...)` | Resolve environment overrides |
| Gameplay | `PlayerSpecificSettingsFactory.Create(...)` | Create player settings |
| Platform | `IPlatformUserProvider.GetUserInfo(cancellationToken)` | Get the current platform user |
| Platform | `IPlatformAuthenticationProvider.GetAuthToken()` | Get the platform access token |
| Platform | `IPlatformAuthenticationProvider.GetCrossPlatformAccessToken(cancellationToken)` | Get the cross-platform access token |
| Platform | `IPlatformFriendsProvider.GetFriendUserIds()` | Get platform friend IDs |
| Platform | `GamePlatformAdapter` | Adapt the active game platform service |
| Presentation | `BsmlParser.Instance` | Get the BSML parser singleton |
| Presentation | `CurvedTextFactory.Create(parent, text, position)` | Create curved BSML text |
| Presentation | `SpriteFactory.Create(image)` | Create a sprite from image bytes |
| Presentation | `SpriteFactory.Destroy(sprite)` | Destroy a sprite and its texture |
| Resources | `EmbeddedResources.Read(assembly, resource)` | Read an embedded resource |
| Rooms | `RoomSettings.Center` | Get the room center |
| Rooms | `RoomSettings.Rotation` | Get the room rotation |
| XR | `XrDeviceDiscovery.RuntimeName` | Get the OpenXR runtime name |
| XR | `XrDeviceDiscovery.LegacyHeadsetModel` | Get the legacy headset model |
| XR | `XrDeviceDiscovery.GetNodeDeviceName(node)` | Get an XR node device name |
| XR | `XrDeviceDiscovery.GetControllerDeviceName(hand)` | Get a controller device name |
