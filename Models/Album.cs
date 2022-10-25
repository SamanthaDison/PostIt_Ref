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
    public int MyProperty { get; set; }
}