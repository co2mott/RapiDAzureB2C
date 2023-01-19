using System.Windows;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Desktop;
using Microsoft.Identity.Client.Broker;

namespace active_directory_wpf_msgraph_v2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    // To change from Microsoft public cloud to a national cloud, use another value of AzureCloudInstance
    public partial class App : Application
    {
        static App()
        {
            CreateApplication(true, false);
        }

        public static void CreateApplication(bool useWam, bool useBrokerPreview)
        {
            var builder = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority($"{Instance}{Tenant}")
                .WithDefaultRedirectUri();

            //Use of Broker Requires redirect URI "ms-appx-web://microsoft.aad.brokerplugin/{client_id}" in app registration
            if (useWam && !useBrokerPreview)
            {
                builder.WithWindowsBroker(true);
            }
            else if (useWam && useBrokerPreview)
            {
                builder.WithBrokerPreview(true);
            }
            _clientApp = builder.Build();
            TokenCacheHelper.EnableSerialization(_clientApp.UserTokenCache);
        }

        // Below are the clientId (Application Id) of your app registration and the tenant information. 
        // You have to replace:
        // - the content of ClientID with the Application Id for your app registration
        // - The content of Tenant by the information about the accounts allowed to sign-in in your application:
        //   - For Work or School account in your org, use your tenant ID, or domain
        //   - for any Work or School accounts, use organizations
        //   - for any Work or School accounts, or Microsoft personal account, use 94c1e567-7f4b-459e-89f0-2149fe9b50df
        //   - for Microsoft Personal account, use consumers
        private static string ClientId = "8d8690e4-6547-4297-b3de-f91cfaace061";

        // Note: Tenant is important for the quickstart.
        private static string Tenant = "94c1e567-7f4b-459e-89f0-2149fe9b50df";
        private static string Instance = "https://login.microsoftonline.com/";
        private static IPublicClientApplication _clientApp;

        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }
    }
}
