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
    /// GetGrade.xaml 的交互逻辑
    /// </summary>
    public partial class GetGrade : Window
    {
        public GetGrade()
        {
            InitializeComponent();
        }



        private void Btn_getgrade_Click(object sender, RoutedEventArgs e)
        {     
                SqlConnection mycon = null; //建立一个新的连接
                try
                {
                    mycon = new SqlConnection(ConnectString.Cstr);                        //创建SQL连接对象
                    mycon.Open();
                    double.Parse(this.cno_GetGrade.Text);
                    double.Parse(this.sno_GetGrade.Text);
                    try
                    {

                        string cno = this.cno_GetGrade.Text;
                        string sno = this.sno_GetGrade.Text;
                        string exam_SC = "Select * From SC WHERE CNO='" + cno + "' AND SNO ='" + sno + "';";
                        string getgrade_content = "select GRADE From SC WHERE CNO='" + cno + "' AND SNO ='" + sno + "';";
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
                                        myda = new SqlDataAdapter(getgrade_content, mycon);
                                        DataSet dm = new DataSet();                                     //创建DtatTable实例
                                        myda.Fill(dm, "View");                                                      //填充table
                                        string[] arrayD = new string[dm.Tables[0].Columns.Count];
                                        foreach (DataRow col in dm.Tables[0].Rows)
                                        {
                                            int i = 0;
                                            for (i = 0; i < dm.Tables[0].Columns.Count; i++)
                                            {
                                                arrayD[i] = (col[i].ToString());
                                            }
                                        }
                                        if (arrayD[0] == null||arrayD[0]=="")
                                            MessageBox.Show("此学生在此课程暂无成绩");
                                        else
                                        {
                                            this.Imessage.Content = "学生:" + cno + ",在课程:" + cno + "的成绩为" + arrayD[0] + "!";
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("该学生未选此课！");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string x = ex.Message;
                                    MessageBox.Show("查询成绩失败!" + x);
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
                        MessageBox.Show("查询成绩失败!" + x);
                    }

                }
                catch
                {

                    MessageBox.Show("输入的学号,课程号须为有效数字!");
                }
                finally
                {
                    mycon.Close();
                }
            }
        }
}
