using PostIt_Ref.Interfaces;

namespace PostIt_Ref.Models;

public class Profile : IRepoItem<string>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
