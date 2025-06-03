using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using System;
using Avalonia.Input;
using Avalonia.Layout;

namespace TovarV2;

public partial class ProductEdit : Window
{
    public string codeUs;
    public TextBlock textblock = new TextBlock();
    public TextBox textbox = new TextBox();
    public TextBox textbox2 = new TextBox();
    public TextBox textbox3 = new TextBox();
    public Button btnInsertOk = new Button();
    public ListBox list = new ListBox();
    public int a = 0;
    public List<Product> matchingProds = new List<Product>();
    public int currentPage = 0;
    public List<Product> currentProd = new List<Product>();

    public ProductEdit()
    {
        InitializeComponent();
        ProdList.SelectionMode = SelectionMode.Single;
        
        this.PagesConfig(ListPr.ListProd);
        switch (ListPr.codeUser)
        {
            case 1:
                EditTovarBtn.IsVisible = false;
                AddElementBtn.IsVisible = false;
                Grid.SetColumn(GoToKorzinaBtn, 0);
                Grid.SetRow(GoToKorzinaBtn, 2);
                GoToKorzinaBtn.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
                break;
            case 0:
                EditTovarBtn.IsVisible=true;
                AddElementBtn.IsVisible = true;
                Grid.SetColumn(GoToKorzinaBtn, 1);
                Grid.SetRow(GoToKorzinaBtn, 2);
                GoToKorzinaBtn.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
                break;

        }
    }


    public void PrevPage_OnClick(object? sender, RoutedEventArgs e)
    {
        int i = ListPr.ListProd.IndexOf(currentProd[0]);
        currentProd.Clear();
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
                currentProd.Add(ListPr.ListProd[j]);
            }
        }
        else
        {
            backBtn.IsVisible = false;
            nextBtn.IsVisible = true;
        }
        int allPages = (int)Math.Ceiling((decimal)(ListPr.ListProd.Count / 2));
        if (ListPr.ListProd.Count % 2 == 1)
        {
            allPages += 1;
        
        }
        if (allPages == 1)
        {
            nextBtn.IsVisible = false;
            backBtn.IsVisible = false;

        }
        int currPage = (int)Math.Ceiling((decimal)ListPr.ListProd.IndexOf(currentProd[0]) / 2)+1;

        pageNum.Text = currPage.ToString() + "/" + allPages;
        if (currPage == allPages && currPage > 1)
        {
            nextBtn.IsVisible = false;
            backBtn.IsVisible = true;
        }

        else if (currPage == 1 && allPages > 1)
        {
            nextBtn.IsVisible = true;
            backBtn.IsVisible = false;
        }
        else if (currPage == 1 && allPages == 1)
        {
            nextBtn.IsVisible = false;
            backBtn.IsVisible = false;
        }
        else if (currPage > 1 && currPage < allPages)
        {
            nextBtn.IsVisible = true;
            backBtn.IsVisible = true;
                
        }
        ProdList.ItemsSource = currentProd.ToList();
    }
    public void DelBtn_Click(object? sender, RoutedEventArgs e)
    {
        int ind = (int)((sender as Button)!).Tag!;

        foreach (ProductSelect prKorz in ListPr.productSelects)
        {
            if (prKorz.nameProdKorz == ListPr.ListProd[ind].nameProd)
            {
                ListPr.productSelects.Remove(prKorz);
                break;
            }
        }
        ListPr.b = 0;
        foreach (ProductSelect prSel in ListPr.productSelects)
        {
            prSel.Id = ListPr.b;
            ListPr.b++;
        }
        ListPr.ListProd.RemoveAt(ind);
        ListPr.b = 0;
        if (ListPr.ListProd.Count > 0)
        {
            ListPr.b = 0;
            foreach (Product prSel in ListPr.ListProd)
            {
                prSel.Id = ListPr.b;
                ListPr.b++;
            }
        }
        currentProd.Clear();
        this.PagesConfig(ListPr.ListProd);
    }
    public void PagesConfig(List<Product> prods)
    {
        int currPage;
        int allPages;
        int i = 0;
        if (prods.Count >= 2)
        {
            backBtn.IsVisible = false;
            nextBtn.IsVisible = true;
            for (i = 0; i < 2; i++)
            {
                currentProd.Add(prods[i]);
            }
        }
        else
        {
            currentProd = prods;
        }
        if (currentProd.Count != 0)
        {
             allPages = (int)Math.Ceiling((decimal)(prods.Count / 2));
            if (prods.Count % 2 == 1)
            {
                allPages += 1;

            }
            if (allPages == 1)
            {
                nextBtn.IsVisible = false;
                backBtn.IsVisible = false;

            } 
            currPage = (int)Math.Ceiling((decimal)prods.IndexOf(currentProd[0]) / 2) + 1;

            pageNum.Text = currPage.ToString() + "/" + allPages;
            if (currPage == allPages && currPage > 1)
            {
                nextBtn.IsVisible = false;
                backBtn.IsVisible = true;
            }

            else if (currPage == 1 && allPages > 1)
            {
                nextBtn.IsVisible = true;
                backBtn.IsVisible = false;
            }
            else if (currPage == 1 && allPages == 1)
            {
                nextBtn.IsVisible = false;
                backBtn.IsVisible = false;
            }
            else if (currPage > 1 && currPage < allPages)
            {
                nextBtn.IsVisible = true;
                backBtn.IsVisible = true;
                
            }
        }
        else
        {
            backBtn.IsVisible = false;
            nextBtn.IsVisible = false;
        }
       

        ProdList.ItemsSource = currentProd.ToList();
    }

    public void NextPage_OnClick(object? sender, RoutedEventArgs e)
    {
        int i = ListPr.ListProd.IndexOf(currentProd[currentProd.Count - 1]);
        currentProd.Clear();
        if (ListPr.ListProd.Count - 1 - i >= 2)
        {
            backBtn.IsVisible=true;
            nextBtn.IsVisible = true;
            for (int j = i + 1; j < i + 3; j++)
            {
                currentProd.Add(ListPr.ListProd[j]);
            }
        }
        else if (ListPr.ListProd.Count - 1 - i == 1)
        {
            currentProd.Add(ListPr.ListProd[ListPr.ListProd.Count - 1]);
            nextBtn.IsVisible = false;
            backBtn.IsVisible = true;
        }
        else
        {
            currentProd.Clear();
            nextBtn.IsVisible = false;
            backBtn.IsVisible = true;
        }
        int allPages = (int)Math.Ceiling((decimal)(ListPr.ListProd.Count / 2));
        if (ListPr.ListProd.Count % 2 == 1)
        {
            allPages += 1;

        }
        if (allPages == 1)
        {
            nextBtn.IsVisible = false;
            backBtn.IsVisible = false ;
        
        }
        int currPage = (int)Math.Ceiling((decimal)ListPr.ListProd.IndexOf(currentProd[0]) / 2) + 1;
        if (currPage == allPages && currPage > 1)
        {
            nextBtn.IsVisible = false;
            backBtn.IsVisible = true;
        }

        else if (currPage == 1 && allPages > 1)
        {
            nextBtn.IsVisible = true;
            backBtn.IsVisible = false;
        }
        else if (currPage == 1 && allPages == 1)
        {
            nextBtn.IsVisible = false;
            backBtn.IsVisible = false;
        }
        else if (currPage > 1 && currPage < allPages)
        {
            nextBtn.IsVisible = true;
            backBtn.IsVisible = true;
                
        }
        pageNum.Text = currPage.ToString() + "/" + allPages;
        ProdList.ItemsSource = currentProd.ToList();
    }
    public void BtnInsert_OnClick(object? sender, RoutedEventArgs e)
    {
        AddTovar addTovar = new AddTovar();
        addTovar.Show();
        this.Close();
    }
    public void BtnEditTovar_OnClick(object? sender, RoutedEventArgs e)
    {
        ListPr.productForEdit = ListPr.ListProd.IndexOf(ProdList.SelectedItem as Product);
        bool checkKorzContainsItem = false;
        foreach (ProductSelect productSelect in ListPr.productSelects)
        {
            if (productSelect.nameProdKorz == ListPr.ListProd[ListPr.productForEdit].nameProd)
            {
                checkKorzContainsItem = true;
                break;
            }
        }
        if (checkKorzContainsItem == true)
        {
            TextBlock errorMsg = new TextBlock();
            errorMsg.Text = "Уберите сначала товар из корзины";
            editing.Children.Add(errorMsg);
        }
        else
        {
            EditTovar editTovar = new EditTovar();
            editTovar.Show();
            this.Close();
        }
    }

    public void AddToKorzBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        bool checkRepeat = true;

        int ind = (int)((sender as Button)!).Tag!;

        for (int i = 0; i < ListPr.productSelects.Count; i++)
        {
            if (ListPr.ListProd[ind].nameProd == ListPr.productSelects[i].nameProdKorz)
            {
                checkRepeat = false;
                break;
            }
        }

        
        if (checkRepeat)
        {
            if (ListPr.productSelects.Count > 0)
            {
                ListPr.productSelects.Add(
                    new ProductSelect()
                    {
                        Id = a,
                        nameProdKorz = ListPr.ListProd[ind].nameProd,
                        priceProdKorz = ListPr.ListProd[ind].priceProd,
                        quantityProdKorz = ListPr.ListProd[ind].quantityProd,
                        bitmapProdkorz = ListPr.ListProd[ind].bitmapProd,
                        quantitySelect = 1,
                    });
                
                a = 0;
                foreach (ProductSelect prSel in ListPr.productSelects)
                {
                    prSel.Id = a;
                    a++;
                }
                
            }
            else
            {
                ListPr.productSelects.Add(
                    new ProductSelect()
                    {
                        Id = a,
                        nameProdKorz = ListPr.ListProd[ind].nameProd,
                        priceProdKorz = ListPr.ListProd[ind].priceProd,
                        quantityProdKorz = ListPr.ListProd[ind].quantityProd,
                        bitmapProdkorz = ListPr.ListProd[ind].bitmapProd,
                        quantitySelect = 1,
                    });
            }
                a++;
        }
    }

        public void BtnKorzina_OnClick(object? sender, RoutedEventArgs e)
    {
        List<Product> prods = new List<Product>();
        prods = ProdList.SelectedItems.Cast<Product>().ToList();

       
        Korzina korzinanew = new Korzina();
        korzinanew.Show();
        Close();

    }
    public void BtnVyhod_OnClick(object? sender, RoutedEventArgs e)
    {
        ListPr.SelectedListProd.Clear();
        ListPr.productSelects.Clear();
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }

  

    public void PoiskTov_OnKeyUp(object? sender, KeyEventArgs e)
    {
        if (FindProd.Text != null || FindProd.Text != "")
        {
            string tovname = FindProd.Text;
            foreach (Product pr in ListPr.ListProd)
            {
           
                if (pr.nameProd.Contains(tovname)) 
                {
                    matchingProds.Add(pr);
                }
           
            }
            currentProd.Clear();
            this.PagesConfig(matchingProds);
        }
        else
        {
            currentProd.Clear();
            this.PagesConfig(ListPr.ListProd);
        }

    }
}