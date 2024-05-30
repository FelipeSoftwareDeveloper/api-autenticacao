using System.ComponentModel.DataAnnotations;

namespace Blank.Application.Dtos;

public class EsqueciMinhaSenhaDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

}
