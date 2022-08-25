namespace ELifeRPG.Domain.Companies;

public readonly struct CompanyId : IComparable<CompanyId>, IEquatable<CompanyId>
{
    public Guid Value { get; }

    public CompanyId(Guid value)
    {
        Value = value;
    }

    public static CompanyId New() => new(Guid.NewGuid());

    public bool Equals(CompanyId other) => this.Value.Equals(other.Value);

    public int CompareTo(CompanyId other) => Value.CompareTo(other.Value);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }
        
        return obj is CompanyId other && Equals(other);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();

    public static bool operator ==(CompanyId a, CompanyId b) => a.CompareTo(b) == 0;

    public static bool operator !=(CompanyId a, CompanyId b) => !(a == b);
}
