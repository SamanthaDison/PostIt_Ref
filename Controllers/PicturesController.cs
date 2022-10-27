namespace PostIt_Ref.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PicturesController : ControllerBase
{
    private readonly PicturesService _ps;
    private readonly Auth0Provider _auth0provider;

    public PicturesController(PicturesService ps, Auth0Provider auth0Provider)
    {
        _ps = ps;
        _auth0provider = auth0Provider;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Picture>> CreatePicture([FromBody] Picture pictureData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            pictureData.CreatorId = userInfo.Id;
            Picture newPicture = _ps.CreatePicture(pictureData);
            newPicture.Creator = userInfo;
            return Ok(newPicture);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}