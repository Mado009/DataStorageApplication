using System.ComponentModel.DataAnnotations;

namespace DataStorageAPI.Model {
    public class LoginRequest {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
