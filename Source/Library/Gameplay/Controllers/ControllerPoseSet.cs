#nullable enable

namespace Legato.Gameplay.Controllers {
    internal readonly struct ControllerPoseSet {
        internal ControllerPose? Shared { get; }
        internal ControllerPose? Left { get; }
        internal ControllerPose? Right { get; }

        internal ControllerPoseSet(ControllerPose? shared = null, ControllerPose? left = null, ControllerPose? right = null) {
            Shared = shared;
            Left = left;
            Right = right;
        }
    }
}
