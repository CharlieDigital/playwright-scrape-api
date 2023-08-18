using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Playwright;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (Environment.GetEnvironmentVariable("IS_CONTAINER") == "true") {
  app.Urls.Add("http://0.0.0.0:8080");
}

app.MapGet("/", async (
  [FromQuery] string url
) => {
  url = HttpUtility.UrlDecode(url);
  using var playwright = await Playwright.CreateAsync();
  await using var browser = await playwright.Chromium.LaunchAsync(new() {
    Headless = true
  });

  await using var context = await browser.NewContextAsync();
  var page = await context.NewPageAsync();
  await page.GotoAsync(url);
  await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
  var text = await page.EvaluateAsync<string>("document.body.innerText");

  return text;
});

app.Run();