using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CarRentalSale.datarepositories;
using CarRentalSale.dtos;
using CarRentalSale.models;
using Newtonsoft.Json;

public class EmailService
{
    private readonly HttpClient _client;

    public EmailService()
    {
        _client = new HttpClient();
    }

    public async Task<bool> SendEmailAsync(EmailRequest emailRequest)
    {
        string apiKey = "here your api key ";

        var emailData = new
        {
            sender = new { name = "CarRentalSale", email = "moosaqrb296@gmail.com" },
            to = new[] { new { email = emailRequest.To, name = "User" } },
            subject = emailRequest.Subject,
            htmlContent = emailRequest.Body
        };

        var json = JsonConvert.SerializeObject(emailData);
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.brevo.com/v3/smtp/email");
        request.Headers.Add("api-key", apiKey);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            string successMessage = await response.Content.ReadAsStringAsync();
            Console.WriteLine(" Email sent successfully!");
            Console.WriteLine($" Response: {successMessage}");
            return true;
        }
        else
        {
            string error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($" Failed to send email: {response.StatusCode}");
            Console.WriteLine($"Details: {error}");
            return false;
        }
    }

    public int ChangePasswordByEmail(ResetPasswordViewModel reset)
    {

        return UsersRepository.ChangeUserPasswordByEmail(reset);
    }

    public  void VerifiedUserEmail(string email)
    {
        try
        {
            UsersRepository.VerifiedUserEmail(email);
        }
        catch (Exception ex) { throw new Exception($"Error  ----->>>>> {ex}"); }

    }

}