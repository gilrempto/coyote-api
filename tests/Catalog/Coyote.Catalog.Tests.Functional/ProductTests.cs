using Coyote.Catalog.Products;
using Coyote.Catalog.Tests.Functional.Seedwork;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Coyote.Catalog.Tests.Functional;

[TestCaseOrderer("Coyote.Catalog.Tests.Functional.Seedwork.PriorityOrderer", "Coyote.Catalog.Tests.Functional")]
public class ProductTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly WebApplicationFactory<Startup> factory;
    public const string RequestUri = "/Catalog/Products";

    public static ProductOutput? Product { get; set; }

    public ProductTests(WebApplicationFactory<Startup> factory)
    {
        this.factory = factory;
    }

    [Fact(DisplayName = "1. Create product"), TestPriority(1)]
    public async Task PostProduct_ReturnsCreatedProduct()
    {
        // Arrange
        var client = factory.CreateClient();
        var request = new ProductInput
        {
            TypeId = Guid.NewGuid(),
            Name = "London Porter",
            Features = new Dictionary<string, object>
                {
                    { "ABV", 5.4m },
                    { "IBU", 37 }
                }
        };

        // Act
        var response = await client.PostAsJsonAsync(RequestUri, request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var responseJson = await response.Content.ReadAsStringAsync();
        Product = JsonConvert.DeserializeObject<ProductOutput>(responseJson);
        Assert.NotEqual(Guid.Empty, Product?.Id);
        Assert.Equal(request.Name, Product?.Name);
        // TODO: Assert product features
    }

    [Fact(DisplayName = "2. List products"), TestPriority(2)]
    public async Task GetProducts_ReturnsNotEmptyProductList()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync(RequestUri);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        var productList = JsonConvert.DeserializeObject<IEnumerable<ProductOutput>>(responseJson);
        Assert.NotEmpty(productList);
    }

    [Fact(DisplayName = "3. Find product"), TestPriority(3)]
    public async Task GetProduct_ReturnsProduct()
    {
        // Arrange
        var client = factory.CreateClient();
        var productId = Product?.Id;

        // Act
        var response = await client.GetAsync($"{RequestUri}/{Product?.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        Product = JsonConvert.DeserializeObject<ProductOutput>(responseJson);
        Assert.Equal(productId, Product?.Id);
    }

    [Fact(DisplayName = "4. Update product"), TestPriority(4)]
    public async Task PutProduct_ReturnsNoContentAndGetsUpdatedProduct()
    {
        // Arrange
        var client = factory.CreateClient();
        var request = new ProductInput
        {
            Name = "London Pride",
            Features = new Dictionary<string, object>
                {
                    { "ABV", 4.1m },
                    { "IBU", 30 }
                }
        };

        // Act
        var response = await client.PutAsJsonAsync($"{RequestUri}/{Product?.Id}", request);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        response = await client.GetAsync($"{RequestUri}/{Product?.Id}");
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        Product = JsonConvert.DeserializeObject<ProductOutput>(responseJson);
        Assert.Equal(request.Name, Product?.Name);
        // TODO: Assert product features
    }

    [Fact(DisplayName = "5. Delete product"), TestPriority(5)]
    public async Task DeleteProduct_ReturnsSuccesStatusCode()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.DeleteAsync($"{RequestUri}/{Product?.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        response = await client.GetAsync($"{RequestUri}/{Product?.Id}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
