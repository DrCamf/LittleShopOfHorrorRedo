using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LittleShopOfHorrorRedo
{
    public class DataAccess
    {
        private string conStr = string.Format("Server=localhost; database=littleshopnetver2; UID=root; password=");
        public MySqlConnection con;


        public DataAccess()
        {
            con = new MySqlConnection(conStr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>


        public List<Customer> GetCustomerListFromDBByID(string strSql)
        {
            using (con = new MySqlConnection(conStr))
            {
                return con.Query<Customer>(strSql).ToList();
            }
        }

        /// <summary>
        /// Method to get all orders
        /// </summary>
        /// <returns>Output List of orders</returns>


        public List<Customer> GetCustomerInfoByOrder()
        {
            List<Customer> customers = new List<Customer>();
            string strSql = "SELECT id, firstName, lastName FROM customers c INNER JOIN orders o ON o.customerID = c.id ORDER BY o.id;";

            using (con = new MySqlConnection(conStr))
            using (var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = strSql;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer(int.Parse(reader["id"].ToString()), reader["c.firstName"].ToString(), reader["c.lastName"].ToString()));
                    }
                }
            }
            return customers;
        }

        public List<SalesPerson> GetSalesInfoByOrder()
        {
            List<SalesPerson> sales = new List<SalesPerson>();
            string strSql = "SELECT firstName, lastName FROM salesperson s INNER JOIN orders o ON o.salesPersonID = s.id ORDER BY o.id;";

            using (con = new MySqlConnection(conStr))
            using (var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = strSql;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sales.Add(new SalesPerson(reader["s.firstName"].ToString(), reader["s.lastName"].ToString()));
                    }
                }
            }
            return sales;
        }




        //public List<Tuple<Orders, Customer, SalesPerson>> GetOrderInfo()
        //{

        //    return new List<Tuple<Orders, Customer, SalesPerson>>();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public List<Wares> GetWareListFromDB(string strSql)
        {
            List<Wares> ware = new List<Wares>();


            using (con = new MySqlConnection(conStr))
            using (var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = strSql;
                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        ware.Add(new Wares(int.Parse(reader["id"].ToString()), reader["name"].ToString(), int.Parse(reader["volume"].ToString()), float.Parse(reader["minPrice"].ToString())));
                    }
                }
            }


            //using (con = new MySqlConnection(conStr))
            //{

            //     con.Query<Wares>(strSql).ToList();
            //}

            return ware;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public List<int> GetListFromDb(string strSql)
        {

            try
            {
                using (con = new MySqlConnection(conStr))
                {

                    return con.Query<int>(strSql).ToList();

                }

            }
            catch { throw new ArgumentException("Database connection down", "original"); }



        }

        public List<float> GetListFloatFromDb(string strSql)
        {
            using (con = new MySqlConnection(conStr))
            {
                return con.Query<float>(strSql).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public List<string> GetListStrFromDb(string strSql)
        {
            using (con = new MySqlConnection(conStr))
            {
                return con.Query<string>(strSql).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public List<SalesPerson> GetSalesPersonListFromDB(string strSql)
        {
            List<SalesPerson> sales = new List<SalesPerson>();
            try
            {
                string query = strSql;
                con = new MySqlConnection(conStr);
                using (con)
                {



                    sales = con.Query<SalesPerson>(strSql).ToList();

                }

            }
            catch
            {

                throw new ArgumentException("Database connection down", "original");
            }

            return sales;
        }

        public List<SalesPerson> GetSalesPersonListFromDBByID(string strSql)
        {
            using (con = new MySqlConnection(conStr))
            {
                return con.Query<SalesPerson>(strSql).ToList();
            }
        }

        /// <summary>
        /// Get a single order object via the id of that object in the database
        /// </summary>
        /// <param name="id">Inout int id of the order</param>
        /// <returns></returns>
       /* public Orders GetSingleOrder(int id)
        {
            Orders order = new Orders();

            using (con = new MySqlConnection(conStr))
            {
                return con.Query<Orders>("Select * FROM Orders WHERE id ='" + id + "';");
            }

            
        }*/

        public List<Zipcode> GetZipCodeListFromDB(string strSql)
        {
            using (con = new MySqlConnection(conStr))
            {
                return con.Query<Zipcode>(strSql).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<WaresType> GetWareTypen()
        {
            List<WaresType> waretypen = new List<WaresType>();
            using (con = new MySqlConnection(conStr))
            using (var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = "SELECT id, waresTypeName FROM WaresType ORDER BY waresTypeName ASC";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        waretypen.Add(new WaresType(int.Parse(reader["id"].ToString()), reader["waresTypeName"].ToString()));
                    }
                }
            }

            return waretypen;

        }

        public IEnumerable<string> GetWareType()
        {
            using (con = new MySqlConnection(conStr))
            using (var cmd = con.CreateCommand())
            {
                con.Open();
                cmd.CommandText = "SELECT waresTypeName FROM WaresType ORDER BY waresTypeName ASC";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader.GetString(reader.GetOrdinal("waresTypeName"));
                    }
                }
            }

        }

        public List<Wares> GetWareInfoInt(int id)
        {

            List<Wares> ware = new List<Wares>();
            try
            {
                con = new MySqlConnection(conStr);
                string strSql = "CALL GetWaresDic ('" + id + "');";
                using (con)
                {
                    con.Open();


                    ware = con.Query<Wares>(strSql).ToList();
                    con.Close();
                }

            }
            catch { throw new ArgumentException("Database connection down", "original"); }

            return ware;
        }

        public Dictionary<int, string> GetWaresBytypes(int id)
        {
            Dictionary<int, string> combo = new Dictionary<int, string>();
            List<Wares> war = new List<Wares>();
            using (con = new MySqlConnection(conStr))
            {
                war = con.Query<Wares>("CALL GetWaresDic ('" + id + "');").ToList();
            }

            foreach (Wares w in war)
            {
                combo.Add(w.waresID, w.wareName);
            }

            return combo;
        }

        //public List<Wares> GetWareInfo(string name)
        //{
        //    List<Wares> ware = new List<Wares>();
        //    try
        //    {

        //        using (con = new MySqlConnection(conStr))
        //        using (var cmd = con.CreateCommand())
        //        {
        //            con.Open();
        //            cmd.CommandText = "SELECT name FROM Wares INNER JOIN WaresType ON Wares.waresTypeID = WaresType.id " +
        //            "WHERE waresTypeID IN(SELECT id FROM WaresType WHERE waresTypeName = '" + name + "');";
        //            using (var reader = cmd.ExecuteReader())
        //            {

        //                while (reader.Read())
        //                {
        //                    ware.Add(new Wares(reader["name"].ToString()));
        //                }
        //            }
        //        }

        //    }
        //    catch { throw new ArgumentException("Database connection down", "original"); }

        //    return ware;

        //}
        public List<Wares> GetWareInfo(string name)
        {
            List<Wares> ware = new List<Wares>();
            try
            {

                using (con = new MySqlConnection(conStr))
                using (var cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "SELECT name FROM Wares INNER JOIN WaresType ON Wares.waresTypeID = WaresType.id " +
                    "WHERE waresTypeID IN(SELECT id FROM WaresType WHERE waresTypeName = '" + name + "') ORDER BY name ASC;";
                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            ware.Add(new Wares(reader["name"].ToString()));
                        }
                    }
                }

            }
            catch { throw new ArgumentException("Database connection down", "original"); }

            return ware;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public string GetIsThereStringDB(string strSql)
        {
            string volumeInfo;
            using (con = new MySqlConnection(conStr))
            {
                volumeInfo = con.Query<string>(strSql).ToString();
            }

            return volumeInfo;

        }

        public int GetWaresIdFromString(int wrid, string name)
        {
            List<int> result = new List<int>(); ;
            string strSql = "CALL GetWaresIDViaPareDuo ('" + wrid + "', '" + name + "');";
            using (con = new MySqlConnection(conStr))
            {
                result = con.Query<int>(strSql).ToList();
                return result[0];
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public int GetLastId(string table)
        {
            int id;
            string mellem;
            string strSql = "SELECT Max(id) * FROM " + table + ";";
            using (con = new MySqlConnection(conStr))
            {
                mellem = con.Query<string>(strSql).ToString();
            }
            if (mellem == null)
                return 0;
            else
            { int.TryParse(mellem, out int result); id = result; }

            return id;
        }

        /// <summary>
        /// Method where we get the minimum price for a given ware from the database
        /// </summary>
        /// <param name="id">Input integer Wares id</param>
        /// <returns>Output a float the minimums price</returns>
        public float GetWareMinPrice(int id)
        {
            string strSql = "SELECT minPrice FROM Wares WHERE id='" + id + "';";
            List<float> price = new List<float>();
            using (con = new MySqlConnection(conStr))
            {
                price = con.Query<float>(strSql).ToList();
            }

            return price[0];
        }




        public int GetLastOrderID()
        {
            int id;
            string strSql = "SELECT MAX(id) FROM orders";
            string mellem;
            using (con = new MySqlConnection(conStr))
            {
                mellem = con.Query<string>(strSql).ToString();
            }
            if (mellem == null)
                return 0;
            else
            { int.TryParse(mellem, out int result); id = result; }

            return id;

        }



        public string GetLastWareType()
        {
            string type;
            string strSql = "SELECT WaresTypeName FROM WaresType ORDER BY MAX(id)";
            using (con = new MySqlConnection(conStr))
            {
                type = (con.Query<string>(strSql).ToString());
            }

            return type;
        }

        public int GetWareVolume(int id)
        {
            string strSql = "SELECT volume FROM Wares WHERE id='" + id + "';";
            List<int> volume = new List<int>();
            using (con = new MySqlConnection(conStr))
            {
                volume = con.Query<int>(strSql).ToList();
            }

            return volume[0];
        }



        /// <summary>
        /// The Main work horse that handles Insert, Update and Delete
        /// </summary>
        /// <param name="strSql">input a string sql sequence</param>
        /// <returns></returns>
        public bool NewOrUpdateDB(string strSql)
        {
            bool isDone = false;
            con = new MySqlConnection(conStr);
            using (con)
            {

                con.Open();
                var cmd = new MySqlCommand(strSql, con);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch { throw new ArgumentException("sql query was in error", "original"); }

                isDone = true;
            }
            return isDone;
        }
    }
}
