using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Harness.Xaml {

    public class Colors : Dictionary<string, Color> { }

    public class Items : Items<BindableObject> { }

    public class Items<T> : List<T> where T : BindableObject {
    }
}