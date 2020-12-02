using System;
using System.Collections.Generic;

namespace LittleShopOfHorrorRedo
{
    /// <summary>
    /// Class for the employes of the little shop implementing the abstract Person class
    /// </summary>
    public class SalesPerson : Person, IGenerelControl, ISpecifigUserControl
    {
        #region Public Properties

        /// <summary>
        /// An implementation of the id from the person class where the setting of the PersonId is set to private
        /// </summary>
        public new int PersonId { get; private set; }

        // Here the role of the salesPerson is set or read
        public string salesRole { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Basic Constructor
        /// </summary>
        public SalesPerson() { }

        /// <summary>
        /// Constructor that takes all the parameters
        /// </summary>
        /// <param name="cid">Input is an integer the Id of the SalesPerson object</param>
        /// <param name="firstname">Input is a string the first name of the SalesPerson object</param>
        /// <param name="lastname">Input is a string the last name of the SalesPerson object</param>
        /// <param name="phone">Input is an integer the phone number of the SalesPerson object</param>
        /// <param name="email">Input is a string the email of the SalesPerson object</param>
        /// <param name="adress">Input is a string the adress of the SalesPerson object</param>
        /// <param name="role">Input is a string hte role of the SalesPerson object</param>
        public SalesPerson(int id, string firstname, string lastname, int phone, string email, string role) : this()
        {
            PersonId = id;
            FirstName = firstname;
            LastName = lastname;
            tlfNr = phone;
            Email = email;
            salesRole = role;
        }

        public SalesPerson(string firstname, string lastname) : this()
        {
            FirstName = firstname;
            LastName = lastname;
        }

        // Database Access object
        DataAccess dBa = new DataAccess();

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sPerson"></param>
        /// <returns></returns>
        public bool NewSalesPerson(SalesPerson sPerson)
        {
            bool created = false;

            int lastId;
            try
            {
                lastId = dBa.GetLastId("Salesperson");
            }
            catch
            {
                throw new ArgumentException("Database connection down", "original");
            }


            // Control to see if there is any in the customer list if not set cNr to 1 or at 1 to total number of customer.Count
            PersonId = lastId < 0 ? 1 : lastId + 1;


            /// <summary>
            /// Stored procedure CreateSalesPerson @firstname string, @lastname string,  @tfl int, @email string, @role string
            /// </summary>
            try
            {
                if (dBa.NewOrUpdateDB("exec CreateSalesPerson @firstname ='" + sPerson.FirstName + "', @lastname = '" + sPerson.LastName + "', @tlf ='"
               + sPerson.tlfNr + "', @email = '" + sPerson.Email + "', @role = '" + sPerson.salesRole + "';"))
                    created = true;
            }
            catch
            {
                throw new ArgumentException("Database connection down", "original");
            }


            return created;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool UpdateSalesPerson(int id, SalesPerson sPerson)
        {
            bool updated = false;
            List<SalesPerson> sPersons = new List<SalesPerson>();
            try
            {
                // Getting all info about a customer from the database where the id equals the input id
                sPersons = dBa.GetSalesPersonListFromDB("SELECT * FROM SalesPerson WHERE id = " + id + ";");


            }
            catch { throw new ArgumentException("Database connection down", "original"); }


            // Control if the input first name is null and setting it to the first name from the database else setting it to the input firstname
            sPerson.FirstName = sPerson.FirstName == null ? sPersons[0].FirstName : sPerson.FirstName;

            // Control if the input last name is null and setting it to the last name from the database else setting it to the input lastname
            sPerson.LastName = sPerson.LastName == null ? sPersons[0].LastName : sPerson.LastName;

            // Control if the input phone number is 0 and setting it to the phone number from the database else setting it to the input phone number 
            sPerson.tlfNr = sPerson.tlfNr == 0 ? sPersons[0].tlfNr : sPerson.tlfNr;

            // Control if the input email is null and setting it to the email from the database else setting it to the input email
            sPerson.Email = sPerson.Email == null ? sPersons[0].Email : sPerson.Email;

            // Control if the input role is null and setting it to the role from the database else setting it to the input role
            sPerson.salesRole = sPerson.salesRole == null ? sPersons[0].salesRole : sPerson.salesRole;

            /// <summary>
            /// Stored procedure UpdateSalesPerson with paramaters @id, @firstname, @lastname, @tfl, @email, @role
            /// </summary>
            if (dBa.NewOrUpdateDB("exec UpdateSalesPerson @id = '" + id + "', @firstname = '" + sPerson.FirstName + "', @lastname = '" + sPerson.LastName + "', @tlf = '" + sPerson.tlfNr + "', @email = '"
                + sPerson.Email + "' , @role = '" + sPerson.salesRole + "';"))
                return updated = true;

            return updated;
        }

        public List<SalesPerson> GetAllSalesPersons()
        {
            List<SalesPerson> sPerson = new List<SalesPerson>();
            List<SalesPerson> sOutPerson = new List<SalesPerson>();
            List<int> ids = new List<int>();
            try
            {
                // Getting the id from the customers and filling the ids List 
                //ids = dBa.GetListFromDb("SELECT id FROM SalesPerson");

                sOutPerson = dBa.GetSalesPersonListFromDB("CALL SelectAllSalesPersonIdName ()");
            }
            catch { throw new ArgumentException("Database Connection down", "original"); }

            /* try
             {
                 // The ids List, zipcode List and customers List are added to another List of Customers obejcts
                 for (int i = 0; i < ids.Count; i++)
                 {
                     sOutPerson.Add(new SalesPerson(ids[i], sPerson[i].FirstName, sPerson[i].LastName, sPerson[i].tlfNr, sPerson[i].Email, sPerson[i].salesRole));
                 }
             }
             catch { throw new ArgumentException("SalesPerson object was null", "original"); }*/

            return sOutPerson;
        }

        private string CreateSalesPersonsByID(List<int> ids)
        {
            string startStr = "SELECT firstName, lastName FROM SalesPerson WHERE id= ";
            string endStr = " UNION ";
            string dasStr = "";
            for (int i = 0; i < ids.Count; i++)
            {
                dasStr += startStr + ids[i] + endStr;
            }

            return dasStr;
        }

        public List<SalesPerson> GetSalesPersonByID(List<int> ids)
        {
            List<SalesPerson> salespersons = new List<SalesPerson>();
            try
            {
                salespersons = dBa.GetSalesPersonListFromDBByID(CreateSalesPersonsByID(ids));
            }
            catch { throw new ArgumentException("Query failure", "original"); }

            return salespersons;
        }

        #endregion

        #region Implemented Methods
        // implementation of deleteperson for SalesPerson
        public override bool DeletePerson(int id)
        {
            bool deleted = false;

            if (dBa.NewOrUpdateDB("DELETE FROM SalesPerson WHERE id = '" + id + "';"))
                deleted = true;

            return deleted;
        }

        // Implement the method for controlling if the Email already exists in the database
        public bool EmailExists(string email)
        {
            throw new NotImplementedException();
        }

        public int Id()
        {
            return PersonId;
        }

        // Implement the method for controlling if the input is a null
        public void IsEmpty(string input)
        {
            if (input != null)
            {
                throw new ArgumentException("Parameter cannot be null", "original");
            }
        }

        // Implement the method for controlling if the input is a float
        public float IsFloat(string flInn)
        {
            if (float.TryParse(flInn, out float price)) { }
            else
            {
                throw new ArgumentException("Parameter must be a float i.g. a number", "original");
            }
            return price;
        }

        // Implement the method for controlling if the input is an integer
        public int IsInt(string intIn)
        {
            if (int.TryParse(intIn, out int number)) { }
            else
            {
                throw new ArgumentException("Parameter must be written in integers", "original");
            }
            return number;
        }

        // Implement the method for controlling if the Phone number is allowed in the danish phone system
        public bool IsRealPhoneNbr(int nbr)
        {
            throw new NotImplementedException();
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
