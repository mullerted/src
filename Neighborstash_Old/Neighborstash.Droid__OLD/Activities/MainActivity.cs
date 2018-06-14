using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;
using MvvmCross.Forms.Droid;
//using MvvmCross.Forms.Presenters;
using MvvmCross.Platform;
using Neighborstash.Core.ViewModels;
using Xamarin.Forms;

namespace Neighborstash.Droid.Activities
{
    [Activity(Label = "Neighborstash", 
        ScreenOrientation = ScreenOrientation.Portrait, 
        MainLauncher = true,
        Icon = "@drawable/icon")]
    public class MainActivity
        : MvxCachingFragmentCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            //ToolbarResource = Resource.Layout.toolbar;
            //TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainView);

            // Button button = FindViewById<Button>(Resource.Id.nsLogin_button);
            //Forms.Init(this, bundle);

            //var formsPresenter = (MvxFormsPagePresenter)Mvx.Resolve<IMvxAndroidViewPresenter>();
            //LoadApplication(formsPresenter.FormsApplication);
        }
    }
}


