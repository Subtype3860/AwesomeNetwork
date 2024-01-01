using AwesomeNetwork.Models;

namespace AwesomeNetwork.ViewModels.Account
{
    public class UserViewModel
    {
        public User User { get; set; }
        public UserViewModel(User user)
        {
            this.User = user;
        }
    }
}
