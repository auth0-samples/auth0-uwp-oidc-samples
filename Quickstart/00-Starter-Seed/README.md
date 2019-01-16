# Login
<img src="https://img.shields.io/badge/community-driven-brightgreen.svg"/> <br>

This example shows how to use the Auth0 OIDC Client with a Windows UWP (Universal Windows Platform) C# application.

You can read a quickstart for this sample [here](https://auth0.com/docs/quickstart/native/windows-uwp-csharp).

This repo is supported and maintained by Community Developers, not Auth0. For more information about different support levels check https://auth0.com/docs/support/matrix .

## Getting started

### Requirements

* Visual Studio 2017.4
* Windows 10 SDK (10.0.10586.0)

### Installation

####  To run this project

1. Ensure that you have replaced the values for `domain` and `clientId` variables inside the `loginButton_Click` event handler of `MainPage.xaml.cs`.

2. Run the application from Visual Studio.

3. Click on the **Login** button in your application in order to Log In with Auth0.

## Usage

### Important Snippets

#### Initialize the OIDC Client and call LoginAsync

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

#### Process Login Result

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

## Contribute

Feel like contributing to this repo? We're glad to hear that! Before you start contributing please visit our [Contributing Guideline](https://github.com/auth0-community/getting-started/blob/master/CONTRIBUTION.md).

Here you can also find the [PR template](https://github.com/auth0-community/auth0-uwp-oidc-samples/blob/master/PULL_REQUEST_TEMPLATE.md) to fill once creating a PR. It will automatically appear once you open a pull request.

## Issues Reporting

Spotted a bug or any other kind of issue? We're just humans and we're always waiting for constructive feedback! Check our section on how to [report issues](https://github.com/auth0-community/getting-started/blob/master/CONTRIBUTION.md#issues)!

Here you can also find the [Issue template](https://github.com/auth0-community/auth0-uwp-oidc-samples/blob/master/ISSUE_TEMPLATE.md) to fill once opening a new issue. It will automatically appear once you create an issue.

## Repo Community

Feel like PRs and issues are not enough? Want to dive into further discussion about the tool? We created topics for each Auth0 Community repo so that you can join discussion on stack available on our repos. Here it is for this one: [auth0-uwp-oidc-samples](https://community.auth0.com/t/auth0-community-oss-auth0-uwp-oidc-samples/15981)

<a href="https://community.auth0.com/">
<img src="/Assets/join_auth0_community_badge.png"/>
</a>

## License

This project is licensed under the MIT license. See the [LICENSE](https://github.com/auth0-community/auth0-uwp-oidc-samples/blob/master/LICENSE) file for more info.

## What is Auth0?

Auth0 helps you to:

* Add authentication with [multiple authentication sources](https://docs.auth0.com/identityproviders), either social like
  * Google
  * Facebook
  * Microsoft
  * Linkedin
  * GitHub
  * Twitter
  * Box
  * Salesforce
  * etc.

  **or** enterprise identity systems like:
  * Windows Azure AD
  * Google Apps
  * Active Directory
  * ADFS
  * Any SAML Identity Provider

* Add authentication through more traditional [username/password databases](https://docs.auth0.com/mysql-connection-tutorial)
* Add support for [linking different user accounts](https://docs.auth0.com/link-accounts) with the same user
* Support for generating signed [JSON Web Tokens](https://docs.auth0.com/jwt) to call your APIs and create user identity flow securely
* Analytics of how, when and where users are logging in
* Pull data from other sources and add it to user profile, through [JavaScript rules](https://docs.auth0.com/rules)

## Create a free Auth0 account

* Go to [Auth0 website](https://auth0.com/signup)
* Hit the **SIGN UP** button in the upper-right corner
