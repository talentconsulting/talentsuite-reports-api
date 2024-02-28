using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TalentConsulting.TalentSuite.Reports.Common
{
    /// <summary>
    /// See: https://enterprisecraftsmanship.com/posts/value-object-better-implementation/
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class ValueObject : ValueObjectBase, IComparable, IComparable<ValueObject>
    {
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (GetUnproxiedType(this) != GetUnproxiedType(obj))
                return false;

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(obj => obj?.GetHashCode() ?? 0)
                .Aggregate(17, (current, hash) => current * 23 + hash);
        }

        public int CompareTo(ValueObject? other)
        {
            return CompareTo(other as object);
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }

        public static bool operator <(ValueObject left, ValueObject right)
        {
            return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
        }

        public static bool operator <=(ValueObject left, ValueObject right)
        {
            return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
        }

        public static bool operator >(ValueObject left, ValueObject right)
        {
            return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
        }

        public static bool operator >=(ValueObject left, ValueObject right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
        }
    }
}
