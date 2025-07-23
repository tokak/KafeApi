
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KafeApi.Application.Helpers;
// TokenHelpers sınıfı, JWT (JSON Web Token) üretmek için kullanılır
public class TokenHelpers
{
    private readonly IConfiguration _configuration;

    // Yapıcı metod: Konfigürasyon ayarlarını (appsettings.json'dan) okumamızı sağlar
    public TokenHelpers(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Bu metod, verilen e-posta bilgisine göre bir JWT token üretir
    public string GenerateToken(string email)
    {
        // Token'ı imzalamak için kullanılan anahtar (appsettings.json dosyasındaki Jwt:Key değerini alıyoruz)
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        // İmzalama algoritması olarak HmacSha256 kullanılıyor (güvenlik için gerekli)
        var creadentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Token içinde yer alacak kullanıcı bilgileri (claim'ler) tanımlanıyor
        var claims = new List<Claim> {
            // Kullanıcının e-posta adresi
            new Claim("email", email),

            // Kullanıcının rolü (örnek olarak "admin" verdik, sabit yazılmış)
            new Claim("role", "admin"),

            // Token'a benzersiz bir ID atanıyor (güvenlik amacıyla)
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // JWT token oluşturuluyor
        var token = new JwtSecurityToken
        (
            issuer: _configuration["Jwt:Issuer"],           // Token'ı kim oluşturdu? (örn: bizim API)
            audience: _configuration["Jwt:Auidience"],      // Token'ı kimler kullanabilir?
            claims: claims,                                 // Token içine eklenen bilgiler (e-mail, rol, vb.)
            expires: DateTime.Now.AddMinutes(30),           // Token ne kadar süre geçerli? (30 dakika)
            signingCredentials: creadentials                // Hangi anahtar ve algoritma ile imzalandı?
        );

        // Token nesnesini string (yani düz yazı) haline getirip döndürüyoruz
        var resulttoken = new JwtSecurityTokenHandler().WriteToken(token);
        return resulttoken;
    }
}
