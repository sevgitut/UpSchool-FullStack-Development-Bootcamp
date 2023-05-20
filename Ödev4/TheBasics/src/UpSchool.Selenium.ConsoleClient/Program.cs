using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium.Chrome;
using UpSchool.Domain.Dtos;
using Microsoft.AspNetCore.SignalR.Client;
using UpSchool.Domain.Utilities;
using System.Net.Http.Json;

Console.WriteLine("UpSchool Crawler");

Console.ReadKey();

new DriverManager().SetUpDriver(new ChromeConfig());

IWebDriver driver = new ChromeDriver();

var hubConnection = new HubConnectionBuilder()
    .WithUrl($"https://localhost:7296/Hubs/SeleniumLogHub")
    .WithAutomaticReconnect()
    .Build();

await hubConnection.StartAsync();


try
{
    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Navigated to Google.com"));

    //var httpClient = new HttpClient();

    //var apiSendNotificationDto = new SendLogNotificationApiDto(CreateLog("Bot started."), hubConnection.ConnectionId);

    //await httpClient.PostAsJsonAsync("https://localhost:7296/api/SeleniumLogs", apiSendNotificationDto);

    driver.Navigate().GoToUrl("https://www.google.com");

    // We are waiting for fun. 
    Thread.Sleep(1500);

    IWebElement searchBox = driver.FindElement(By.Name("q"));
    searchBox.SendKeys("UpSchool");
    searchBox.Submit();

    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Search submitted with \"UpSchool\"parameter."));

    // We are waiting for the results to load.
    Thread.Sleep(3000);

    // // UP School.io is the first result we want to click on.
    IWebElement firstResult = driver.FindElements(By.CssSelector("div.g")).FirstOrDefault();
    if (firstResult != null)
    {
        IWebElement link = firstResult.FindElement(By.TagName("a"));
        link.Click();
        await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Mission Accomplished"));
    }
    else
    {
        Console.WriteLine("No search results found.");
    }

    Console.ReadKey();

    await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Bot stopped"));

    driver.Quit();
}
catch (Exception exception)
{
    driver.Quit();
}

SeleniumLogDto CreateLog(string message) => new SeleniumLogDto(message);