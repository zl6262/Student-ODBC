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
    /// SetGrade.xaml 的交互逻辑
    /// </summary>
    public partial class SetGrade : Window
    {
        public SetGrade()
        {
            InitializeComponent();
        }

        private void Btn_setgrade_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection mycon = null; //建立一个新的连接
            try
            {
                mycon = new SqlConnection(ConnectString.Cstr);                        //创建SQL连接对象
                mycon.Open();
                double.Parse(this.cno_SetGrade.Text);
                double.Parse(this.sno_SetGrade.Text);
                double.Parse(this.grade_set.Text);
                try
                {

                    string cno = this.cno_SetGrade.Text;
                    string sno = this.sno_SetGrade.Text;
                    string grade = this.grade_set.Text;
                    string exam_SC = "Select GRADE From SC WHERE CNO='" + cno + "' AND SNO ='" + sno + "';"; //判断是否选课
                    string update_content = "UPDATE  SC SET GRADE='" + grade + "' WHERE SNO='" + sno + "' AND CNO ='"+cno+"';";
                    SqlDataAdapter myda = new SqlDataAdapter(exam_SC, mycon);
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
                    if (arrayA[0]!=null)
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand(update_content, mycon);
                            cmd.ExecuteNonQuery();    //执行sql 语句
                            this.Imessage.Content = "学生:" + sno + ",课程："+cno+"的成绩："+grade+"修改成功!";
                        }
                        catch (Exception ex)
                        {
                            string x = ex.Message;
                            MessageBox.Show("设置失败!" + x);
                        }
                    }
                    else
                    {
                        MessageBox.Show("在选课系统中并没有该学生选择该课程!");
                    }
                }
                catch (Exception ex)
                {
                    string x = ex.Message;
                    MessageBox.Show("设置失败!" + x);
                }
            }
            catch
            {

                MessageBox.Show("输入的学号，课程号以及成绩须为有效数字!");
            }
            finally
            {
                mycon.Close();
            }

            }
        }
}
