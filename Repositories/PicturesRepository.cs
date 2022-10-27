namespace PostIt_Ref.Repositories;

public class PicturesRepository
{
    private readonly IDbConnection _db;

    public PicturesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Picture CreatePicture(Picture pictureData)
    {
        string sql = @"
        INSERT INTO sdpictures(imgUrl, albumId, creatorId)
        VALUES(@ImgUrl, @AlbumId, @CreatorId);
        SELECT LAST_INSERT_ID();";
        int id = _db.ExecuteScalar<int>(sql, pictureData);
        pictureData.Id = id;
        return pictureData;
    }

    internal List<Picture> GetPicturesByAlbum(int albumId)
    {
        string sql = @"
        SELECT
        p.*,
        a.*
       FROM sdpictures p
       JOIN accounts a ON a.id = p.creatorId
       WHERE p.albumId = @albumId;";
        return _db.Query<Picture, Profile, Picture>(sql, (picture, profile) =>
        {
            picture.Creator = profile;
            return picture;
        }, new { albumId }).ToList();
    }
}