using finanzauto_Back.DTOs;
using finanzauto_Back.Helpers;
using finanzauto_Back.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace finanzauto_Back.Services
{
    public class TeachersService
    {
        public readonly IConfiguration _config;
        public readonly ApplicationDbContext _dbContext;
        public TeachersService(IConfiguration config, ApplicationDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;

        }
        //Funcion para crear un profesor
        public async Task<int> CreateTeacherAsync(CreateTeacherRequestModel request, string JWT)
        {
            try
            {
                var today = Utilities.GetTodayCOP();
                var idFromJWT = Utilities.GetIdFromJWT(JWT);

                Teacher toCreate = request.CopyToDTO();
                toCreate.Creationdate = today;
                toCreate.Lastupdate = today;
                toCreate.Createby = idFromJWT;
                toCreate.Updatedby = idFromJWT;

                var Created = (await _dbContext.Teachers.AddAsync(toCreate)).Entity;

                await _dbContext.SaveChangesAsync();

                return Created.Id;
            }
            catch
            {
                throw;
            }
        }

        //Funcion para asignar un curso a un profesor
        public async Task<int> AsignTeachToCourseAsync(AsignTeacherCourse request, string JWT)
        {
            try
            {
                var today = Utilities.GetTodayCOP();
                var idFromJWT = Utilities.GetIdFromJWT(JWT);

                TeacherCourse toCreate = new()
                {
                    CourseId = request.CourseId,
                    Createby = idFromJWT,
                    Creationdate = today,
                    Lastupdate = today,
                    TeacherId = request.TeacherId,
                    Updatedby = idFromJWT
                };

                var Created = (await _dbContext.TeacherCourses.AddAsync(toCreate)).Entity;

                await _dbContext.SaveChangesAsync();

                return Created.Id;
            }
            catch
            {
                throw;
            }
        }


        public async Task<List<Teacher>> GetTeachersAsync()
        {
            try
            {
                var data = await _dbContext.Teachers
                    .Include(x => x.TeacherCourses)
                    .ThenInclude(x => x.Course)
                    .ToListAsync();


                if (data == null)
                    throw new Exception("UserFault - Profesores no encontrados");

                return data;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Teacher> GetTeacherByFirstNameAsync(string firstName)
        {
            try
            {
                var data = await _dbContext.Teachers
                    .Include(x => x.TeacherCourses)
                    .ThenInclude(x => x.Course)
                    .FirstOrDefaultAsync(x => x.FirstName.ToUpper().Trim() == firstName.ToUpper().Trim());
                  
                if (data == null)
                    throw new Exception("UserFault - Profesores no encontrados");

                return data;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateTeacherAsync(UpdateTeacherRequestModel request, string JWT)
        {
            try
            {
                var data = await _dbContext.Teachers.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (data == null)
                    throw new Exception("UserFault - Profesor no encontrado");

                var today = Utilities.GetTodayCOP();
                var idFromJWT = Utilities.GetIdFromJWT(JWT);

                data.Lastupdate = today;
                data.Updatedby = idFromJWT;
                data.FirstName = request.FirstName;
                data.LastName = request.LastName;

                _dbContext.Teachers.Update(data);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteTeacherAsync(int Id)
        {
            try
            {
                var data = await _dbContext.Teachers
                    .Include(x => x.TeacherCourses)
                    .FirstOrDefaultAsync(x => x.Id == Id);

                if (data == null)
                    throw new Exception("UserFault - Profesor no encontrado");

                if (data.TeacherCourses != null)
                    _dbContext.TeacherCourses.RemoveRange(data.TeacherCourses);

                _dbContext.Teachers.Remove(data);

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
