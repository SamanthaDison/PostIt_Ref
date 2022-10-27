namespace PostIt_Ref.Repositories;

public class CollaboratorsRepository
{
    private readonly IDbConnection _db;

    public CollaboratorsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal AlbumMember CreateCollab(AlbumMember collabData)
    {
        string sql = @"
       INSERT INTO sdalbumMembers(albumId, accountId)
       VALUES(@AlbumId, @AccountId);
       SELECT LAST_INSERT_ID();";
        int id = _db.ExecuteScalar<int>(sql, collabData);
        collabData.Id = id;
        return collabData;
    }


    internal List<Collaborator> GetCollabsByAlbum(int albumId)
    {
        string sql = @"
      SELECT
      a.*,
      am.*
      FROM sdalbumMembers am
      JOIN accounts a ON a.id = am.accountId
      WHERE am.albumId = @albumId;";
        return _db.Query<Collaborator, AlbumMember, Collaborator>(sql, (collab, am) =>
        {
            collab.AlbumMemberId = am.Id;
            collab.AlbumId = am.AlbumId;
            return collab;
        }, new { albumId }).ToList();
    }


    internal List<Album> GetMyAlbums(string userId)
    {
        string sql = @"
        SELECT
        alb.*,
        a.*
        FROM sdalbumMembers am
        JOIN sdalbums alb ON alb.id = am.albumId
        JOIN accounts a ON a.id = alb.creatorId
        WHERE am.accountId = @userId;";
        return _db.Query<Album, Profile, Album>(sql, (album, profile) =>
        {
            album.Creator = profile;
            return album;
        }, new { userId }).ToList();

    }
}