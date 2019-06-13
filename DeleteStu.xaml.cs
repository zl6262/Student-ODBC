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

namespace WpfApp1
{
    /// <summary>
    /// DeleteStu.xaml 的交互逻辑
    /// </summary>
    public partial class DeleteStu : Window
    {
        public DeleteStu()
        {
            InitializeComponent();
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection mycon = null; //建立一个新的连接
            try
            {
                double.Parse(this.sno_delete.Text);
                string sno = this.sno_delete.Text;
                string update_content = "DELETE  FROM S " + " WHERE SNO='" + sno + "';";
                try
                {
                    mycon = new SqlConnection(ConnectString.Cstr);
                    SqlCommand cmd = new SqlCommand(update_content, mycon);
                    mycon.Open();
                    cmd.ExecuteNonQuery();    //执行sql 语句
                    this.Imessage.Content = "学生:" + sno + "删除成功!";
                }
                catch (Exception ex)
                {
                    string x = ex.Message;
                    MessageBox.Show("删除失败!" + x);
                }
                finally
                {
                    mycon.Close();
                }
            }
            catch
            {
                MessageBox.Show("学号需为有效数字.");
            }

        }
    }
}

