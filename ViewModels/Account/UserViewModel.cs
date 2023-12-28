
using AwesomeNetworkMy.Models;
using System.ComponentModel.DataAnnotations;

namespace AwesomeNetworkMy.ViewModels.Account;

public class UserViewModel
{
    public User User { get; set; }

    public UserViewModel(User user)
    {
        User = user;
    }
}