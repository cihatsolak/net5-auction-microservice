using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ESourcing.Sourcing.Controllers
{
    public class BidController : BaseController
    {
        #region Fields
        private readonly IBidRepository _bidRepository;
        #endregion

        #region Ctor
        public BidController(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }
        #endregion

        #region Methods
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> SendBid([FromBody] Bid bid)
        {
            await _bidRepository.SendBidAsync(bid);
            return Ok();
        }

        [HttpGet("{id:minlength(24)}")]
        [ProducesResponseType(typeof(IEnumerable<Bid>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBidsByAuctionId(string id)
        {
            var bids = await _bidRepository.GetBidsByAuctionIdAsync(id);
            if (bids is null || !bids.Any())
            {
                return NotFound();
            }

            return Ok(bids);
        }

        [HttpGet("{id:minlength(24)}")]
        [ProducesResponseType(typeof(Bid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Bid>> GetWinnerBid(string id)
        {
            Bid bid = await _bidRepository.GetWinnerBidAsync(id);
            if (bid is null)
            {
                return NotFound();
            }

            return Ok(bid);
        }
        #endregion
    }
}
