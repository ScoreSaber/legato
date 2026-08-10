#nullable enable

namespace Legato {
    internal sealed class GameplayAdditionalInformation {
        internal readonly string backButtonText;
        internal readonly bool useTestNoteCutSoundEffects;
        internal readonly bool startPaused;

        internal GameplayAdditionalInformation(string? backButtonText = null, bool useTestNoteCutSoundEffects = false, bool startPaused = false) {
            this.backButtonText = backButtonText ?? string.Empty;
            this.useTestNoteCutSoundEffects = useTestNoteCutSoundEffects;
            this.startPaused = startPaused;
        }
    }
}
