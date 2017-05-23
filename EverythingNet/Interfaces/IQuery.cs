using System.Linq;

namespace EverythingNet.Interfaces
{
  public interface IQuery
  {
    IQueryable Name(string namePattern);

    IQueryable Size(string sizePattern);
  }
}
