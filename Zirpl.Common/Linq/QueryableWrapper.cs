//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Zirpl.Collections;

//namespace Zirpl.Linq
//{
//    public class QueryableWrapper<T> : IQueryable<T>
//    {
//        private readonly IQueryable<T> _innerQueryable;
//        private readonly Action<T> _onEnumerationAction;

//        public QueryableWrapper(IQueryable<T> queryable, Action<T> onEnumerationAction)
//        {
//            this._onEnumerationAction = onEnumerationAction;
//            this._innerQueryable = queryable;
//        }

//        public IEnumerator<T> GetEnumerator()
//        {
//            return new EnumeratorWrapper<T>(this._innerQueryable.GetEnumerator(), this._onEnumerationAction);
//        }

//        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//        {
//            return new EnumeratorWrapper<T>(this._innerQueryable.GetEnumerator(), this._onEnumerationAction);
//        }

//        public Type ElementType
//        {
//            get { return this._innerQueryable.ElementType; }
//        }

//        public System.Linq.Expressions.Expression Expression
//        {
//            get { return this._innerQueryable.Expression; }
//        }

//        public IQueryProvider Provider
//        {
//            get { return this._innerQueryable.Provider; }
//        }
//    }
//}
