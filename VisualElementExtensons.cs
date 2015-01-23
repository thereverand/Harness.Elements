using System.Linq;
using Harness.Elements;
using Xamarin.Forms;

namespace Harness {

    public static class VisualElementExtensons {

        public static T Resource<T>(this VisualElement element, string key) {
            return (T)element.Resources[key];
        }

        public static ColorScheme GetScheme(this VisualElement element) {
            return element.Behaviors.OfType<ColorScheme>().FirstOrDefault();
        }

        public static void ChangePalette(this VisualElement element, Palette palette) {
            var scheme = element.GetScheme();
            if (scheme != null)
                scheme.Palette = palette;
        }
    }
}