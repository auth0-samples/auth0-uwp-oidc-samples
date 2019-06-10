using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Auth0.OidcClient;
using IdentityModel.OidcClient;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        readonly string[] _connectionNames = new string[]
        {
            "Username-Password-Authentication",
            "google-oauth2",
            "twitter",
            "facebook",
            "github",
            "windowslive"
        };

        private Auth0Client client;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string domain = "{DOMAIN}";
            string clientId = "{CLIENT_ID}";

            Auth0ClientOptions clientOptions = new Auth0ClientOptions
            {
                Domain = domain,
                ClientId = clientId
            };
            client = new Auth0Client(clientOptions);
            clientOptions.PostLogoutRedirectUri = clientOptions.RedirectUri;

            var extraParameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(connectionNameAutoSuggestBox.Text))
                extraParameters.Add("connection", connectionNameAutoSuggestBox.Text);

            if (!string.IsNullOrEmpty(audienceTextBox.Text))
                extraParameters.Add("audience", audienceTextBox.Text);

            DisplayResult(await client.LoginAsync(extraParameters: extraParameters));
        }

        private void DisplayResult(LoginResult loginResult)
        {
            // Display error
            if (loginResult.IsError)
            {
                resultTextBox.Text = loginResult.Error;
                return;
            }

            // Hide login button
            loginButton.Visibility = Visibility.Collapsed;
            // Display logout button
            logoutButton.Visibility = Visibility.Visible;

            // Display result
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Tokens");
            sb.AppendLine("------");
            sb.AppendLine($"id_token: {loginResult.IdentityToken}");
            sb.AppendLine($"access_token: {loginResult.AccessToken}");
            sb.AppendLine($"refresh_token: {loginResult.RefreshToken}");
            sb.AppendLine();

            sb.AppendLine("Claims");
            sb.AppendLine("------");
            foreach (var claim in loginResult.User.Claims)
            {
                sb.AppendLine($"{claim.Type}: {claim.Value}");
            }

            resultTextBox.Text = sb.ToString();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            connectionNameAutoSuggestBox.ItemsSource = _connectionNames;
        }

        private async void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            await client.LogoutAsync();
            // Hide logout button
            logoutButton.Visibility = Visibility.Collapsed;
            // Display login button
            loginButton.Visibility = Visibility.Visible;
            // Clean up form
            resultTextBox.Text = "";
            connectionNameAutoSuggestBox.ItemsSource = _connectionNames;
            audienceTextBox.Text = "";
        }
    }
}
