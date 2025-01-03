using finanzauto_Back.DTOs;
using finanzauto_Back.Helpers;
using finanzauto_Back.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace finanzauto_Back.Services
{
    public class CoursesService
    {
        public readonly IConfiguration _config;
        public readonly ApplicationDbContext _dbContext;
        public CoursesService(IConfiguration config, ApplicationDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;

        }

        public async Task<int> CreateCourseAsync(CreateCourseRequestModel request, string JWT)
        {
            try
            {
                var today = Utilities.GetTodayCOP();
                var idFromJWT = Utilities.GetIdFromJWT(JWT);

                Course toCreate = request.CopyToDTO();
                toCreate.Creationdate = today;
                toCreate.Lastupdate = today;
                toCreate.Createby = idFromJWT;
                toCreate.Updatedby = idFromJWT;

                var Created = (await _dbContext.Courses.AddAsync(toCreate)).Entity;

                await _dbContext.SaveChangesAsync();

                return Created.Id;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            try
            {
                var data = await _dbContext.Courses
                    .ToListAsync();


                if (data == null)
                    throw new Exception("UserFault - Cursos no encontrados");

                return data;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Course> GetCourseByNameAsync(string Name)
        {
            try
            {
                var data = await _dbContext.Courses
                    .FirstOrDefaultAsync(x => x.Name.ToUpper().Trim() == Name.ToUpper().Trim());

                if (data == null)
                    throw new Exception("UserFault - Curso no encontrado");

                return data;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateCourseAsync(UpdateCourseRequestModel request, string JWT)
        {
            try
            {
                var data = await _dbContext.Courses.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (data == null)
                    throw new Exception("UserFault - Curso no encontrado");

                var today = Utilities.GetTodayCOP();
                var idFromJWT = Utilities.GetIdFromJWT(JWT);

                data.Lastupdate = today;
                data.Updatedby = idFromJWT;
                data.Name = request.Name;
                data.Description = request.Description;

                _dbContext.Courses.Update(data);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteCoursesAsync(int Id)
        {
            try
            {
                var data = await _dbContext.Courses
                    .Include(x => x.TeacherCourses)
                    .ThenInclude(x => x.Teacher)
                    .Include(x => x.Scores)
                    .ThenInclude(x => x.Student)
                    .FirstOrDefaultAsync(x => x.Id == Id);

                if (data == null)
                    throw new Exception("UserFault - Curso no encontrado");

                if (data.TeacherCourses != null)
                    _dbContext.TeacherCourses.RemoveRange(data.TeacherCourses);

                if (data.Scores != null)
                    _dbContext.Scores.RemoveRange(data.Scores);

                _dbContext.Courses.Remove(data);

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
