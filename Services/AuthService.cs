using finanzauto_Back.Helpers;
using finanzauto_Back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace finanzauto_Back.Services
{
    public class AuthService
    {
        public readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<UserLogged> IniciarSesionAsync(DefaultLogin request)
        {
            try
            {

                if (request.UserName == "test-finanzauto" && request.Password == "DOKA@@HW78FKJANWD789$3")
                {
                    return new UserLogged()
                    {
                        Id = 1,
                        Nombres = "TEST",
                        Email = "test@finanzauto.com",
                    };
                }
                else
                {
                    throw new Exception("Error Validacion - Credenciales Incorrectas");
                }

            }
            catch
            {
                throw;
            }
        }
    }
}
