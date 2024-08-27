using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPGoostBook.Models;
using RPGoostBook.Repository;
using System.ComponentModel.DataAnnotations;

namespace RPGoostBook.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "������� ��� ������������")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "������� ������")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "�������� ����� ��� ������.");
                }
            }

            // ��������� ����� ������������
            await _signInManager.SignOutAsync();

            // �������������� ������������ �� ������� ��������
            return RedirectToPage("/Index");
        }
    }
}