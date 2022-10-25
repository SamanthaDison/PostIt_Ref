namespace PostIt_Ref.Services;

public class AlbumsService
{
    private readonly AlbumsRepository _ar;

    public AlbumsService(AlbumsRepository ar)
    {
        _ar = ar;
    }

    internal List<Album> GetAllAlbums()
    {
        return _ar.GetAllAlbums();
    }
}