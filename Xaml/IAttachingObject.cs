using System.Collections.Generic;
using Xamarin.Forms;

namespace Harness.Xaml {

    /// <summary>
    /// Indicates a BindableObject where all IAttachedObjects attempting to attach to it should be attached to the
    /// provided Target BindableObject instead.
    /// </summary>
    public interface IAttachingObject {

        IList<IAttachedObject> Attached { get; }

        BindableObject Target { get; }
    }
}