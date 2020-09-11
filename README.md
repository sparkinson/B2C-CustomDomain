# Custom Domains for Azure AD B2C using Azure AppServices via URL rewrite rules

A descriptive text about how to configure this can be found here [http://www.redbaronofazure.com/?p=7685](http://www.redbaronofazure.com/?p=7685) but if you just want to try it out, you can follow the steps below.

1. Deploy an Azure AppService website
OS = Windows
Runtime stack = ASP.NET v4.7

2. Edit web.config and replace "yourtenant.b2clogin.com" with your tenant name, like "***mytenantname***.b2clogin.com"

The web.config contains a solution for rerouting requests to 
https://sts.mydomain.com/yourtenant.onmicrosoft.com/B2C_1A_signup_signin/v2.0/.well-known/openid-configuration
to an Azure Function so that it can rewrite the JSON contents before we return it to the client. Otherwise authorization_endpoint, etc, would point to the B2C tenant and not to sts.mydomain.com.

Remove this rule if you don't plan to use this function.

3. Upload Web.config and applicationHost.xdt to AppService
[https://docs.microsoft.com/en-us/azure/app-service/deploy-ftp](https://docs.microsoft.com/en-us/azure/app-service/deploy-ftp)

- web.config              --> /site/wwwroot
- applicationHost.xdt     --> /site

4. CORS
If you are using your own HTML pages with B2C, update the CORS settings on the Azure Storage account to allow GET,OPTIONS for https://myapp.azurewebsites.net
[https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-ui-customization#3-configure-cors](https://docs.microsoft.com/en-us/azure/active-directory-b2c/custom-policy-ui-customization#3-configure-cors)

5. Test
test run a B2C Policy to see that it works, like

  https://yourtenant.b2clogin.com/yourtenant.onmicrosoft.com/oauth2/v2.0/authorize?p=B2C_1A_signup_signin&client_id=...

then try

  https://myapp.azurewebsites.net/yourtenant.onmicrosoft.com/oauth2/v2.0/authorize?p=B2C_1A_signup_signin&client_id=...


6. Configure Custom Domains for the AppService
https://docs.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-custom-domain

7. Repeat 4 and add your domain to CORS

8. Test again

  https://sts.mydomain.com/yourtenant.onmicrosoft.com/oauth2/v2.0/authorize?p=B2C_1A_signup_signin&client_id=...
