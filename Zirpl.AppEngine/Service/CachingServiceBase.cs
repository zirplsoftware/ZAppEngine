using System;
using System.Collections.Generic;

namespace Zirpl.AppEngine.Service
{
    public abstract class CachingServiceBase<TData> : IService
    {
        private Object _syncRoot;

        protected IList<TData> DataCache { get; set; }

        protected Object SyncRoot
        {
            get
            {
                if (this._syncRoot == null)
                {
                    System.Threading.Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
                }
                return this._syncRoot;
            }
        }

        public virtual void InvalidateCache()
        {
            lock (this.SyncRoot)
            {
                this.DataCache = null;
            }
        }

        protected virtual bool ShouldRetrieve
        {
            get
            {
                return this.DataCache == null
                       || this.LastRetrievalTime == null
                       || DateTime.Now > this.LastRetrievalTime.Value.AddMilliseconds(this.RetrievalIntervalMilliseconds);
            }
        }

        protected virtual DateTime? LastRetrievalTime { get; set; }
        public abstract long RetrievalIntervalMilliseconds { protected get; set;  }

    }
}
