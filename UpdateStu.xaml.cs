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
    /// UpdateStu.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateStu : Window
    {
        public UpdateStu()
        {
            InitializeComponent();
        }
        private void Btn_update_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection mycon = null; //建立一个新的连接
            try
            {
                double.Parse(this.sno_update.Text);
                double.Parse(this.sage_update.Text);

                string sname = this.sname_update.Text;
                string sno = this.sno_update.Text;
                string sage = this.sage_update.Text;
                string sdept = this.sdept_update.Text;
                if (sname.Equals("") || sdept.Equals(""))
                {
                    MessageBox.Show("输入信息不能为空.");
                }
                else
                {
                    string update_content = "UPDATE  S SET SNAME='" + sname + "',SAGE=" + sage + ", SDEPT='" + sdept + "' WHERE SNO='" + sno + "';";
                    try
                    {
                        mycon = new SqlConnection(ConnectString.Cstr);
                        SqlCommand cmd = new SqlCommand(update_content, mycon);
                        mycon.Open();
                        cmd.ExecuteNonQuery();    //执行sql 语句
                        this.Imessage.Content = "学生:" + sname + "修改成功!";
                    }
                    catch (Exception ex)
                    {
                        string x = ex.Message;
                        MessageBox.Show("修改失败!" + x);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }

            catch
            {
                MessageBox.Show("学号和姓名需为有效数字.");
            }

        }
    }
}

