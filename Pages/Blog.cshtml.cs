using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TwinMIME.Data;
using TwinMIME.Models;

namespace TwinMIME.Pages
{
    public class BlogModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            Comments = new List<BlogComment>(); 
        }

        public List<BlogComment> Comments { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

        public void OnGet()
        {
            LoadArticleData();
        }

        private void LoadArticleData()
        {
            Comments = _context.BlogComments
                .Where(c => c.ArticleId == 1)
                .OrderByDescending(c => c.CreatedDate)
                .ToList();

            LikeCount = _context.ArticleReactions.Count(r => r.ArticleId == 1 && r.IsLike);
            DislikeCount = _context.ArticleReactions.Count(r => r.ArticleId == 1 && !r.IsLike);
        }

        public IActionResult OnPostAddComment([Required] string commentText)
        {
            if (!ModelState.IsValid)
            {
                LoadArticleData(); 
                return Page();
            }

            var user = _userManager.GetUserAsync(User).Result;
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var comment = new BlogComment
            {
                Author = user.UserName,
                Text = commentText,
                CreatedDate = DateTime.Now,
                ArticleId = 1
            };

            _context.BlogComments.Add(comment);
            _context.SaveChanges();

            return RedirectToPage();
        }

        public JsonResult OnPostRateArticle([FromBody] ReactionRequest request)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return new JsonResult(new { error = "Необходимо войти в систему" });
            }

            var existingReaction = _context.ArticleReactions
                .FirstOrDefault(r => r.ArticleId == 1 && r.UserId == userId);

            if (existingReaction != null)
            {
                if (existingReaction.IsLike == request.IsLike)
                {
                    _context.ArticleReactions.Remove(existingReaction);
                }
                else
                {
                    existingReaction.IsLike = request.IsLike;
                    _context.Update(existingReaction);
                }
            }
            else
            {
                var newReaction = new ArticleReaction
                {
                    UserId = userId,
                    ArticleId = 1,
                    IsLike = request.IsLike,
                    CreatedDate = DateTime.Now
                };
                _context.ArticleReactions.Add(newReaction);
            }

            _context.SaveChanges();

            var likeCount = _context.ArticleReactions.Count(r => r.ArticleId == 1 && r.IsLike);
            var dislikeCount = _context.ArticleReactions.Count(r => r.ArticleId == 1 && !r.IsLike);

            return new JsonResult(new { likeCount, dislikeCount });
        }

        public class ReactionRequest
        {
            public bool IsLike { get; set; }
        }
    }
}