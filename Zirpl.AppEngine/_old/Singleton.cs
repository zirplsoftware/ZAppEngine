//using System;
//using System.Collections.Generic;

//namespace Zirpl.AppEngine._old
//{
//    /// <summary>
//    /// A statically compiled "singleton" used to store objects throughout the 
//    /// lifetime of the app domain. Not so much singleton in the pattern's 
//    /// sense of the word as a standardized way to store single instances.
//    /// </summary>
//    /// <typeparam name="T">The type of object to store.</typeparam>
//    /// <remarks>Access to the instance is not synchrnoized.</remarks>
//    public class Singleton<T>
//    {
//        private static readonly IDictionary<Type, object> ALL_SINGLETONS;
//        private static T INSTANCE;

//        static Singleton()
//        {
//            ALL_SINGLETONS = new Dictionary<Type, object>();
//        }

//        /// <summary>Dictionary of type to singleton instances.</summary>
//        protected static IDictionary<Type, object> AllSingletons
//        {
//            get { return ALL_SINGLETONS; }
//        }
        
//        /// <summary>The singleton instance for the specified type T. Only one instance (at the time) of this object for each type of T.</summary>
//        public static T Instance
//        {
//            get { return INSTANCE; }
//            protected set
//            {
//                INSTANCE = value;
//                AllSingletons[typeof(T)] = value;
//            }
//        }
//    }
//}
