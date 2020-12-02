using System;
using System.Collections.Generic;
using System.Text;

namespace LittleShopOfHorrorRedo
{
    public class Wares : IGenerelControl
    {



        #region Public Properties

        public int waresID { get; set; }

        public string wareName { get; set; }

        public string description { get; set; }

        public float buyPrice { get; set; }

        public int volume { get; set; }

        public string volumeType { get; set; }
        public int volumeTypeID { get; set; }

        #endregion



        #region Constructor
        public Wares()
        {

        }

        public Wares(string wName)
        {
            wareName = wName;
        }

        public Wares(int wid, string wname, int wvolume, float wprice)
        {
            waresID = wid;
            wareName = wname;
            volume = wvolume;
            buyPrice = wprice;
        }

        public Wares(int wid, string wname, string wdescrip, float wprice, int wvolume, int wvolumetypeid) : this()
        {
            waresID = wid;
            wareName = wname;
            description = wdescrip;
            buyPrice = wprice;
            volume = wvolume;
            volumeTypeID = wvolumetypeid;
        }



        DataAccess dBa = new DataAccess();
        #endregion



        #region Methods
        /// <summary>
        /// This takes control of the input of new wares 
        /// </summary>
        /// <param name="wares">Input a wares object</param>
        /// <returns>output a bool</returns>
        public bool NewWares(Wares ware1)
        {
            bool wareMade = false;

            Wares ware = new Wares();
            List<Wares> wares = new List<Wares>();
            int lastid = dBa.GetLastId("Wares");

            ware1.waresID = lastid < 0 ? 1 : lastid + 1;

            /// <summary>
            /// Stored procedure InsertWare with parameters  @wareName varchar(255), @volume float, @volumeType int
            /// </summary>
            if (dBa.NewOrUpdateDB("CALL InsertWare (Name = '" + ware1.wareName + "', Volume = ' " + ware1.volume + "' , VolumeType = '" + ware1.volumeType + "');") &&

            /// <summary>
            /// Stored procedure InsertBoughtWare with parameters  @boughtprice float, @volume int, @volumeType int, @waresID int
            /// </summary>
            dBa.NewOrUpdateDB("CALL InsertBoughtWare (Boughtprice = '" + ware1.buyPrice + "', Volume = ' " + ware1.volume + "' , VolumeType = '"
            + ware1.volumeTypeID + "', WaresID = '" + ware1.waresID + "',BoughtDate = '" + "" + "');"))
                wareMade = true;

            return wareMade;
        }

        /// <summary>
        /// Method to insert new wares than already exists in the database
        /// </summary>
        /// <param name="ware"></param>
        /// <returns></returns>
        public bool NewBoughtWare(Wares ware)
        {

            /// <summary>
            /// Stored procedure InsertBoughtWare with parameters  @boughtprice float, @volume int, @volumeType int, @waresID int
            /// </summary>
            try
            {
                if (dBa.NewOrUpdateDB("CALL InsertBoughtWare (Boughtprice = '" + ware.buyPrice + "', Volume = ' " + ware.volume + "' , VolumeType = '"
            + ware.volumeTypeID + "', WaresID = '" + ware.waresID + "',BoughtDate = '" + "" + "');"))
                    return true;
                else
                    return false;
            }
            catch { return false; throw new ArgumentException("Insert bought ware failed", "original"); }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ware"></param>
        /// <returns></returns>
        public bool updateWares(int id, Wares ware)
        {
            bool updated = false;
            int volume = dBa.GetWareVolume(id);
            /// <summary>
            /// Stored procedure UpdateWare with parameters @id int, @volume int
            /// </summary>
            volume = volume + ware.volume;
            if (dBa.NewOrUpdateDB("CALL UpdateWare (Id = '" + id + "', Volume = '" + volume + "');"))
                updated = true;

            return updated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool deleteWares(int id)
        {
            bool deleted = false;

            //Database delete ware by id 
            if (dBa.NewOrUpdateDB("DELETE FROM Wares WHERE id = " + id + ";"))
                deleted = true;

            return deleted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Wares> GetWaresByType(int id)
        {


            List<Wares> ware = new List<Wares>();
            ware = dBa.GetWareInfoInt(id);

            if (ware == null || ware.Count == 0)
                throw new ArgumentException("Wares list is empty", "original");
            else
                return ware;
        }

        public List<int> WareIDs(int typeid)
        {
            List<int> mel = new List<int>();
            mel = dBa.GetListFromDb("SELECT id FROM wares WHERE waresTypeID= " + typeid + " ORDER BY name ASC");
            return mel;
        }

        public Dictionary<int, string> GetWareTypesDic(int id)
        {
            Dictionary<int, string> comboSource = new Dictionary<int, string>();
            try
            {
                comboSource = dBa.GetWaresBytypes(id);
            }
            catch { throw new ArgumentException("Query Failure", "original"); }


            return comboSource;
        }

        /// <summary>
        /// Method to all the wares in the database
        /// </summary>
        /// <returns>Output a list of Wares</returns>
        public List<Wares> GeAllWareListFromDB()
        {
            List<Wares> ware = new List<Wares>();
           

            try
            {
                ware = dBa.GetWareListFromDB("SELECT id, name, volume, minPrice FROM Wares");
            }
            catch { throw new ArgumentException("Qeury error", "original"); }



            return ware;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //public List<Wares> GetWareDescrip(string name)
        //{
        //    List<Wares> ware = new List<Wares>();
        //    ware = dBa.GetWareInfo( name );
        //    if (ware == null || ware.Count == 0)
        //        throw new ArgumentException("Wares list is empty", "original");
        //    else
        //        return ware;
        //}

        public List<Wares> GetWareDescrip(string name)
        {
            List<Wares> ware = new List<Wares>();
            ware = dBa.GetWareInfo(name);
            if (ware == null || ware.Count == 0)
                throw new ArgumentException("Wares list is empty", "original");
            else
                return ware;
        }

        /// <summary>
        /// Method to get wares id via a string wareName of wares
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetidViaString(int wid, string name)
        {
            int waid;
            try
            {
                waid = dBa.GetWaresIdFromString(wid, name);

                return waid;
            }
            catch { throw new ArgumentException("no wares id available", "original"); }

        }

        /// <summary>
        /// Method to get the current volume of a given ware in the database
        /// </summary>
        /// <param name="id">input the id of the ware</param>
        /// <returns>Output an integer with the volume of the ware</returns>
        public int GetWareVolume(int id)
        {
            int vol = 0;
            try
            {
                vol = dBa.GetWareVolume(id);
            }
            catch { throw new ArgumentException("Query Failure", "original"); }



            return vol;
        }

        /// <summary>
        /// Method to get the mininum price for a ware in the database
        /// </summary>
        /// <param name="id">Input the id of the ware</param>
        /// <returns>Output a float of the minimum price for the ware</returns>
        public float GetMinPrice(int id)
        {
            float price;
            try
            {
                price = dBa.GetWareMinPrice(id);
            }
            catch { throw new ArgumentException("Query Failure", "original"); }

            return price;
        }

        /// <summary>
        /// Method that find the volume 0f a given ware id and subtracts the new volume with it
        /// </summary>
        /// <param name="id">Input The id of the give ware</param>
        /// <param name="newVolume">Input The new volume </param>
        /// <returns>Output a boolean</returns>
        public bool UpdateWaresVolume(List<int> ids, List<float> newVolume, bool isSale)
        {
            List<float> volume = new List<float>();
            try
            {
                volume = dBa.GetListFloatFromDb(GetVolumesFromDBString(ids));
            }
            catch { throw new ArgumentException("Query Failure", "original"); }

            if (volume.Count == 0)
            {
                return false;
            }
            else
            {
                if (isSale)
                    for (int i = 0; i < newVolume.Count; i++)
                    {
                        volume[i] = volume[i] - newVolume[i];
                    }
                else
                    for (int i = 0; i < newVolume.Count; i++)
                    {
                        volume[i] = volume[i] + newVolume[i];
                    }

                try
                {
                    dBa.NewOrUpdateDB(CreateVolumeString(newVolume, ids));
                    return true;

                }
                catch { throw new ArgumentException("Query Failure", "original"); }
            }
        }


        private string CreateVolumeString(List<float> volumes, List<int> waresIds)
        {
            string startStr = "UPDATE Wares SET volume = ";
            string mellemStr = "  WHERE Wares.id = ";
            string endStr = "; ";
            string dasSql = "";

            for (int i = 0; i < volumes.Count; i++)
            {
                dasSql += startStr + volumes[i] + mellemStr + waresIds[i] + endStr;
            }

            return dasSql;
        }

        private string GetVolumesFromDBString(List<int> waresIds)
        {
            string startStr = "SELECT volume FROM Wares WHERE id = ";
            string endStr = "; ";
            string dasSql = "";

            for (int i = 0; i < waresIds.Count; i++)
            {
                dasSql += startStr + waresIds[i] + endStr;
            }

            return dasSql;

        }




        /*
        public Wares GetWaresVolInfo(string name)
        {
            Wares war = new Wares();
            war = dBa.GetWareInfo(name);

            return war;
        }*/
        #endregion

        #region Implemented Methods

        public int Id()
        {
            return waresID;
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

        public void IsEmpty(string input)
        {
            if (input != null)
            {
                throw new ArgumentException("Parameter cannot be null", "original");
            }
        }

        public bool IsStrFloat(string str)
        {
            if (float.TryParse(str, out float price))
                return true;
            else
                return false;
        }

        public bool IsStrInt(string str)
        {
            if (int.TryParse(str, out int number))
                return true;
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
