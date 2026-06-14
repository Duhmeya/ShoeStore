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

namespace ShoeStore.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditProductWindow.xaml
    /// </summary>
    public partial class AddEditProductWindow : Window
    {
        public AddEditProductWindow()
        {
            InitializeComponent();
            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            using (var context = new shoestoretext())
            {
                CmbCategory.ItemsSource = context.Categories.ToList();
                CmbManufacturer.ItemsSource = context.Manufacturers.ToList();
                CmbSupplier.ItemsSource = context.Suppiers.ToList();
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new shoestoretext())
            {
                var product = context.Products.Find(TbArticle.Text);
                if (product == null)
                {
                    MessageBox.Show("Товар с таким артикулом не найден!");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(TbName.Text))
                    product.Name = TbName.Text;
                if (!string.IsNullOrWhiteSpace(TbDescription.Text))
                    product.Descriotion = TbDescription.Text;
                if (!string.IsNullOrWhiteSpace(TbPrice.Text) && decimal.TryParse(TbPrice.Text, out decimal price))
                    product.Price = price;
                if (!string.IsNullOrWhiteSpace(TbUnit.Text))
                    product.Unit = TbUnit.Text;
                if (!string.IsNullOrWhiteSpace(TbQuantity.Text) && int.TryParse(TbQuantity.Text, out int qty))
                    product.StockQartity = qty;
                if (!string.IsNullOrWhiteSpace(TbDiscount.Text) && int.TryParse(TbDiscount.Text, out int disc))
                    product.Discount = disc;
                if (CmbCategory.SelectedItem != null)
                    product.CategoryId = ((Category)CmbCategory.SelectedItem).Id;
                if (CmbManufacturer.SelectedItem != null)
                    product.ManufactureId = ((Manufacturer)CmbManufacturer.SelectedItem).Id;
                if (CmbSupplier.SelectedItem != null)
                    product.SuppieId = ((Suppier)CmbSupplier.SelectedItem).Id;
                context.SaveChanges();
                MessageBox.Show("Сохранено!");
                Close();
            }
        }
    }
}
