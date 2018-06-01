using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Platform;

namespace Neighborstash.Core.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

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
    }
}
