//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Zirpl.Linq
//{
//    public class QueryProviderWrapper<T> : IQueryProvider
//    {
//        private readonly IQueryProvider _innerQueryProvider;
//        private readonly Action<T> _onEnumerationAction;
//        public QueryProviderWrapper(IQueryProvider queryProvider, Action<T> onEnumerationAction)
//        {
//            this._onEnumerationAction = onEnumerationAction;
//            this._innerQueryProvider = queryProvider;
//        }
//        public IQueryable<TElement> CreateQuery<TElement>(System.Linq.Expressions.Expression expression)
//        {
//            throw new NotImplementedException();
//            //return new QueryableWrapper<TElement>(this._innerQueryProvider.CreateQuery<TElement>(expression), this._onEnumerationAction);
//        }

//        public IQueryable CreateQuery(System.Linq.Expressions.Expression expression)
//        {
//            return new QueryableWrapper<T>(this._innerQueryProvider.CreateQuery<T>(expression), this._onEnumerationAction);
//        }

//        public TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
//        {
//            throw new NotImplementedException();
//        }

//        public object Execute(System.Linq.Expressions.Expression expression)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
