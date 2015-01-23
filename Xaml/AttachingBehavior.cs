using System.Collections.Generic;
using Xamarin.Forms;

namespace Harness.Xaml {

    /// <summary>
    /// Generic base class for Behaviors utilizing properties with IAttachedObject values.
    /// </summary>
    /// <typeparam name="T">The type this behavior can be applied to.</typeparam>
    /// <remarks>
    /// Provides transparent support for IAttachedObjects.
    /// </remarks>
    public class AttachingBehavior<T> : Behavior<T>, IAttachingObject
        where T : BindableObject {

        protected AttachingBehavior() {
            X.SetAttached(this, new List<IAttachedObject>());
        }

        /// <summary>
        /// The IAttachedObject instances attached using this Behavior.
        /// </summary>
        public IList<IAttachedObject> Attached {
            get { return X.GetAttached(this); }
        }

        public BindableObject Target { get; private set; }

        protected override void OnAttachedTo(T bindable) {
            Target = bindable;
            foreach (var attached in Attached) {
                attached.OnAttachedTo(Target);
            }
        }

        protected override void OnDetachingFrom(T bindable) {
            Target = null;
            foreach (var attached in Attached) {
                attached.OnDetachedFrom(Target);
            }
        }
    }
}