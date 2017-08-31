namespace EverythingNet.Interfaces
{
  public enum Bpp
  {
    Bpp1 = 1,
    Bpp8 = 8,
    Bpp16 = 16,
    Bpp24 = 24,
    Bpp32 = 32
  }

  /// <summary>
  ///   Only jpg, png, gif and bmp file are supported with these queries
  /// </summary>
  public interface IImageQueryable
  {
    IQueryable Width(int width);

    IQueryable Height(int height);

    IQueryable Portrait();

    IQueryable Landscape();

    IQueryable BitDepth(Bpp bpp);
  }
}