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

namespace Litvin.WindowsFolder
{
    /// <summary>
    /// Логика взаимодействия для PerfomanceWindow.xaml
    /// </summary>
    public partial class ShipWindow : Window
    {
        DGClass dGClass;
        SqlCommand sqlCommand;
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=(local)\SQLEXPRESS;
                                Initial Catalog=PP03Litvin;
                                Integrated Security=True");
        SqlDataReader dataReader;


        public ShipWindow()
        {
            InitializeComponent();
            dGClass = new DGClass(DataGrd);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[Ship]");
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[Ship]");
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

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            TabCntl.SelectedIndex = 1;
        }

        private void DataGrd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGrd.SelectedItem == null)
            {
                MBClass.ErrorMB("Вы не выбрали строку");
            }
            else
            {
                try
                {
                    VariableClass.CustomerId = dGClass.SelectId();
                    TabCntl.SelectedIndex = 2;
                    dGClass.LoadDG("Select * From dbo.[Ship]");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * from dbo.[Ship] " +
                    $"Where id='{VariableClass.CustomerId}'",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                ShipID2.Text = dataReader[1].ToString();
                ShipName2.Text = dataReader[2].ToString();
                ShipType2.Text = dataReader[3].ToString();
                DateCreate2.Text = dataReader[4].ToString();
                Probeg2.Text = dataReader[5].ToString();
                SeatCount2.Text = dataReader[6].ToString();
                EngineType2.Text = dataReader[7].ToString();
                PrivodType2.Text = dataReader[8].ToString();
                RazmeshenieCorpusa2.Text = dataReader[9].ToString();
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

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[Ship] " +
                $"Where ShipName Like '%{SearchTb.Text}%'");
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ShipID.Text))
            {
                MBClass.ErrorMB("Не введено id корабля");
                ShipID.Focus();
            }
            else if (string.IsNullOrWhiteSpace(ShipName.Text))
            {
                MBClass.ErrorMB("Имя корабля не введено");
                ShipName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(ShipType.Text))
            {
                MBClass.ErrorMB("Тип корабля не введен");
                ShipType.Focus();
            }
            else if (string.IsNullOrWhiteSpace(DateCreate.Text))
            {
                MBClass.ErrorMB("Дата создания не введена");
                DateCreate.Focus();
            }
            else if (string.IsNullOrWhiteSpace(Probeg.Text))
            {
                MBClass.ErrorMB("Пробег не введен");
                Probeg.Focus();
            }
            else if (string.IsNullOrWhiteSpace(SeatCount.Text))
            {
                MBClass.ErrorMB("Кол-во мест не введено");
                SeatCount.Focus();
            }
            else if (string.IsNullOrWhiteSpace(EngineType.Text))
            {
                MBClass.ErrorMB("Тип двигателя не введен");
                EngineType.Focus(); 
            }
            else if (string.IsNullOrWhiteSpace(PrivodType.Text))
            {
                MBClass.ErrorMB("Тип привода не введен");
                PrivodType.Focus(); 
            }
            else if (string.IsNullOrWhiteSpace(RazmeshenieCorpusa.Text))
            {
                MBClass.ErrorMB("Размещение не введено");
                RazmeshenieCorpusa.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into dbo.[Ship] " +
                        "(ShipID, ShipName, ShipType, DateCreate, Probeg, SeatCount, EngineType, " +
                        "PrivodType, RazmeshenieCorpusa) " +
                        $"Values ('{ShipID.Text}'," +
                        $"'{ShipName.Text}'," +
                        $"'{ShipType.Text}'," +
                        $"'{DateCreate.Text}'," +
                        $"'{Probeg.Text}'," +
                        $"'{SeatCount.Text}'," +
                        $"'{EngineType.Text}'," +
                        $"'{PrivodType.Text}'," +
                        $"'{RazmeshenieCorpusa.Text}')",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMB($"Корабль {ShipName.Text} " + " успешно добавлен");
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
            try
            {
                sqlConnection.Open();
                sqlCommand =
                    new SqlCommand("Update " +
                    "dbo.[Ship] " +
                    $"Set ShipID='{ShipID2.Text}'," +
                    $"ShipName='{ShipName2.Text}'," +
                    $"ShipType='{ShipType2.Text}'," +
                    $"DateCreate='{DateCreate2.Text}'," +
                    $"Probeg='{Probeg2.Text}'," +
                    $"SeatCount='{SeatCount2.Text}'," +
                    $"EngineType='{EngineType2.Text}'," +
                    $"PrivodType='{PrivodType2.Text}'," +
                    $"RazmeshenieCorpusa='{RazmeshenieCorpusa2.Text}' " +
                    $"Where id='{VariableClass.CustomerId}'",
                    sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB($"Данные корабля " +
                    $"{ShipID2.Text}" +
                    $" успешно отредактированы");
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
}
