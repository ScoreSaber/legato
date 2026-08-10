#nullable enable

using BeatSaberMarkupLanguage.Components;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Legato {
    internal static class ButtonIconImageExtensions {
        internal static Image GetImage(this ButtonIconImage buttonIcon) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            return buttonIcon.image;
#else
            return buttonIcon.Image;
#endif
        }

        internal static void SetImage(this ButtonIconImage buttonIcon, Image image) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            buttonIcon.image = image;
#else
            buttonIcon.Image = image;
#endif
        }
    }

    internal static class ExternalComponentsExtensions {
        internal static void AddExternalComponent(this ExternalComponents externalComponents, Component component) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            externalComponents.components.Add(component);
#else
            externalComponents.Components.Add(component);
#endif
        }
    }

    internal static class ModalKeyboardExtensions {
        internal static void ShowWithEnterHandler(this ModalKeyboard modalKeyboard, Action<string> enterPressedHandler) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            modalKeyboard.keyboard.EnterPressed += enterPressedHandler;
            modalKeyboard.modalView.Show(true, true);
#else
            modalKeyboard.Keyboard.EnterPressed += enterPressedHandler;
            modalKeyboard.ModalView.Show(true, true);
#endif
        }
    }
}
