using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.ViewModels.Account;

namespace TodoList.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<TodoUser> _userManager;
		private readonly SignInManager<TodoUser> _signInManager;

		public AccountController(UserManager<TodoUser> userManager, SignInManager<TodoUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}


		[HttpGet]
		[Route("Register")]

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new TodoUser()
				{
					UserName = model.UserName,
					Email = model.Email,
					EmailConfirmed = true
				};

				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					return Redirect("/login");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}

			}
			return View(model);
		}

		[HttpGet]
		[Route("LogIn")]

		public IActionResult Login(string returnUrl = null)
		{
			if (_signInManager.IsSignedIn(User))
				return Redirect("/");
			ViewData["returnUrl"] = returnUrl;
			return View();
		}

		[HttpPost]
		[Route("LogIn")]

		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		{
			if (_signInManager.IsSignedIn(User))
                return Redirect("/");

            if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(
					model.UserName, model.Password, model.RememberMe, true);

				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
						return Redirect("/");

                    return Redirect("/");
                }

				if (result.IsLockedOut)
				{
					ViewData["ErrorMessage"] = "اکانت شما به دلیل پنج بار ورود ناموفق به مدت پنج دقیق قفل شده است";
					return View(model);
				}

				ModelState.AddModelError("", "رمزعبور یا نام کاربری اشتباه است");
			}
			return View(model);
		}



		[Route("LogOut")]
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return Redirect("/Login");
		}

	}

}

