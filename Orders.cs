using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LittleShopOfHorrorRedo
{
    public class Orders : IGenerelControl
    {
        #region Public Properties

        public int ordreID { get; private set; }
        public int customerID { get; set; }

        public string CustomerName { get; set; }

        public string SalesPersonName { get; set; }
        public int salesPersonID { get; set; }
        public string ordreDate { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Parameters free constructor 
        /// </summary>
        public Orders()
        {

        }

        /// <summary>
        /// Constructor with parameters that make up the ones the go into an insert to the database
        /// </summary>
        /// <param name="oDate">Input the date of the order</param>
        /// <param name="salespersonid">Input the id of the sales person</param>
        /// <param name="customerid">Input the id of the customer</param>
        public Orders(string oDate, int salespersonid, int customerid) : this()
        {
            ordreDate = oDate;
            salesPersonID = salespersonid;
            customerID = customerid;
        }

        public Orders(string oDate) : this()
        {
            ordreDate = oDate;
        }

        DataAccess dBa = new DataAccess();
        #endregion

        #region Methods

        /// <summary>
        /// Method that inserts an ordre object into the database
        /// </summary>
        /// <param name="ordre"> Input an ordre object</param>
        /// <returns> Output a boolean</returns>
        public bool NewOrder(Orders ordre)
        {

            string strSql = "";
            strSql = "INSERT INTO Orders(orderDate, customerID, salesPersonID) VALUES('" + ordre.ordreDate + "', " + ordre.customerID + ", " + ordre.salesPersonID + "); ";
            try
            {
                if (dBa.NewOrUpdateDB(strSql))
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }

        /// <summary>
        /// Method that delete a row in the table Orders by ID
        /// </summary>
        /// <param name="id">Input int id of the order to delete</param>
        /// <returns>Output a boolean</returns>
        public bool DeleteOrder(int id)
        {
            try
            {
                if (dBa.NewOrUpdateDB("DELETE FROM Orders WHERE id = '" + id + "';"))
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }

        public bool UpdateOrder(Orders order)
        {
            // A List of Customer objects
            List<Orders> UpdateOrder = new List<Orders>();
            return true;
            /*

            try
            {
                // Getting all info about a customer from the database where the id equals the input id
                UpdateOrder = dBa.GetCustomerListFromDB("SELECT * FROM Customers WHERE id = " + id + ";");

                // Getting the zip code from a customer from the database where the id equals the input id
                // You currently must do this since this would not be shown in a Customer oject else
                zipList = dBa.GetListFromDb("SELECT zip FROM Customers WHERE id = " + id + ";");
            }
            catch { throw new ArgumentException("Database connection down", "original"); }

            // Control if the input first name is null and setting it to the first name from the database else setting it to the input firstname
            customer.FirstName = customer.FirstName == null ? UpdateCus[0].FirstName : customer.FirstName;

            // Control if the input last name is null and setting it to the last name from the database else setting it to the input lastname
            customer.LastName = customer.LastName == null ? UpdateCus[0].LastName : customer.LastName;

            // Control if the input phone number is 0 and setting it to the phone number from the database else setting it to the input phone number 
            customer.tlfNr = customer.tlfNr == 0 ? UpdateCus[0].tlfNr : customer.tlfNr;

            // Control if the input email is null and setting it to the email from the database else setting it to the input email
            customer.Email = customer.Email == null ? UpdateCus[0].Email : customer.Email;

            // Control if the input adress is null and setting it to the adress from the database else setting it to the adress
            customer.Adress = customer.Adress == null ? UpdateCus[0].Adress : customer.Adress;

            // Control if the input zip code is 0 and setting it to the zip code from the database else setting it to the input zip code 
            customer.zipCode = customer.zipCode == 0 ? zipList[0] : customer.zipCode;


            /// <summary>
            /// Stored procedure UpdateCustomer with paramaters @id, @firstname, @lastname, @tfl, @email, @adress, @zip
            /// </summary>
            if (dBa.NewOrUpdateDB("exec UpdateCustomer @id = '" + id + "', @firstname = '" + customer.FirstName + "', @lastname = '" + customer.LastName + "', @tlf = '" + customer.tlfNr + "', @email = '"
                + customer.Email + "' , @adress = '" + customer.Adress + "', @zip = '" + customer.zipCode + "';"))
                return updated = true;

            return updated;*/
        }

        /// <summary>
        /// Method to set the orderId at 1 higher than what is in the database
        /// </summary>
        /// <returns></returns>
        public int SetOrderID()
        {
            int oId = 0;
            int mel;
            try
            {
                mel = dBa.GetLastOrderID();

            }
            catch { throw new ArgumentException("Query failure", "original"); }
            oId = mel == 0 ? 1 : mel + 1;
            return oId;
        }

        /// <summary>
        /// Method that get all the orders in a list
        /// </summary>
        /// <returns>Output List of orders</returns>
        public List<Orders> ShowAllOrders()
        {

            string strSql = "SELECT orderDate FROM orders o ORDER BY o.id;";

            using (dBa.con)
            {
                return dBa.con.Query<Orders>(strSql).ToList();
            }

        }

        /// <summary>
        /// Method that get all orders by customer id
        /// </summary>
        /// <param name="id">Input int id of customer</param>
        /// <returns>Output List of orders</returns>
        public List<Orders> ShowOrdersByCustmer(int id)
        {
            string strSql = "SELECT orderDate FROM orders o " +
                " ORDER BY o.orderDate WHERE customerID = " + id + " ;";

            using (dBa.con)
            {
                return dBa.con.Query<Orders>(strSql).ToList();
            }
        }

        /// <summary>
        /// Method that get all orders by sales person id
        /// </summary>
        /// <param name="id">Input int id of sales person</param>
        /// <returns>Output List of orders</returns>
        public List<Orders> ShowOrdersBySalesPerson(int id)
        {
            List<Orders> orders = new List<Orders>();

            return orders;
        }

        /// <summary>
        /// Get all methods by date
        /// </summary>
        /// <param name="date">Input string date</param>
        /// <returns>Output List of orders</returns>
        public List<Orders> ShowOrdersByDate(string date)
        {
            List<Orders> orders = new List<Orders>();

            return orders;
        }
        #endregion

        #region Implemented methods

        public int Id()
        {
            return ordreID;
        }

        public void IsEmpty(string input)
        {
            if (input != null)
            {
                throw new ArgumentException("Parameter cannot be null", "original");
            }
        }

        public float IsFloat(string flInn)
        {
            if (float.TryParse(flInn, out float price)) { }
            else
            {
                throw new ArgumentException("Parameter must be a float i.g. a number", "original");
            }
            return price;
        }

        public int IsInt(string intIn)
        {
            if (int.TryParse(intIn, out int number)) { }
            else
            {
                throw new ArgumentException("Parameter must be written in integers", "original");
            }
            return number;
        }

        public bool IsStrFloat(string str)
        {
            throw new NotImplementedException();
        }

        public bool IsStrInt(string str)
        {
            throw new NotImplementedException();
        }

        bool IGenerelControl.IsEmpty(string input)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
