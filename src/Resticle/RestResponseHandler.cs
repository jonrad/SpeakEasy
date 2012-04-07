namespace Resticle
{
    public class RestResponseHandler : IRestResponseHandler
    {
        private readonly IRestResponse response;

        public RestResponseHandler(IRestResponse response)
        {
            this.response = response;
        }

        public T Unwrap<T>()
        {
            var deserializer = response.Deserializer;

            return deserializer.Deserialize<T>(response.Body);
        }
    }
}