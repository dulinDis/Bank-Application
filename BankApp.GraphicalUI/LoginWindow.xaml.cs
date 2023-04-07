using ATM_excercise;
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

namespace BankApp.GraphicalUI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>

    public partial class LoginWindow : Window
    {
        private readonly BankService _bankService;

        public LoginWindow(BankService bankService)
        {
            InitializeComponent();
            _bankService = bankService;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = AccountNumberTextBox.Text;

            if (string.IsNullOrEmpty(userInput))
            {
                ErrorTextBlock.Text = "Please enter an account number";
                return;
            } 

            try
            {
                long accountNumber = Utils.ReadLong(userInput);

                Account account = _bankService.LogUserIntoAccount(accountNumber);
                if (account == null)
                {
                    ErrorTextBlock.Text = "Account not found";
                    return;
                }

                // Open the account window with the account object
               // AccountWindow accountWindow = new AccountWindow(account);
               // accountWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = ex.Message;
            }
        }


    }
}
