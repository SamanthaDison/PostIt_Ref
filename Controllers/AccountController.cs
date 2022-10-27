namespace PostIt_Ref.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly Auth0Provider _auth0Provider;
    private readonly CollaboratorsService _cs;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider, CollaboratorsService cs)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _cs = cs;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_accountService.GetOrCreateProfile(userInfo));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("collaborators")]
    [Authorize]
    public async Task<ActionResult<List<MyAlbum>>> GetMyAlbums()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<MyAlbum> myAlbums = _cs.GetMyAlbums(userInfo.Id);
            return Ok(myAlbums);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
