using Litvin.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using Litvin.WindowsFolder;

namespace Litvin.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=(local)\SQLEXPRESS;
                                Initial Catalog=PP03Litvin;
                                Integrated Security=True");
        SqlCommand sqlCommand;
        DGClass dGClass;
        CBClass cB = new CBClass();
        SqlDataReader dataReader;

        public AdminWindow()
        {
            InitializeComponent();
            dGClass = new DGClass(DataGrd);
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGrd.SelectedItem == null)
            {
                MBClass.ErrorMB("Вы не выбрали строку");
            }
            else
            {
                try
                {
                    VariableClass.UserId = dGClass.SelectId();
                    TabCntl.SelectedIndex = 2;
                    dGClass.LoadDG("Select * From dbo.[User]");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * from dbo.[User] " +
                    $"Where UserID='{VariableClass.UserId}'",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                loginTb2.Text = dataReader[1].ToString();
                PassTb2.Text = dataReader[2].ToString();
                RoleCb.SelectedValue = dataReader[3].ToString();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass = PassTb.Text;
            string zagl = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string cif = "123457890";
            string znak = "!@#$%^&*()_+";

            if (string.IsNullOrWhiteSpace(loginTb.Text))
            {
                MBClass.ErrorMB("Некоректный логин");
                loginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PassTb.Text))
            {
                MBClass.ErrorMB("Некоректный пароль");
                PassTb.Focus();
            }
            else if (zagl.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать заглавную букву");
                PassTb.Focus();
            }
            else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать маленькую букву");
                PassTb.Focus();
            }
            else if (cif.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать цифру");
                PassTb.Focus();
            }
            else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать " +
                    "один из этих знаков " + znak);
                PassTb.Focus();
            }
            else if (RoleCb2.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Не выбрана роль");
                RoleCb2.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into dbo.[User] " +
                        "(Login, Password, RoleID) " +
                        $"Values ('{loginTb.Text}'," +
                        $"'{PassTb.Text}'," +
                        $"'{RoleCb2.SelectedValue.ToString()}')",
                    sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB($"Пользователь {loginTb.Text} " +
                        $"успешно добавлен");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        private void RedBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass = PassTb2.Text;
            string zagl = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string cif = "123457890";
            string znak = "!@#$%^&*()_+";

            if (string.IsNullOrWhiteSpace(loginTb2.Text))
            {
                MBClass.ErrorMB("Некоректный логин");
                loginTb2.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PassTb2.Text))
            {
                MBClass.ErrorMB("Некоректный пароль");
                PassTb2.Focus();
            }
            else if (zagl.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать заглавную букву");
                PassTb2.Focus();
            }
            else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать маленькую букву");
                PassTb2.Focus();
            }
            else if (cif.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать цифру");
                PassTb2.Focus();
            }
            else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMB("Пароль должен содержать " +
                    "один из этих знаков " + znak);
                PassTb2.Focus();
            }
            else if (RoleCb.SelectedIndex == -1)
            {
                MBClass.ErrorMB("Не выбрана роль");
                RoleCb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand =
                        new SqlCommand("Update " +
                        "dbo.[User] " +
                        $"Set [Login] ='{loginTb2.Text}'," +
                        $"[Password]='{PassTb2.Text}'," +
                        $"RoleId='{RoleCb.SelectedValue.ToString()}' " +
                        $"Where UserID='{VariableClass.UserId}'",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB($"Данные пользователя " +
                        $"{loginTb2.Text}" +
                        $"успешно отредактированы");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            TabCntl.SelectedIndex = 1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[User]");
            cB.RoleCBLoad(RoleCb);
            cB.RoleCBLoad(RoleCb2);
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[User] " +
                $"Where Login Like '%{SearchTb.Text}%'");
        }

        private void BackIm_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void ExitIm_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[User]");
        }
    }
}
