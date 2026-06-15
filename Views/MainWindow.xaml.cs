using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;
using ShoeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp5;

namespace ShoeStore.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
            SetupIserInterface();
            DisplayProducts(_allProducts);
            //using (var db = new shoestoretext())
            //{
            //    LoadSupplierFilter(db); 
            //}
        }
        
        private List<Product> _allProducts;

        private void LoadProducts()
        {
            using (var db = new shoestoretext())
            {
                _allProducts = db.Products
                   .Include( p => p.Category)
                   .Include(p => p.Suppie)
                   .Include(p => p.Manufacture)
                   .ToList();
            }
            TbStatus.Text = $"товаров: {_allProducts.Count}";
        }


        private void LoadProductImage (Image imageControl, string imagePath)
        {
            try
            {
                if (string.IsNullOrEmpty(imagePath))
                {
                    imageControl.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/picture.jpg"));
                }
                else
                {
                    string fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", imagePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        imageControl.Source = new BitmapImage(new Uri(fullPath));
                    }
                    else
                    {
                        imageControl.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/picture.jpg"));
                    }
                }
            }
            catch {
            imageControl.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/picture.jpg"));
            }

        }
        //отображение товара
        private void DisplayProducts(List<Product> Products) 
        {
        ItemsProducts.Items.Clear();

            foreach (var product in Products) {
                var border = new Border
                {
                    Margin = new Thickness(5),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(200, 200, 200)),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(5),
                    Padding = new Thickness(10),
                };

                if (product.StockQartity == 0)
                {
                    border.Background = new SolidColorBrush(Color.FromRgb(173, 216, 230));
                }
                else if (product.Discount > 15)
                {
                    border.Background = new SolidColorBrush(Color.FromRgb(46, 139, 87));
                }
                else
                {
                    border.Background = Brushes.White;
                }
                
                var mainStack = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                };

                var photoImage = new Image
                {
                    Width = 100, Height = 100,
                    Stretch = Stretch.Uniform,
                    Margin = new Thickness(0, 0, 15, 0)
                };
                LoadProductImage(photoImage, product.Imagepath);
                mainStack.Children.Add(photoImage);

                var infoPanel = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    VerticalAlignment = VerticalAlignment.Center
                };
                var headerPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 0, 10, 0)
                };
                var categoryTextBlock = new TextBlock
                {
                    Text = product.Category? .Name ?? "Без категории",
                    FontWeight = FontWeights.Bold, FontSize = 14,
                    Foreground = new SolidColorBrush(Color.FromRgb(100, 100, 100)),
                    Margin = new Thickness(0, 0, 10, 0)
                };
                headerPanel.Children.Add(categoryTextBlock);

                var nameTextBlack = new TextBlock
                {
                    Text = product.Name,
                    FontWeight = FontWeights.Bold, FontSize = 14,
                };
                headerPanel.Children.Add(nameTextBlack);
                infoPanel.Children.Add(headerPanel);

                var descTextBlock = new TextBlock
                {
                    Text = $"Описание: {product.Descriotion ?? "-"}",
                    FontSize = 12,
                    Foreground = new SolidColorBrush(Color.FromRgb(80, 80, 80)),
                    TextTrimming = TextTrimming.CharacterEllipsis, MaxWidth = 400,
                    Margin = new Thickness(0, 0, 0, 3)
                };
                infoPanel.Children.Add(descTextBlock);

                var manufactureTextBlock = new TextBlock
                {
                    Text = $"Производитель: {product.Manufacture? .Name ?? "-"}",
                    FontSize = 12, Margin = new Thickness(0, 0, 0, 3)
                };
                infoPanel.Children.Add (manufactureTextBlock);

                var SuppierTextBlock = new TextBlock
                {
                    Text = $"Поставщик: {product.Suppie?.Name ?? "-"}",
                    FontSize = 12, Margin = new Thickness(0, 0, 0, 3)
                };
                infoPanel.Children.Add(SuppierTextBlock);

                var pricePanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 0, 0, 3)
                };

                if (product.Discount > 0)
                {
                    var oldPrice = new TextBlock
                    {
                        Text = $"{product.Price:N2} $",
                        TextDecorations = TextDecorations.Strikethrough,
                        Foreground = Brushes.Red, FontSize = 12
                    };
                    pricePanel.Children.Add(oldPrice);

                    decimal discountedPrice = product.Price * (100 - product.Discount);

                    var newPrice = new TextBlock
                    {
                        Text = $"{discountedPrice:N2} $",
                        FontWeight = FontWeights.Bold, FontSize = 13,
                        Margin = new Thickness(5, 0, 0, 0)
                    };
                    pricePanel.Children.Add(newPrice);
                }
                else {
                    var price = new TextBlock
                    {
                        Text = $"Цена: {product.Price:N2} $", FontSize= 12
                    };
                    pricePanel.Children.Add(price);

                    var unitTextBlock = new TextBlock
                    {
                        Text = $"Еденица измерения: {product.Unit}",
                        FontSize = 12, Margin = new Thickness(0, 0, 0, 3)
                    };
                    infoPanel.Children.Add(unitTextBlock);

                    var quantityTextBlock = new TextBlock 
                    {
                        Text = $"Количество на складе: {product.StockQartity}",
                        FontSize = 12, Foreground = product.StockQartity == 0
                        ? Brushes.Red :
                        new SolidColorBrush(Color.FromRgb(0, 100, 0))
                    };
                    infoPanel.Children.Add(quantityTextBlock);
                    mainStack.Children.Add(infoPanel);
                } 

                //var discountPanel = new StackPanel
                //{
                //    Orientation = Orientation.Vertical,
                //    VerticalAlignment = VerticalAlignment.Center,
                //    Margin = new Thickness (20, 0, 10, 0)
                //};
                //if (product.Discount > 0)
                //{
                //    var discountBadge = new Border
                //    {
                //        Background = new SolidColorBrush(Color.FromRgb(220, 50, 50)),
                //        CornerRadius = new CornerRadius(3),
                //        Padding = new Thickness(8, 3, 8, 3),
                //        HorizontalAlignment = HorizontalAlignment.Center,
                //    };
                //    var discountText = new TextBlock
                //    {
                //        Text = $"-{product.Discount}%",
                //        Foreground = Brushes.White, FontSize = 14,
                //        FontWeight = FontWeights.Bold
                //    };
                //    discountBadge.Child = discountText;
                //    discountPanel.Children.Add(discountBadge);

                //    var discountLabel = new TextBlock
                //    {
                //        Text = "Действующая\nскидка", FontSize = 11,
                //        Foreground = new SolidColorBrush(Color.FromRgb(120, 120, 120)),
                //        Margin = new Thickness(0, 5, 0, 0)
                //    };
                //    discountPanel.Children.Add(discountLabel);
                //}
                //else {

                //    var noDiscount = new TextBlock
                //    {
                //        Text = "-",
                //        FontSize= 14,
                //        Foreground = new SolidColorBrush(Color.FromRgb(180, 180, 180)),
                //        VerticalAlignment = VerticalAlignment.Center
                //    };
                //    discountPanel.Children.Add (noDiscount);
                //}
                //mainStack.Children.Add(discountPanel);

                if (IsCurrentUserAdmin())
                {
                    var actionPanel = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(15, 0, 0, 0)
                    };

                    var editButton = new Button
                    {
                        Content = "Edit", Width = 100, Height = 30, FontSize = 12,
                        Background = new SolidColorBrush(Color.FromRgb(70, 130, 180)),
                        Foreground = Brushes.White, Tag = product, Cursor = Cursors.Hand,
                        Margin = new Thickness(0, 0, 0, 5)
                    };
                    editButton.Click += EditButton_Click;
                    actionPanel.Children.Add(editButton);
                    var deleteButton = new Button
                    {
                        Content = "Delete", Width = 100, Height = 30, FontSize = 12,
                        Background = new SolidColorBrush(Color.FromRgb(200, 60, 60)),
                        Foreground = Brushes.White, Tag = product, Cursor = Cursors.Hand,
                    };
                    deleteButton.Click += DeleteButton_Click;
                    actionPanel.Children.Add(deleteButton);
                    mainStack.Children.Add(actionPanel);
                };
                border.Child = mainStack;
                ItemsProducts.Items.Add(border);
            }
        }

        private bool IsCurrentUserAdmin()
        {
          return App.CurrentUser != null && App.CurrentUser.RoleId == 1;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var addEditProductWindow = new AddEditProductWindow();
            addEditProductWindow.Show();
           
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button)?.Tag as Product;
            var result = MessageBox.Show($"Удалить {product.Name}?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                using (var context = new shoestoretext())
                {
                    context.Products.Remove(context.Products.Find(product.Article));
                    context.SaveChanges();
                }
                MessageBox.Show("Удалено!");
            }
        }

        private void BtnExit_Click (object sender, RoutedEventArgs e)
        {
            App.CurrentUser = null;
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();  
        }

        private void SetupIserInterface()
        {
            if (App.CurrentUser == null)
            {
                TbUserName.Text = "Гость";
                FilterPanel.Visibility = Visibility.Collapsed;
                return;
            }
            TbUserName.Text = $"{App.CurrentUser.Fullname}";
        }

        //private void LoadSupplierFilter(shoestoretext db)
        //{
        //    CmbSupplier.Items.Clear();
        //    CmbSupplier.Items.Add("Все поставщики");
        //    var suppliers = db.Suppiers
        //    .Select(s => s.Name)
        //        .Distinct()
        //        .OrderBy(s => s)
        //        .ToList();
        //    foreach (var supplier in suppliers)
        //    {
        //        CmbSupplier.Items.Add(supplier);
        //    }
        //    CmbSupplier.SelectedIndex = 0;
        //}
        //private void CmbSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ApplyFiltersAndSort();
        //}

        //private void ApplyFiltersAndSort()
        //{
        //    if (_allProducts == null) return;
        //    IEnumerable<Product> filtered = _allProducts.AsEnumerable();
        //    string searchTerm = TbSearch.Text?.Trim().ToLower() ?? "";
        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        filtered = filtered.Where(p =>
        //        (p.Name?.ToLower().Contains(searchTerm) ?? false) ||
        //        (p.Descriotion?.ToLower().Contains(searchTerm) ?? false) ||
        //        (p.Article?.ToLower().Contains(searchTerm) ?? false) ||
        //        (p.Category?.Name?.ToLower().Contains(searchTerm) ?? false) ||
        //        (p.Manufacture?.Name?.ToLower().Contains(searchTerm) ?? false) ||
        //        (p.Suppie?.Name?.ToLower().Contains(searchTerm) ?? false)
        //        );
        //    }
        //}
    }
}
