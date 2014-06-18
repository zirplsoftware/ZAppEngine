#if !NET35 && !NET35CLIENT
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Zirpl.AppEngine.Model
{
    // An System.Collections.ObjectModel.ObservableCollection that raises
    // individual item removal notifications on clear and prevents adding duplicates.
    public class FixupCollection<T> : ObservableCollection<T>
    {
        protected override void ClearItems()
        {
            new List<T>(this).ForEach(t => Remove(t));
        }

        protected override void InsertItem(int index, T item)
        {
            if (!this.Contains(item))
            {
                base.InsertItem(index, item);
            }
        }
    }
}
#endif