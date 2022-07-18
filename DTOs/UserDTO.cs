using System.ComponentModel.DataAnnotations;

namespace MovieWebApp.Dtos
{
    public class UserDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string fullname { get; set; }
        public DateTime birthday { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public byte[] passwordSalt;
    }
}
