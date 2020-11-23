using AutoMapper;
using DatingApp.api.Data;
using DatingApp.api.Dto;
using DatingApp.api.Helpers;
using DatingApp.api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        public IConfiguration _config { get; }

        public AuthController(IAuthRepository authRepository, IConfiguration config, IMapper mapper)
        {
            _authRepository = authRepository;
            _config = config;
            _mapper = mapper;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Post(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if (await _authRepository.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists");

            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);
            var userToReturn = _mapper.Map<UserForDetailedDTO>(createdUser);
            return CreatedAtRoute("GetUser", new { controller = "User", id = createdUser.Id }, userToReturn);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);
            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userToReturn = _mapper.Map<UserForListDTO>(userFromRepo);

            return Ok(
                new
                {
                    token = tokenHandler.WriteToken(token),
                    user = userToReturn
                });




        }
    }
}
