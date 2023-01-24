using RestSharp;

namespace PresentationTest.Interfaces
{
    public interface ITestApiHelper
    {
        RestClient SetUrl(string baseUrl, string endPoint);
        RestRequest CreateGetRequest();
        RestRequest CreateGetRequest<T>(T payload, string token) where T : class;
        RestRequest CreatePostRequest<T>(T payload) where T : class;
        RestRequest CreatePostRequest<T>(T payload, string token) where T : class;
        Task<RestResponse> GetResponseAsync();
    }
}
