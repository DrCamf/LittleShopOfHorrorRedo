using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LittleShopOfHorrorRedo
{
    /// <summary>
    /// Zipcode class is primarely there for to get an object to handle the zipcode and city output from the database 
    /// and to be used to get a list for comboboxes in the gui
    /// </summary>
    public class Zipcode
    {
        #region Public Properties
        public int zipCode { get; set; }

        public string City { get; set; }

        #endregion

        #region Constructor

        public Zipcode()
        {

        }

        public Zipcode(int zip, string city) : this()
        {
            zipCode = zip;
            City = city;
        }

        DataAccess dBa = new DataAccess();

        #endregion

        #region Method

        /// <summary>
        /// Simple method to get a list of zipcode objects from the database
        /// </summary>
        /// <returns>Outout List of zipcodes</returns>
        public List<string> GetCustomerZipCity()
        {
            List<string> cus = new List<string>();
            List<int> cusID = new List<int>();
            try
            {
                using (dBa.con)
                {
                    try
                    {
                        cusID = dBa.con.Query<int>("SELECT id FROM ZipCity").ToList();
                    }
                    catch { throw new ArgumentException("No id from databse", "original"); }


                    cus = dBa.con.Query<string>("SELECT city FROM ZipCity").ToList();
                }

            }
            catch { throw new ArgumentException("Database query failed", "original"); }

            List<string> saml = new List<string>();
            for (int i = 0; i < cus.Count; i++)
            {
                saml.Add(cusID[i] + " " + cus[i]);
            }

            return saml;
        }

        /// <summary>
        /// Method to get the city name via the ZipCode
        /// </summary>
        /// <param name="zip">Input zipcode</param>
        /// <returns>Output string cityname</returns>
        public string GetCity(int zip)
        {
            string zCIty;
            try
            {
                zCIty = dBa.GetIsThereStringDB("SELECT city FROM ZipCity WHERE id='" + zip + "';");
            }
            catch { throw new ArgumentException("Database query failed", "original"); }

            return zCIty;
        }

        #endregion

    }
}
