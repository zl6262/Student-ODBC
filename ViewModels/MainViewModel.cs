using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Input;
using System.Windows.Controls;

namespace WpfApp1.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {

        }


        internal class PropertyNodeItem
        {
            public string Name { get; set; }
            public List<PropertyNodeItem> Children { get; set; }
            public PropertyNodeItem()
            {
                Children = new List<PropertyNodeItem>();
            }
        }
        internal class TreeViewModel : ViewModelBase
        {
            private List<PropertyNodeItem> _propertyNodeItems;
            public List<PropertyNodeItem> PropertyNodeItems
            {
                get { return _propertyNodeItems; }
                set
                {
                    _propertyNodeItems = value;
                    this.OnPropertyChanged("PropertyNodeItems");
                }
            }
            public TreeViewModel()
            {
                PropertyNodeItems = ShowTreeView();
            }
            private List<PropertyNodeItem> ShowTreeView()
            {
                SqlConnection mycon = new SqlConnection(ConnectString.Cstr);                        //创建SQL连接对象
                mycon.Open();                                                        //打开
                IList<string> tablenames = GetTableName.GetTableNames(mycon);
                List<PropertyNodeItem> itemList1 = new List<PropertyNodeItem>();
                {
                    for (int i = 0; i < tablenames.Count(); i++)
                    {
                        PropertyNodeItem temp1 = new PropertyNodeItem();
                        temp1.Name = tablenames[i]; //https://bbs.csdn.net/topics/391542725?page=1

                        //temp[i].Name = tablenames[i];
                        IList<string> tablecolumns = GetColumnName.GetColumnNames(mycon, tablenames[i]);
                        for (int t = 0; t < tablecolumns.Count(); t++)
                        {
                            PropertyNodeItem ctemp = new PropertyNodeItem();
                            ctemp.Name = tablecolumns[t];
                            temp1.Children.Add(ctemp);
                        }
                        itemList1.Add(temp1);
                    }
                    return itemList1;
                }
            }

            public static implicit operator TreeView(TreeViewModel v)
            {
                throw new NotImplementedException();
            }
        }




        #region 查找所有表的名字
        public class GetTableName
        {                              //寻找当前的所有所有表的名字
            public static IList<string> GetTableNames(SqlConnection e)  // static  表示着是类的静态方法，而不是实例
            {
                List<string> tablenames = new List<string>();
                DataTable dt = e.GetSchema("Tables");
                foreach (DataRow row in dt.Rows)
                {
                    string TableType = (string)row["TABLE_TYPE"];
                    if (TableType.Contains("TABLE"))
                    {
                        tablenames.Add(row["TABLE_NAME"].ToString());
                    }
                }
                return tablenames;
            }
        }
        #endregion

        #region 查找表的所有列的名字
        public class GetColumnName
        {
            public static IList<string> GetColumnNames(SqlConnection e, string tablename)
            {
                List<string> tablecolumns = new List<string>();
                string[] restrictions = new string[4];
                restrictions[2] = tablename;
                DataTable dt = e.GetSchema("Columns", restrictions); // 获取架构 // https://blog.csdn.net/osmeteor/article/details/17095149
                foreach (DataRow row in dt.Rows)
                {
                    string TableType = (string)row["COLUMN_NAME"];
                    tablecolumns.Add(row["COLUMN_NAME"].ToString());
                }
                return tablecolumns;
            }
        }
        #endregion
    }
}
