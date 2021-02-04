using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos;
using api.Errors;
using api.Extensions;
using AutoMapper;
using core.Interfaces;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<UserDto> GetCurrentUser()
        {
            var user = await _userManager.FindByClaimsPrincipleAsync(HttpContext.User);
            UserDto userDto = null;

            if (user != null)
            {
                userDto = new UserDto
                {
                    Token = _tokenService.CreateToken(user),
                    DisplayName = user.DisplayName,
                    Email = user.Email
                };
            }

            return userDto;
        }

        [HttpGet("emailexists")]
        public async Task<bool> CheckIfEmailExists([FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return (user != null);
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<AddressDto> GetUserAddress()
        {
            var user = await _userManager.FindByClaimsPrincipleWithAddressAsync(HttpContext.User);
            AddressDto address = null;

            if (user != null)
            {
                address = _mapper.Map<Address, AddressDto>(user.Address);
            }

            return address;
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<AddressDto> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindByClaimsPrincipleWithAddressAsync(HttpContext.User);
            AddressDto updatedAddress = null;

            if (user != null)
            {
                user.Address = _mapper.Map<AddressDto, Address>(address);

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    updatedAddress = _mapper.Map<Address, AddressDto>(user.Address);
                }
            }

            return updatedAddress;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return Unauthorized(new ApiResponse(401));

            var results = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!results.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
                Email = user.Email
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var inUse = await CheckIfEmailExists(registerDto.Email);

            if (inUse)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = new [] 
                    {
                        "Email address is in use"
                    }
                });
            }

            var user = new AppUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                DisplayName = registerDto.DisplayName,
            };

            var results = await _userManager.CreateAsync(user, registerDto.Password);

            if (!results.Succeeded)
                return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}