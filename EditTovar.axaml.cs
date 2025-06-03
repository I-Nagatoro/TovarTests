using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System;

namespace TovarV2;

public partial class EditTovar : Window
{
    public Bitmap bitmapToBind;
    public EditTovar()
    {
         InitializeComponent();
        ImagePath.Source = ListPr.ListProd[ListPr.productForEdit].bitmapProd; 
        nameTovar.Text = ListPr.ListProd[ListPr.productForEdit].nameProd;
        priceTovar.Text = ListPr.ListProd[ListPr.productForEdit].priceProd.ToString();
        quantityTovar.Text = ListPr.ListProd[ListPr.productForEdit].quantityProd.ToString();
    }
    public async void AddTovarImg_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        OpenFileDialog _openFileDialog = new OpenFileDialog();
       
        var result = await _openFileDialog.ShowAsync(this);
        if (result == null) return;
        string path = string.Join("", result);

        if (result != null)
        {

            bitmapToBind = new Bitmap(path);
        }
        ImagePath.Source = bitmapToBind;


    }

    public void EditOk_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ListPr.ListProd[ListPr.productForEdit].nameProd = nameTovar.Text;
        ListPr.ListProd[ListPr.productForEdit].quantityProd = Convert.ToInt32(quantityTovar.Text);
        ListPr.ListProd[ListPr.productForEdit].priceProd = Convert.ToInt32(priceTovar.Text);
        ListPr.ListProd[ListPr.productForEdit].bitmapProd = bitmapToBind;
        ProductEdit productEdit = new ProductEdit();
        productEdit.Show();
        this.Close();
    }
}