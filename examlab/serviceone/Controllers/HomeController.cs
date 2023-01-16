using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private HttpClient _client;

    public HomeController()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("URL"));
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }
    
    [HttpGet("network")]
    public IActionResult NetworkRequest()
    {
        var response = _client.GetAsync("Book").Result;
        return Ok(response.Content.ReadAsStringAsync().Result);
    }
}
