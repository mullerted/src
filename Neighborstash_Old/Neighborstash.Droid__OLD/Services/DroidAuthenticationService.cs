using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid;
using Neighborstash.Core.Contracts;

namespace Neighborstash.Droid.Services
{
    public class DroidAuthenticationService : IAuthenticationService
    {
        public void Login()
        {
            //var auth = new OAuth2Authenticator(
            //    clientId: "238979913332995", // your OAuth2 client id (For FB Also called App-ID)
            //    scope: "email", // the scopes for the particular API you're accessing, delimited by "+" symbols
            //    authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"), // the auth URL for the service (i.e FB, Twitter)
            //    redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html")); // the redirect URL for the service
            //auth.Completed += (sender, eventArgs) => {
            //    if (eventArgs.IsAuthenticated)
            //    {
            //        //Saves Token, and Calls LoginSuccess() to change Screen
            //        //App.getToken(eventArgs.Account.Properties["access_token"]);
            //        //LoginPage.LoginSuccess();
            //    }
            //    else
            //    {
            //        //LoginPage.LoginCancel();
            //    }
            //};

            //Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext.StartActivity(auth.GetUI(this));
            
        }
    }
}