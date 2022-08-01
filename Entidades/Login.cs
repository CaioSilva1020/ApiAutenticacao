using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("Login")]
    public class Login
    {
        [Key]
        public int LoginId { get; set; }

        [Required]
        [StringLength(80)]
        public string? LoginNome { get; set; }

        [Required]
        [StringLength(80)]
        public string? Email { get; set; }

        [StringLength(2000)]
        [Required]
        public string? Senha { get; set; }

        [StringLength(2000)]
        public string? Sal { get; set; }

        [StringLength(2000)]
        public string? ConfirmacaoSenha { get; set; }
        
    }
}