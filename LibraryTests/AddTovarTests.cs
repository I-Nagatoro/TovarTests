using NUnit.Framework;
using Avalonia.Media.Imaging;
using TovarV2;
using System;
using System.Collections.Generic;
using System.IO;

namespace LibraryTests
{
    [TestFixture]
    public class AddTovarTests
    {
        private AddTovar addTovar;

        [SetUp]
        public void Setup()
        {
            // Сбросим список продуктов и счетчик
            ListPr.ListProd = new List<Product>();
            ListPr.b = 0;

            // Создадим окно
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

        // Эмуляция загрузки изображения
        private void SimulateAddImage()
        {
            addTovar.bitmapToBind = LoadTestBitmap();
            addTovar.ImagePath.Source = addTovar.bitmapToBind;
        }

        [Test]
        public void AddTovarImg_Click_ShouldSetBitmapAndImageSource()
        {
            SimulateAddImage();

            Assert.That(addTovar.bitmapToBind, Is.Not.Null);
            Assert.That(addTovar.ImagePath.Source, Is.EqualTo(addTovar.bitmapToBind));
        }

        [Test]
        public void AddTovarOk_Click_ShouldShowErrorIfNameExists()
        {
            // Добавим товар с именем "TestName"
            ListPr.ListProd.Add(new Product { Id = 0, nameProd = "TestName", priceProd = 100, quantityProd = 1 });

            addTovar.nameTov.Text = "TestName";
            addTovar.priceTov.Text = "100";
            addTovar.quantityTov.Text = "5";

            addTovar.AddTovarOk_Click(null, null);

            Assert.That(addTovar.errorMsg.Text, Is.EqualTo("Товар с таким именем уже имеется в каталоге"));
            Assert.That(ListPr.ListProd.Count, Is.EqualTo(1)); // Никаких новых товаров не добавлено
        }

        [Test]
        public void AddTovarOk_Click_ShouldAddProduct_WhenInputsValidAndNameUnique()
        {
            addTovar.nameTov.Text = "UniqueName";
            addTovar.priceTov.Text = "150";
            addTovar.quantityTov.Text = "3";
            SimulateAddImage();

            addTovar.AddTovarOk_Click(null, null);

            Assert.That(addTovar.errorMsg.Text, Is.Empty);
            Assert.That(ListPr.ListProd.Count, Is.EqualTo(1));
            var prod = ListPr.ListProd[0];
            Assert.That(prod.nameProd, Is.EqualTo("UniqueName"));
            Assert.That(prod.priceProd, Is.EqualTo(150));
            Assert.That(prod.quantityProd, Is.EqualTo(3));
            Assert.That(prod.bitmapProd, Is.EqualTo(addTovar.bitmapToBind));
            Assert.That(ListPr.b, Is.EqualTo(1));
        }

        [Test]
        public void AddTovarOk_Click_ShouldThrowFormatException_WhenPriceOrQuantityInvalid()
        {
            addTovar.nameTov.Text = "Name";

            addTovar.priceTov.Text = "notanumber";
            addTovar.quantityTov.Text = "5";

            Assert.Throws<FormatException>(() => addTovar.AddTovarOk_Click(null, null));

            addTovar.priceTov.Text = "100";
            addTovar.quantityTov.Text = "notanumber";

            Assert.Throws<FormatException>(() => addTovar.AddTovarOk_Click(null, null));
        }
    }
}
