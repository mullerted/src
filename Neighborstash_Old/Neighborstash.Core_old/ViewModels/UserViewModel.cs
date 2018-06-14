using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Neighborstash.Core.Contracts;

namespace Neighborstash.Core.ViewModels
{
    public class UserDetailViewModel : BaseViewModel,IUserViewModel
    {
        private string _userName;
        private string _userEmail;
        private string _firstName;
        private string _lastName;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public string UserEmail
        {
            get => _userEmail;
            set
            {
                _userEmail = value;
                RaisePropertyChanged(()=>UserEmail);
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged(()=>FirstName);
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged(() =>LastName);
            }
        }

        public override string ToString()
        {
          var sb = new StringBuilder();
            foreach (var info in this.GetType().GetProperties())
            {
                var value = info.GetValue(this, null) ?? "(null)";
                sb.AppendLine($"{info?.Name}:{value.ToString()}");

            }

            return sb.ToString();
        }


        public MvxCommand CloseCommand { get; set; }

        public UserDetailViewModel() : base()
        {
            CloseCommand = new MvxCommand(() => { Close(this); });
        }

        public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        protected override async Task InitializeAsync()
        {
            // pick the selected user 
            //user = await _userDemographicsDataService.GetUserDemogratphics(_username);
        }

        private readonly IUserDataService _userDataService;

        public MvxCommand AddToSavedUsersCommand
        {
            get
            {
                return new MvxCommand(async () =>
                    {
                       // await _userDataService.AddSavedUser(_userDataService.GetAciveUser().UserId, SelectedUser.);

                        //await _dialogService.ShowAlertAsync();
                    });


            }
        }
    }

    public class MainViewModel: UserDetailViewModel { }
}
