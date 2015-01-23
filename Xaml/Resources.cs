using Xamarin.Forms;

namespace Harness.Xaml {

    public static class Resources {

        public static void SafeAddResource(VisualElement view, string key, object value) {
            if (view == null) return;
            if (view.Resources == null) view.Resources = new ResourceDictionary();
            else if (view.Resources.ContainsKey(key))
                RemoveResource(view, key);
            AddResource(view, key, value);
        }

        private static void AddResource(VisualElement view, string key, object value) {
            view.Resources.Add(key, value);
        }

        public static void SafeRemoveResource(VisualElement view, string key) {
            if (view == null) return;
            if (view.Resources == null) return;

            RemoveResource(view, key);
        }

        private static void RemoveResource(VisualElement view, string key) {
            view.Resources.Remove(key);
        }
    }
}