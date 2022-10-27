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

    internal List<MyAlbum> GetMyAlbums(string userId)
    {
        return _cr.GetMyAlbums(userId);
    }

    internal void RemoveCollab(int collabId, string userId)
    {
        AlbumMember foundCollab = _cr.GetById(collabId);
        if (foundCollab == null)
        {
            throw new Exception("Album Member not found");
        }
        // TODO get album and check if user is album creator for checking auth
        if (foundCollab.AccountId != userId)
        {
            throw new Exception("Unauthorized to remove collab");
        }
        _cr.RemoveCollab(collabId);
    }
}