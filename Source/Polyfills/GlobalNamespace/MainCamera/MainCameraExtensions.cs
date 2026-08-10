#nullable enable

using IPA.Utilities;
using UnityEngine;
#if !BEAT_SABER_1_29_0
using UnityEngine.SpatialTracking;
#endif

namespace Legato {
    // 1.29 doesn't have these camera bits, so there is nothing to clone there
    internal static class MainCameraExtensions {
        internal static void CopyTrackedPoseDriverTo(this MainCamera mainCamera, Camera spectatorCamera) {
#if !BEAT_SABER_1_29_0
            mainCamera.gameObject.GetComponent<TrackedPoseDriver>().CopyComponent<TrackedPoseDriver>(spectatorCamera.gameObject);
#endif
        }

        internal static void RebuildDepthTextureControllerFor(this MainCamera mainCamera, Camera spectatorCamera) {
#if !BEAT_SABER_1_29_0
            // recreate this since Instantiate leaves it without its Zenject objects
            Component.Destroy(spectatorCamera.gameObject.GetComponent<DepthTextureController>());
            mainCamera.gameObject.GetComponent<DepthTextureController>().CopyComponent<DepthTextureController>(spectatorCamera.gameObject);
#endif
        }
    }
}
