using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LittleShopOfHorrorRedo
{
    /// <summary>
    /// The Customer is a extendsion of the person class, which properties specific for the customer part of the program and database
    /// </summary>
    public class Customer : Person, IGenerelControl, ISpecifigUserControl
    {
        #region Public Properties

        /// <summary>
        /// An implementation of the id from the person class where the setting of the PersonId is set to private
        /// </summary>
        public new int PersonId { get; private set; }


        /// <summary>
        /// An Adress is specific for customer object
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// zip code is specific for customer object, it has to be integer with a max of 4 characters
        /// </summary>
        public int zipCode { get; set; }



        /// <summary>
        /// A simple string that colleckt the public properties in a one line 
        /// </summary>
        public string FullInfo
        {
            get
            {
                return $"{ FirstName } { LastName } ({ Email }) ({ tlfNr }) { Adress } { zipCode } ";
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// The basic constructor
        /// </summary>
        public Customer() { }

        /// <summary>
        /// Constructor that takes all the parameters
        /// </summary>
        /// <param name="cid">Input is an integer the Id of the customer object</param>
        /// <param name="firstname">Input is a string the first name of the customer object</param>
        /// <param name="lastname">Input is a string the last name of the customer object</param>
        /// <param name="phone">Input is an integer the phone number of the customer object</param>
        /// <param name="email">Input is a string the email of the customer object</param>
        /// <param name="adress">Input is a string the adress of the customer object</param>
        /// <param name="zip">Input is an integer the zip code of the customer object</param>
        public Customer(string firstname, string lastname, int phone, string email, string adress, int zip) : this()
        {

            FirstName = firstname;
            LastName = lastname;
            tlfNr = phone;
            Email = email;
            Adress = adress;
            zipCode = zip;

        }

        public Customer(int id, string firstname, string lastname, int phone, string email, string adress, int zip) : this()
        {
            PersonId = id;
            FirstName = firstname;
            LastName = lastname;
            tlfNr = phone;
            Email = email;
            Adress = adress;
            zipCode = zip;

        }

        public Customer(int id, string firstname, string lastname) : this()
        {
            PersonId = id;
            FirstName = firstname;
            LastName = lastname;
        }

        // Database Access object
        DataAccess dBa = new DataAccess();

        #endregion

        #region Methods

        /// <summary>
        /// Here you Add the input to create a new Customer
        /// </summary>
        /// <param name="customer">input a list of customer</param>
        /// <param name="firstName">input first name</param>
        /// <param name="lastName">input sir name</param>
        /// <param name="email">input email</param>
        /// <param name="tlfnr">input phonenumber</param>
        /// <param name="address">input adress</param>
        /// <param name="zip">input zipcode</param>
        /// <returns>output a Customer object</returns>
        public bool NewPerson(Customer cus)
        {
            bool userMade = false;

            // Getting the id of the last entry in the customers table from the database
            /* int lastId;
             try
             {
                 lastId = dBa.GetLastId("Customers");
             }
             catch { throw new ArgumentException("Database connection down", "original"); }

             // Control to see if there is any in the customer list if not set cNr to 1 or at 1 to total number of customer.Count
             PersonId = lastId < 0 ? 1 : lastId + 1;*/

            /// <summary>
            /// Stored procedure CreateCustomer @firstname string, @lastname string,  @tfl int, @email string, @adress string, @zip int
            /// Inserts a customer object into the database
            /// </summary>
            try
            {
                if (dBa.NewOrUpdateDB("CALL CreateCustomer ('" + cus.FirstName + "', '" + cus.LastName + "', '"
               + cus.tlfNr + "', '" + cus.Email + "', '" + cus.Adress + "', '" + cus.zipCode + "');"))
                    userMade = true;
            }
            catch
            {
                userMade = false;
                throw new ArgumentException("Database connection down", "original");
            }

            return userMade;
        }


        /// <summary>
        /// Customer implemenation of the delete person method from abstract Person class
        /// </summary>
        /// <param name="id">the id of the customer to delete</param>
        /// <returns></returns>
        public override bool DeletePerson(int id)
        {
            bool deleted = false;
            try
            {
                if (dBa.NewOrUpdateDB("DELETE FROM Customer WHERE id = '" + id + "';"))
                    deleted = true;
            }
            catch
            {
                deleted = false;
            }


            return deleted;
        }

        /// <summary>
        /// In the UpdateCustomer an ID is used to find which customer from the database,
        ///  this is updated with the data from a Customer object 
        /// </summary>
        /// <param name="id">Input an integer The ID from customer object</param>
        /// <param name="customer">Input a Customer object</param>
        /// <returns>Output a boolean</returns>
        public bool UpdateCustomer(int id, Customer customer)
        {
            bool updated = false;

            // A List of Customer objects
            List<Customer> UpdateCus = new List<Customer>();

            // A List of integers for the zip code of the Customer object
            List<int> zipList = new List<int>();

            try
            {
                // Getting all info about a customer from the database where the id equals the input id

                string strSql = "SELECT * FROM Customers WHERE id = " + id + ";";

                using (dBa.con)
                {
                    UpdateCus = dBa.con.Query<Customer>(strSql).ToList();
                }


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
            if (dBa.NewOrUpdateDB("CALL UpdateCustomer ('" + id + "', '" + customer.FirstName + "', '" + customer.LastName + "', '" + customer.tlfNr + "', '"
                + customer.Email + "', '" + customer.Adress + "', '" + customer.zipCode + "');"))
                return updated = true;

            return updated;
        }

        /// <summary>
        /// A method to get a list of all info on customers
        /// </summary>
        /// <returns></returns>
        public List<Customer> ShowAllCustomers()
        {
            // A list of Customer object from the database


            // Getting the ID from a customer from the database 
            // You currently must do this since this would not be shown in a Customer oject else
            List<int> ids = new List<int>();

            // Getting the zip code from a customer from the database where the id equals the input id
            // You currently must do this since this would not be shown in a Customer oject else
            List<int> zipcode = new List<int>();

            // One customer list to fill from the result of the other 3 list
            List<Customer> cust = new List<Customer>();
            {


                // Getting all the rest info from the customers and filling the customers List

                string strSql = "SELECT id, firstName, lastName, tlfnr, email, adress, zip FROM customers";

                List<Customer> customers = new List<Customer>();
                try
                {

                    using (dBa.con)
                    using (var cmd = dBa.con.CreateCommand())
                    {
                        dBa.con.Open();
                        cmd.CommandText = strSql;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customers.Add(new Customer(int.Parse(reader["id"].ToString()),
                                    reader["firstName"].ToString(),
                                    reader["lastName"].ToString(),
                                    int.Parse(reader["tlfnr"].ToString()),
                                    reader["email"].ToString(),
                                    reader["adress"].ToString(),
                                    int.Parse(reader["zip"].ToString())));
                            }
                        }
                    }
                    return customers;

                }
                catch { throw new ArgumentException("Database connection failure", "original"); }

            }
        }

        public List<Customer> ShowAllCustomerNames()
        {
            string strSql = "SELECT id, firstName, lastName FROM Customers ORDER BY lastName ASC;";
            List<Customer> customers = new List<Customer>();
            try
            {

                using (dBa.con)
                using (var cmd = dBa.con.CreateCommand())
                {
                    dBa.con.Open();
                    cmd.CommandText = strSql;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer(int.Parse(reader["id"].ToString()), reader["firstName"].ToString(), reader["lastName"].ToString()));
                        }
                    }
                }
                return customers;





            }
            catch { throw new ArgumentException("Customer object was null", "original"); }

        }

        /// <summary>
        /// Create a sql query that can span severel lines
        /// </summary>
        /// <param name="ids">Input a list of ids of the customers that are searches for</param>
        /// <returns>Output a string with a select query</returns>
        private string CreateCustomerSearchByID(List<int> ids)
        {
            string startStr = "SELECT firstName, lastName FROM Customers WHERE id= ";
            string endStr = " UNION ";
            string dasStr = "";
            for (int i = 0; i < ids.Count; i++)
            {
                dasStr += startStr + ids[i] + endStr;
            }
            dasStr = dasStr.Remove(dasStr.Length - 1, 1);
            return dasStr;
        }

        /// <summary>
        /// Method that gets the customer name by id
        /// </summary>
        /// <param name="ids">Input a list of ids of the customers that are searches for</param>
        /// <returns>Output a List of Customer objects</returns>
        public List<Customer> GetCustomersByID(List<int> ids)
        {
            List<Customer> customers = new List<Customer>();
            List<Customer> mellem = new List<Customer>();

            try
            {
                string strSql = "SELECT id, firstName, lastName FROM Customers ";
                using (dBa.con)
                {
                    using (var cmd = dBa.con.CreateCommand())
                    {
                        dBa.con.Open();
                        cmd.CommandText = strSql;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mellem.Add(new Customer(int.Parse(reader["id"].ToString()), reader["firstName"].ToString(), reader["lastName"].ToString()));
                            }
                        }
                    }
                }

            }
            catch { throw new ArgumentException("Query failure", "original"); }

            foreach (Customer c in mellem)
            {
                foreach (int i in ids)
                {
                    if (c.PersonId == i)
                    {
                        customers.Add(c);
                    }
                }

            }


            return customers;
        }

        #endregion

        #region Implemented methods

        /*public int Id()
        {
            return customerID;
        }*/

        // Implement the method for controlling if the input is a float
        public float IsFloat(string flInn)
        {
            if (float.TryParse(flInn, out float price)) { }
            else
                throw new ArgumentException("Parameter must be a float i.g. a number", "original");

            return price;
        }

        // Implement the method for controlling if the input is an integer
        public int IsInt(string intIn)
        {
            if (int.TryParse(intIn, out int number)) { }
            else
                throw new ArgumentException("Parameter must be written in integers", "original");

            return number;
        }

        // Implement the method for controlling if the input is a null
        public void IsEmpty(string input)
        {
            if (input != null)
                throw new ArgumentNullException(nameof(input));
        }

        public bool IsStrEmpty(string input)
        {
            if (input == "")
                return false;
            else
                return true;
        }

        // Implement the method for controlling if the Email already exists in the database
        public bool EmailExists(string email)
        {
            bool isEmailThere = false;

            try
            {
                if (email == (string)dBa.GetIsThereStringDB("SELECT email FROM Customers WHERE email = '" + email + "';"))
                    return isEmailThere = true;
            }
            catch { throw new ArgumentException("Database output was null", "original"); }

            return isEmailThere;
        }

        // Implement the method for controlling if the Phone number is allowed in the danish phone system
        public bool IsRealPhoneNbr(int nbr)
        {
            if (nbr < 10000000 || nbr > 99999999)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsStrFloat(string str)
        {
            throw new NotImplementedException();
        }

        public bool IsStrInt(string str)
        {
            if (int.TryParse(str, out int number)) { return true; }
            else
                return false;


        }

        bool IGenerelControl.IsEmpty(string input)
        {
            throw new NotImplementedException();
        }



        #endregion
    }
}
