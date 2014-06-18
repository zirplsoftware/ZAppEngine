using System;
using Zirpl.AppEngine.Service;

namespace Zirpl.AppEngine.Model
{
    public static class PersistableExtensions
    {
        public static bool EvaluateIsPersisted(this IPersistable persistable)
        {
            if (persistable == null)
                throw new ArgumentNullException("persistable");

            bool persisted = false;
            if (persistable.GetId() is Int32)
            {
                persisted = ((Int32)persistable.GetId()) > 0;
            }
            else if (persistable.GetId() is String)
            {
                persisted = !String.IsNullOrEmpty((String)persistable.GetId());
            }
            else if (persistable.GetId() is Int64)
            {
                persisted = ((long)persistable.GetId()) > 0;
            }
            else if (persistable.GetId() is Int16)
            {
                persisted = ((Int32)persistable.GetId()) > 0;
            }
            else if (persistable.GetId() is Byte)
            {
                persisted = ((Byte)persistable.GetId()) > 0;
            }
            else if (persistable.GetId() is Guid)
            {
                persisted = !((Guid)persistable.GetId()).Equals(Guid.Empty);
            }
            else
            {
                throw new UnexpectedCaseException("EvaluateIsPersisted is not implemented for the Id Type.", persistable.GetId().GetType());
            }
            return persisted;
        }

        public static bool EvaluateEquals(this IPersistable persistable, Object other)
        {
            if (persistable == null)
                throw new ArgumentNullException("persistable");

            if (persistable == other) return true;
            if (other == null) return false;
            if (!persistable.GetType().Equals(other.GetType())) return false;

            var otherPersistable = (IPersistable)other;
            if (!(persistable.IsPersisted && otherPersistable.IsPersisted)) return false;

            if (persistable.GetId().Equals(otherPersistable.GetId())) return true;
            return false;
        }

        public static int EvaluateGetHashCode(this IPersistable persistable)
        {
            if (persistable == null)
                throw new ArgumentNullException("persistable");

            if (persistable.GetId() == null)
            {
                return 0;
            }
            return persistable.GetId().GetHashCode();
        }

        public static bool IsNullId<TId>(this TId id)
        {
            if (id is String)
            {
                String idString = id as String;
                return idString.IsNullId();
            }
            else if (id is int?)
            {
                int? idNullableInt = id as int?;
                return idNullableInt.IsNullId();
            }
            else if (id is byte?)
            {
                byte? idNullablebyte = id as byte?;
                return idNullablebyte.IsNullId();
            }
            else if (id is int)
            {
                int? idNullableInt = id as int?;
                return idNullableInt.IsNullId();
            }
            else if (id is byte)
            {
                byte? idNullablebyte = id as byte?;
                return idNullablebyte.IsNullId();
            }
            else if (id is Guid)
            {
                Guid? idNullableGuid = id as Guid?;
                return idNullableGuid.IsNullId();
            }
            else if (id is Guid?)
            {
                Guid? idNullableGuid = id as Guid?;
                return idNullableGuid.IsNullId();
            }
            else if (id == null)
            {
                return true;
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", String.Format("Unsupported Id type {0}", typeof(TId).Name));
            }
        }

        public static bool IsNullId(this String idString)
        {
            return String.IsNullOrEmpty(idString)
                   || idString.Trim() == "0"
                   || idString.Trim() == "";
        }

        public static bool IsNullId(this int idInt)
        {
            return idInt == 0;
        }

        public static bool IsNullId(this int? idInt)
        {
            return !idInt.HasValue || idInt.Value == 0;
        }

        public static bool IsNullId(this byte idInt)
        {
            return idInt == 0;
        }

        public static bool IsNullId(this byte? idInt)
        {
            return !idInt.HasValue || idInt.Value == 0;
        }

        public static bool IsNullId(this Guid? id)
        {
            return !id.HasValue || id.Value.Equals(Guid.Empty);
        }

        public static int ToIntId(this String idString)
        {
            if (idString.IsNullId())
            {
                return 0;
            }
            else
            {
                return Int32.Parse(idString);
            }
        }

        public static int? ToNullableIntId(this String idString)
        {
            if (idString.IsNullId())
            {
                return null;
            }
            else
            {
                return Int32.Parse(idString);
            }
        }

        public static int? ToNullableIntId(this int id)
        {
            if (id.IsNullId())
            {
                return null;
            }
            else
            {
                return id;
            }
        }
        public static Guid ToGuidId(this String idString)
        {
            if (idString.IsNullId())
            {
                return Guid.Empty;
            }
            else
            {
#if !NET35 && !NET35CLIENT
                return Guid.Parse(idString);
#else
                return new Guid(idString);
#endif
            }
        }

        public static Guid? ToNullableGuidId(this String idString)
        {
            if (idString.IsNullId())
            {
                return null;
            }
            else
            {
#if !NET35 && !NET35CLIENT
                return Guid.Parse(idString);
#else
                return new Guid(idString);
#endif
            }
        }

        public static Guid? ToNullableIntId(this Guid id)
        {
            if (id.IsNullId())
            {
                return null;
            }
            else
            {
                return id;
            }
        }

        public static byte ToByteId(this String idString)
        {
            if (idString.IsNullId())
            {
                return 0;
            }
            else
            {
                return Byte.Parse(idString);
            }
        }

        public static byte? ToNullableByteId(this String idString)
        {
            if (idString.IsNullId())
            {
                return null;
            }
            else
            {
                return Byte.Parse(idString);
            }
        }

        public static TEntity ToEntity<TEntity>(this String idString, ISupportsGetById<TEntity, Int32> supportsGet)
            where TEntity : class, IPersistable<Int32>
        {
            if (idString.IsNullId())
            {
                return null;
            }
            else
            {
                return supportsGet.Get(idString.ToIntId());
            }
        }

        public static TEntity ToEntity<TEntity>(this int idInt, ISupportsGetById<TEntity, Int32> supportsGet)
            where TEntity : class, IPersistable<Int32>
        {
            if (idInt.IsNullId())
            {
                return null;
            }
            else
            {
                return supportsGet.Get(idInt);
            }
        }

        public static TEntity ToEntity<TEntity>(this int? idInt, ISupportsGetById<TEntity, Int32> supportsGet)
            where TEntity : class, IPersistable<Int32>
        {
            if (idInt.IsNullId())
            {
                return null;
            }
            else
            {
                return supportsGet.Get(idInt.Value);
            }
        }

        public static TEntity ToEntity<TEntity>(this String idString, ISupportsGetById<TEntity, byte> supportsGet)
            where TEntity : class, IPersistable<byte>
        {
            if (idString.IsNullId())
            {
                return null;
            }
            else
            {
                return supportsGet.Get(idString.ToByteId());
            }
        }

        public static TEntity ToEntity<TEntity>(this byte idByte, ISupportsGetById<TEntity, byte> supportsGet)
            where TEntity : class, IPersistable<byte>
        {
            if (idByte.IsNullId())
            {
                return null;
            }
            else
            {
                return supportsGet.Get(idByte);
            }
        }

        public static TEntity ToEntity<TEntity>(this byte? idByte, ISupportsGetById<TEntity, byte> supportsGet)
            where TEntity : class, IPersistable<byte>
        {
            if (idByte.IsNullId())
            {
                return null;
            }
            else
            {
                return supportsGet.Get(idByte.Value);
            }
        }
        
        public static String ToNullableIdString(this int idInt)
        {
            return idInt.ToNullableIdString(false);
        }
        public static String ToNullableIdString(this int idInt, bool useZeroForNull)
        {
            if (idInt.IsNullId())
            {
                if (useZeroForNull)
                {
                    return 0.ToString();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return idInt.ToString();
            }
        }

        public static String ToNullableIdString(this int? idInt)
        {
            return idInt.ToNullableIdString(false);
        }
        public static String ToNullableIdString(this int? idInt, bool useZeroForNull)
        {
            if (idInt.IsNullId())
            {
                if (useZeroForNull)
                {
                    return 0.ToString();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return idInt.ToString();
            }
        }

        public static String ToNullableIdString(this byte idInt)
        {
            return idInt.ToNullableIdString(false);
        }
        public static String ToNullableIdString(this byte idInt, bool useZeroForNull)
        {
            if (idInt.IsNullId())
            {
                if (useZeroForNull)
                {
                    return 0.ToString();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return idInt.ToString();
            }
        }

        public static String ToNullableIdString(this byte? idInt)
        {
            return idInt.ToNullableIdString(false);
        }
        public static String ToNullableIdString(this byte? idInt, bool useZeroForNull)
        {
            if (idInt.IsNullId())
            {
                if (useZeroForNull)
                {
                    return 0.ToString();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return idInt.ToString();
            }
        }
    }
}
