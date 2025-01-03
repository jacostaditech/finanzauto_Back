using System.IdentityModel.Tokens.Jwt;

namespace finanzauto_Back.Helpers
{
    public static class Utilities
    {
        public static DateTime GetTodayCOP()
        {
            try
            {
                string timeZoneId = "SA Pacific Standard Time"; //Colombia

                DateTime localTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));

                return localTime;
            }
            catch
            {
                throw;
            }
        }

        public static int GetIdFromJWT(string JWT)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();

                if (handler.CanReadToken(JWT))
                {
                    var jwtToken = handler.ReadJwtToken(JWT);

                    var claims = jwtToken.Claims;

                    var userIdClaim = claims.FirstOrDefault(c => c.Type == "sid")?.Value;

                    return Convert.ToInt32(userIdClaim);
                }

                return 0;
            }
            catch
            {
                throw;
            }
        }

    }
}
