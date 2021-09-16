using ESourcing.UI.Core.Common;
using ESourcing.UI.Core.ResultModels;
using ESourcing.UI.Models.Bids;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ESourcing.UI.Clients
{
    public class BidClient
    {
        #region Fields
        private readonly HttpClient _httpClient;
        #endregion

        #region Ctor
        public BidClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri($"{ServicesConstants.LocalSourcingBaseAddress}Bid/");
        }
        #endregion

        #region Methods
        public async Task<Result<List<BidViewModel>>> GetBidsByAuctionId(string auctionId)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"GetBidsByAuctionId/{auctionId}");
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return new Result<List<BidViewModel>>(false, ResultConstants.RecordNotFound);
            }

            var responseMessage = await httpResponseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<BidViewModel>>(responseMessage);
            if (result is null)
                return new Result<List<BidViewModel>>(false, ResultConstants.RecordNotFound);

            return new Result<List<BidViewModel>>(true, ResultConstants.RecordFound, result);
        }

        public async Task<Result<string>> SendBidAsync(BidViewModel bidViewModel)
        {
            StringContent content = new(JsonConvert.SerializeObject(bidViewModel));
            content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

            var response = await _httpClient.PostAsync("SendBid", content);
            if (!response.IsSuccessStatusCode)
            {
                return new Result<string>(false, ResultConstants.CreateNotSuccessfully);
            }

            var result = await response.Content.ReadAsStringAsync();
            return new Result<string>(true, ResultConstants.CreateSuccessfully, result);           
        }
        #endregion
    }
}
