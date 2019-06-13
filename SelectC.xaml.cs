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
    /// SelectC.xaml 的交互逻辑
    /// </summary>
    public partial class SelectC : Window
    {
        public SelectC()
        {
            InitializeComponent();
        }

        private void Btn_select_C_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection mycon = null; //建立一个新的连接
            try
            {
                mycon = new SqlConnection(ConnectString.Cstr);                        //创建SQL连接对象
                mycon.Open();
                double.Parse(this.cno_selcetC.Text);
                double.Parse(this.sno_selectC.Text);
                try
                {
                    string cno = this.cno_selcetC.Text;
                    string sno = this.sno_selectC.Text;
                    string insert_content = "Insert INTO SC(CNO,SNO) VALUES('" + cno + "','" + sno + "');";
                    string tempS_content = "select * from S where SNO ='" + sno + "';";
                    string tempC_content = "select * from C where CNO ='" + cno + "';";
                    SqlDataAdapter myda = new SqlDataAdapter(tempS_content, mycon);
                    DataSet dt = new DataSet();
                    myda.Fill(dt, "View");
                    string[] arrayA = new string[dt.Tables[0].Columns.Count];
                    foreach (DataRow col in dt.Tables[0].Rows)
                    {
                        int i = 0;
                        for (i = 0; i < dt.Tables[0].Columns.Count; i++)
                        {
                            arrayA[i] = (col[i].ToString());
                        }
                    }
                    if (arrayA[0] != null)
                    {
                        myda = new SqlDataAdapter(tempC_content, mycon);
                        dt = new DataSet();
                        myda.Fill(dt, "View");
                        string[] arrayB = new string[dt.Tables[0].Columns.Count];
                        foreach (DataRow col in dt.Tables[0].Rows)
                        {
                            int i = 0;
                            for (i = 0; i < dt.Tables[0].Columns.Count; i++)
                            {
                                arrayB[i] = (col[i].ToString());
                            }
                        }
                        if (arrayB[0] != null)
                        {
                            try
                            {
                                SqlCommand cmd = new SqlCommand(insert_content, mycon);
                                cmd.ExecuteNonQuery();    //执行sql 语句
                                this.Imessage.Content = "学生:" + sno + "，选课:" + cno + ",成功!";
                            }
                            catch (Exception ex)
                            {
                                string x = ex.Message;
                                MessageBox.Show("选课失败!" + x);
                            }

                        }
                        else
                        { MessageBox.Show("无此课程"); }
                    }
                    else
                    {
                        MessageBox.Show("无此学号");
                    }
                }catch(Exception ex)
              {
                    string x = ex.Message;
                    MessageBox.Show("添加失败!" + x);
                }

            }
            catch
            {
                
                MessageBox.Show("输入的学号和课程号需为数字!");
            }
            finally
            {
                mycon.Close();
            }
        }
    }
}
