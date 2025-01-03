namespace finanzauto_Back.Models
{
   
    public class DefaultResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
    }

    public class DefaultLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLogged
    {
        public int Id {  get; set; }
        public string Nombres { get; set; }
        public string Email { get; set; }
    }

    public class DefaultLoginResponse
    {
        public string token { get; set; }
    }


}
