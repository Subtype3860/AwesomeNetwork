﻿
using AwesomeNetwork.Models;
using System.ComponentModel.DataAnnotations;

namespace AwesomeNetwork.ViewModels.Account;

public class UserViewModel
{
    public User User { get; set; }

    public UserViewModel(User user)
    {
        User = user;
    }
}