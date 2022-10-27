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
        string sql = @"SELECT
      alb.*,
      COUNT(am.id) AS MemberCount,
      a.*
      FROM sdalbums alb
      JOIN accounts a on a.id = alb.creatorId
      LEFT JOIN sdalbumMembers am ON am.albumId = alb.id
      GROUP BY alb.id;";
        return _db.Query<Album, Profile, Album>(sql, (album, profile) =>
        {
            album.Creator = profile;
            return album;
        }).ToList();
    }

    internal Album GetById(int albumId)
    {
        string sql = @"SELECT 
        alb.*,
        a.*
       FROM sdalbums alb
       JOIN accounts a ON a.id = alb.creatorId
       WHERE alb.id = @albumId;";
        return _db.Query<Album, Profile, Album>(sql, (album, profile) =>
        {
            album.Creator = profile;
            return album;
        }, new { albumId }).FirstOrDefault();

    }

    internal Album CreateAlbum(Album albumData)
    {
        string sql = @"
        INSERT INTO sdalbums
        (title, coverImg, creatorId, category)
        VALUES(@Title, @CoverImg, @CreatorId, @Category);
        SELECT LAST_INSERT_ID();";
        int id = _db.ExecuteScalar<int>(sql, albumData);
        albumData.Id = id;
        return albumData;
    }

    internal void ArchiveAlbum(Album foundAlbum)
    {
        string sql = @"
       UPDATE sdalbums 
       SET
       archived = @Archived
       WHERE id = @Id;";
        var rowsAffected = _db.Execute(sql, foundAlbum);
        if (rowsAffected == 0)
        {
            throw new Exception("Unable to archive album");
        }
    }
}