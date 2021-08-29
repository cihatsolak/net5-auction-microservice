using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ordering.Application.Queries;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using Ordering.Application.Responses;
using System.Collections.Generic;
using System.Net;
using Ordering.Application.Commands.OrderCreate;

namespace ESourcing.Order.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;
        #endregion

        #region Ctor
        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        #endregion

        #region Methods
        [HttpGet("{userName:minlength(1)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrdersByUsername(string userName)
        {
            GetOrdersBySellerUsernameQuery getOrdersBySellerUsernameQuery = new(userName);
            var orderResponseList = await _mediator.Send(getOrdersBySellerUsernameQuery);
            if (orderResponseList is null || !orderResponseList.Any())
            {
                _logger.LogWarning($"No order found for {userName}.");
                return NotFound();
            }

            return Ok(orderResponseList);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> OrderCreate([FromBody] OrderCreateCommand orderCreateCommand)
        {
            var orderResponse = await _mediator.Send(orderCreateCommand);
            return Ok(orderResponse);
        }
        #endregion
    }
}
