namespace EverythingNet.Interfaces
{
  using EverythingNet.Query;

  public interface ISizeQueryable : IQueryable
  {
    IQuery Equal(int value);

    IQuery Equal(int value, SizeUnit u);

    IQuery Equal(Sizes size);

    IQuery GreaterThan(int value);

    IQuery GreaterThan(int value, SizeUnit u);

    IQuery GreaterOrEqualThan(int value);

    IQuery GreaterOrEqualThan(int value, SizeUnit u);

    IQuery LessThan(int value);

    IQuery LessThan(int value, SizeUnit u);

    IQuery LessOrEqualThan(int value);

    IQuery LessOrEqualThan(int value, SizeUnit u);

    IQuery Between(int min, int max);

    IQuery Between(int min, int max, SizeUnit u);

    IQuery Between(int min, SizeUnit umin, int max, SizeUnit umax);
  }
}