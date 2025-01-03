using finanzauto_Back.Models;

namespace finanzauto_Back.Helpers
{
    public interface IJwtBearerConfiguration
    {
        public string CreateToken(UserLogged user);
    }
}
