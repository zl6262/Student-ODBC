using System;
using System.Collections.Generic;
using System.Data;
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
    /// AddStu.xaml 的交互逻辑
    /// </summary>
    public partial class AddStu : Window
    {
        public AddStu()
        {
            InitializeComponent();
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection mycon = null; //建立一个新的连接
                try
                {
                    double.Parse(this.sno_add.Text);
                    double.Parse(this.sage_add.Text);

                    string sname = this.name_add.Text;
                    string sno = this.sno_add.Text;
                    string sage = this.sage_add.Text;
                    string sdept = this.sdept_add.Text;
                    if (sname.Equals("") || sdept.Equals(""))
                    {
                        MessageBox.Show("输入信息不能为空.");
                    }
                    else
                    {
                        string insert_content = "Insert INTO S(SNO,SNAME,SAGE,SDEPT) VALUES('" + sno + "','" + sname + "'," + sage + ",'" + sdept + "');";
                        try
                        {
                            mycon = new SqlConnection(ConnectString.Cstr);
                            SqlCommand cmd = new SqlCommand(insert_content, mycon);
                            mycon.Open();
                            cmd.ExecuteNonQuery();    //执行sql 语句
                        this.Imessage.Content = "学生:" + sname + "添加成功!";
                        }
                        catch (Exception ex)
                        {
                            string x = ex.Message;
                            MessageBox.Show("添加失败!"+x);
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

