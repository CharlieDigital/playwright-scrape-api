using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Playwright;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
var app = builder.Build();
var port = 8080;
app.UseCors(cors => cors
  .AllowAnyHeader()
  .AllowAnyMethod()
  .WithOrigins(
    "http://localhost:3000"
  ));

app.MapGet("/", async (
  [FromQuery] string url,
  [FromQuery] string? selector
) => {
  url = HttpUtility.UrlDecode(url);
  selector = HttpUtility.UrlDecode(selector);

  using var playwright = await Playwright.CreateAsync();
  await using var browser = await playwright.Chromium.LaunchAsync(new() {
    Headless = true
  });

  await using var context = await browser.NewContextAsync();
  var page = await context.NewPageAsync();
  await page.GotoAsync(url);
  await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

  var text = string.IsNullOrEmpty(selector)
    ? await page.EvaluateAsync<string>("document.body.innerText")
    : await page.EvaluateAsync<string>($"document.querySelector('{selector}')");

  return text;
});

app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();