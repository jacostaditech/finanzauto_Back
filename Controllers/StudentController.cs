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
    public class StudentController : Controller
    {
        public readonly IConfiguration _config;
        private readonly ILogger<StudentController> _logger;
        public readonly ApplicationDbContext _dbContext;
        public readonly IJwtBearerConfiguration _JwtBearerConfigurationService;

        public StudentController(
            IConfiguration config,
            ILogger<StudentController> logger,
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
        public async Task<IActionResult> CreateStudent(CreateStudentRequestModel request)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"StudentController.CreateStudent : {JsonSerializer.Serialize(request)}");
                StudentsService studentsService = new(_config, _dbContext);

                var headers = Request.Headers;
                string JWT = (headers["Authorization"]).ToString().Replace("Bearer ", "");
                if (string.IsNullOrWhiteSpace(JWT))
                    return StatusCode(StatusCodes.Status400BadRequest);

                var IdCreated = await studentsService.CreateStudentAsync(request, JWT);

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
        public async Task<IActionResult> GetStudents()
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"StudentController.GetStudents : ");
                StudentsService studentsService = new(_config, _dbContext);

                var data = await studentsService.GetStudentsAsync();

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
        [HttpGet("GetStudentByIdentification")]
        public async Task<IActionResult> GetStudentByIdentification(long Identification)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"StudentController.GetStudentByIdentification : {Identification}");
                StudentsService studentsService = new(_config, _dbContext);

                var data = await studentsService.GetStudentByIdentificationAsync(Identification);

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
        public async Task<IActionResult> UpdateStudent(UpdateStudentRequestModel request)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"StudentController.UpdateStudent : {JsonSerializer.Serialize(request)}");
                StudentsService studentsService = new(_config, _dbContext);

                var headers = Request.Headers;
                string JWT = (headers["Authorization"]).ToString().Replace("Bearer ", "");
                if (string.IsNullOrWhiteSpace(JWT))
                    return StatusCode(StatusCodes.Status400BadRequest);

                await studentsService.UpdateStudentAsync(request, JWT);

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
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"StudentController.DeleteStudent : {Id}");
                StudentsService studentsService = new(_config, _dbContext);

                var headers = Request.Headers;
                string JWT = (headers["Authorization"]).ToString().Replace("Bearer ", "");
                if (string.IsNullOrWhiteSpace(JWT))
                    return StatusCode(StatusCodes.Status400BadRequest);

                var IdCreated = await studentsService.DeleteStudentAsync(Id);

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
