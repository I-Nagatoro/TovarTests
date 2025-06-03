using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Layout;
using Avalonia.Media.Imaging;

namespace TovarV2
{
    public class ProductSelect
    {
        
        public int Id { get; set; } 
        public string nameProdKorz { get; set; }
        public int priceProdKorz { get; set; }
        public int quantityProdKorz { get; set; }
        public Bitmap bitmapProdkorz { get; set; }
        public int quantitySelect { get; set; }
    }
}
