using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ESourcing.Sourcing.Controllers
{
    public class AuctionController : BaseController
    {
        #region Fields
        private readonly IAuctionRepository _auctionRepository;
        private readonly ILogger<AuctionController> _auctionLogger;
        #endregion

        #region Ctor
        public AuctionController(
            IAuctionRepository auctionRepository,
            ILogger<AuctionController> auctionLogger)
        {
            _auctionRepository = auctionRepository;
            _auctionLogger = auctionLogger;
        }
        #endregion

        #region Methods
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Auction>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAuctions()
        {
            var auctions = await _auctionRepository.GetAuctionsAsync();
            if (auctions is null || !auctions.Any())
            {
                return NotFound();
            }

            return Ok(auctions);
        }

        [HttpGet("{id:minlength(24)}")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAuctionById(string id)
        {
            var auction = await _auctionRepository.GetAuctionByIdAsync(id);
            if (auction is null)
            {
                _auctionLogger.LogError($"Auction with id: {id}, hasn't been found in database.");
                return NotFound();
            }

            return Ok(auction);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAuction([FromBody] Auction auction)
        {
            await _auctionRepository.InsertAsync(auction);
            return CreatedAtRoute(nameof(GetAuctionById), new { id = auction.Id }, auction);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateAuction([FromBody] Auction auction)
        {
            bool isSuccess = await _auctionRepository.UpdateAsync(auction);
            if (!isSuccess)
            {
                return Problem();
            }

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<Auction>> DeleteAuctionById(string id)
        {
            bool isSuccess = await _auctionRepository.DeleteAsync(id);
            if (!isSuccess)
            {
                return Problem();
            }

            return NoContent();
        }
        #endregion
    }
}
