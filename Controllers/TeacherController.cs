using finanzauto_Back.DTOs;
using finanzauto_Back.Helpers;
using finanzauto_Back.Models;
using finanzauto_Back.Models.Requests;
using finanzauto_Back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace finanzauto_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : Controller
    {
        public readonly IConfiguration _config;
        private readonly ILogger<TeacherController> _logger;
        public readonly ApplicationDbContext _dbContext;
        public readonly IJwtBearerConfiguration _JwtBearerConfigurationService;

        public TeacherController(
            IConfiguration config,
            ILogger<TeacherController> logger,
            ApplicationDbContext dbContext,
            IJwtBearerConfiguration JwtBearerConfigurationService)
        {
            _config = config;
            _logger = logger;
            _dbContext = dbContext;
            _JwtBearerConfigurationService = JwtBearerConfigurationService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateTeacherRequestModel request)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"TeacherController.CreateTeacher : {JsonSerializer.Serialize(request)}");
                TeachersService teachersService = new(_config, _dbContext);

                var headers = Request.Headers;
                string JWT = (headers["Authorization"]).ToString().Replace("Bearer ", "");
                if (string.IsNullOrWhiteSpace(JWT))
                    return StatusCode(StatusCodes.Status400BadRequest);

                var IdCreated = await teachersService.CreateTeacherAsync(request, JWT);

                response = new DefaultResponse()
                {
                    Data = IdCreated,
                    Message = "Creado satisfactoriamente",
                    StatusCode = 200
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                if (ex.ToString().Contains("UserFault - "))
                {
                    response = new DefaultResponse()
                    {
                        Data = "[]",
                        Message = $"{ex.Message.ToString().Replace("UserFault - ", "")}",
                        StatusCode = 500
                    };
                }
                else
                {
                    response = new DefaultResponse()
                    {
                        Data = "[]",
                        Message = "Error general del sistema",
                        StatusCode = 500
                    };
                }


                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [Authorize]
        [HttpPost("AsignTeachToCourse")]
        public async Task<IActionResult> AsignTeachToCourse(AsignTeacherCourse request)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"TeacherController.AsignTeachToCourse : {JsonSerializer.Serialize(request)}");
                TeachersService teachersService = new(_config, _dbContext);

                var headers = Request.Headers;
                string JWT = (headers["Authorization"]).ToString().Replace("Bearer ", "");
                if (string.IsNullOrWhiteSpace(JWT))
                    return StatusCode(StatusCodes.Status400BadRequest);

                var IdCreated = await teachersService.AsignTeachToCourseAsync(request, JWT);

                response = new DefaultResponse()
                {
                    Data = IdCreated,
                    Message = "Creado satisfactoriamente",
                    StatusCode = 200
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                if (ex.ToString().Contains("UserFault - "))
                {
                    response = new DefaultResponse()
                    {
                        Data = "[]",
                        Message = $"{ex.Message.ToString().Replace("UserFault - ", "")}",
                        StatusCode = 500
                    };
                }
                else
                {
                    response = new DefaultResponse()
                    {
                        Data = "[]",
                        Message = "Error general del sistema",
                        StatusCode = 500
                    };
                }


                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"TeacherController.GetTeachers : ");
                TeachersService teachersService = new(_config, _dbContext);

                var data = await teachersService.GetTeachersAsync();

                response = new DefaultResponse()
                {
                    Data = data,
                    Message = "Consultado satisfactoriamente",
                    StatusCode = 200
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                if (ex.ToString().Contains("UserFault - "))
                {
                    response = new DefaultResponse()
                    {
                        Data = "[]",
                        Message = $"{ex.Message.ToString().Replace("UserFault - ", "")}",
                        StatusCode = 500
                    };
                }
                else
                {
                    response = new DefaultResponse()
                    {
                        Data = "[]",
                        Message = "Error general del sistema",
                        StatusCode = 500
                    };
                }


                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [Authorize]
        [HttpGet("GetTeacherByFirstName")]
        public async Task<IActionResult> GetTeacherByFirstName(string FirstName)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"TeacherController.GetTeacherByFirstName : {FirstName}");
                TeachersService teachersService = new(_config, _dbContext);

                var data = await teachersService.GetTeacherByFirstNameAsync(FirstName);

                response = new DefaultResponse()
                {
                    Data = data,
                    Message = "Consultado satisfactoriamente",
                    StatusCode = 200
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                if (ex.ToString().Contains("UserFault - "))
                {
                    response = new DefaultResponse()
                    {
                        Data = "[]",
                        Message = $"{ex.Message.ToString().Replace("UserFault - ", "")}",
                        StatusCode = 500
                    };
                }
                else
                {
                    response = new DefaultResponse()
                    {
                        Data = "[]",
                        Message = "Error general del sistema",
                        StatusCode = 500
                    };
                }


                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherRequestModel request)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"TeacherController.UpdateTeacher : {JsonSerializer.Serialize(request)}");
                TeachersService teachersService = new(_config, _dbContext);

                var headers = Request.Headers;
                string JWT = (headers["Authorization"]).ToString().Replace("Bearer ", "");
                if (string.IsNullOrWhiteSpace(JWT))
                    return StatusCode(StatusCodes.Status400BadRequest);

                await teachersService.UpdateTeacherAsync(request, JWT);

                response = new DefaultResponse()
                {
                    Data = "[]",
                    Message = "Actualizado satisfactoriamente",
                    StatusCode = 200
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                if (ex.ToString().Contains("UserFault - "))
                {
                    response = new DefaultResponse()
                    {
                        Data = "[]",
                        Message = $"{ex.Message.ToString().Replace("UserFault - ", "")}",
                        StatusCode = 500
                    };
                }
                else
                {
                    response = new DefaultResponse()
                    {
                        Data = "[]",
                        Message = "Error general del sistema",
                        StatusCode = 500
                    };
                }


                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteTeacher(int Id)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"TeacherController.DeleteTeacher: {Id}");
                TeachersService teachersService = new(_config, _dbContext);

                var headers = Request.Headers;
                string JWT = (headers["Authorization"]).ToString().Replace("Bearer ", "");
                if (string.IsNullOrWhiteSpace(JWT))
                    return StatusCode(StatusCodes.Status400BadRequest);

                var IdCreated = await teachersService.DeleteTeacherAsync(Id);

                response = new DefaultResponse()
                {
                    Data = IdCreated,
                    Message = "Eliminado satisfactoriamente",
                    StatusCode = 200
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                if (ex.ToString().Contains("UserFault - "))
                {
                    response = new DefaultResponse()
                    {
                        Data = "[]",
                        Message = $"{ex.Message.ToString().Replace("UserFault - ", "")}",
                        StatusCode = 500
                    };
                }
                else
                {
                    response = new DefaultResponse()
                    {
                        Data = "[]",
                        Message = "Error general del sistema",
                        StatusCode = 500
                    };
                }


                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
