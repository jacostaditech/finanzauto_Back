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
    public class CourseController : Controller
    {
        public readonly IConfiguration _config;
        private readonly ILogger<CourseController> _logger;
        public readonly ApplicationDbContext _dbContext;
        public readonly IJwtBearerConfiguration _JwtBearerConfigurationService;

        public CourseController(
            IConfiguration config,
            ILogger<CourseController> logger,
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
        public async Task<IActionResult> CreateCourse(CreateCourseRequestModel request)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"CourseController.CreateCourse : {JsonSerializer.Serialize(request)}");
                CoursesService coursesService = new(_config, _dbContext);

                var headers = Request.Headers;
                string JWT = (headers["Authorization"]).ToString().Replace("Bearer ", "");
                if (string.IsNullOrWhiteSpace(JWT))
                    return StatusCode(StatusCodes.Status400BadRequest);

                var IdCreated = await coursesService.CreateCourseAsync(request, JWT);

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
        public async Task<IActionResult> GetCourses()
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"CourseController.GetCourses : ");
                CoursesService coursesService = new(_config, _dbContext);

                var data = await coursesService.GetCoursesAsync();

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
        [HttpGet("GetCourseByName")]
        public async Task<IActionResult> GetCourseByName(string Name)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"CourseController.GetCourseByName : {Name}");
                CoursesService coursesService = new(_config, _dbContext);

                var data = await coursesService.GetCourseByNameAsync(Name);

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
        public async Task<IActionResult> UpdateCourse(UpdateCourseRequestModel request)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"CourseController.UpdateCourse : {JsonSerializer.Serialize(request)}");
                CoursesService coursesService = new(_config, _dbContext);

                var headers = Request.Headers;
                string JWT = (headers["Authorization"]).ToString().Replace("Bearer ", "");
                if (string.IsNullOrWhiteSpace(JWT))
                    return StatusCode(StatusCodes.Status400BadRequest);

                await coursesService.UpdateCourseAsync(request, JWT);

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
        public async Task<IActionResult> DeleteCourse(int Id)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"CourseController.DeleteCourse: {Id}");
                CoursesService coursesService = new(_config, _dbContext);

                var headers = Request.Headers;
                string JWT = (headers["Authorization"]).ToString().Replace("Bearer ", "");
                if (string.IsNullOrWhiteSpace(JWT))
                    return StatusCode(StatusCodes.Status400BadRequest);

                var IdCreated = await coursesService.DeleteCoursesAsync(Id);

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
