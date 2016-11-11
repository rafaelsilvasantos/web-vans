using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebVansSite.Helpers.Validator;

namespace WebVansSite.Models.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O Email não é valido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Tipo")]
        public int TipoPessoa { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "CPF")]
        [CustomValidationCPF(ErrorMessage = "O CPF é inválido.")]
        public string CPF { get; set; }

        [Display(Name = "CNPJ")]
        [CustomValidationCNPJ(ErrorMessage = "O CNPJ é inválido.")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TipoPessoa == 1 && string.IsNullOrEmpty(CPF))
            {
                yield return new ValidationResult("O CPF é obrigatório.");
            }

            if (TipoPessoa == 2 && string.IsNullOrEmpty(CNPJ))
            {
                yield return new ValidationResult("O CNPJ é obrigatório.");
            }
        }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar de mim?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O Email não é valido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name="Tipo")]
        public int TipoPessoa { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "CPF")]
        [CustomValidationCPF(ErrorMessage="O CPF é inválido.")]
        public string CPF { get; set; }

        [Display(Name = "CNPJ")]
        [CustomValidationCNPJ(ErrorMessage = "O CNPJ é inválido.")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A {0} deve conter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare("Password", ErrorMessage = "A Senha e a Confirmação de senha não estão iguais.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TipoPessoa == 1 && string.IsNullOrEmpty(CPF))
            {
                yield return new ValidationResult("O CPF é obrigatório.");
            }

            if (TipoPessoa == 2 && string.IsNullOrEmpty(CNPJ))
            {
                yield return new ValidationResult("O CNPJ é obrigatório.");
            }
        }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O Email não é valido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A {0} deve conter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare("Password", ErrorMessage = "A Senha e a Confirmação de senha não estão iguais.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
