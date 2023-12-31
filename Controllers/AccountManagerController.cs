using AutoMapper;
using AwesomeNetwork.Models;
using AwesomeNetwork.Data.Repository;
using AwesomeNetwork.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AwesomeNetwork.Ext;
using AwesomeNetwork.Data.UnitofWork;



namespace AwesomeNetwork.Controllers
{
    public class AccountManagerController : Controller
    {
        private IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private IUnitOfWork _unitOfWork;
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
            return RedirectToAction("Login");
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
            if (!ModelState.IsValid) return RedirectToAction("Index", "Home");
            var user = _mapper.Map<User>(model);
            var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("MyPage", "AccountManager");
            }
            ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            return RedirectToAction("Index","Home");
        }

        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId!);

                user.Convert(model);

                var result = await _userManager.UpdateAsync(user!);
                return RedirectToAction(result.Succeeded ? "MyPage" : "Edit", "AccountManager");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View( model);
            }
        }
    }
}