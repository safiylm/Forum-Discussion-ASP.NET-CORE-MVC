using Forum_descussion_ASP.NET_core_mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum_descussion_ASP.NET_core_mvc.Controllers
{
    public class ListUserController : Controller
    {

        private readonly ForumContext _context;

        public ListUserController(ForumContext context)
        {
            _context = context;
        }


        // GET: UserModels
        public async Task<IActionResult> Index()
        {
            return _context.UserModel != null ?
                        View(await _context.UserModel.ToListAsync()) :
                        Problem("Entity set 'ForumContext.UserModel'  is null.");
        }
    }
}
