namespace PostIt_Ref.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollaboratorsController : ControllerBase
{
    private readonly Auth0Provider _auth0provider;
    private readonly CollaboratorsService _cs;

    public CollaboratorsController(Auth0Provider auth0provider, CollaboratorsService cs)
    {
        _auth0provider = auth0provider;
        _cs = cs;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<AlbumMember>> CreateCollab([FromBody] AlbumMember collabData)
    {
        try
        {
            Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
            collabData.AccountId = userInfo.Id;
            AlbumMember newCollab = _cs.CreateCollab(collabData);
            return Ok(newCollab);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}