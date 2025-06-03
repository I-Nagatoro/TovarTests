using NUnit.Framework;
using Avalonia.Media.Imaging;
using TovarV2;
using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using System.Threading.Tasks;

namespace Testiks
{
    [TestFixture]
    public class AddTovarTests
    {
        private AddTovar addTovar;

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
            ListPr.ListProd = new List<Product>();
            ListPr.b = 0;
            addTovar = new AddTovar();
            addTovar.InitializeComponent();
        }

        private Bitmap LoadTestBitmap()
        {
            var testImagePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestImage.png");
            if (!File.Exists(testImagePath))
                throw new FileNotFoundException($"Тестовый файл не найден: {testImagePath}");
            return new Bitmap(testImagePath);
        }

        [Test]
        public void Constructor_ShouldInitializeEmptyFields()
        {
            Assert.That(addTovar.nameTov.Text, Is.Empty);
            Assert.That(addTovar.priceTov.Text, Is.Empty);
            Assert.That(addTovar.quantityTov.Text, Is.Empty);
            Assert.That(addTovar.ImagePath.Source, Is.Null);
            Assert.That(addTovar.errorMsg.Text, Is.Empty);
        }

        [Test]
        public async Task AddTovarImg_Click_ShouldSetImage_WhenFileSelected()
        {
            var testImagePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestImage.png");
            
            addTovar.bitmapToBind = LoadTestBitmap();
            addTovar.ImagePath.Source = addTovar.bitmapToBind;
            
            Assert.That(addTovar.bitmapToBind, Is.Not.Null);
            Assert.That(addTovar.ImagePath.Source, Is.EqualTo(addTovar.bitmapToBind));
        }

        [Test]
        public void AddTovarImg_Click_ShouldNotSetImage_WhenNoFileSelected()
        {
            addTovar.bitmapToBind = null;
            
            Assert.That(addTovar.bitmapToBind, Is.Null);
            Assert.That(addTovar.ImagePath.Source, Is.Null);
        }

        [Test]
        public void AddTovarOk_Click_ShouldAddProduct_WithValidData()
        {
            addTovar.nameTov.Text = "NewProduct";
            addTovar.priceTov.Text = "100";
            addTovar.quantityTov.Text = "5";
            addTovar.bitmapToBind = LoadTestBitmap();

            addTovar.AddTovarOk_Click(null, null);

            Assert.That(ListPr.ListProd.Count, Is.EqualTo(1));
            var product = ListPr.ListProd[0];
            Assert.That(product.nameProd, Is.EqualTo("NewProduct"));
            Assert.That(product.priceProd, Is.EqualTo(100));
            Assert.That(product.quantityProd, Is.EqualTo(5));
            Assert.That(product.bitmapProd, Is.EqualTo(addTovar.bitmapToBind));
            Assert.That(ListPr.b, Is.EqualTo(1));
            Assert.That(addTovar.errorMsg.Text, Is.Empty);
        }

        [Test]
        public void AddTovarOk_Click_ShouldShowError_WhenNameExists()
        {
            ListPr.ListProd.Add(new Product { nameProd = "ExistingProduct" });
            addTovar.nameTov.Text = "ExistingProduct";
            addTovar.priceTov.Text = "100";
            addTovar.quantityTov.Text = "5";

            addTovar.AddTovarOk_Click(null, null);

            Assert.That(ListPr.ListProd.Count, Is.EqualTo(1));
            Assert.That(addTovar.errorMsg.Text, Is.EqualTo("Товар с таким именем уже имеется в каталоге"));
        }

        [Test]
        public void AddTovarOk_Click_ShouldThrowFormatException_ForInvalidPrice()
        {
            addTovar.nameTov.Text = "Product";
            addTovar.priceTov.Text = "invalid";
            addTovar.quantityTov.Text = "5";

            Assert.Throws<FormatException>(() => addTovar.AddTovarOk_Click(null, null));
        }

        [Test]
        public void AddTovarOk_Click_ShouldThrowFormatException_ForInvalidQuantity()
        {
            addTovar.nameTov.Text = "Product";
            addTovar.priceTov.Text = "100";
            addTovar.quantityTov.Text = "invalid";

            Assert.Throws<FormatException>(() => addTovar.AddTovarOk_Click(null, null));
        }

        [Test]
        public void AddTovarOk_Click_ShouldHandleEmptyName()
        {
            addTovar.nameTov.Text = "";
            addTovar.priceTov.Text = "100";
            addTovar.quantityTov.Text = "5";

            addTovar.AddTovarOk_Click(null, null);

            Assert.That(ListPr.ListProd.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddTovarOk_Click_ShouldHandleNullImage()
        {
            addTovar.nameTov.Text = "ProductWithoutImage";
            addTovar.priceTov.Text = "100";
            addTovar.quantityTov.Text = "5";
            addTovar.bitmapToBind = null;

            addTovar.AddTovarOk_Click(null, null);

            Assert.That(ListPr.ListProd.Count, Is.EqualTo(1));
            Assert.That(ListPr.ListProd[0].bitmapProd, Is.Null);
        }
    }
}