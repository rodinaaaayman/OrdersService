using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersService.Infrastructure.Data;
using OrdersService.Domain.Models;

namespace OrdersService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvoicesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoices>>> GetInvoices()
        {
            return await _context.Invoices.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Invoices>> GetInvoice(int Id)
        {
            var invoice = await _context.Invoices.FindAsync(Id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteInvoice(int Id)
        {
            var invoice = await _context.Invoices.FindAsync(Id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(int Id)
        {
            return _context.Invoices.Any(e => e.InvoiceId == Id);
        }
    }
}
