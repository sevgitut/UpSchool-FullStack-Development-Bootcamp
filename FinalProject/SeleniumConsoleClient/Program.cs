using Domain.Entities;
using Microsoft.AspNetCore.SignalR.Client;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using static System.Net.Mime.MediaTypeNames;
using Application.Common.Models.Log;
using Domain.Enums;
using Domain.Filters;
using Domain.Dtos;

class Program
{
    static async Task Main(string[] args)
    {

        new DriverManager().SetUpDriver(new ChromeConfig());

        IWebDriver driver = new ChromeDriver();

        var orderHubConnection = new HubConnectionBuilder()
                 .WithUrl("http://localhost:5208/Hubs/OrderHub")
                 .WithAutomaticReconnect()
                 .Build();

        await orderHubConnection.StartAsync();

        var productHubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5208/Hubs/ProductHub")
            .WithAutomaticReconnect()
            .Build();

        await productHubConnection.StartAsync();

        var hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5208/Hubs/SeleniumLogHub")
            .WithAutomaticReconnect()
            .Build();

        await hubConnection.StartAsync();

        await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Bot started.", OrderStatus.BotStarted));

        List<Product> productList = new List<Product>();

        int amount = 0;
        while (amount <= 0)
        {
            Console.WriteLine(" Kaç tane ürün kazımak istiyorsunuz?");
            int.TryParse(Console.ReadLine(), out amount);
        }

        string selection = "";
        while (selection != "1" && selection != "2" && selection != "3")
        {
            Console.WriteLine("Hangi ürünleri kazımak istersiniz? (1-İndirimdekiler, 2-Normal Fiyatlı Ürünler 3-Hepsi)");
            selection = Console.ReadLine();
        }

        static ProductCrawlType GetProductCrawlType(string selection)
        {
            switch (selection)
            {
                case "1":
                    return ProductCrawlType.OnDiscount;
                case "2":
                    return ProductCrawlType.NonDiscount;
                case "3":
                    return ProductCrawlType.All;
                default:
                    return ProductCrawlType.All;
            }
        }



        await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Crawling started", OrderStatus.CrawlingStarted));

        string url = "https://4teker.net/?currentPage=";
        int itemsFetched = 0;
        int pageIndex = 1;

        var orderDto = OrderAdd(Guid.NewGuid(), amount, itemsFetched, GetProductCrawlType(selection));
        await orderHubConnection.InvokeAsync("AddOrder", orderDto);

        List<Product> allProducts = new List<Product>();

        while (true)
        {
            driver.Navigate().GoToUrl(url + pageIndex);
            Thread.Sleep(1000);

            var products = driver.FindElements(By.XPath("/html/body/section/div/div/div/div/img"));
            var productCards = driver.FindElements(By.ClassName("card-body"));

            for (int i = 0; i < products.Count; i++)
            {
                string pictureUrl = products[i].GetAttribute("src");
                string name = productCards[i].FindElement(By.ClassName("product-name")).Text.Trim();

                var priceElements = productCards[i].FindElements(By.CssSelector(".price, .sale-price"));


                bool hasSale = false;
                decimal SalePrice = 0;
                decimal Price = 0;

                foreach (var priceElement in priceElements)
                {
                    if (priceElement.GetAttribute("class").Contains("sale-price"))
                    {
                        hasSale = true;
                        SalePrice = Decimal.Parse(priceElement.Text.Trim('$'));
                    }
                    else if (priceElement.GetAttribute("class").Contains("price"))
                    {
                        Price = Decimal.Parse(priceElement.Text.Trim('$'));
                    }

                }

                if ((selection == "1" && hasSale) || (selection == "2" && !hasSale) || selection == "3")
                {
                    allProducts.Add(new Product { Name = name, Picture = pictureUrl, Price = Price, SalePrice = SalePrice });
                }
            }

            if (products.Count == 0)
            {
                break;
            }

            pageIndex++;

           await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog($"{pageIndex}. sayfa kazıma tamamlandı.", OrderStatus.CrawlingCompleted));
        }

        allProducts = ApplyProductFilter(selection, allProducts);

        while (itemsFetched < amount && allProducts.Count > 0)
        {
            int randomIndex = new Random().Next(allProducts.Count);
            Product randomProduct = allProducts[randomIndex];
            productList.Add(randomProduct);
            allProducts.RemoveAt(randomIndex);
            itemsFetched++;
        }

        driver.Quit();

        Console.WriteLine($"Toplam {itemsFetched} adet ürün bulundu.");

        await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Crawling completed.", OrderStatus.CrawlingCompleted));

        foreach (Product product in productList)
        {
            Console.WriteLine("Ürün Adı: " + product.Name);
            Console.WriteLine("Resim: " + product.Picture);
            Console.WriteLine("Fiyat: " + product.Price);
            Console.WriteLine("İndirimli Fiyat: " + product.SalePrice);
            Console.WriteLine();

            var productDto = ProductAdd(product.Id, orderDto.Id, product.Name, product.Picture,product.SalePrice, product.Price, product.IsOnSale);

            await productHubConnection.InvokeAsync("AddProduct", productDto);



            allProducts.Add(product);
        }

        await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog("Order completed.", OrderStatus.OrderCompleted));
    }


    static List<Product> ApplyProductFilter(string selection, List<Product> products)
    {
        switch (selection)
        {
            case "1":
                return ProductFilter.FilterBySalePrice(products);
            case "2":
                return ProductFilter.FilterByPrice(products);
            case "3":
                return ProductFilter.FilterAll(products);
            default:
                return products;
        }
    }

    static SeleniumLogDto CreateLog(string message, OrderStatus status) => new SeleniumLogDto(message, status);

    static ProductDto ProductAdd(Guid id, Guid orderId, string name, string pictureUrl, decimal? salePrice, decimal price, bool IsOnSale) =>
        new ProductDto(id, orderId, name, pictureUrl, salePrice, price, IsOnSale);

    static OrderDto OrderAdd(Guid id, int requestedAmount, int totalAmount, ProductCrawlType productCrawlType) =>
        new OrderDto(id, requestedAmount, totalAmount, productCrawlType);
}

















