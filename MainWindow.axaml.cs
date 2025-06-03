using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;

namespace TovarV2
{
    public partial class MainWindow : Window
    {
        public List<Product> products = new List<Product>();

        public const string codeAdm = "0", codeUs = "1";
       
        public MainWindow()
        {
           
            InitializeComponent();
        }

        public void BtnVhod_OnClick(object? sender, RoutedEventArgs e)
        {
            string code = CodeInput.Text;
            if (code == "0")
            {
                ListPr.codeUser = 0;
                ProductEdit productEdit = new ProductEdit();
                productEdit.Show();

            }
            else if (code == "1")
            {
                ListPr.codeUser = 1;
                ProductEdit productEdit = new ProductEdit();
                productEdit.Show();

            }

            this.Close();

        }
    }
}