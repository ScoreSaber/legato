#nullable enable

using System;

namespace BeatSaberMarkupLanguage.Util {
    internal static class MainMenuAwaiter {
        internal static event Action MainMenuInitializing {
            add => value();
            remove { }
        }
    }
}
