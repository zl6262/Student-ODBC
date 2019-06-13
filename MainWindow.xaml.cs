using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tvProerties.DataContext = new MainViewModel.TreeViewModel();

        }
        private void Query_all_S_Click(object sender, RoutedEventArgs e)
        {
            string sql = "select * from S  ";                //SQL查询语句

            SqlConnection mycon = new SqlConnection(ConnectString.Cstr);                        //创建SQL连接对象
            mycon.Open();                                                        //打开
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);                  //实例化SqlDtatAdapter并执行SQL语句，至于什么是SQLDataAdapter，
                                                                                   //就是用来连接DataSet与数据库的，DataSet是C#中用来保存数据库数据的，
                                                                                   //在这里没有用DataSet，不过原理是一样的，SQLDataAdapter从数据库中取得数据
                                                                                   //然后再DataSet中创建列与行来填充，个人理解。
            DataTable dt = new DataTable();                                     //创建DtatTable实例
            myda.Fill(dt);                                                      //填充table
            dataGrid.ItemsSource = dt.DefaultView;                                  //这里在WPF界面中拖拽一个DataGrid，然后用DataTable进行填充。
            mycon.Close();
        }
        private void Sql_button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection mycon = null; //建立一个新的连接
            string sql_content = this.sql_str.Text;
            if (sql_content.Equals(""))
            {
                MessageBox.Show("sql语句不能为空.");
            }
            else
            {
                try
                {
                    mycon = new SqlConnection(ConnectString.Cstr);                        //创建SQL连接对象
                    mycon.Open();
                    SqlDataAdapter myda = new SqlDataAdapter(sql_content, mycon);                  //实例化SqlDtatAdapter并执行SQL语句，至于什么是SQLDataAdapter，
                                                                                                   //就是用来连接DataSet与数据库的，DataSet是C#中用来保存数据库数据的，
                                                                                                   //在这里没有用DataSet，不过原理是一样的，SQLDataAdapter从数据库中取得数据
                                                                                                   //然后再DataSet中创建列与行来填充，个人理解。
                    DataTable dt = new DataTable();                                     //创建DtatTable实例
                    myda.Fill(dt);                                                      //填充table
                    dataGrid.ItemsSource = dt.DefaultView;                                  //这里在WPF界面中拖拽一个DataGrid，然后用DataTable进行填充。
                }
                catch (Exception ex)
                {
                    string x = ex.Message;
                    MessageBox.Show(x);
                }
                finally
                {
                    mycon.Close();
                }
            }
        }
        private void Pwd_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #region TreeView 右击事件t

        //右®¨°击¡Â事º?件t

        private void DataTreeView_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)

        {

            var treeViewItem = VisualUpwardSeach<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;

            //vartreeViewItem = sender as TreeViewItem;

            if (treeViewItem != null)

            {

                treeViewItem.ContextMenu = GetItemRightContextMenu(treeViewItem);
                
                treeViewItem.Focus();

                e.Handled = true;

            }

        }

        static DependencyObject VisualUpwardSeach<T>(DependencyObject source)

        {

            while (source != null && source.GetType() != typeof(T))

            {

                source = VisualTreeHelper.GetParent(source);

            }



            return source;

        }

        //右®¨°键¨¹上¦?下?文?菜?单Ì£¤

        ContextMenu GetItemRightContextMenu(TreeViewItem t)

        {

            ContextMenu menu = new ContextMenu();

            MenuItem menuItem1 = new MenuItem();

            menuItem1.Header = "添加数据";

            menuItem1.Click += new RoutedEventHandler(menuItem_Click);

            menu.Items.Add(menuItem1);



            return menu;

        }



        void menuItem_Click(object sender, RoutedEventArgs e)

        {

            MessageBox.Show((e.Source as FrameworkElement).Name);
            
        }

        #endregion

        private void Sql_str_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            string sql_content = "";
            SqlConnection mycon = null; //建立一个新的连接
            try
            {
                double.Parse(this.find_sno.Text);
                sql_content = "select * from S where SNO =" + this.find_sno.Text;
                try
                {
                    mycon = new SqlConnection(ConnectString.Cstr);                        //创建SQL连接对象
                    mycon.Open();
                    SqlDataAdapter myda = new SqlDataAdapter(sql_content, mycon);                  //实例化SqlDtatAdapter并执行SQL语句，至于什么是SQLDataAdapter，
                                                                                                   //就是用来连接DataSet与数据库的，DataSet是C#中用来保存数据库数据的，
                                                                                                   //在这里没有用DataSet，不过原理是一样的，SQLDataAdapter从数据库中取得数据
                                                                                                   //然后再DataSet中创建列与行来填充，个人理解。
                    DataSet dt = new DataSet();                                     //创建DtatTable实例
                    myda.Fill(dt, "View");                                                      //填充table
                    string[] arrayA = new string[dt.Tables[0].Columns.Count];
                    foreach (DataRow col in dt.Tables[0].Rows)
                    {
                        int i = 0;
                        for (i = 0; i < dt.Tables[0].Columns.Count; i++)
                        {
                            arrayA[i] = (col[i].ToString());
                        }
                    }
                    if (arrayA[0] == null)
                        MessageBox.Show("无此学号");
                    else
                    {
                        this.sno.Content = arrayA[0];
                        this.sname.Content = arrayA[1];
                        this.sage.Content = arrayA[2];
                        this.sdept.Content = arrayA[3];
                    }
                }
                catch (Exception ex)
                {
                    string x = ex.Message;
                    MessageBox.Show(x);
                }
                finally
                {
                    mycon.Close();
                }
            }
            catch
            {
                MessageBox.Show("输入学号必须为数字！");
            }
        }

        private void Any_student_find_Click(object sender, RoutedEventArgs e)
        {
            string sql_content = "";
            #region 判断输入是字符还是数字
            try
            {
                double.Parse(this.any_student.Text);
                     sql_content = "select student.SNO,student.SNAME,student.Sage,student.SDEPT,course.CNAME,student_course.GRADE from SC as student_course right join S as student on student.SNO=student_course.sno left join C as course on course.cno =student_course.cno where Student.SNO='"+ this.any_student.Text +"' or student.SAGE="+this.any_student.Text+";";
                //   sql_content = "select S.SNO,S.SNAME,C.CNAME,SC.GRADE from S,SC,C where S.SNO= '"+ this.any_student.Text+"' or SAGE="+this.any_student.Text +" AND S.SNO=SC.SNO AND C.CNO=SC.CNO;";
            }
            catch
            {
                sql_content = "select student.SNO,student.SNAME,student.Sage,student.SDEPT,course.CNAME,student_course.GRADE from SC as student_course right join S as student on student.SNO=student_course.sno left join C as course on course.cno =student_course.cno where Student.SNAME= N'" + this.any_student.Text + "' or student.SDEPT= N'" + this.any_student.Text + "';";
            }
            #endregion
            SqlConnection mycon = null; //建立一个新的连接
           
            try
            {
                mycon = new SqlConnection(ConnectString.Cstr);                        //创建SQL连接对象
                mycon.Open();
                SqlDataAdapter myda = new SqlDataAdapter(sql_content, mycon);                  //实例化SqlDtatAdapter并执行SQL语句，至于什么是SQLDataAdapter，
                                                                                               //就是用来连接DataSet与数据库的，DataSet是C#中用来保存数据库数据的，
                                                                                               //在这里没有用DataSet，不过原理是一样的，SQLDataAdapter从数据库中取得数据
                                                                                               //然后再DataSet中创建列与行来填充，个人理解。
                                                                                               //然后再DataSet中创建列与行来填充，个人理解。
                DataTable dt = new DataTable();                                     //创建DtatTable实例
                myda.Fill(dt);                                                      //填充table
                dataGrid.ItemsSource = dt.DefaultView;                                  //这里在WPF界面中拖拽一个DataGrid，然后用DataTable进行填充。
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                MessageBox.Show(x);
            }
            finally
            {
                mycon.Close();
            }
        }

        private void AddS_Click(object sender, RoutedEventArgs e)
        {
            AddStu addStu = new AddStu();
            addStu.Show();
        }

        private void UpdateS_Click(object sender, RoutedEventArgs e)
        {
            UpdateStu updateStu = new UpdateStu();
            updateStu.Show();
        }

        private void DeleteS_Click(object sender, RoutedEventArgs e)
        {
            DeleteStu deleteStu = new DeleteStu();
            deleteStu.Show();
        }

        private void Query_all_C_Click(object sender, RoutedEventArgs e)
        {
            string sql = "select * from C  ";                //SQL查询语句

            SqlConnection mycon = new SqlConnection(ConnectString.Cstr);                        //创建SQL连接对象
            mycon.Open();
            try
            {
                SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);

                DataTable dt = new DataTable();

                myda.Fill(dt);                                                      //填充table
                dataGrid.ItemsSource = dt.DefaultView;
            }catch(SqlException ex)
            {
                string x = ex.ToString();
                MessageBox.Show("表名无效!");
            }
            finally { mycon.Close(); }
        }

        private void Query_all_SC_Click(object sender, RoutedEventArgs e)
        {
            string sql = "select * from SC  ";                //SQL查询语句

            SqlConnection mycon = new SqlConnection(ConnectString.Cstr);                        //创建SQL连接对象
            mycon.Open();                                                        //打开
            SqlDataAdapter myda = new SqlDataAdapter(sql, mycon);                  //实例化SqlDtatAdapter并执行SQL语句，至于什么是SQLDataAdapter，
                                                                                   //就是用来连接DataSet与数据库的，DataSet是C#中用来保存数据库数据的，
                                                                                   //在这里没有用DataSet，不过原理是一样的，SQLDataAdapter从数据库中取得数据
                                                                                   //然后再DataSet中创建列与行来填充，个人理解。
            DataTable dt = new DataTable();                                     //创建DtatTable实例
            myda.Fill(dt);                                                      //填充table
            dataGrid.ItemsSource = dt.DefaultView;                                  //这里在WPF界面中拖拽一个DataGrid，然后用DataTable进行填充。
            mycon.Close();
        }

        private void Select_c_Click(object sender, RoutedEventArgs e)
        {
            SelectC selectC = new SelectC();
            selectC.Show();
        }

        private void Quit_c_Click(object sender, RoutedEventArgs e)
        {
            QuitC quitC = new QuitC();
            quitC.Show();
        }

        private void Get_grade_Click(object sender, RoutedEventArgs e)
        {
            GetGrade getGrade = new GetGrade();
            getGrade.Show();
        }

        private void Set_grade_Click(object sender, RoutedEventArgs e)
        {
            SetGrade setGrade = new SetGrade();
            setGrade.Show();
        }

        private void Create_C_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection mycon = null; //建立一个新的连接
            string create_str = "create table C(CNO varchar(15) Primary KEY,CNAME varchar(50) NOT NULL,TEACHER varchar(50));";
                    try
                    {
                        mycon = new SqlConnection(ConnectString.Cstr);
                        SqlCommand cmd = new SqlCommand(create_str, mycon);
                        mycon.Open();
                        cmd.ExecuteNonQuery();     //执行sql 语句
                MessageBox.Show("课程表创建成功!");
                tvProerties.DataContext = new MainViewModel.TreeViewModel();
            }
                    catch (Exception ex)
                    {
                        string x = ex.Message;
                        MessageBox.Show("创建表失败!" + x);
                    }
                    finally
                    {
                        mycon.Close();
                    }
        }
        
    
     

        private void Delete_C_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection mycon = null; //建立一个新的连接
            string drop_str = "DROP TABLE C ;";
            try
            {
                mycon = new SqlConnection(ConnectString.Cstr);
                SqlCommand cmd = new SqlCommand(drop_str, mycon);
                mycon.Open();
                cmd.ExecuteNonQuery();    //执行sql 语句
                MessageBox.Show("课程表删除成功!");
                tvProerties.DataContext = new MainViewModel.TreeViewModel();
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                MessageBox.Show("删除表失败!" + x);
            }
            finally
            {
                mycon.Close();
            }
        }

        private void Modify_C_Click(object sender, RoutedEventArgs e)
        {
            Modify_C modify_C = new Modify_C();
            modify_C.Show();
        }

        private void Csetdata_Click(object sender, RoutedEventArgs e)
        {
            AddC addC = new AddC();
            addC.Show();
        }

        public void Treeview()
        {
            tvProerties.DataContext = new MainViewModel.TreeViewModel();
        }
        public void copyright_click(object sender, RoutedEventArgs e)
        {
            copyright copyright_ins = new copyright();
            copyright_ins.Show();
        }

        private void Search_course_btn_Click(object sender, RoutedEventArgs e)
        {
            string sql_content = "";
            string sql_ctest = "";
            #region 判断输入是字符还是数字
            try
            {
                double.Parse(this.search_course.Text);
                sql_ctest = "select * from C where CNO ='" + this.search_course.Text + "';";
                sql_content = "select S.SNO,S.SNAME,S.SAGE,C.CNAME,C.TEACHER,SC.GRADE from S,C,SC where C.CNO in(select CNO from C where CNO= '" + this.search_course.Text + "'"+ ") AND S.SNO=SC.SNO AND C.CNO= SC.CNO";
                //   sql_content = "select S.SNO,S.SNAME,C.CNAME,SC.GRADE from S,SC,C where S.SNO= '"+ this.any_student.Text+"' or SAGE="+this.any_student.Text +" AND S.SNO=SC.SNO AND C.CNO=SC.CNO;";
            }
            catch
            {
                sql_ctest = "select * from C where CNAME = N'" + this.search_course.Text + "';";
                sql_content = "select S.SNO,S.SNAME,S.SAGE,C.CNAME,C.TEACHER,SC.GRADE from S,C,SC where C.CNO in(select CNO from C where CNAME =N'" + this.search_course.Text + "') AND S.SNO=SC.SNO AND C.CNO= SC.CNO";
            }
            #endregion
            SqlConnection mycon = null; //建立一个新的连接

            try
            {
                mycon = new SqlConnection(ConnectString.Cstr);                        //创建SQL连接对象
                mycon.Open();
                SqlDataAdapter myda = new SqlDataAdapter(sql_ctest, mycon);
                DataSet dp = new DataSet();
                myda.Fill(dp, "View");
                string[] arrayA = new string[dp.Tables[0].Columns.Count];
                foreach (DataRow col in dp.Tables[0].Rows)
                {
                    int i = 0;
                    for (i = 0; i < dp.Tables[0].Columns.Count; i++)
                    {
                        arrayA[i] = (col[i].ToString());
                    }
                }
                if (arrayA[0] != null)
                {
                    myda = new SqlDataAdapter(sql_content, mycon);                  //实例化SqlDtatAdapter并执行SQL语句，至于什么是SQLDataAdapter，
                                                                                    //就是用来连接DataSet与数据库的，DataSet是C#中用来保存数据库数据的，
                                                                                    //在这里没有用DataSet，不过原理是一样的，SQLDataAdapter从数据库中取得数据
                                                                                    //然后再DataSet中创建列与行来填充，个人理解。
                                                                                    //然后再DataSet中创建列与行来填充，个人理解。
                    DataTable dt = new DataTable();                                     //创建DtatTable实例
                    myda.Fill(dt);                                                      //填充table
                    dataGrid.ItemsSource = dt.DefaultView;                                  //这里在WPF界面中拖拽一个DataGrid，然后用DataTable进行填充。


                    DataSet ds = new DataSet();
                    myda.Fill(ds, "View");
                    string[] arrayB = new string[ds.Tables[0].Columns.Count];
                    int student_number = ds.Tables[0].Rows.Count;
                    this.result_students_count.Content = student_number.ToString();
                    this.result_course_name.Content = ds.Tables[0].Rows[0][3];
                    double grade_sum = 0;
                    try
                    {
                        foreach (DataRow col in ds.Tables[0].Rows)
                        {
                            grade_sum += double.Parse(col[5].ToString()); //将object 类型转成 string 类型再转成int型
                        }
                        this.average_grade.Content = (grade_sum / student_number).ToString();
                    }
                    catch
                    {
                        this.average_grade.Content = "该课程的成绩尚未完全统计。";
                    }
                }
                else
                { MessageBox.Show("请检查输入信息，该课程不存在"); }
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                MessageBox.Show("目前无人选择该课程" );
            }
            finally
            {
                mycon.Close();
            }
        }
    }
}
