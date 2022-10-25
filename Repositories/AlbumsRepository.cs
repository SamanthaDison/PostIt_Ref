namespace PostIt_Ref.Repositories;

public class AlbumsRepository
{
    private readonly IDbConnection _db;

    public AlbumsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal List<Album> GetAllAlbums()
    {
        throw new NotImplementedException();
    }
}