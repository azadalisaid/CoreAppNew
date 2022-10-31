using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreApp.Models;

namespace CoreApp.Controllers
{
    [Route("api/PaymentDetail")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly PaymentDetailContext _context;
       
        public PaymentDetailController(PaymentDetailContext context)
        {
            _context = context;
            
        }

        // GET: api/PaymentDetail
        [Route("GetPaymentDetails")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetailList>>> GetPaymentDetails(string Name)
        {
            if (Name != null && Name != string.Empty)
            {
                return await (from c in _context.PaymentDetails
                              join d in _context.CardTypes on c.CardTypeId equals d.CardTypeId
                              where c.CardOwnerName.Contains(Name)
                              select new PaymentDetailList
                              {
                                  PaymentDetailId = c.PaymentDetailId,
                                  CardOwnerName = c.CardOwnerName,
                                  CardNumber = c.CardNumber,
                                  SecurityCode = c.SecurityCode,
                                  ExpirationDate = c.ExpirationDate,
                                  CardTypeId = c.CardTypeId,
                                  CardTypeName = d.CardTypeName
                              }).ToListAsync();
            }
            else
            {
                return await (from c in _context.PaymentDetails
                              join d in _context.CardTypes on c.CardTypeId equals d.CardTypeId
                              select new PaymentDetailList
                              {
                                  PaymentDetailId = c.PaymentDetailId,
                                  CardOwnerName = c.CardOwnerName,
                                  CardNumber = c.CardNumber,
                                  SecurityCode = c.SecurityCode,
                                  ExpirationDate = c.ExpirationDate,
                                  CardTypeId = c.CardTypeId,
                                  CardTypeName = d.CardTypeName
                              }).ToListAsync();
            }
        }

        [Route("GetCardTypes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardType>>> GetCardTypes()
        {
            return await _context.CardTypes.ToListAsync();
        }

        // GET: api/PaymentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);

            if (paymentDetail == null)
            {
                return NotFound();
            }

            return paymentDetail;
        }

        // PUT: api/PaymentDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("UpdatePayment")]
        [HttpPost]
        public async Task<IActionResult> PutPaymentDetail(PaymentDetail paymentDetail)
        {
            //if (paymentDetail.PaymentDetailId)
            //{
            //    return BadRequest();
            //}
            _context.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(paymentDetail.PaymentDetailId))
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

        // POST: api/PaymentDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.PaymentDetailId }, paymentDetail);
        }

        // DELETE: api/PaymentDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            _context.PaymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentDetailExists(int id)
        {
            return _context.PaymentDetails.Any(e => e.PaymentDetailId == id);
        }
    }
}
