using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Enums
{
    public static class EnumExtensionMethods
    {
        // taken from: http://journal.stuffwithstuff.com/2008/03/05/checking-flags-in-c-enums/

        public static bool ContainsFlag<T>(this T value, T flags) where T : struct
        {
            Type type = typeof(T);

            // only works with enums
            if (!type.IsEnum) throw new ArgumentException("The type parameter T must be an enum type.");

            // handle each underlying type
            Type numberType = Enum.GetUnderlyingType(type);

            if (numberType == typeof(int))
            {
                return BoolFunction<int>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType == typeof(sbyte))
            {
                return BoolFunction<sbyte>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType == typeof(byte))
            {
                return BoolFunction<byte>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType == typeof(short))
            {
                return BoolFunction<short>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType == typeof(ushort))
            {
                return BoolFunction<ushort>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType == typeof(uint))
            {
                return BoolFunction<uint>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType == typeof(long))
            {
                return BoolFunction<long>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType == typeof(ulong))
            {
                return BoolFunction<ulong>(value, flags, (a, b) => (a & b) == b);
            }
            else
            {
                throw new ArgumentException("Unknown enum underlying type " + numberType.Name + ".");
            }
        }

        //public static bool IsFlagNotSet<T>(this T value, T flags) where T : struct
        //{
        //    Type type = typeof(T);

        //    // only works with enums
        //    if (!type.IsEnum) throw new ArgumentException("The type parameter T must be an enum type.");

        //    // handle each underlying type
        //    Type numberType = Enum.GetUnderlyingType(type);

        //    if (numberType == typeof(int))
        //    {
        //        return BoolFunction<int>(value, flags, (a, b) => b != a && (a & ~b) == 0);
        //    }
        //    else if (numberType == typeof(sbyte))
        //    {
        //        return BoolFunction<sbyte>(value, flags, (a, b) => b != a  && (a & ~b) == 0);
        //    }
        //    else if (numberType == typeof(byte))
        //    {
        //        return BoolFunction<byte>(value, flags, (a, b) => b != a  && (a & ~b) == 0);
        //    }
        //    else if (numberType == typeof(short))
        //    {
        //        return BoolFunction<short>(value, flags, (a, b) => b != a  && (a & ~b) == 0);
        //    }
        //    else if (numberType == typeof(ushort))
        //    {
        //        return BoolFunction<ushort>(value, flags, (a, b) => b != a  && (a & ~b) == 0);
        //    }
        //    else if (numberType == typeof(uint))
        //    {
        //        return BoolFunction<uint>(value, flags, (a, b) => b != a  && (a & ~b) == 0);
        //    }
        //    else if (numberType == typeof(long))
        //    {
        //        return BoolFunction<long>(value, flags, (a, b) => b != a  && (a & ~b) == 0);
        //    }
        //    else if (numberType == typeof(ulong))
        //    {
        //        return BoolFunction<ulong>(value, flags, (a, b) => b != a  && (a & ~b) == 0);
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Unknown enum underlying type " + numberType.Name + ".");
        //    }
        //}
        public static T AddFlag<T>(this T value, T flags) where T : struct
        {
            Type type = typeof(T);

            // only works with enums
            if (!type.IsEnum) throw new ArgumentException("The type parameter T must be an enum type.");

            // handle each underlying type
            Type numberType = Enum.GetUnderlyingType(type);

            if (numberType == typeof(int))
            {
                return (T)Enum.ToObject(type, NumberFunction<int>(value, flags, (a, b) => a | b));
            }
            else if (numberType == typeof(sbyte))
            {
                return (T)Enum.ToObject(type, NumberFunction<sbyte>(value, flags, (a, b) => (sbyte)(a | b)));
            }
            else if (numberType == typeof(byte))
            {
                return (T)Enum.ToObject(type, NumberFunction<byte>(value, flags, (a, b) => (byte)(a | b)));
            }
            else if (numberType == typeof(short))
            {
                return (T)Enum.ToObject(type, NumberFunction<short>(value, flags, (a, b) => (short)(a | b)));
            }
            else if (numberType == typeof(ushort))
            {
                return (T)Enum.ToObject(type, NumberFunction<ushort>(value, flags, (a, b) => (ushort)(a | b)));
            }
            else if (numberType == typeof(uint))
            {
                return (T)Enum.ToObject(type, NumberFunction<uint>(value, flags, (a, b) => a | b));
            }
            else if (numberType == typeof(long))
            {
                return (T)Enum.ToObject(type, NumberFunction<long>(value, flags, (a, b) => a | b));
            }
            else if (numberType == typeof(ulong))
            {
                return (T)Enum.ToObject(type, NumberFunction<ulong>(value, flags, (a, b) => a | b));
            }
            else
            {
                throw new ArgumentException("Unknown enum underlying type " + numberType.Name + ".");
            }
        }

        public static T RemoveFlag<T>(this T value, T flags) where T : struct
        {
            Type type = typeof(T);

            // only works with enums
            if (!type.IsEnum) throw new ArgumentException("The type parameter T must be an enum type.");

            // handle each underlying type
            Type numberType = Enum.GetUnderlyingType(type);

            if (numberType == typeof(int))
            {
                return (T)Enum.ToObject(type, NumberFunction<int>(value, flags, (a, b) => a & ~b));
            }
            else if (numberType == typeof(sbyte))
            {
                return (T)Enum.ToObject(type, NumberFunction<sbyte>(value, flags, (a, b) => (sbyte)(a & ~b)));
            }
            else if (numberType == typeof(byte))
            {
                return (T)Enum.ToObject(type, NumberFunction<byte>(value, flags, (a, b) => (byte)(a & ~b)));
            }
            else if (numberType == typeof(short))
            {
                return (T)Enum.ToObject(type, NumberFunction<short>(value, flags, (a, b) => (short)(a & ~b)));
            }
            else if (numberType == typeof(ushort))
            {
                return (T)Enum.ToObject(type, NumberFunction<ushort>(value, flags, (a, b) => (ushort)(a & ~b)));
            }
            else if (numberType == typeof(uint))
            {
                return (T)Enum.ToObject(type, NumberFunction<uint>(value, flags, (a, b) => a & ~b));
            }
            else if (numberType == typeof(long))
            {
                return (T)Enum.ToObject(type, NumberFunction<long>(value, flags, (a, b) => a & ~b));
            }
            else if (numberType == typeof(ulong))
            {
                return (T)Enum.ToObject(type, NumberFunction<ulong>(value, flags, (a, b) => a & ~b));
            }
            else
            {
                throw new ArgumentException("Unknown enum underlying type " + numberType.Name + ".");
            }
        }

        private static bool BoolFunction<T>(object value, object flags, Func<T, T, bool> op)
            where T : struct
        {
            return op((T)value, (T)flags);
        }

        private static T NumberFunction<T>(object value, object flags, Func<T, T, T> op)
        {
            return op((T)value, (T)flags);
        }

        //public static bool IsSet(this Fruits fruits, Fruits flags)
        //{
        //    return (fruits & flags) == flags;
        //}

        //public static bool IsNotSet(this Fruits fruits, Fruits flags)
        //{
        //    return (fruits & (~flags)) == 0;
        //}

        //public static Fruits Set(this Fruits fruits, Fruits flags)
        //{
        //    return fruits | flags;
        //}

        //public static Fruits Clear(this Fruits fruits, Fruits flags)
        //{
        //    return fruits & (~flags);
        //}
    }
}
