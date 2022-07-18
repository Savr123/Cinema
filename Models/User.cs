using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWebApp.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string fullname { get; set; }
        public DateTime birthday { get; set; }
        public string email { get; set; }
        public byte[] password { get; set; }
        public byte[] passwordSalt { get; set; }

    }
}