namespace Neighborstash.Core.ViewModels
{
    internal interface IUserViewModel
    {
         string UserName { get; set; }
         string UserEmail { get; set; }
         string FirstName { get; set; }
         string LastName { get; set; }
    }
}