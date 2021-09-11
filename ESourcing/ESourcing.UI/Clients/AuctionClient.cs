using ESourcing.UI.Core.Common;
using ESourcing.UI.Core.ResultModels;
using ESourcing.UI.Models.Auctions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ESourcing.UI.Clients
{
    public class AuctionClient
    {
        #region Fields
        private readonly HttpClient _httpClient;
        #endregion

        #region Ctor
        public AuctionClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri($"{ServicesConstants.LocalSourcingBaseAddress}Auction/");
        }
        #endregion

        #region Methods
        public async Task<Result<List<AuctionViewModel>>> GetAuctionsAsync()
        {
            var httpResponseMessage = await _httpClient.GetAsync("GetAuctions");
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return new Result<List<AuctionViewModel>> (false, ResultConstants.RecordNotFound);
            }

            var responseMessage = await httpResponseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<AuctionViewModel>>(responseMessage);
            if (result is null)
                return new Result<List<AuctionViewModel>>(false, ResultConstants.RecordNotFound);

            return new Result<List<AuctionViewModel>>(false, ResultConstants.RecordFound, result);
        }

        public async Task<Result<AuctionViewModel>> CreateAuctionAsync(AuctionViewModel auctionViewModel)
        {
            StringContent content = new(JsonConvert.SerializeObject(auctionViewModel));
            content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

            var httpResponseMessage = await _httpClient.PostAsync("CreateAuction", content);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return new Result<AuctionViewModel>(false, ResultConstants.CreateNotSuccessfully);
            }

            var responseMessage = await httpResponseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuctionViewModel>(responseMessage);
            if (result is null)
                return new Result<AuctionViewModel>(false, ResultConstants.CreateNotSuccessfully);

            return new Result<AuctionViewModel>(true, ResultConstants.CreateSuccessfully, result);
        }
        #endregion
    }
}