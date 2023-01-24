using System.Text.Json;
using Newtonsoft.Json;
using RestSharp;

namespace PresentationTest.Helpers
{
    public static class TestHandleContent
    {
        public static T GetContent<T>(RestResponse response) where T: class
        {
            var content = response.Content;
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
