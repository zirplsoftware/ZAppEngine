using System;

namespace Zirpl.AppEngine
{
    public delegate void ChangedValueDelegate<T>(Object sender, ChangedValueEventArgs<T> args);
}
