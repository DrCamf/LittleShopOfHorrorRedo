using System;
using System.Collections.Generic;
using System.Text;

namespace LittleShopOfHorrorRedo
{
    public class VolumeType : Type
    {
        #region Public Properties
        public int volumtTypeID { get; private set; }
        public string volumeType { get; set; }

        #endregion

        #region Constructor
        public VolumeType()
        {

        }

        public VolumeType(int volumetypeid, string volumetype) : this()
        {
            volumtTypeID = volumetypeid;
            volumeType = volumetype;
        }

        DataAccess dBa = new DataAccess();
        #endregion

        #region Methods

        public bool NewVolumeType(string type)
        {
            return true;
        }

        public override List<string> GetAllTypes()
        {
            List<string> types = new List<string>();
            try
            {
                types = dBa.GetListStrFromDb("SELECT volumeTypeName FROM VolumeType");
            }
            catch { throw new ArgumentException("Query Failure", "original"); }


            return types;
        }


        #endregion
    }
}
