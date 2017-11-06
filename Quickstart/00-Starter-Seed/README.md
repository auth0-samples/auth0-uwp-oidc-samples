# Login

This example shows how to add ***Login/SignUp*** to your Windows Universal Platform application using the [Auth0 OIDC Client for .NET](https://github.com/auth0/auth0-oidc-client-net).

You can read a quickstart for this sample [here](https://auth0.com/docs/quickstart/native/windows-uwp-csharp). 

## Requirements

* Visual Studio 2017.4
* Windows 10 SDK (10.0.10586.0)

## To run this project

1. Ensure that you have replaced the values for `domain` and `clientId` variables inside the `loginButton_Click` event handler of `MainPage.xaml.cs`.

2. Run the application from Visual Studio.

3. Click on the **Login** button in your application in order to Log In with Auth0.

## Important Snippets

### 1. Initialize the OIDC Client and call LoginAsync

You can initialize Auth0 OIDC Client by instantiating a new instance of the `Auth0Client` class, passing a `Auth0ClientOptions` instance with the configuration settings.

To initiate the login process, you can call the `LoginAsync` method, optionally passing an `extraParameters` parameter containing extra parameters such as the `audience` and `connection`.

```csharp
// MainPage.xaml.cs

private async void loginButton_Click(object sender, RoutedEventArgs e)
{
    string domain = "{DOMAIN}";
    string clientId = "{CLIENT_ID}";

    var client = new Auth0Client(new Auth0ClientOptions
    {
        Domain = domain,
        ClientId = clientId
    });

    var extraParameters = new Dictionary<string, string>();

    if (!string.IsNullOrEmpty(connectionNameAutoSuggestBox.Text))
        extraParameters.Add("connection", connectionNameAutoSuggestBox.Text);

    if (!string.IsNullOrEmpty(audienceTextBox.Text))
        extraParameters.Add("audience", audienceTextBox.Text);

    DisplayResult(await client.LoginAsync(extraParameters: extraParameters));
}
```

### 2. Process Login Result

You can check the `LoginResult` returned by calling the `LoginAsync` to see whether authentication was successful. The `IsError` flag will indicate if there was an error, and if so, you can extract the error message from the `Error` property.

If the authentication was successful, you can obtain the user's information from the claims, and also obtain the tokens.

```csharp
// MainPage.xaml.cs

private void DisplayResult(LoginResult loginResult)
{
    // Display error
    if (loginResult.IsError)
    {
        resultTextBox.Text = loginResult.Error;
        return;
    }

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
```