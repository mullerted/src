using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace Neighborstash.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel, IDisposable
    {
        protected IMvxMessenger Messenger;

        public BaseViewModel()
        {
            
        }
        public BaseViewModel(IMvxMessenger messenger)
        {
            Messenger = messenger;
        }

        protected async Task ReloadDataAsync()
        {
            try
            {
                await InitializeAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw;
            }
        }

        protected virtual Task InitializeAsync()
        {
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
