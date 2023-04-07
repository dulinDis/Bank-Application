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
using System.Windows.Navigation;
using System.Windows.Shapes;

using ATM_excercise;

namespace BankApp.GraphicalUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BankService bank;

        public MainWindow()
        {
            InitializeComponent();
            bank = new BankService();

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            CreateAccountWindow createAccountWindow = new CreateAccountWindow(bank);
            createAccountWindow.Show();
            this.Close();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("account create");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
