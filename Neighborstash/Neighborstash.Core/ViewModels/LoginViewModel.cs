using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Core.ViewModels;
using Neighborstash.Core.Contracts;

namespace Neighborstash.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public override void Start()
        { }

        public string  Email { get; set; }
        public string Password { get; set; }
        private MvxCommand _loginwithFacebook;

        public IMvxCommand LoginWithFacebook
        {

            get
            {
                _loginwithFacebook = _loginwithFacebook ??
                                     (_loginwithFacebook = new MvxCommand(_authenticationService.Login));

                return _loginwithFacebook;
            }
        }

    }
}
