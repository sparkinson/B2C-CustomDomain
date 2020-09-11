# B2C-CustomDomain
Custom Domains for Azure AD B2C using Azure AppServices via URL rewrite rules

1. Deploy an Azure AppService website
OS = Windows
Runtime stack = ASP.NET v4.7

2. Edit web.config and replace "yourtenant.b2clogin.com" with your tenant name, like "***mytenantname***.b2clogin.com"

3. Upload Web.config and applicationHost.xdt to AppService
[https://docs.microsoft.com/en-us/azure/app-service/deploy-ftp](https://docs.microsoft.com/en-us/azure/app-service/deploy-ftp)

web.config              --> /site/wwwroot
applicationHost.xdt     --> /site

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
