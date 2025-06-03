using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Interactivity;

namespace TovarV2;

public partial class Korzina : Window
{
    public int b = 0;
    public int quantityCount = 0;
    public string codeUs;
    public ListBox quantitylistBox = new ListBox();
    public List<Product> products = new List<Product>();
    public List <ProductSelect> currentProdSel = new List<ProductSelect>();

    public Korzina()
    {
        InitializeComponent();
        A();


        int i = 0;
        if (ListPr.productSelects.Count >= 2)
        {
            backBtn.IsVisible = false;
            nextBtn.IsVisible = true;
            for (i = 0; i < 2; i++)
            {
                currentProdSel.Add(ListPr.productSelects[i]);
            }
        }
        else
        {
            currentProdSel = ListPr.productSelects;
        }
        if (currentProdSel.Count != 0)
        {
            int allPages = (int)Math.Ceiling((decimal)(ListPr.productSelects.Count / 2));
            if (ListPr.productSelects.Count % 2 == 1)
            {
                allPages += 1;

            }
            if (allPages == 1)
            {
                nextBtn.IsVisible = false;
                backBtn.IsVisible = false;

            }
            int currPage = (int)Math.Ceiling((decimal)ListPr.productSelects.IndexOf(currentProdSel[0]) / 2) + 1;

            pageNum.Text = currPage.ToString() + "/" + allPages;

        }
        else
        {
            backBtn.IsVisible = false;
            nextBtn.IsVisible = false;
        }

        if (currentProdSel.Count == 1)
        {
            if (currentProdSel[0].nameProdKorz == ListPr.productSelects[ListPr.productSelects.Count - 1].nameProdKorz)
            {
                nextBtn.IsVisible = false;
            }
        }
        else if (currentProdSel.Count == 2)
        {
            if ((currentProdSel[1].nameProdKorz == ListPr.productSelects[ListPr.productSelects.Count - 1].nameProdKorz) || (currentProdSel[0].nameProdKorz == ListPr.productSelects[ListPr.productSelects.Count - 1].nameProdKorz))
            {
                nextBtn.IsVisible = false;
            }
        }
        
        ProdListInKorz.ItemsSource = currentProdSel.ToList();
    }
    public void A()
    {
        ProdListInKorz.ItemsSource = ListPr.productSelects.Select(x => new
        {
            x.Id,
            x.nameProdKorz,
            x.priceProdKorz,
            x.quantityProdKorz,
            x.bitmapProdkorz,
            x.quantitySelect, 
        }).ToList();
    }
    public void PagesConfig()
    {

        int i = 0;
        if (ListPr.productSelects.Count >= 2)
        {
            backBtn.IsVisible = false;
            nextBtn.IsVisible = true;
            for (i = 0; i < 2; i++)
            {
                currentProdSel.Add(ListPr.productSelects[i]);
            }
        }
        else
        {
            currentProdSel = ListPr.productSelects;
        }
        if (currentProdSel.Count != 0)
        {
            int allPages = (int)Math.Ceiling((decimal)(ListPr.productSelects.Count / 2));
            if (ListPr.productSelects.Count % 2 == 1)
            {
                allPages += 1;

            }
            if (allPages == 1)
            {
                nextBtn.IsVisible = false;
                backBtn.IsVisible = false;

            }
            int currPage = (int)Math.Ceiling((decimal)ListPr.productSelects.IndexOf(currentProdSel[0]) / 2) + 1;

            pageNum.Text = currPage.ToString() + "/" + allPages;

        }
       
        else
        {
            backBtn.IsVisible = false;
            nextBtn.IsVisible = false;
        }
        if ((currentProdSel.Count == 1 && ListPr.productSelects.IndexOf(currentProdSel[0]) == ListPr.productSelects.Count - 1) ||
            (currentProdSel.Count == 2 && ListPr.productSelects.IndexOf(currentProdSel[1]) == ListPr.productSelects.Count - 1))
        {
            nextBtn.IsVisible = false;
        }
        ProdListInKorz.ItemsSource = currentProdSel.ToList();
    }

    public void PrevPage_OnClick(object? sender, RoutedEventArgs e)
    {
        int i = ListPr.productSelects.IndexOf(currentProdSel[0]);
        currentProdSel.Clear();
        if (i >= 2)
        {
            if (i - 1 == 1)
            {
                backBtn.IsVisible = false;
            }
            else
            {
                backBtn.IsVisible = true;
            }
            nextBtn.IsVisible = true;
            for (int j = i - 2; j <= i - 1; j++)
            {
                currentProdSel.Add(ListPr.productSelects[j]);
            }
        }
        else
        {
            backBtn.IsVisible = false;
            nextBtn.IsVisible = true;
        }
        int allPages = (int)Math.Ceiling((decimal)(ListPr.productSelects.Count / 2));
        if (ListPr.productSelects.Count % 2 == 1)
        {
            allPages += 1;

        }
        if (allPages == 1)
        {
            nextBtn.IsVisible = false;
            backBtn.IsVisible = false;
        }
        int currPage = (int)Math.Ceiling((decimal)ListPr.productSelects.IndexOf(currentProdSel[0]) / 2) + 1;
        if ((currentProdSel.Count == 1 && ListPr.productSelects.IndexOf(currentProdSel[0]) == ListPr.productSelects.Count - 1) ||
            (currentProdSel.Count == 2 && ListPr.productSelects.IndexOf(currentProdSel[1]) == ListPr.productSelects.Count - 1))
        {
            nextBtn.IsVisible = false;
        }
        pageNum.Text = currPage.ToString() + "/" + allPages;
        ProdListInKorz.ItemsSource = currentProdSel.ToList();
    }

   
    public void NextPage_OnClick(object? sender, RoutedEventArgs e)
    {

        int i = ListPr.productSelects.IndexOf(currentProdSel[currentProdSel.Count - 1]);
        currentProdSel.Clear();
        if (ListPr.productSelects.Count - 1 - i >= 2)
        {
            backBtn.IsVisible = true;
            nextBtn.IsVisible = true;
            for (int j = i + 1; j < i + 3; j++)
            {
                currentProdSel.Add(ListPr.productSelects[j]);
            }
        }
        else if (ListPr.productSelects.Count - 1 - i == 1)
        {
            currentProdSel.Add(ListPr.productSelects[ListPr.productSelects.Count - 1]);
            nextBtn.IsVisible = false;
            backBtn.IsVisible = true;
        }
        else
        {
            currentProdSel.Clear();
            nextBtn.IsVisible = false;
            backBtn.IsVisible = true;
        }
        int allPages = (int)Math.Ceiling((decimal)(ListPr.productSelects.Count / 2));
        if (ListPr.productSelects.Count % 2 == 1)
        {
            allPages += 1;

        }
        if (allPages == 1)
        {
            nextBtn.IsVisible = false;
            backBtn.IsVisible = false;

        }
        int currPage = (int)Math.Ceiling((decimal)ListPr.productSelects.IndexOf(currentProdSel[0]) / 2) + 1;
      
            if ((currentProdSel.Count == 1 && ListPr.productSelects.IndexOf(currentProdSel[0]) == ListPr.productSelects.Count-1) ||
            (currentProdSel.Count == 2 && ListPr.productSelects.IndexOf(currentProdSel[1]) == ListPr.productSelects.Count-1))
            {
                nextBtn.IsVisible = false;
            }
        pageNum.Text = currPage.ToString() + "/" + allPages;
        ProdListInKorz.ItemsSource = currentProdSel.ToList();
    }
    public void DelBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        int ind = (int)((sender as Button)!).Tag!;

        ListPr.productSelects.RemoveAt(ind);
        
        if (ListPr.productSelects.Count > 0)
        {
            b = 0;
            foreach (ProductSelect prSel in ListPr.productSelects)
            {
                prSel.Id = b;
                b++;
            }
        }
        currentProdSel.Clear();
        this.PagesConfig();
    }

    public void ReturnProdEdit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ProductEdit productEdit = new ProductEdit();
        productEdit.Show();
        this.Close();
    }

    public void PodschetOrderBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        bool checkDataCorrect = true;
        int allprice = 0;
        int quantity;

        foreach (ProductSelect productSelect in ListPr.productSelects)
        {
            allprice += productSelect.priceProdKorz * productSelect.quantitySelect;
            
        }

       
        podschetstoimosti.Text = "Общая стоимость составляет: " + allprice.ToString() + " руб.";
        
    }


    public void Exit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ListPr.SelectedListProd.Clear();
        ListPr.productSelects.Clear();
        MainWindow mainwindow = new MainWindow();
        mainwindow.Show();
        this.Close();
    }

    public void UvelBtn_OnClick(object? sender, RoutedEventArgs e)
    {
      
       int ind = (int)((sender as Button)!).Tag!;
      ListPr.productSelects[ind].quantitySelect++;
      ListPr.productSelects[ind].quantitySelect = CheckQuantity(ListPr.productSelects[ind].quantitySelect,
        ListPr.productSelects[ind].quantityProdKorz);

        ProdListInKorz.ItemsSource = currentProdSel.ToList();
    }
    public void UmenBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        int ind = (int)((sender as Button)!).Tag!;
        ListPr.productSelects[ind].quantitySelect--;
        ListPr.productSelects[ind].quantitySelect = CheckQuantity(ListPr.productSelects[ind].quantitySelect,
            ListPr.productSelects[ind].quantityProdKorz);
        ProdListInKorz.ItemsSource = currentProdSel.ToList();
    }
    public int CheckQuantity(int quantitySel, int quantityMagaz)
    {
        bool check = true;
        if (quantitySel < 1)
        {
            quantitySel = 1;
        }
        else if (quantitySel > quantityMagaz)
        {
            quantitySel = quantityMagaz;
            podschetstoimosti.Text = "Ошибка. Введенное количество товара больше имеющегося в магазине";
        }
        else
        {
            podschetstoimosti.Text = "";
        }
        return quantitySel;
    }

}