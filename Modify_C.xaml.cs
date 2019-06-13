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
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// Modify_C.xaml 的交互逻辑
    /// </summary>
    public partial class Modify_C : Window
    {
        public Modify_C()
        {
            InitializeComponent();
        }
        private void Properity_delete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection mycon = null; //建立一个新的连接
            try
            {
                double test1 = double.Parse(this.property_C.Text);
                if (test1 != 0)
                { MessageBox.Show("请输入具有正确格式的内容"); }
                
            }
            catch
            {
                string field_name = this.property_C.Text;
                string field_type = this.field_type.Text;
                string add_field_sql = "alter table C drop column " + field_name + " ;";
                try
                {
                    mycon = new SqlConnection(ConnectString.Cstr);
                    SqlCommand cmd = new SqlCommand(add_field_sql, mycon);
                    mycon.Open();
                    cmd.ExecuteNonQuery();    //执行sql 语句
                    this.Imessage.Content = "列:" + field_name + "删除成功!";
                    foreach (Window window in Application.Current.Windows)
                    {
                        if(window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).Treeview();
                        }
                    }
                }
                catch (Exception ex)
                {
                    string a = ex.Message;
                    MessageBox.Show("列删除失败,请检查输入格式!"+a);
                }
            }
            finally
            {
                mycon.Close();
            }

        }

        private void Properity_add_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection mycon = null; //建立一个新的连接
            try
            {
                double test1 = double.Parse(this.property_C.Text);
                double test2 = double.Parse(this.field_type.Text);
                if (test1 != 0 | test2 != 0)
                { MessageBox.Show("请输入具有正确格式的内容"); }
            }
            catch
            {
                string field_name = this.property_C.Text;
                string field_type = this.field_type.Text;
                string add_field_sql = "alter table C add " + field_name + " " + field_type + " ;";
                try
                {
                    mycon = new SqlConnection(ConnectString.Cstr);
                    SqlCommand cmd = new SqlCommand(add_field_sql, mycon);
                    mycon.Open();
                    cmd.ExecuteNonQuery();    //执行sql 语句
                    this.Imessage.Content = "列:" + field_name + "添加成功!";
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).Treeview();
                        }
                    }
                }
                catch (Exception ex)
                {
                    string a = ex.Message;
                    MessageBox.Show("列添加失败,请检查输入格式!"+a);
                }
            }
            finally
            {
                mycon.Close();
            }
        }
    }
}
