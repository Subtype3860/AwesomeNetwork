using AwesomeNetwork.Models;

namespace AwesomeNetwork.ViewModels.Account
{
    public class UserViewModel(User user)
    {
        public User User { get; set; } = user;
    }
}
