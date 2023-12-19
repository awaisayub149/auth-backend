using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

// <summary>
// Service responsible for generating and validating JWT tokens.
public class TokenService
{
    // <summary>
    // Initializes a new instance of the <see cref="TokenService"/> class.
    private readonly string _secretKey;
    public TokenService(string secretKey)
    {
        _secretKey = secretKey;
    }

    // <summary>
    // Generates a JWT token based on the provided email.
    // <param name="email">The email for which the token is generated.</param>
    // <returns>Generated JWT token.</returns>
    public string GenerateToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddHours(1), // Set token expiration time
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    // <summary>
    // Validates a JWT token and returns the claims principal if successful.
    // <param name="token">The JWT token to validate.</param>
    // <returns>Claims principal if token validation is successful; otherwise, returns null.</returns>
    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return principal;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
