using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CitraDigitalAndroid.Helpers
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
