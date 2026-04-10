using Lab2;
using Microsoft.EntityFrameworkCore;
namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Add_rates(object sender, EventArgs e)
        {
            DB_operations operations = new DB_operations();
            if (!string.IsNullOrEmpty(From.Text) && !string.IsNullOrEmpty(To.Text))
            {
                Rate_label.Text= await operations.Add(From.Text, To.Text);
            }
             
        }
        private void show_currencies(object sender, EventArgs e)
        {
            DB_operations operations = new DB_operations();
            CurrenciesCollectionView.ItemsSource = operations.show_currency();
        }
        private void show_rate(object sender, EventArgs e)
        {
            DB_operations operations = new DB_operations();
            CurrenciesCollectionView.ItemsSource = operations.show_rates();
        }

        private void add_to_database(object sender, EventArgs e)
        {
            DB_operations operations = new DB_operations();
            if (!string.IsNullOrEmpty(Code_entry.Text) && !string.IsNullOrEmpty(Rate_entry.Text))
            {
                Add_label.Text = operations.add_to_db(Code_entry.Text, decimal.Parse(Rate_entry.Text));
            }


        }
        private void show_sort_down(object sender, EventArgs e)
        {
            DB_operations operations = new DB_operations();
            CurrenciesCollectionView.ItemsSource = operations.show_sorted_down();


        }
        private void show_sort_up(object sender, EventArgs e)
        {
            DB_operations operations = new DB_operations();
            CurrenciesCollectionView.ItemsSource = operations.show_sorted_up();


        }
        private void show_filtered(object sender, EventArgs e)
        {
            DB_operations operations = new DB_operations();
            if (!string.IsNullOrEmpty(filtr_entry.Text))
            {
                CurrenciesCollectionView.ItemsSource = operations.show_filtered(filtr_entry.Text);
            }
            


        }
    }

}
