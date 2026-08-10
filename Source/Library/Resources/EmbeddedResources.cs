#nullable enable

using BeatSaberMarkupLanguage;
using System.Reflection;

namespace Legato.Resources {
    internal static class EmbeddedResources {
        internal static byte[] Read(Assembly assembly, string resource) {
#pragma warning disable CS0618
            return Utilities.GetResource(assembly, resource);
#pragma warning restore CS0618
        }
    }
}
