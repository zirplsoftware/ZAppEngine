using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class NameEvaluator : IMemberEvaluator
    {
        private String _named;
        private IEnumerable<String> _namedAny;

        internal String Named
        {
            get { return _named; }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException("value");
                if (_named != null) throw new InvalidOperationException("Cannot call Named twice. Use NamedAny instead.");
                if (_namedAny != null) throw new InvalidOperationException("Cannot call both Named and NamedAny.");

                _named = value;
            }
        }
        internal IEnumerable<String> NamedAny
        {
            get { return _namedAny; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (!value.Any()) throw new ArgumentException("names must have at least one entry", "value");
                if (value.Any(String.IsNullOrEmpty)) throw new ArgumentException("An entry in the names provided was null", "value");
                if (_namedAny != null) throw new InvalidOperationException("Cannot call NamedAny twice.");
                if (_named != null) throw new InvalidOperationException("Cannot call both Named and NamedAny.");

                _namedAny = value;
            }
        }
        internal bool IgnoreCase { get; set; }

        public bool IsMatch(MemberInfo memberInfo)
        {
            if (NamedAny.Any())
            {
                var namesList = IgnoreCase
                       ? from o in NamedAny select o.ToLowerInvariant()
                       : NamedAny;
                return namesList.Contains(IgnoreCase
                    ? memberInfo.Name.ToLowerInvariant()
                    : memberInfo.Name);
            }
            else if (Named != null)
            {
                return IgnoreCase
                    ? memberInfo.Name.ToLowerInvariant().Equals(Named.ToLowerInvariant())
                    : memberInfo.Name.Equals(Named);
            }
            return true;
        }
    }
}
