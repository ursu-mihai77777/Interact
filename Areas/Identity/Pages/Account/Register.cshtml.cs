using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ThirdDelivery.Models;
using ThirdDelivery.Services;
using System.ComponentModel.DataAnnotations;

public class RegisterModel : PageModel
{
    private readonly IAuthService _authService;

    public RegisterModel(IAuthService authService)
    {
        _authService = authService;
    }

    [BindProperty, Required, EmailAddress] public string Email { get; set; }
    [BindProperty, Required, DataType(DataType.Password)] public string Password { get; set; }
    [BindProperty, Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!Email.EndsWith("@gmail.com"))
        {
            ModelState.AddModelError(string.Empty, "Only Gmail addresses are allowed.");
            return Page();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = new User
        {
            UserName = Email.Split('@')[0],
            Email = Email,
            EmailConfirmed = true
        };

        var result = await _authService.RegisterAsync(user, Password);
        if (result.Succeeded)
        {
            return RedirectToPage("/Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }
}
