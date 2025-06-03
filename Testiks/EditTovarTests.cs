using NUnit.Framework;
using Avalonia.Media.Imaging;
using TovarV2;
using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace Testiks
{
    [TestFixture]
    public class EditTovarTests
    {
        private EditTovar editTovar;
        private Product testProduct;

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            var app = BuildAvaloniaApp()
                .SetupWithoutStarting();
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
        }

        [SetUp]
        public void Setup()
        {
            testProduct = new Product
            {
                Id = 1,
                nameProd = "Original Product",
                priceProd = 150,
                quantityProd = 8,
                bitmapProd = null
            };

            ListPr.ListProd = new List<Product> { testProduct };
            ListPr.productForEdit = 0;

            editTovar = new EditTovar();
            editTovar.InitializeComponent();
        }

        private Bitmap LoadTestBitmap()
        {
            var testImagePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestImage.png");
            if (!File.Exists(testImagePath))
                throw new FileNotFoundException($"Test file not found: {testImagePath}");
            return new Bitmap(testImagePath);
        }

        [Test]
        public void Constructor_ShouldPopulateFieldsFromListProd()
        {
            Assert.That(editTovar.nameTovar.Text, Is.EqualTo("Original Product"));
            Assert.That(editTovar.priceTovar.Text, Is.EqualTo("150"));
            Assert.That(editTovar.quantityTovar.Text, Is.EqualTo("8"));
            Assert.That(editTovar.ImagePath.Source, Is.Null);
        }

        [Test]
        public void Constructor_ShouldSetInitialBitmap()
        {
            // Arrange: Create a product with an image
            var testBitmap = LoadTestBitmap();
            ListPr.ListProd[0].bitmapProd = testBitmap;
            
            // Act: Recreate the window
            editTovar = new EditTovar();
            editTovar.InitializeComponent();
            
            // Assert
            Assert.That(editTovar.bitmapToBind, Is.EqualTo(testBitmap));
            Assert.That(editTovar.ImagePath.Source, Is.EqualTo(testBitmap));
        }

        [Test]
        public async Task AddTovarImg_Click_ShouldUpdateBitmap_WhenFileSelected()
        {
            // Arrange
            var testImagePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestImage.png");
            
            // Act: Simulate file selection
            editTovar.bitmapToBind = new Bitmap(testImagePath);
            editTovar.ImagePath.Source = editTovar.bitmapToBind;
            
            // Assert
            Assert.That(editTovar.bitmapToBind, Is.Not.Null);
            Assert.That(editTovar.ImagePath.Source, Is.EqualTo(editTovar.bitmapToBind));
        }

        [Test]
        public void AddTovarImg_Click_ShouldNotChangeImage_WhenNoFileSelected()
        {
            // Arrange
            var originalBitmap = editTovar.bitmapToBind;
            
            // Act: Simulate canceled file dialog
            editTovar.bitmapToBind = null; // No change
            
            // Assert
            Assert.That(editTovar.bitmapToBind, Is.EqualTo(originalBitmap));
        }

        [Test]
        public void EditOk_Click_ShouldUpdateAllProductProperties()
        {
            // Arrange
            editTovar.nameTovar.Text = "Updated Product";
            editTovar.priceTovar.Text = "250";
            editTovar.quantityTovar.Text = "15";
            editTovar.bitmapToBind = LoadTestBitmap();

            // Act
            editTovar.EditOk_Click(null, null);

            // Assert
            var updatedProduct = ListPr.ListProd[0];
            Assert.That(updatedProduct.nameProd, Is.EqualTo("Updated Product"));
            Assert.That(updatedProduct.priceProd, Is.EqualTo(250));
            Assert.That(updatedProduct.quantityProd, Is.EqualTo(15));
            Assert.That(updatedProduct.bitmapProd, Is.EqualTo(editTovar.bitmapToBind));
        }

        [Test]
        public void EditOk_Click_ShouldHandleNullImage()
        {
            // Arrange
            editTovar.nameTovar.Text = "Product Without Image";
            editTovar.priceTovar.Text = "300";
            editTovar.quantityTovar.Text = "20";
            editTovar.bitmapToBind = null;

            // Act
            editTovar.EditOk_Click(null, null);

            // Assert
            var updatedProduct = ListPr.ListProd[0];
            Assert.That(updatedProduct.bitmapProd, Is.Null);
        }

        [Test]
        public void EditOk_Click_ShouldNotChangeOriginal_WhenPriceInvalid()
        {
            // Arrange
            var originalName = testProduct.nameProd;
            var originalPrice = testProduct.priceProd;
            
            editTovar.nameTovar.Text = "New Name";
            editTovar.priceTovar.Text = "invalid";
            editTovar.quantityTovar.Text = "10";

            // Act & Assert
            Assert.Throws<FormatException>(() => editTovar.EditOk_Click(null, null));
            
            // Verify original product wasn't changed
            Assert.That(testProduct.nameProd, Is.EqualTo(originalName));
            Assert.That(testProduct.priceProd, Is.EqualTo(originalPrice));
        }

        [Test]
        public void EditOk_Click_ShouldNotChangeOriginal_WhenQuantityInvalid()
        {
            // Arrange
            var originalQuantity = testProduct.quantityProd;
            
            editTovar.nameTovar.Text = "New Name";
            editTovar.priceTovar.Text = "200";
            editTovar.quantityTovar.Text = "invalid";

            // Act & Assert
            Assert.Throws<FormatException>(() => editTovar.EditOk_Click(null, null));
            
            // Verify original product wasn't changed
            Assert.That(testProduct.quantityProd, Is.EqualTo(originalQuantity));
        }

        [Test]
        public void EditOk_Click_ShouldHandleEmptyName()
        {
            // Arrange
            editTovar.nameTovar.Text = "";
            editTovar.priceTovar.Text = "100";
            editTovar.quantityTovar.Text = "5";

            // Act
            editTovar.EditOk_Click(null, null);

            // Assert
            Assert.That(testProduct.nameProd, Is.EqualTo(""));
        }

        [Test]
        public void EditOk_Click_ShouldHandleZeroValues()
        {
            // Arrange
            editTovar.nameTovar.Text = "Zero Values";
            editTovar.priceTovar.Text = "0";
            editTovar.quantityTovar.Text = "0";

            // Act
            editTovar.EditOk_Click(null, null);

            // Assert
            Assert.That(testProduct.priceProd, Is.EqualTo(0));
            Assert.That(testProduct.quantityProd, Is.EqualTo(0));
        }

        [Test]
        public void EditOk_Click_ShouldHandleLargeNumbers()
        {
            // Arrange
            editTovar.nameTovar.Text = "Large Numbers";
            editTovar.priceTovar.Text = "999999999";
            editTovar.quantityTovar.Text = "2147483647"; // max int

            // Act
            editTovar.EditOk_Click(null, null);

            // Assert
            Assert.That(testProduct.priceProd, Is.EqualTo(999999999));
            Assert.That(testProduct.quantityProd, Is.EqualTo(int.MaxValue));
        }
    }
}