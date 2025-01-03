
using finanzauto_Back.DTOs;
using finanzauto_Back.Helpers;
using finanzauto_Back.Models;
using finanzauto_Back.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace finanzauto_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        public readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;
        public readonly ApplicationDbContext _dbContext;
        public readonly IJwtBearerConfiguration _JwtBearerConfigurationService;

        public AuthController(IConfiguration config, ILogger<AuthController> logger, ApplicationDbContext dbContext,
            IJwtBearerConfiguration JwtBearerConfigurationService)
        {
            _config = config;
            _logger = logger;
            _dbContext = dbContext;
            _JwtBearerConfigurationService = JwtBearerConfigurationService;

        }

        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion(DefaultLogin request)
        {
            DefaultResponse response = new();

            try
            {
                _logger.LogInformation($"AuthController.IniciarSesion : {JsonSerializer.Serialize(request)}");

                AuthService authService = new(_config);

                var loggedUser = await authService.IniciarSesionAsync(request);

                if (loggedUser != null)
                {
                    var token = _JwtBearerConfigurationService.CreateToken(loggedUser);

                    response = new()
                    {
                        Data = new DefaultLoginResponse()
                        {
                            token = token
                        },
                        Message = "Consultado correctamente",
                        StatusCode = 200
                    };
                }

                _logger.LogInformation($"AuthController.IniciarSesion : Exito");

                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                if (ex.Message.ToString().Contains("Error Validacion - "))
                {
                    response = new()
                    {
                        Data = "[]",
                        Message = ex.Message.ToString().Replace("Error Validacion - ", ""),
                        StatusCode = 409
                    };

                    return StatusCode(StatusCodes.Status409Conflict, response);
                }

                response = new()
                {
                    Data = "[]",
                    Message = "Error general del sistema",
                    StatusCode = 500
                };

                return StatusCode(StatusCodes.Status500InternalServerError, response);

            }
        }
    }

}
