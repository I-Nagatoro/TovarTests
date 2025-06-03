using NUnit.Framework;
using Avalonia.Controls;
using Avalonia;
using System.Collections.Generic;
using System.Linq;
using TovarV2;

namespace Testiks;

[TestFixture]
public class KorzinaTests
{
    private Korzina korzina;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        AppBuilder.Configure<App>().UsePlatformDetect().SetupWithoutStarting();
    }

    [SetUp]
    public void Setup()
    {
        ListPr.productSelects = new List<ProductSelect>
        {
            new ProductSelect { Id = 0, nameProdKorz = "Prod1", priceProdKorz = 100, quantityProdKorz = 5, quantitySelect = 1 },
            new ProductSelect { Id = 1, nameProdKorz = "Prod2", priceProdKorz = 200, quantityProdKorz = 10, quantitySelect = 2 },
            new ProductSelect { Id = 2, nameProdKorz = "Prod3", priceProdKorz = 300, quantityProdKorz = 7, quantitySelect = 3 }
        };

        korzina = new Korzina
        {
            backBtn = new Button(),
            nextBtn = new Button(),
            pageNum = new TextBlock(),
            ProdListInKorz = new ListBox(),
            podschetstoimosti = new TextBlock()
        };
    }

    [Test]
    public void PagesConfig_Should_ShowCorrectPageNum()
    {
        korzina.PagesConfig();

        var expectedPage = $"{ListPr.productSelects.IndexOf(korzina.currentProdSel[0]) / 2 + 1}/{(int)Math.Ceiling(ListPr.productSelects.Count / 2.0)}";

        Assert.That(korzina.pageNum.Text, Is.EqualTo(expectedPage));
    }

    [Test]
    public void PrevPage_Should_ChangeItems()
    {
        korzina.PagesConfig();
        korzina.NextPage_OnClick(null, null);
        var firstOnSecondPage = korzina.currentProdSel.First().nameProdKorz;

        korzina.PrevPage_OnClick(null, null);

        Assert.That(korzina.currentProdSel.Any(x => x.nameProdKorz != firstOnSecondPage), Is.True);
    }

    [Test]
    public void NextPage_Should_ChangeItems()
    {
        korzina.PagesConfig();
        var firstBefore = korzina.currentProdSel.First().nameProdKorz;

        korzina.NextPage_OnClick(null, null);

        Assert.That(korzina.currentProdSel.Any(x => x.nameProdKorz != firstBefore), Is.True);
    }

    [Test]
    public void DelBtn_Click_Should_RemoveItemAndRecalculatePages()
    {
        korzina.PagesConfig();
        var initialCount = ListPr.productSelects.Count;
        var button = new Button { Tag = 0 };

        korzina.DelBtn_Click(button, null);

        Assert.Multiple(() =>
        {
            Assert.That(ListPr.productSelects.Count, Is.EqualTo(initialCount - 1));
            Assert.That(korzina.currentProdSel.Count, Is.LessThanOrEqualTo(2));
            Assert.That(korzina.pageNum.Text, Is.Not.Null.And.Not.Empty);
        });
    }

    [Test]
    public void UvelBtn_Should_NotExceedMax()
    {
        ListPr.productSelects[0].quantitySelect = ListPr.productSelects[0].quantityProdKorz;
        var button = new Button { Tag = 0 };

        korzina.UvelBtn_OnClick(button, null);

        Assert.That(ListPr.productSelects[0].quantitySelect, Is.EqualTo(ListPr.productSelects[0].quantityProdKorz));
    }

    [Test]
    public void UmenBtn_Should_NotGoBelowOne()
    {
        ListPr.productSelects[0].quantitySelect = 1;
        var button = new Button { Tag = 0 };

        korzina.UmenBtn_OnClick(button, null);

        Assert.That(ListPr.productSelects[0].quantitySelect, Is.EqualTo(1));
    }

    [Test]
    public void PodschetOrder_Should_CalculateCorrectly()
    {
        korzina.PodschetOrderBtn_Click(null, null);
        int expectedSum = ListPr.productSelects.Sum(x => x.priceProdKorz * x.quantitySelect);

        Assert.That(korzina.podschetstoimosti.Text, Is.EqualTo($"Общая стоимость составляет: {expectedSum} руб."));
    }

    [Test]
    public void PagesConfig_Should_HandleEmptyList()
    {
        ListPr.productSelects.Clear();
        korzina.PagesConfig();

        Assert.Multiple(() =>
        {
            Assert.That(korzina.ProdListInKorz.Items, Is.Empty);
            Assert.That(korzina.pageNum.Text, Is.EqualTo("0/0"));
        });
    }

    [Test]
    public void UvelBtn_Should_HandleInvalidIndex()
    {
        var button = new Button { Tag = -1 };
        Assert.DoesNotThrow(() => korzina.UvelBtn_OnClick(button, null));
    }

    [Test]
    public void UmenBtn_Should_HandleInvalidIndex()
    {
        var button = new Button { Tag = -10 };
        Assert.DoesNotThrow(() => korzina.UmenBtn_OnClick(button, null));
    }

    [Test]
    public void DelBtn_Should_HandleInvalidIndex()
    {
        var button = new Button { Tag = 100 };
        Assert.DoesNotThrow(() => korzina.DelBtn_Click(button, null));
    }
}
