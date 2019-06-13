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
    /// QuitC.xaml 的交互逻辑
    /// </summary>
    public partial class QuitC : Window
    {
        public QuitC()
        {
            InitializeComponent();
        }

        private void Btn_quit_C_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection mycon = null; //建立一个新的连接
            try
            {
                mycon = new SqlConnection(ConnectString.Cstr);                        //创建SQL连接对象
                mycon.Open();
                double.Parse(this.cno_quitC.Text);
                double.Parse(this.sno_quitC.Text);
                try
                {

                    string cno = this.cno_quitC.Text;
                    string sno = this.sno_quitC.Text;
                    string exam_SC = "Select * From SC WHERE CNO='" + cno + "' AND SNO ='" + sno + "';";
                    string insert_content = "DELETE From SC WHERE CNO='" + cno + "' AND SNO ='" + sno + "';";
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
                                myda = new SqlDataAdapter(exam_SC, mycon);
                                dt = new DataSet();
                                myda.Fill(dt, "View");
                                string[] arrayC = new string[dt.Tables[0].Columns.Count];
                                foreach (DataRow col in dt.Tables[0].Rows)
                                {
                                    int i = 0;
                                    for (i = 0; i < dt.Tables[0].Columns.Count; i++)
                                    {
                                        arrayC[i] = (col[i].ToString());
                                    }
                                }
                                if (arrayC[0] != null)
                                {
                                    SqlCommand cmd = new SqlCommand(insert_content, mycon);
                                    cmd.ExecuteNonQuery();    //执行sql 语句
                                    this.Imessage.Content = "学生:" + sno + "，退课:" + cno + ",成功!";
                                }
                                else
                                {
                                    MessageBox.Show("该学生未选此课！");
                                }
                            }
                            catch (Exception ex)
                            {
                                string x = ex.Message;
                                MessageBox.Show("退课失败!" + x);
                            }

                        }
                        else
                        { MessageBox.Show("无此课程"); }
                    }
                    else
                    {
                        MessageBox.Show("无此学号");
                    }
                }
                catch (Exception ex)
                {
                    string x = ex.Message;
                    MessageBox.Show("退课失败!" + x);
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
