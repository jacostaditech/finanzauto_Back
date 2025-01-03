using finanzauto_Back.DTOs;
using finanzauto_Back.Helpers;
using finanzauto_Back.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace finanzauto_Back.Services
{
    public class StudentsService
    {
        public readonly IConfiguration _config;
        public readonly ApplicationDbContext _dbContext;
        public StudentsService(IConfiguration config, ApplicationDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;

        }

        public async Task<int> CreateStudentAsync(CreateStudentRequestModel request, string JWT)
        {
            try
            {
                var today = Utilities.GetTodayCOP();
                var idFromJWT = Utilities.GetIdFromJWT(JWT);

                if (request.Gender != "M" && request.Gender != "F")
                    throw new Exception("UserFault - El género tiene que ser M o F");

                Student toCreate = request.CopyToDTO();
                toCreate.Creationdate = today;
                toCreate.Lastupdate = today;
                toCreate.Createby = idFromJWT;
                toCreate.Updatedby = idFromJWT;

                var Created = (await _dbContext.Students.AddAsync(toCreate)).Entity;

                await _dbContext.SaveChangesAsync();

                return Created.Id;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            try
            {
                var data = await _dbContext.Students
                    .Include(x => x.Scores)
                    .ThenInclude(x => x.Course)
                    .ThenInclude(x => x.TeacherCourses)
                    .ThenInclude(x => x.Teacher)
                    .ToListAsync();

                if (data == null)
                    throw new Exception("UserFault - Estudiante no encontrado");

                return data;
            }
            catch
            {
                throw;
            }
        }


        public async Task<Student> GetStudentByIdentificationAsync(long Identification)
        {
            try
            {
                var data = await _dbContext.Students
                    .Include(x => x.Scores)
                    .ThenInclude(x => x.Course)
                    .ThenInclude(x => x.TeacherCourses)
                    .ThenInclude(x => x.Teacher)
                    .FirstOrDefaultAsync(x => x.Identification == Identification);

                if (data == null)
                    throw new Exception("UserFault - Estudiante no encontrado");

                return data;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateStudentAsync(UpdateStudentRequestModel request, string JWT)
        {
            try
            {
                var data = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (data == null)
                    throw new Exception("UserFault - Estudiante no encontrado");

                var today = Utilities.GetTodayCOP();
                var idFromJWT = Utilities.GetIdFromJWT(JWT);

                if (!string.IsNullOrWhiteSpace(request.Gender))
                {
                    if (request.Gender != "M" && request.Gender != "F")
                        throw new Exception("UserFault - El género tiene que ser M o F");
                }
                else
                {
                    throw new Exception("UserFault - El género tiene que ser M o F");
                }

                data.Lastupdate = today;
                data.Updatedby = idFromJWT;
                data.Identification = request.Identification;
                data.FirstName = request.FirstName;
                data.LastName = request.LastName;
                data.Gender = request.Gender;

                _dbContext.Students.Update(data);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteStudentAsync(int Id)
        {
            try
            {
                var data = await _dbContext.Students
                    .Include(x => x.Scores)
                    .FirstOrDefaultAsync(x => x.Id == Id);

                if (data == null)
                    throw new Exception("UserFault - Estudiante no encontrado");

                if (data.Scores != null)
                    _dbContext.Scores.RemoveRange(data.Scores);

                _dbContext.Students.Remove(data);

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
