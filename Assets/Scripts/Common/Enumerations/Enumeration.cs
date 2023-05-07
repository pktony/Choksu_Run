using System;

public class Enumeration : IComparable
{
    public string Name { get; protected set; }

    public int ID { get; protected set; }

    protected Enumeration(int id, string name)
    {
        ID = id;
        Name = name;
    }

    public override bool Equals(object obj)
    {
        var compareValue = (Enumeration)obj;

        if (compareValue == null) return false;

        return compareValue.GetType().Equals(this.GetType()) && compareValue.ID == this.ID;
    }

    public int CompareTo(object obj) => ID.CompareTo(((Enumeration)obj).ID);


    public override int GetHashCode()
    {
        return "Enumeration".GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
