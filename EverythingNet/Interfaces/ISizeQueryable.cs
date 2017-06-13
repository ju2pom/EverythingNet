using EverythingNet.Query;

namespace EverythingNet.Interfaces
{
  public interface ISizeQueryable : IQueryable
  {
    IQueryable Equal(int value);

    IQueryable Equal(int value, SizeUnit u);

    IQueryable Equal(Sizes size);

    IQueryable GreaterThan(int value);

    IQueryable GreaterThan(int value, SizeUnit u);

    IQueryable GreaterOrEqualThan(int value);

    IQueryable GreaterOrEqualThan(int value, SizeUnit u);

    IQueryable LessThan(int value);

    IQueryable LessThan(int value, SizeUnit u);

    IQueryable LessOrEqualThan(int value);

    IQueryable LessOrEqualThan(int value, SizeUnit u);

    IQueryable Between(int min, int max);

    IQueryable Between(int min, int max, SizeUnit u);

    IQueryable Between(int min, SizeUnit umin, int max, SizeUnit umax);
  }
}