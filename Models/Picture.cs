using PostIt_Ref.Interfaces;

namespace PostIt_Ref.Models;

public class Picture : IRepoItem<int>, ICreated
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
    public string ImgUrl { get; set; }
    public int AlbumId { get; set; }
}