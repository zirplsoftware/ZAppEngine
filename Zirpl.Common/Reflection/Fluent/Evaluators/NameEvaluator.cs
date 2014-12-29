using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal class NameEvaluator : IMatchEvaluator
    {
        private String _name;
        private IEnumerable<String> _names;

        internal bool StartsWith { get; set; }
        internal bool EndsWith { get; set; }
        internal bool Contains { get; set; }
        internal bool All { get; set; }
        internal bool Any { get; set; }

        internal String Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException("value");
                if (_name != null) throw new InvalidOperationException("Cannot call Name twice");
                if (_names != null) throw new InvalidOperationException("Cannot call both Name and Names.");

                _name = value;
            }
        }
        internal IEnumerable<String> Names
        {
            get { return _names; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (!value.Any()) throw new ArgumentException("names must have at least one entry", "value");
                if (value.Any(String.IsNullOrEmpty)) throw new ArgumentException("An entry in the names provided was null", "value");
                if (_names != null) throw new InvalidOperationException("Cannot call Names twice.");
                if (_name != null) throw new InvalidOperationException("Cannot call both Name and Names.");

                _names = value;
            }
        }
        internal bool IgnoreCase { get; set; }

        public bool IsMatch(MemberInfo memberInfo)
        {
            var nameToCheck = GetNameToCheck(memberInfo);
            if (Names != null
                && Names.Any())
            {
                var namesList = IgnoreCase
                       ? from o in Names select o.ToLowerInvariant()
                       : Names;
                return namesList.Contains(IgnoreCase
                    ? nameToCheck.ToLowerInvariant()
                    : nameToCheck);
            }
            else if (Name != null)
            {
                return IgnoreCase
                    ? nameToCheck.ToLowerInvariant().Equals(Name.ToLowerInvariant())
                    : nameToCheck.Equals(Name);
            }
            return true;
        }

        protected virtual String GetNameToCheck(MemberInfo memberInfo)
        {
            return memberInfo.Name;
        }
    }
}
