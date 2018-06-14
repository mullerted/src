using MvvmCross.Core.ViewModels;

namespace Neighborstash.Core.ViewModels
{
    public class FirstViewModel
        : MvxViewModel
    {
        string hello = "Welcome to Neighborstash!";
        public string Hello
        {
            get { return hello; }
            set { SetProperty(ref hello, value); }
        }
    }
}
