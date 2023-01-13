namespace TalentConsulting.TalentSuite.Reports.Common;

/// <summary>
/// See: https://enterprisecraftsmanship.com/posts/value-object-better-implementation/
/// </summary>
[Serializable]
public abstract class ValueObject : ValueObjectBase, IComparable, IComparable<ValueObject>
{
    private int? _cachedHashCode;

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
        if (!_cachedHashCode.HasValue)
        {
            _cachedHashCode = GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        return _cachedHashCode.Value;
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
