using Android.Net;
using CitraDigitalAndroid.Droid;
using CitraDigitalAndroid.Helpers;
using Javax.Net.Ssl;
using System.Net.Http;
using Xamarin.Android.Net;

[assembly: Xamarin.Forms.Dependency(typeof(HTTPClientHandlerCreationServiceAndroid))]
namespace CitraDigitalAndroid.Droid
{
    public class HTTPClientHandlerCreationServiceAndroid : IHTTPClientHandlerCreationService
    {

        HttpClientHandler IHTTPClientHandlerCreationService.GetInsecureHandler()
        {
            throw new System.NotImplementedException();
        }
    }

    public class IgnoreSSLClientHandler : AndroidClientHandler
    {
        protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return SSLCertificateSocketFactory.GetInsecure(1000, null);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection)
        {
            return new IgnoreSSLHostnameVerifier();
        }
    }

    public class IgnoreSSLHostnameVerifier : Java.Lang.Object, IHostnameVerifier
    {
        public bool Verify(string hostname, ISSLSession session)
        {
            return true;
        }
    }
}