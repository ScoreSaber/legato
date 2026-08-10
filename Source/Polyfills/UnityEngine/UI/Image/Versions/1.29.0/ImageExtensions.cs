#nullable enable

using System.Threading.Tasks;
using UnityEngine.UI;

namespace Legato {
    internal static class ImageExtensions {
        public static Task SetImageAsync(this Image image, string location) {
            BeatSaberMarkupLanguage.BeatSaberUI.SetImage(image, location);
            return Task.CompletedTask;
        }
    }
}
