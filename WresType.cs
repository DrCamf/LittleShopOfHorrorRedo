using System;
using System.Collections.Generic;
using System.Linq;


namespace LittleShopOfHorrorRedo
{
    public class WaresType : Type, IGenerelControl
    {

        #region Public Properties
        public int wareTypeId { get; set; }

        public string waresTypeName { get; set; }

        #endregion

        #region Constructor
        public WaresType()
        {

        }

        public WaresType(int id, string name) : this()
        {
            wareTypeId = id;
            waresTypeName = name;
        }

        DataAccess dBa = new DataAccess();

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override List<string> GetAllTypes()
        {
            List<string> mel = new List<string>();
            mel = dBa.GetWareType().ToList();
            return mel;
        }

        public List<WaresType> GetAllTypen()
        {
            List<WaresType> waretypen = dBa.GetWareTypen();

            return waretypen;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override List<int> GetTypeID()
        {
            List<int> mel = new List<int>();
            mel = dBa.GetListFromDb("SELECT id FROM WaresType ORDER BY waresTypeName ASC");
            return mel;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetFirstType()
        {
            string mel;
            mel = dBa.GetLastWareType();
            return mel;
        }

        #endregion

        #region Implemented methods
        public float IsFloat(string flInn)
        {
            throw new NotImplementedException();
        }

        public int IsInt(string intIn)
        {
            throw new NotImplementedException();
        }

        public void IsEmpty(string input)
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
