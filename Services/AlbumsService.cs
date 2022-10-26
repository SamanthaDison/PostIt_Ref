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
        //    REVIEW if archived.. need to filter out
        return _ar.GetAllAlbums();

    }

    internal Album GetById(int albumId)
    {
        // REVIEW if archived.... need to check if userinfo is creator
        Album foundAlbum = _ar.GetById(albumId);
        if (foundAlbum == null)
        {
            throw new Exception("Unable to find album");
        }
        return foundAlbum;
    }

    internal Album CreateAlbum(Album albumData)
    {
        return _ar.CreateAlbum(albumData);
    }

    internal void ArchiveAlbum(int albumId, string userId)
    {
        Album foundAlbum = GetById(albumId);
        if (foundAlbum.CreatorId != userId)
        {
            throw new Exception("Unauthorized to archive");
        }
        foundAlbum.Archived = true;
        _ar.ArchiveAlbum(foundAlbum);
    }
}