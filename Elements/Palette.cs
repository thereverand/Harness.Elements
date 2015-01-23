using System.Collections.Generic;
using Harness.Xaml;
using Xamarin.Forms;

namespace Harness.Elements {

    [ContentProperty("Value")]
    public class Color : BindableObject {

        public string Key {
            get { return (string)GetValue(X.KeyProperty); }
            set { SetValue(X.KeyProperty, value); }
        }

        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create<Color, Xamarin.Forms.Color>(p => p.Value, Xamarin.Forms.Color.Transparent);

        public Xamarin.Forms.Color Value {
            get { return (Xamarin.Forms.Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
    }

    [ContentProperty("Colors")]
    public class Palette : AttachedObject<VisualElement> {

        public Palette() {
            Colors = new List<Color>();
        }

        public string Name {
            get { return X.GetName(this); }
        }

        public static readonly BindableProperty ColorsProperty =
            BindableProperty.Create<Palette, IList<Color>>(p => p.Colors, new List<Color>());

        public IList<Color> Colors {
            get { return (IList<Color>)GetValue(ColorsProperty); }
            set { SetValue(ColorsProperty, value); }
        }

        private static string Key(string name, string key) {
            return string.Format("{0}/{1}", name, key);
        }

        public override void OnAttachedTo(VisualElement bindable) {
            foreach (var color in Colors) {
                Resources.SafeAddResource(bindable, Key(Name, color.Key), color.Value);
            }
        }

        public override void OnDetachedFrom(VisualElement bindable) {
            foreach (var color in Colors)
                Resources.SafeRemoveResource(bindable, Key(Name, color.Key));
        }
    }
}