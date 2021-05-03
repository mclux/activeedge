using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveEdge.Infrastructure.DTO
{
    class AuthDto
    {
    }

    public class LoginRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
