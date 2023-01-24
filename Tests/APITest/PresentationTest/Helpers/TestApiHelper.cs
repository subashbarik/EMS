using PresentationTest.Interfaces;
using RestSharp;

namespace PresentationTest.Helpers
{
    public class TestApiHelper : ITestApiHelper
    {
        private RestClient _restClient;
        private RestRequest _restRequest;

        public RestClient SetUrl(string baseUrl, string endPoint)
        {
            var url = Path.Combine(baseUrl, endPoint);
            _restClient = new RestClient(url);
            return _restClient;
        }
        public RestRequest CreateGetRequest()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Get
            };
            _restRequest.AddHeaders(new Dictionary<string, string>
            {
                { "Accept", "application/json" },
                { "Content-Type", "application/json" }

            });
            return _restRequest;
        }
        public RestRequest CreateGetRequest<T>(T payload, string token) where T : class
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Get
            };
            var bearerToken = "Bearer " + token;
            _restRequest.AddHeader("Accept", "application/json");
            _restRequest.AddHeader("Authorization", bearerToken);
            _restRequest.AddBody(payload);
            _restRequest.RequestFormat = DataFormat.Json;
            return _restRequest;
        }

        public RestRequest CreatePostRequest<T>(T payload) where T: class
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Post
            };
            _restRequest.AddHeader("Accept", "application/json");
            _restRequest.AddBody(payload);
            _restRequest.RequestFormat = DataFormat.Json;
            return _restRequest;
        }
        public RestRequest CreatePostRequest<T>(T payload, string token) where T : class
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Post
            };
            var bearerToken = "Bearer " + token;
            _restRequest.AddHeader("Accept", "application/json");
            _restRequest.AddHeader("Authorization", bearerToken);
            _restRequest.AddBody(payload);
            _restRequest.RequestFormat = DataFormat.Json;
            return _restRequest;
        }

        public async Task<RestResponse> GetResponseAsync()
        {
            return await _restClient.ExecuteAsync(_restRequest);
        }

        
    }
}
