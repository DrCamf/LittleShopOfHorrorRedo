using System;
using System.Collections.Generic;
using System.Text;

namespace LittleShopOfHorrorRedo
{
    public class OrdreLine : IGenerelControl
    {
        #region Public Properties
        public int Ordreid { get; set; }
        public int Waresid { get; set; }
        public int ordreLineid { get; private set; }

        public float salesPrice { get; set; }

        public float Volume { get; set; }

        public string volumeType { get; set; }
        public int volumeTypeid { get; set; }

        #endregion

        #region Constructor

        public OrdreLine()
        {

        }

        /// <summary>
        /// Constructor with several parameters that mirrors the db tabel
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="volume"></param>
        /// <param name="volumetypeid"></param>
        /// <param name="waresid"></param>
        /// <param name="salesprice"></param>
        public OrdreLine(int orderid, int volume, int volumetypeid, int waresid, float salesprice) : this()
        {
            Ordreid = orderid;
            Volume = volume;
            volumeTypeid = volumetypeid;
            Waresid = waresid;
            salesPrice = salesprice;
        }


        public OrdreLine(int volume, float salesprice) : this()
        {
            Volume = volume;
            salesPrice = salesprice;
        }


        DataAccess dBa = new DataAccess();

        #endregion

        #region Methods


        /// <summary>
        /// Method create the sql string with the List of ordrelines 
        /// </summary>
        /// <param name="lines">Input list of orderlines</param>
        /// <returns>Output a string of a sql string</returns>
        private string CreateOrderLines(List<OrdreLine> lines)
        {
            OrdreLine ordreLines = new OrdreLine();

            string hugeSql = "INSERT INTO OrderLines (ordersID, voulume, salesPrice, waresID, volumeTypeID) VALUES ('";
            string secondLine = "('";
            string endOfLIne = "'),";
            string middelLine = "', '";
            string dasSql = "";

            for (int i = 0; i < lines.Count; i++)
            {
                if (i == 0)
                    dasSql += hugeSql + lines[i].Ordreid + middelLine + lines[i].Volume + middelLine + lines[i].salesPrice + middelLine + lines[i].Waresid + middelLine + lines[i].volumeTypeid + endOfLIne;
                else
                    dasSql += secondLine + lines[i].Ordreid + middelLine + lines[i].Volume + middelLine + lines[i].salesPrice + middelLine + lines[i].Waresid + middelLine + lines[i].volumeTypeid + endOfLIne;
            }

            // Replace the last , with a ;
            dasSql = dasSql.Remove(dasSql.Length - 1, 1) + ";";

            return dasSql;
        }



        /// <summary>
        /// This method inserts the order lines into the database 
        /// </summary>
        /// <param name="lines">Input List of orderlines</param>
        /// <returns>Output a boolean</returns>
        public bool NewOrderLinesInDB(List<OrdreLine> lines)
        {
            //Calling the method to make the sql string
            string strSql = CreateOrderLines(lines);
            if (dBa.NewOrUpdateDB(strSql))
                return true;
            else
                return false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool UpdateOrdreLine(int id, OrdreLine line)
        {
            bool updated = false;



            return updated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteOrdreLine(int id)
        {
            bool deleted = false;



            return deleted;
        }

        #endregion

        #region Implemented Methods

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

        public void IsEmpty(string input)
        {
            if (input != null)
            {
                throw new ArgumentException("Parameter cannot be null", "original");
            }
        }

        public int Id()
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
