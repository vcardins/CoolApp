using System.ComponentModel.DataAnnotations;

namespace CoolApp.Models
{
    public class ContacUsViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Assunto")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Mensagem")]
        public string Message { get; set; }
    }
}