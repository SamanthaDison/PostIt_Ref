// break out the difference between data models and view models

namespace PostIt_Ref.Models;

public class Collaborator : Profile
{
    public int AlbumMemberId { get; set; }
    public int AlbumId { get; set; }

}