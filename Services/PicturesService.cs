namespace PostIt_Ref.Services;

public class PicturesService
{
    private readonly PicturesRepository _pr;

    public PicturesService(PicturesRepository pr)
    {
        _pr = pr;
    }

    internal Picture CreatePicture(Picture pictureData)
    {
        return _pr.CreatePicture(pictureData);
    }

    internal List<Picture> GetPicturesByAlbum(int albumId)
    {
        return _pr.GetPicturesByAlbum(albumId);
    }
}