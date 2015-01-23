using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Harness.Support;

namespace Harness {

    public abstract class ViewModel : INotifyPropertyChanged {

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INotifyPropertyChanged Members

        public void Set<TY>(Expression<Func<TY>> member, TY value) {
            var property = Expressions.MemberExpressionToMember(member) as PropertyInfo;
            if (property != null) {
                property.SetValue(this, value);
                OnPropertyChanged(property.Name);
            }
            throw new ArgumentException("member is not a Property Expression", "member");
        }

        public void OnPropertyChanged(string property) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}