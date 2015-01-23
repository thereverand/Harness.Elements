using System;
using Xamarin.Forms;

namespace Harness.Xaml {

    /// <summary>
    /// Defines an object that attaches to another.
    /// </summary>
    public interface IAttachedObject {

        event Action<BindableObject> Attached;

        event Action<BindableObject> Detached;

        bool IsAttached { get; }

        BindableObject AttachedTo { get; }

        void OnAttachedTo(BindableObject bindable);

        void OnDetachedFrom(BindableObject bindable);
    }
}