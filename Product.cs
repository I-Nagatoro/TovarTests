using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace TovarV2
{
    public class Product
    {
        public int Id { get; set; }
        public string nameProd { get; set; }
        public int priceProd { get; set; }
        public int quantityProd { get; set; }
        public Bitmap bitmapProd { get; set; }
    }

    public static class ListPr
    {
        public static int b = 0;
        public static List<Product> ListProd = new List<Product>();
        public static List<Product> SelectedListProd = new List<Product>();
        public static int codeUser = new int();
        public static List<ProductSelect> productSelects = new List<ProductSelect>();
        public static int productForEdit = new int();
    }
}
