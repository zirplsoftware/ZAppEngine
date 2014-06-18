using System;

namespace Zirpl.AppEngine
{
    public class ChangedValueEventArgs<T> : EventArgs
    {
        public ChangedValueEventArgs(T oldValue, T newValue)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public ChangedValueEventArgs()
        {
        }

        public T NewValue { get; set; }
        
        public T OldValue { get; set;}
        
    }
}