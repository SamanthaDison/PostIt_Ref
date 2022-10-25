namespace PostIt_Ref.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly Auth0Provider _auth0provider;
    private readonly AlbumsService _as;

    public AlbumsController(Auth0Provider auth0provider, AlbumsService @as)
    {
        _auth0provider = auth0provider;
        _as = @as;
    }

    [HttpGet]
    public ActionResult<List<Album>> GetAllAbums()
    {
        try
        {
            List<Album> albums = _as.GetAllAlbums();
            return Ok(albums);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}