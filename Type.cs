
using System.Collections.Generic;


namespace LittleShopOfHorrorRedo
{
    public class Type
    {
        public int typeID { get; set; }

        public string typeName { get; set; }


        public Type()
        {

        }
        DataAccess dBa = new DataAccess();

        public virtual bool NewType(string NType)
        {
            return true;
        }

        public virtual List<string> GetAllTypes()
        {
            List<string> mel = new List<string>();
            //mel = dBa.GetWareType().ToList();
            return mel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual List<int> GetTypeID()
        {
            List<int> mel = new List<int>();

            return mel;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetFirstType()
        {
            string mel = "";

            return mel;
        }
    }
}
