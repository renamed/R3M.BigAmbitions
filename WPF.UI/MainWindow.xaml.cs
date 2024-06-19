using System.Windows;

namespace WPF.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnInsertWarehouse_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Insert warehouse", AppDomain.CurrentDomain.FriendlyName, MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void BtnInsertBusiness_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Insert business", AppDomain.CurrentDomain.FriendlyName, MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void BtnInsertProduct_Click(object sender, RoutedEventArgs e)
    { 
        MessageBox.Show("Insert business", AppDomain.CurrentDomain.FriendlyName, MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}