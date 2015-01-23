using Xamarin.Forms;

namespace Harness.Xaml {

    public static class Names {

        public static void AddName(string name, BindableObject value) {
            BindableObject target = null;
            bool isAttached = false;
            var attachedObject = value as IAttachedObject;
            if (attachedObject != null) {
                if (attachedObject.AttachedTo == null) {
                    attachedObject.Attached += o => {
                        CreateResource(o as VisualElement, true, name, value);
                    };
                    return;
                }
                target = attachedObject.AttachedTo;
                isAttached = true;
            } else
                target = value;

            CreateResource(target as VisualElement, isAttached, name, value);
        }

        public static void RemoveName(BindableObject bindable, string name) {
            Resources.SafeRemoveResource(bindable as VisualElement, name);
        }

        private static VisualElement FindResourceTarget(VisualElement target, bool isAttached) {
            if (!isAttached)
                return target.ParentView ?? target;

            return target;
        }

        private static void CreateResource(VisualElement target, bool isAttached, string name, BindableObject value) {
            if (target == null) return;
            var parent = FindResourceTarget(target, isAttached);
            if (parent == null) return;
            X.DynamicResource(parent, name, value);
        }
    }
}