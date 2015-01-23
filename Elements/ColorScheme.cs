using System.Collections.Generic;
using Harness.Xaml;
using Xamarin.Forms;

namespace Harness.Elements {

    [ContentProperty("Palette")]
    public class ColorScheme : AttachingBehavior<VisualElement> {

        public static readonly BindableProperty NameProperty =
            BindableProperty.Create<ColorScheme, string>(p => p.Name, null);

        public string Name {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly BindableProperty PaletteProperty =
            AttachedObject.Property<ColorScheme, Palette>(
                c => c.Palette,
                null,
                (a, p) => {
                    if (p != null) X.SetName(p, null);
                },
                (a, p) => {
                    if (p != null) X.SetName(p, p.Name ?? "Scheme");
                });

        public Palette Palette {
            get { return (Palette)GetValue(PaletteProperty); }
            set { SetValue(PaletteProperty, value); }
        }
    }
}