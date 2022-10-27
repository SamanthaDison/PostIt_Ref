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

    internal AlbumMember GetById(int collabId)
    {
        string sql = @"
       SELECT
       *
       FROM sdalbumMembers
       WHERE id = @collabId";
        return _db.QueryFirstOrDefault<AlbumMember>(sql, new { collabId });
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

    internal void RemoveCollab(int collabId)
    {
        string sql = @"
        DELETE FROM sdalbumMembers
        WHERE id = @collabId
        LIMIT 1;";
        _db.Execute(sql, new { collabId });
    }

    internal List<MyAlbum> GetMyAlbums(string userId)
    {
        string sql = @"
        SELECT
        alb.*,
        am.id AS collabId,
       
        a.*
        FROM sdalbumMembers am
        JOIN sdalbums alb ON alb.id = am.albumId
        JOIN accounts a ON a.id = alb.creatorId

        WHERE am.accountId = @userId;";
        return _db.Query<MyAlbum, Profile, MyAlbum>(sql, (album, profile) =>
        {
            album.Creator = profile;
            album.AccountId = profile.Id;
            return album;
        }, new { userId }).ToList();

    }
}