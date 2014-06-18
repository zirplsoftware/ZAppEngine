using System;

namespace Zirpl.AppEngine.Model
{
    public interface IRange :IComparable
    {
        object Minimum { get; set;}
        object Maximum { get; set;}
        bool IsValid { get;}
    }
}
