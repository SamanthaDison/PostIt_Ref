namespace PostIt_Ref.Services;

public class CollaboratorsService
{
    private readonly CollaboratorsRepository _cr;

    public CollaboratorsService(CollaboratorsRepository cr)
    {
        _cr = cr;
    }

    internal AlbumMember CreateCollab(AlbumMember collabData)
    {
        return _cr.CreateCollab(collabData);
    }

    internal List<Collaborator> GetCollabsByAlbum(int albumId)
    {
        return _cr.GetCollabsByAlbum(albumId);
    }

    internal List<Album> GetMyAlbums(string userId)
    {
        return _cr.GetMyAlbums(userId);
    }
}