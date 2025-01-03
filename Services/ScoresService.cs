using finanzauto_Back.DTOs;
using finanzauto_Back.Helpers;
using finanzauto_Back.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace finanzauto_Back.Services
{
    public class ScoresService
    {
        public readonly IConfiguration _config;
        public readonly ApplicationDbContext _dbContext;
        public ScoresService(IConfiguration config, ApplicationDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;

        }

        public async Task<int> CreateScoreAsync(CreateScoreRequestModel request, string JWT)
        {
            try
            {
                var today = Utilities.GetTodayCOP();
                var idFromJWT = Utilities.GetIdFromJWT(JWT);

                Score toCreate = request.CopyToDTO();
                toCreate.Creationdate = today;
                toCreate.Lastupdate = today;
                toCreate.Createby = idFromJWT;
                toCreate.Updatedby = idFromJWT;

                var Created = (await _dbContext.Scores.AddAsync(toCreate)).Entity;

                await _dbContext.SaveChangesAsync();

                return Created.Id;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Score>> GetScoresAsync()
        {
            try
            {
                var data = await _dbContext.Scores
                    .Include(x => x.Course)
                    .ThenInclude(x => x.TeacherCourses)
                        .ThenInclude(x => x.Teacher)
                    .Include(x => x.Student)
                    .ToListAsync();

                data.ForEach(x => x.Course.Scores = new List<Score>());
                data.ForEach(x => x.Student.Scores = new List<Score>());

                if (data == null)
                    throw new Exception("UserFault - Puntuaciones no encontradas");

                return data;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Score>> GetScoreByStudentIdAsync(int studentId)
        {
            try
            {
                var data = await _dbContext.Scores
                    .Include(x => x.Course)
                    .ThenInclude(x => x.TeacherCourses)
                        .ThenInclude(x => x.Teacher)
                    .Include(x => x.Student)
                    .Where(x => x.StudentId == studentId)
                    .ToListAsync();

                data.ForEach(x => x.Course.Scores = new List<Score>());
                data.ForEach(x => x.Student.Scores = new List<Score>());

                if (data == null)
                    throw new Exception("UserFault - Puntuaciones no encontradas");

                return data;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Score>> GetScoreByCourseIdAsync(int courseId)
        {
            try
            {
                var data = await _dbContext.Scores
                    .Include(x => x.Course)
                    .ThenInclude(x => x.TeacherCourses)
                        .ThenInclude(x => x.Teacher)
                    .Include(x => x.Student)
                    .Where(x => x.CourseId == courseId)
                    .ToListAsync();

                data.ForEach(x => x.Course.Scores = new List<Score>());
                data.ForEach(x => x.Student.Scores = new List<Score>());

                if (data == null)
                    throw new Exception("UserFault - Puntuaciones no encontradas");

                return data;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateScoreAsync(UpdateScoreRequestModel request, string JWT)
        {
            try
            {
                var data = await _dbContext.Scores.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (data == null)
                    throw new Exception("UserFault - Puntuacion no encontrada");

                var today = Utilities.GetTodayCOP();
                var idFromJWT = Utilities.GetIdFromJWT(JWT);

                data.Lastupdate = today;
                data.Updatedby = idFromJWT;
                data.StudentId = request.StudentId;
                data.CourseId = request.CourseId;
                data.Score1 = request.Score1;

                _dbContext.Scores.Update(data);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteScoreAsync(int Id)
        {
            try
            {
                var data = await _dbContext.Scores
                    .FirstOrDefaultAsync(x => x.Id == Id);

                if (data == null)
                    throw new Exception("UserFault - Puntuacion no encontrada");


                _dbContext.Scores.Remove(data);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
