#nullable enable

using System.Collections.Generic;
using UnityEngine.XR;

namespace Legato.XR {
    public static class XrDeviceDiscovery {
        public static string RuntimeName {
            get {
#if BEAT_SABER_1_29_0
                return string.Empty;
#else
                return UnityEngine.XR.OpenXR.OpenXRRuntime.name ?? string.Empty;
#endif
            }
        }

        public static string LegacyHeadsetModel {
            get {
#if BEAT_SABER_1_29_0
#pragma warning disable CS0618
                return XRDevice.model;
#pragma warning restore CS0618
#else
                return string.Empty;
#endif
            }
        }

        public static string? GetNodeDeviceName(XRNode node) {
            var devices = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(node, devices);
            return devices.Count == 0 ? null : devices[0].name;
        }

        public static string? GetControllerDeviceName(InputDeviceCharacteristics hand) {
            var devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller | hand, devices);
            return devices.Count == 0 ? null : devices[0].name;
        }
    }
}
