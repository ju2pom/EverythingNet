namespace EverythingNet.Query
{
  using System;
  using System.Collections.Generic;

  using EverythingNet.Core;
  using EverythingNet.Interfaces;

  public enum SizeUnit
  {
    Kb = 1,
    Mb,
    Gb
  }

  public enum Sizes
  {
    Empty,
    Unknown,
    Tiny,
    Small,
    Medium,
    Large,
    Huge,
    Gigantic
  }

  internal class SizeQueryable : Queryable, ISizeQueryable
  {
    private ISizeRef sizeRef;

    public SizeQueryable(IQueryGenerator parent)
      : base(parent)
    {
      this.Flags = RequestFlags.EVERYTHING_REQUEST_SIZE;
      this.IsFast = EverythingWrapper.Everything_IsFileInfoIndexed(EverythingWrapper.FileInfoIndex.FileSize)
                    && EverythingWrapper.Everything_IsFileInfoIndexed(EverythingWrapper.FileInfoIndex.FolderSize);
    }

    public IQuery Equal(int value)
    {
      return this.Equal(value, SizeUnit.Kb);
    }

    public IQuery Equal(int value, SizeUnit u)
    {
      this.sizeRef = new SizeComparison(value, string.Empty, u);

      return new Query(this);
    }

    public IQuery Equal(Sizes s)
    {
      this.sizeRef = new DefaultSize(s);

      return new Query(this);
    }

    public IQuery GreaterThan(int value)
    {
      return this.GreaterThan(value, SizeUnit.Kb);
    }

    public IQuery GreaterThan(int value, SizeUnit u)
    {
      return this.Comparison(value, ">", u);
    }

    public IQuery GreaterOrEqualThan(int value)
    {
      return this.GreaterOrEqualThan(value, SizeUnit.Kb);
    }

    public IQuery GreaterOrEqualThan(int value, SizeUnit unit)
    {
      return this.Comparison(value, ">=", unit);
    }

    public IQuery LessThan(int value)
    {
      return this.LessThan(value, SizeUnit.Kb);
    }

    public IQuery LessThan(int value, SizeUnit u)
    {
      return this.Comparison(value, "<", u);
    }

    public IQuery LessOrEqualThan(int value)
    {
      return this.LessOrEqualThan(value, SizeUnit.Kb);
    }

    public IQuery LessOrEqualThan(int value, SizeUnit unit)
    {
      return this.Comparison(value, "<=", unit);
    }

    public IQuery Between(int min, int max)
    {
      return this.Between(min, max, SizeUnit.Kb);
    }

    public IQuery Between(int min, int max, SizeUnit u)
    {
      return this.Between(min, u, max, u);
    }

    public IQuery Between(int min, SizeUnit u1, int max, SizeUnit u2)
    {
      if (min * Math.Pow(10, (int)u1) > max * Math.Pow(10, (int)u2))
      {
        throw new InvalidOperationException($"Minimum value must be lower or equal to max value: min={min}{u1} max={max}{u2}");
      }

      this.sizeRef = new BetweenSize(min, u1, max, u2);

      return new Query(this);
    }

    public override IEnumerable<string> GetQueryParts()
    {
      foreach (var queryPart in base.GetQueryParts())
      {
        yield return queryPart;
      }

      yield return this.sizeRef.ToString();
    }

    private IQuery Comparison(int value, string comparison, SizeUnit unit)
    {
      this.sizeRef = new SizeComparison(value, comparison, unit);

      return new Query(this);
    }

    private interface ISizeRef
    {
    }

    private class DefaultSize : ISizeRef
    {
      private readonly Sizes sizes;

      public DefaultSize(Sizes sizes)
      {
        this.sizes = sizes;
      }

      public override string ToString()
      {
        return $"size:{this.sizes.ToString().ToLower()}";
      }
    }

    private class SizeComparison : ISizeRef
    {
      private readonly int size;
      private readonly string comparison;
      private readonly SizeUnit unit;

      public SizeComparison(int size, string comparison, SizeUnit u = SizeUnit.Kb)
      {
        this.size = size;
        this.comparison = comparison;
        this.unit = u;
      }

      public override string ToString()
      {
        return $"size:{this.comparison}{this.size}{this.unit.ToString().ToLower()}";
      }
    }

    private class BetweenSize : ISizeRef
    {
      private readonly int minimum;
      private readonly SizeUnit minimumUnit;
      private readonly int maximum;
      private readonly SizeUnit maximumUnit;

      public BetweenSize(int min, SizeUnit minUnit, int max, SizeUnit maxUnit)
      {
        this.minimum = min;
        this.minimumUnit = minUnit;
        this.maximum = max;
        this.maximumUnit = maxUnit;
      }

      public override string ToString()
      {
        return $"size:{this.minimum}{this.minimumUnit}-{this.maximum}{this.maximumUnit}";
      }
    }
  }
}