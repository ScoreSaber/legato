#nullable enable

using BeatSaberMarkupLanguage;
using UnityEngine;

namespace Legato.Presentation {
    internal static class SpriteFactory {
        internal static Sprite Create(byte[] image) {
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(image);
            return Utilities.LoadSpriteFromTexture(texture);
        }

        internal static void Destroy(Sprite sprite) {
            if (sprite == null) {
                return;
            }

            Texture2D texture = sprite.texture;
            UnityEngine.Object.Destroy(sprite);
            if (texture != null) {
                UnityEngine.Object.Destroy(texture);
            }
        }
    }
}
