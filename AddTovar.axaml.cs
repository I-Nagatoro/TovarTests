using Avalonia.Controls;
using System.Linq;
using System;
using System.IO;
using System.Windows;
using Avalonia.Media.Imaging;
using Avalonia.Skia.Helpers;
using System.Drawing.Imaging;
using System.Drawing;
using System.Threading.Tasks;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Image = System.Drawing.Image;
using Avalonia.Threading;


namespace TovarV2
{
    public partial class AddTovar : Window
    {
      public string path;
      public Bitmap bitmapToBind;
        public AddTovar()
        {
            InitializeComponent();
        }

        public async void AddTovarImg_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog();
            var result =  await _openFileDialog.ShowAsync(this);
            if (result == null) return;
            string path = string.Join("", result);

            if (result != null)
            {
             
                bitmapToBind = new Bitmap(path);
            }
            ImagePath.Source = bitmapToBind;

           
        }


        public void AddTovarOk_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
           
            errorMsg.Text = string.Empty;
            bool checkListProdContainsThisName = false;
            foreach (Product prod in ListPr.ListProd)
            {
                if (nameTov.Text == prod.nameProd)
                {
                    checkListProdContainsThisName = true;
                }
            }
            if (checkListProdContainsThisName)
            {
                errorMsg.Text = "Товар с таким именем уже имеется в каталоге";
            }
            else
            {
                ListPr.ListProd.Add(new Product()
                {
                    Id = ListPr.b,
                    nameProd = nameTov.Text,
                    priceProd = Convert.ToInt32(priceTov.Text),
                    quantityProd = Convert.ToInt32(quantityTov.Text),
                    bitmapProd = bitmapToBind
                });

                ListPr.b++;
                ProductEdit productEdit = new ProductEdit();
                productEdit.Show();
                this.Close();
            }
        }

    }
}

