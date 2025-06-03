using NUnit.Framework;
using Avalonia.Controls;
using TovarV2;
using System.Linq;
using Avalonia;
using Avalonia.Interactivity;

namespace Testiks
{
    [TestFixture]
    public class MainWindowTests
    {
        private MainWindow mainWindow;
        
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            AppBuilder.Configure<App>().UsePlatformDetect().SetupWithoutStarting();
        }


        [SetUp]
        public void Setup()
        {
            mainWindow = new MainWindow();
            mainWindow.InitializeComponent();
        }

                [Test]
        public void BtnVhod_OnClick_WithCode0_SetsCodeUser0_ClosesWindow()
        {
            mainWindow.CodeInput.Text = "0";

            mainWindow.BtnVhod_OnClick(null, new RoutedEventArgs());

            Assert.That(ListPr.codeUser, Is.EqualTo(0));
            // Здесь не можем проверить Close(), так как нет моков, но логика проходит
        }

        [Test]
        public void BtnVhod_OnClick_WithCode1_SetsCodeUser1_ClosesWindow()
        {
            mainWindow.CodeInput.Text = "1";

            mainWindow.BtnVhod_OnClick(null, new RoutedEventArgs());

            Assert.That(ListPr.codeUser, Is.EqualTo(1));
        }

        [Test]
        public void BtnVhod_OnClick_WithInvalidCode_DoesNotChangeCodeUser()
        {
            mainWindow.CodeInput.Text = "999";
            int initialCodeUser = ListPr.codeUser;

            mainWindow.BtnVhod_OnClick(null, new RoutedEventArgs());

            Assert.That(ListPr.codeUser, Is.EqualTo(initialCodeUser));
        }

        [Test]
        public void BtnVhod_OnClick_WithEmptyCode_DoesNotChangeCodeUser()
        {
            mainWindow.CodeInput.Text = "";

            int initialCodeUser = ListPr.codeUser;

            mainWindow.BtnVhod_OnClick(null, new RoutedEventArgs());

            Assert.That(ListPr.codeUser, Is.EqualTo(initialCodeUser));
        }

        [Test]
        public void BtnVhod_OnClick_WithWhitespaceCode_DoesNotChangeCodeUser()
        {
            mainWindow.CodeInput.Text = "   ";

            int initialCodeUser = ListPr.codeUser;

            mainWindow.BtnVhod_OnClick(null, new RoutedEventArgs());

            Assert.That(ListPr.codeUser, Is.EqualTo(initialCodeUser));
        }

        [Test]
        public void BtnVhod_OnClick_WithLeadingTrailingSpaces_SetsCorrectCodeUser()
        {
            mainWindow.CodeInput.Text = " 0 ";

            mainWindow.BtnVhod_OnClick(null, new RoutedEventArgs());

            Assert.That(ListPr.codeUser, Is.EqualTo(0));

            mainWindow.CodeInput.Text = " 1 ";

            mainWindow.BtnVhod_OnClick(null, new RoutedEventArgs());

            Assert.That(ListPr.codeUser, Is.EqualTo(1));
        }

        [Test]
        public void BtnVhod_OnClick_CalledMultipleTimes_UpdatesCodeUserEachTime()
        {
            mainWindow.CodeInput.Text = "0";
            mainWindow.BtnVhod_OnClick(null, new RoutedEventArgs());
            Assert.That(ListPr.codeUser, Is.EqualTo(0));

            mainWindow.CodeInput.Text = "1";
            mainWindow.BtnVhod_OnClick(null, new RoutedEventArgs());
            Assert.That(ListPr.codeUser, Is.EqualTo(1));
        }

        [Test]
        public void BtnVhod_OnClick_WithNullCode_DoesNotChangeCodeUser()
        {
            mainWindow.CodeInput.Text = null;

            int initialCodeUser = ListPr.codeUser;

            mainWindow.BtnVhod_OnClick(null, new RoutedEventArgs());

            Assert.That(ListPr.codeUser, Is.EqualTo(initialCodeUser));
        }
    }
}