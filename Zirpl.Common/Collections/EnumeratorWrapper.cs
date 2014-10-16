using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Collections
{
    public class EnumeratorWrapper<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _innerEnumerator;
        private readonly Action<T> _onEnumerationAction;

        public EnumeratorWrapper(IEnumerator<T> enumerator, Action<T> onEnumerationAction)
        {
            this._onEnumerationAction = onEnumerationAction;
            this._innerEnumerator = enumerator;
        }

        public T Current
        {
            get { return this._innerEnumerator.Current; }
        }

        public void Dispose()
        {
            this._innerEnumerator.Dispose();
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this._innerEnumerator.Current; }
        }

        public bool MoveNext()
        {
            bool isAnother = this._innerEnumerator.MoveNext();
            if (isAnother)
            {
                this._onEnumerationAction(this.Current);
            }
            return isAnother;
        }

        public void Reset()
        {
            this._innerEnumerator.Reset();
        }
    }
}
