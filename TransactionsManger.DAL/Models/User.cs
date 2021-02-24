using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransactionsManager.DAL.Models
{
    public class User
    {
        [Required(ErrorMessage = "Login is required")]
        [MinLength(6)]
        [StringLength(35)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6)]
        [PasswordPropertyText]
        [StringLength(35)]
        public string Password { get; set; }
    }
}
