using ESourcing.UI.Core.Common;
using ESourcing.UI.Core.ResultModels;
using ESourcing.UI.Models.Products;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESourcing.UI.Clients
{
    public class ProductClient
    {
        #region Fields
        private readonly HttpClient _httpClient;
        #endregion

        #region Ctor
        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(ServicesConstants.BaseAddress);
        }
        #endregion

        #region Methods
        public async Task<Result<List<ProductViewModel>>> GetProductsAsync()
        {
            var httpResponseMessage = await _httpClient.GetAsync("/Product/GetProducts");
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return new Result<List<ProductViewModel>>(false, ResultConstants.RecordNotFound);
            }

            string responseData = await httpResponseMessage.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductViewModel>>(responseData);
            if (products is null || !products.Any())
            {
                return new Result<List<ProductViewModel>>(false, ResultConstants.RecordNotFound);
            }

            return new Result<List<ProductViewModel>>(true, ResultConstants.RecordFound, products);
        }
        #endregion
    }
}
