using PostIt_Ref.Interfaces;

namespace PostIt_Ref.Models;

public class Album : IRepoItem<int>, ICreated
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
    public string Title { get; set; }
    public string CoverImg { get; set; }
    public bool Archived { get; set; }
    public string Category { get; set; }
    public int MemberCount { get; set; }
}

public class MyAlbum : Album
{
    public int CollabId { get; set; }
    public string AccountId { get; set; }
}