using AwesomeNetwork.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeNetwork.ViewModels.Account
{
    public class SearchViewModel
    {
        public List<User>? UserList { get; set; }
    }
}
