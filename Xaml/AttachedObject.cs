using System;
using System.Linq.Expressions;
using Harness.Elements;
using Harness.Support;
using Xamarin.Forms;

namespace Harness.Xaml {

    public static class AttachedObject {

        private static void AttachedObjectPropertyChanging<T>(BindableObject bindable, T oldValue, T newValue, Action<BindableObject, T> changing) {
            if (oldValue == null) return;
            var attachable = (IAttachedObject)newValue;
            var attaching = ((IAttachingObject)bindable);

            if (attaching != null) {
                bindable = attaching.Target;
                attaching.Attached.Remove(attachable);
            }
            attachable.OnDetachedFrom(bindable);
            if (changing != null)
                changing(bindable, newValue);
        }

        private static void AttachedObjectPropertyChanged<T>(BindableObject b, T o, T v, Action<BindableObject, T> changed) {
            if (v == null) return;
            var attachable = (IAttachedObject)v;
            var attaching = ((IAttachingObject)b);

            if (attaching != null) {
                b = attaching.Target;
                attaching.Attached.Add(attachable);
            }
            attachable.OnAttachedTo(b);
            if (changed != null)
                changed(b, v);
        }

        /// <summary>
        /// Creates a BindableProperty with a IAttachedObject value.
        /// </summary>
        /// <typeparam name="TOwner"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <param name="defaultValue"></param>
        /// <param name="changing"></param>
        /// <param name="changed"></param>
        /// <returns></returns>
        public static BindableProperty Property<TOwner, T>(
            Expression<Func<TOwner, T>> property,
            T defaultValue,
            Action<BindableObject, T> changing = null,
            Action<BindableObject, T> changed = null)
            where TOwner : BindableObject
            where T : IAttachedObject {
            return BindableProperty.Create(
                Expressions.MemberExpressionToMember(property).Name,
                typeof(T),
                typeof(TOwner),
                defaultValue,
                propertyChanging: (b, o, v) => {
                    AttachedObjectPropertyChanging(b, (T)o, (T)v, changing);
                },
                propertyChanged: (b, o, v) => {
                    AttachedObjectPropertyChanged(b, (T)o, (T)v, changed);
                });
        }

        /// <summary>
        /// Creates an attached BindableProperty with a IAttachedObject value.
        /// </summary>
        /// <typeparam name="TOwner"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="changing"></param>
        /// <param name="changed"></param>
        /// <returns></returns>
        public static BindableProperty AttachedProperty<TOwner, T>(
            string name,
            T defaultValue,
            Action<BindableObject, T> changing = null,
            Action<BindableObject, T> changed = null)
            where TOwner : BindableObject
            where T : IAttachedObject {
            return BindableProperty.CreateAttached(
                name,
                typeof(T),
                typeof(TOwner),
                defaultValue,
                 propertyChanging: (b, o, v) => {
                     AttachedObjectPropertyChanging(b, (T)o, (T)v, changing);
                 },
                propertyChanged: (b, o, v) => {
                    AttachedObjectPropertyChanged(b, (T)o, (T)v, changed);
                });
        }
    }

    /// <summary>
    /// Base class for Attached Objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AttachedObject<T> : BindableObject, IAttachedObject where T : BindableObject {

        public event Action<T> Attached;

        public event Action<T> Detached;

        event Action<BindableObject> IAttachedObject.Attached {
            add { Attached += value; }
            remove { Attached -= value; }
        }

        event Action<BindableObject> IAttachedObject.Detached {
            add { Attached += value; }
            remove { Attached -= value; }
        }

        public T AttachedTo { get; protected set; }

        public bool IsAttached { get { return AttachedTo == null; } }

        BindableObject IAttachedObject.AttachedTo {
            get { return AttachedTo; }
        }

        public virtual void OnAttachedTo(T bindable) {
        }

        public virtual void OnDetachedFrom(T bindable) {
        }

        public void OnAttachedTo(BindableObject bindable) {
            var target = bindable as T;
            if (target == null)
                return;

            AttachedTo = target;
            OnAttachedTo(target);
            if (Attached != null)
                Attached(target);
        }

        public void OnDetachedFrom(BindableObject bindable) {
            AttachedTo = null;
            var target = bindable as T;
            if (target != null)
                OnDetachedFrom(target);
            if (Detached != null)
                Detached(target);
        }
    }
}