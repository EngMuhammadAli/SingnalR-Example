using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SingnalR_Example.Models;
using SingnalR_Example.NewFolder;
using SingnalR_Example.SingnalRHub;

namespace SingnalR_Example.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<ItemHub> _hubContext;

        public ItemsController(ApplicationDbContext context, IHubContext<ItemHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View(_context.Items.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveItem", item);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("UpdateItem", item);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("DeleteItem", item.Id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
