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

using System.Data;
using System.Data.SqlClient;

public class ConnectString
{
    private static string connstr;
    public static string Cstr
    {
        get { return connstr; }
        set { connstr = value; }
    }

}

namespace WpfApp1
{
    /// <summary>
    /// login.xaml 的交互逻辑
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userid = this.user.Text;
            string password = this.pwd.Password;
            SqlConnection SqlCon = null; //建立一个新的连接
            if (userid.Equals("") || password.Equals(""))
            {
                MessageBox.Show("用户名或密码不能为空");
            }
            else
            {
                ConnectString.Cstr = "Data Source=ZL2017210494;Initial Catalog=Database_app;User ID=" + userid + ";Password=" + password;
                try
                {
                  SqlCon = new SqlConnection(ConnectString.Cstr);
                        SqlCon.Open();
                }
                catch
                {
                    MessageBox.Show("用户名密码错误！");
                }
                finally
                {
                    if (SqlCon.State == ConnectionState.Closed || SqlCon.State == ConnectionState.Broken)
                    {
                        SqlCon.Close();
                        MessageBox.Show( "连接失败");
                    }
                    else
                    {
                        SqlCon.Close();
                        MainWindow Mwindow = new MainWindow();
                        Mwindow.Show();
                        this.Close();
                    }
                }
            }
        }
    }
};
