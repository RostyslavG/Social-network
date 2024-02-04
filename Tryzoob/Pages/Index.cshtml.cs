using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tryzoob.Data;
using Tryzoob.Models;

namespace Tryzoob.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TryzoobContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<IndexModel> _logger;

        public List<Author> AuthorsList { get; set; }
        [BindProperty]
        public Publication Publication { get; set; }
        [BindProperty]
        public IFormFile DataUrl { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Publication> Publications { get; set; }

        public IndexModel(ILogger<IndexModel> logger, TryzoobContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            _env = env;
        }

        public void OnGet()
        {
            var currentUserId = HttpContext.Session.GetInt32("LoggedId");
            var selectedId = HttpContext.Session.GetInt32("SelectedUserId");
            if (currentUserId != null)
            {
                if (selectedId == null)
                    Publications = _context.Publication.OrderByDescending(p => p.CreatedAt).Where(p => p.AuthorId == currentUserId).ToList();
                else
                    Publications = _context.Publication.OrderByDescending(p => p.CreatedAt).Where(p => p.AuthorId == selectedId).ToList();
            }
            else
                Publications = _context.Publication.OrderByDescending(p => p.CreatedAt).ToList();
            AuthorsList = _context.Author.ToList();
        }

        public async Task<IActionResult> OnPostPublicationAsync()
        {
            var currentUserId = HttpContext.Session.GetInt32("LoggedId");
            if (currentUserId != null)
                Publication.AuthorId = currentUserId.Value;
            Publication.CreatedAt = DateTime.Now;
            Publication.DisLikes = 0;
            Publication.Likes = 0;
            if (DataUrl != null)
            {
                string path = Path.Combine(this._env.WebRootPath, "images", DataUrl.FileName);
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    DataUrl.CopyTo(fs);
                }
                Publication.DataUrl = "/images/" + DataUrl.FileName;
            }
            _context.Publication.Add(Publication);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public FileResult OnGetDownloadFile(string fileName)
        {
            string path = Path.Combine(this._env.WebRootPath, "images", fileName);
            byte[] data = System.IO.File.ReadAllBytes(path);
            return File(data, "application/octet-stream", fileName);
        }

        public IActionResult OnGetSelect(int authorid)
        {
            //HttpContext.Session.SetInt32("LoggedId", authorid);
            HttpContext.Session.SetInt32("SelectedUserId", authorid);
            return RedirectToPage();    
        }

        public JsonResult OnGetLikes(int pubid)
        {
            var currentUserId = HttpContext.Session.GetInt32("LoggedId");
            Publication pub = _context.Publication.Find(pubid);
            if (currentUserId == null)
                return new JsonResult("Залогінся !!!");
            Like existingLike = _context.Like.FirstOrDefault(l => l.PublicationId == pubid && l.AuthorId == currentUserId);

            if (existingLike != null)
            {
                _context.Like.Remove(existingLike);
                pub.Likes -= 1;
            }
            else
            {
                Like newLike = new Like()
                {
                    AuthorId = currentUserId.Value,
                    PublicationId = pubid,
                    CreatedAt = DateTime.Now
                };
                _context.Like.Add(newLike);
                pub.Likes += 1;
            }
            _context.SaveChanges();
            return new JsonResult(pub.Likes);
        }

        public JsonResult OnGetDisLikes(int pubid)
        {
            var currentUserId = HttpContext.Session.GetInt32("LoggedId");
            Publication pub = _context.Publication.Find(pubid);
            if (currentUserId == null)
                return new JsonResult("Залогінся !!!");
            DizLike existingDislike = _context.DizLike.FirstOrDefault(l => l.PublicationId == pubid && l.AuthorId == currentUserId);

            if (existingDislike != null)
            {
                _context.DizLike.Remove(existingDislike);
                pub.DisLikes -= 1;
            }
            else
            {
                DizLike newDislike = new DizLike()
                {
                    AuthorId = currentUserId.Value,
                    PublicationId = pubid,
                    CreatedAt = DateTime.Now
                };
                _context.DizLike.Add(newDislike);
                pub.DisLikes += 1;
            }
            _context.SaveChanges();
            return new JsonResult(pub.DisLikes);
        }
    }
}