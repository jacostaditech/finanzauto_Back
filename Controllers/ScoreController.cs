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
    public class ScoreController : Controller
    {
        public readonly IConfiguration _config;
        private readonly ILogger<ScoreController> _logger;
        public readonly ApplicationDbContext _dbContext;
        public readonly IJwtBearerConfiguration _JwtBearerConfigurationService;

        public ScoreController(
            IConfiguration config,
            ILogger<ScoreController> logger,
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
        public async Task<IActionResult> CreateScore(CreateScoreRequestModel request)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"ScoreController.CreateScore : {JsonSerializer.Serialize(request)}");
                ScoresService scoresService = new(_config, _dbContext);

                var headers = Request.Headers;
                string JWT = (headers["Authorization"]).ToString().Replace("Bearer ", "");
                if (string.IsNullOrWhiteSpace(JWT))
                    return StatusCode(StatusCodes.Status400BadRequest);

                var IdCreated = await scoresService.CreateScoreAsync(request, JWT);

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
        public async Task<IActionResult> GetScores()
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"ScoreController.GetScores : ");
                ScoresService scoresService = new(_config, _dbContext);

                var data = await scoresService.GetScoresAsync();

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
        [HttpGet("GetScoreByStudentId")]
        public async Task<IActionResult> GetScoreByStudentId(int studentId)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"ScoreController.GetScoreByStudentId : {studentId}");
                ScoresService scoresService = new(_config, _dbContext);

                var data = await scoresService.GetScoreByStudentIdAsync(studentId);

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
        [HttpGet("GetScoreByCourseId")]
        public async Task<IActionResult> GetScoreByCourseId(int courseId)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"ScoreController.GetScoreByCourseId : {courseId}");
                ScoresService scoresService = new(_config, _dbContext);

                var data = await scoresService.GetScoreByCourseIdAsync(courseId);

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
        public async Task<IActionResult> UpdateScore(UpdateScoreRequestModel request)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"ScoreController.UpdateScore : {JsonSerializer.Serialize(request)}");
                ScoresService scoresService = new(_config, _dbContext);

                var headers = Request.Headers;
                string JWT = (headers["Authorization"]).ToString().Replace("Bearer ", "");
                if (string.IsNullOrWhiteSpace(JWT))
                    return StatusCode(StatusCodes.Status400BadRequest);

                await scoresService.UpdateScoreAsync(request, JWT);

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
        public async Task<IActionResult> DeleteScore(int Id)
        {
            DefaultResponse response = new DefaultResponse();

            try
            {
                _logger.LogInformation($"ScoreController.DeleteScore: {Id}");
                ScoresService scoresService = new(_config, _dbContext);

                var headers = Request.Headers;
                string JWT = (headers["Authorization"]).ToString().Replace("Bearer ", "");
                if (string.IsNullOrWhiteSpace(JWT))
                    return StatusCode(StatusCodes.Status400BadRequest);

                var IdCreated = await scoresService.DeleteScoreAsync(Id);

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
