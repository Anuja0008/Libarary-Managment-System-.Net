using Microsoft.AspNetCore.Mvc;
using YourProject.Models;
using YourProject.Repositories;

[Route("api/[controller]")]
[ApiController]
public class MemberController : ControllerBase 
{
    private readonly MemberRepository _memberRepository;

    public MemberController(MemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Member member)
    {
        // Validate the member data (automatic due to [Required] data annotations)
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Register the member and hash the password
        var registeredMember = await _memberRepository.RegisterMember(member, member.Password);

        // Return the registered member data (or any other appropriate response)
        return CreatedAtAction(nameof(Register), new { id = registeredMember.Id }, registeredMember);
    }
}
