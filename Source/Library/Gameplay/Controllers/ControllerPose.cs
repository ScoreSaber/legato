#nullable enable

using UnityEngine;

namespace Legato.Gameplay.Controllers {
    internal readonly struct ControllerPose {
        internal Vector3 Position { get; }
        internal Vector3 Rotation { get; }

        internal ControllerPose(Vector3 position, Vector3 rotation) {
            Position = position;
            Rotation = rotation;
        }
    }
}
