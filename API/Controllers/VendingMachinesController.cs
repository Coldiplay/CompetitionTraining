using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DB;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VendingMachinesController : ControllerBase
    {
        private readonly CompetitionContext _context;

        public VendingMachinesController(CompetitionContext context)
        {
            _context = context;
        }

        // GET: api/VendingMachines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendingMachine>>> GetVendingMachines()
        {
            return await _context.VendingMachines
                //.Include(e => e.CreatorCompany)
                //.Include(e => e.CriticalThresholdTemplate)
                //.Include(e => e.Engineer)
                //.Include(e => e.Maintenances)
                //.Include(e => e.Manager)
                //.Include(e => e.NotificationTemplate)
                //.Include(e => e.Operator)
                //.Include(e => e.Products)
                //.Include(e => e.Sales)
                //.Include(e => e.ServicePriority)
                //.Include(e => e.Status)
                //.Include(e => e.Technician)
                //.Include(e => e.User)
                //.Include(e => e.WorkMode)
                //.Include(e => e.PaymentTypes)
                .ToListAsync();
        }

        // GET: api/VendingMachines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VendingMachine>> GetVendingMachine(string id)
        {
            var vendingMachine = await _context.VendingMachines.FindAsync(id);

            if (vendingMachine == null)
            {
                return NotFound();
            }

            return vendingMachine;
        }

        // PUT: api/VendingMachines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendingMachine(string id, VendingMachine vendingMachine)
        {
            if (id != vendingMachine.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendingMachine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendingMachineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VendingMachines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VendingMachine>> PostVendingMachine(VendingMachine vendingMachine)
        {
            _context.VendingMachines.Add(vendingMachine);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VendingMachineExists(vendingMachine.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVendingMachine", new { id = vendingMachine.Id }, vendingMachine);
        }

        // DELETE: api/VendingMachines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendingMachine(string id)
        {
            var vendingMachine = await _context.VendingMachines.FindAsync(id);
            if (vendingMachine == null)
            {
                return NotFound();
            }

            _context.VendingMachines.Remove(vendingMachine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendingMachineExists(string id)
        {
            return _context.VendingMachines.Any(e => e.Id == id);
        }
    }
}
