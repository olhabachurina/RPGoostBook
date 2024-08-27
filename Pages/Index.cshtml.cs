using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RPGoostBook.Models;
using RPGoostBook.Repository;

namespace RPGoostBook.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(IRepository<Message> messageRepository, UserManager<IdentityUser> userManager)
        {
            _messageRepository = messageRepository;
            _userManager = userManager;
        }

        [BindProperty]
        public string Content { get; set; }

        public IEnumerable<Message> Messages { get; private set; }

        public async Task OnGetAsync()
        {
            Messages = await _messageRepository.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Content))
            {
                var user = await _userManager.GetUserAsync(User);
                var message = new Message
                {
                    UserName = user.UserName,
                    Content = Content,
                    DatePosted = DateTime.Now
                };

                await _messageRepository.AddAsync(message);
                return RedirectToPage();
            }

            return Page();
        }
    }
}