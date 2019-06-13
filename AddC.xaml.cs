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
    /// AddC.xaml 的交互逻辑
    /// </summary>
    public partial class AddC : Window
    {
        public AddC()
        {
            InitializeComponent();
        }

        private void Btn_addC_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection mycon = null; //建立一个新的连接
            try
            {
                double.Parse(this.cno_add.Text);

                string cname = this.class_name_add.Text;
                string cno = this.cno_add.Text;
                string teacher = this.teacher_add.Text;

                if (teacher.Equals(""))
                {
                    MessageBox.Show("输入信息不能为空.");
                }
                else
                {
                    string insert_content = "Insert INTO C(CNO,CNAME,TEACHER) VALUES('" + cno + "','" + cname + "','" + teacher + "');";
                    try
                    {
                        mycon = new SqlConnection(ConnectString.Cstr);
                        SqlCommand cmd = new SqlCommand(insert_content, mycon);
                        mycon.Open();
                        cmd.ExecuteNonQuery();    //执行sql 语句
                        this.Imessage.Content = "课程:" + cname + "添加成功!";
                    }
                    catch (Exception ex)
                    {
                        string x = ex.Message;
                        MessageBox.Show("添加失败!" + x);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }

            catch
            {
                MessageBox.Show("课程号需为有效数字.");
            }

        }

        private void BtnC_delete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection mycon = null; //建立一个新的连接
            try
            {
                double.Parse(this.cno_add.Text);
                string cno = this.cno_add.Text;
                    string delete_content = "Delete From C Where CNO= '" + cno + "';";
                    try
                    {
                        mycon = new SqlConnection(ConnectString.Cstr);
                        SqlCommand cmd = new SqlCommand(delete_content, mycon);
                        mycon.Open();
                        cmd.ExecuteNonQuery();    //执行sql 语句
                        this.Imessage.Content = "课程:" + cno + "删除成功!";
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
                MessageBox.Show("课程号需为有效数字.");
            }

        }
    }
}
