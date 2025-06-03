using NUnit.Framework;
using System.Collections.Generic;
using Avalonia;
using TovarV2;

namespace Testiks;

[TestFixture]
public class ProductEditTests
{
    private ProductEdit productEdit;
    
    [OneTimeSetUp]
    public static void InitializeAvalonia()
    {
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .SetupWithoutStarting();
    }

    [SetUp]
    public void Setup()
    {
        productEdit = new ProductEdit();

        ListPr.ListProd = new List<Product>
        {
            new Product { Id = 0, nameProd = "Product1", priceProd = 100, quantityProd = 10 },
            new Product { Id = 1, nameProd = "Product2", priceProd = 200, quantityProd = 5 },
            new Product { Id = 2, nameProd = "Product3", priceProd = 300, quantityProd = 8 }
        };
    }

    [Test]
    public void PagesConfig_Should_ShowFirstPageWithTwoItems_When_MoreThanTwoProducts()
    {
        productEdit.PagesConfig(ListPr.ListProd);

        Assert.That(productEdit.currentProd.Count, Is.EqualTo(2));
        Assert.That(productEdit.currentProd[0].nameProd, Is.EqualTo("Product1"));
        Assert.That(productEdit.currentProd[1].nameProd, Is.EqualTo("Product2"));
    }

    [Test]
    public void PagesConfig_Should_ShowAllItems_When_OneOrZeroProducts()
    {
        var oneProduct = new List<Product>
        {
            new Product { Id = 0, nameProd = "OnlyProduct", priceProd = 50, quantityProd = 3 }
        };

        productEdit.PagesConfig(oneProduct);

        Assert.That(productEdit.currentProd.Count, Is.EqualTo(1));
        Assert.That(productEdit.currentProd[0].nameProd, Is.EqualTo("OnlyProduct"));
    }

    [Test]
    public void AddToKorzBtn_OnClick_Should_AddProductToProductSelects_IfNotExists()
    {
        ListPr.productSelects.Clear();
        productEdit.a = 0;

        var sender = new Avalonia.Controls.Button();
        sender.Tag = 1;

        productEdit.AddToKorzBtn_OnClick(sender, null);

        Assert.That(ListPr.productSelects.Count, Is.EqualTo(1));
        Assert.That(ListPr.productSelects[0].nameProdKorz, Is.EqualTo("Product2"));
        Assert.That(ListPr.productSelects[0].quantitySelect, Is.EqualTo(1));
    }

    [Test]
    public void AddToKorzBtn_OnClick_Should_NotAddDuplicateProduct()
    {
        ListPr.productSelects.Clear();
        ListPr.productSelects.Add(new ProductSelect { nameProdKorz = "Product1", quantitySelect = 1 });
        productEdit.a = ListPr.productSelects.Count;

        var sender = new Avalonia.Controls.Button();
        sender.Tag = 0;

        productEdit.AddToKorzBtn_OnClick(sender, null);

        // Кол-во товаров в корзине не должно увеличиться, так как товар уже есть
        Assert.That(ListPr.productSelects.Count, Is.EqualTo(1));
    }
}
