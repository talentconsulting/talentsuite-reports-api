using System.Diagnostics.CodeAnalysis;

namespace TalentConsulting.TalentSuite.Reports.Common;

[ExcludeFromCodeCoverage]
[Serializable]
public abstract class ValueObjectBase
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public int CompareTo(object? obj)
    {
        Type thisType = GetUnproxiedType(this);
        Type otherType = GetUnproxiedType(obj!);

        if (thisType != otherType)
            return string.Compare(thisType.ToString(), otherType.ToString(), StringComparison.Ordinal);

        var other = (ValueObject?)obj;

        object[] components = GetEqualityComponents().ToArray();
        object[]? otherComponents = other?.GetEqualityComponents().ToArray();
        ArgumentNullException.ThrowIfNull(otherComponents);

        for (int i = 0; i < components.Length; i++)
        {
            int comparison = CompareComponents(components[i], otherComponents[i]);
            if (comparison != 0)
                return comparison;
        }

        return 0;
    }

    protected static Type GetUnproxiedType(object obj)
    {
        const string EFCoreProxyPrefix = "Castle.Proxies.";
        const string NHibernateProxyPostfix = "Proxy";

        Type type = obj.GetType();
        string typeString = type.ToString();

        if (typeString.Contains(EFCoreProxyPrefix) || typeString.EndsWith(NHibernateProxyPostfix))
            return type.BaseType!;

        return type;
    }

    private int CompareComponents(object? object1, object? object2)
    {
        if (object1 is null && object2 is null)
            return 0;

        if (object1 is null)
            return -1;

        if (object2 is null)
            return 1;

        if (object1 is IComparable comparable1 && object2 is IComparable comparable2)
            return comparable1.CompareTo(comparable2);

        return object1.Equals(object2) ? 0 : -1;
    }
}