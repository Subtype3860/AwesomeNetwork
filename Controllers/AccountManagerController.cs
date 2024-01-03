using AutoMapper;
using AwesomeNetwork.Data.UnitofWork;
using AwesomeNetwork.Ext;
using AwesomeNetwork.Models;
using AwesomeNetwork.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace;

public class AccountManagerController : Controller
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUnitOfWork _unitOfWork;
    public AccountManagerController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _unitOfWork = unitOfWork;
    }
    [Route("Login")]
    [HttpGet]
    public IActionResult Login()
    {
        return View("Login");
    }
    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }
    [Route("MyPage")]
    [Authorize]
    [HttpGet]
    public IActionResult MyPage()
    {
        var user = User;

        var result = _userManager.GetUserAsync(user);

        return View("User", new UserViewModel(result.Result!));
    }
    [Route("Login")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {

            var user = _mapper.Map<User>(model);

            var result = await _signInManager.PasswordSignInAsync(user.Email!, model.Password!, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("MyPage", "AccountManager");
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
        }
        return RedirectToAction("Index", "Home");
    }
    [Route("Logout")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    [Authorize]
    [Route("Update")]
    public async Task<IActionResult> Update(User user)
    {
        if (ModelState.IsValid)
        {
            var model = await _userManager.FindByIdAsync(user.Id);
            return View(model);
        }
        
        return View("User", user);
        
    }
    [Authorize]
    [Route("Update")]
    [HttpPost]
    public async Task<IActionResult> Update(UserEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.UserId!);

            user!.Convert(model);

            var result = await _userManager.UpdateAsync(user!);
            return RedirectToAction(result.Succeeded ? "MyPage" : "Update", "AccountManager");
        }
        else
        {
            ModelState.AddModelError("", "Некорректные данные");
            return View("Edit", model);
        }
    }
}