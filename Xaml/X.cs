using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Harness.Xaml {

    public static class X {

        public static readonly BindableProperty KeyProperty =
            BindableProperty.CreateAttached(
                "Key",
                typeof(string),
                typeof(X),
                string.Empty);

        public static string GetKey(BindableObject bindable) {
            return (string)bindable.GetValue(KeyProperty);
        }

        public static void SetKey(BindableObject bindable, string value) {
            bindable.SetValue(KeyProperty, value);
        }

        /// <summary>
        /// Identifies the X.Name attached BindableProperty.
        /// </summary>
        public static readonly BindableProperty NameProperty =
            BindableProperty.CreateAttached(
                "Name",
                typeof(string),
                typeof(X),
                null,
                propertyChanging: (b, o, v) => {
                    Names.RemoveName(b, (string)o);
                },
                propertyChanged: (b, o, v) => {
                    if (v == null) return;
                    Names.AddName((string)v, b);
                });

        public static string GetName(BindableObject bindable) {
            return (string)bindable.GetValue(NameProperty);
        }

        public static void SetName(BindableObject bindable, string value) {
            bindable.SetValue(NameProperty, value);
        }

        /// <summary>
        /// Creates a DynamicResource with the specified key safely.
        /// It will initialize the ResourceDictionary if necessary and
        /// remove any other resource with the same key. If the value
        /// </summary>
        /// <param name="element">the VisualElement to add the Resource to</param>
        /// <param name="key">The Resource Key</param>
        /// <param name="value">The DynamicResource's Value</param>
        public static void DynamicResource(VisualElement element, string key, object value) {
            Resources.SafeAddResource(element, key, value);
        }

        /// <summary>
        /// Identifies the X.Attached attached BindableProperty.
        /// </summary>
        /// <remarks>
        /// X.Attached attaches IAttachedObjects to arbitrary BindableObjects.
        /// It can be used in your code to create properties holding multiple attachables.
        /// <seealso cref="AttachingBehavior{T}"/>
        /// </remarks>
        public static readonly BindableProperty AttachedProperty =
            BindableProperty.CreateAttached(
                "Attached",
                typeof(IList<IAttachedObject>),
                typeof(X),
                new List<IAttachedObject>(),
                propertyChanging: (b, o, v) => {
                    if (o == null) return;
                    foreach (var attachable in ((IList<IAttachedObject>)o)) {
                        if (!attachable.IsAttached) attachable.OnDetachedFrom(b);
                    }
                },
                propertyChanged: (b, o, v) => {
                    if (v == null) return;
                    foreach (var attachable in ((IList<IAttachedObject>)v)) {
                        if (!attachable.IsAttached) attachable.OnAttachedTo(b);
                    }
                });

        public static IList<IAttachedObject> GetAttached(BindableObject bindable) {
            return (IList<IAttachedObject>)bindable.GetValue(AttachedProperty);
        }

        public static void SetAttached(BindableObject bindable, IList<IAttachedObject> value) {
            bindable.SetValue(AttachedProperty, value);
        }
    }
}