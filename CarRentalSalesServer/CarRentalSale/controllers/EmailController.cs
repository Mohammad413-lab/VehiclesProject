using CarRentalSale.datarepositories;
using CarRentalSale.enums;
using CarRentalSale.models;
using CarRentalSale.services;
using CarRentalSale.validationservices;
using Microsoft.AspNetCore.Mvc;

public class EmailController(IConfiguration configuration) : Controller
{

    private readonly IConfiguration _configration = configuration;

    [HttpGet]
    public IActionResult ResetPassword(string token, string email)
    {
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Token is missing.");
        }

        var model = new ResetPasswordViewModel { Token = token, Email = email };

        return View("ViewPagePassword", model);
    }

    [HttpGet]
    public IActionResult VerifiedUserEmail(string token)
    {
        TokenService tokenService = new TokenService(_configration);
        EmailService emailService = new EmailService();
        string? email = tokenService.GetEmailFromToken(token);
        if (email == null)
        {
            return View("EmailError");
        }
        emailService.VerifiedUserEmail(email);
        return View("EmailVerfiedSuc");


    }


    [HttpPost]
    public IActionResult ResetPassword(ResetPasswordViewModel resetModel)
    {

        Console.WriteLine("token is ------>" + resetModel.Token);
        Console.WriteLine("NewPassword is  ------>" + resetModel.NewPassword);
        Console.WriteLine("Email is  ------>" + string.IsNullOrEmpty(resetModel.Email));
        ValidationService validationService = new ValidationService();
        if (!validationService.IsValidPassword(resetModel.NewPassword))
        {
            return View("ViewPagePassword", resetModel);
        }



        if (!ModelState.IsValid)
        {
            return View("ViewPagePassword", resetModel);
        }
        EmailService emailService = new EmailService();
        int state = emailService.ChangePasswordByEmail(resetModel);
        switch (state)
        {
            case (int)ChangePasswordState.PasswordChanged: return RedirectToAction("PasswordChanged");
            case (int)ChangePasswordState.EmailNotFound: return RedirectToAction("EmailNotFound");
        }
        return View("ViewPagePassword", resetModel);


    }

    [HttpGet]
    public IActionResult UpdateEmail(string token, int id)
    {
        Console.WriteLine(id);
        var config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build();

        var tokenService = new TokenService(config);
        string email = tokenService.GetEmailFromToken(token);
        if (email != null)
        {
            UsersRepository.UpdateUserEmail(id, email);
            return View("EmailVerfiedSuc");
        }
        return View("EmailError");

    }

    public IActionResult EmailVerfiedSuc()
    {
        return View();
    }

    public IActionResult EmailError()
    {
        return View();
    }
    public IActionResult PasswordChanged()
    {
        return View();
    }
    public IActionResult EmailNotFound()
    {
        return View();
    }
}