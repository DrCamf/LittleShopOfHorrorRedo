using System;
using System.Collections.Generic;
using System.Text;

namespace LittleShopOfHorrorRedo
{
    public class Account : IGenerelControl, ISpecifigUserControl
    {
        #region Public Properties

        public string aLogin { get; set; }

        public string aPassword { get; set; }

        #endregion

        #region Constructor

        public Account()
        {

        }

        public Account(string login, string pass) : this()
        {
            aLogin = login;
            aPassword = pass;
        }

        #endregion

        #region Methods

        public bool CreateNewAccount(Account account)
        {



            return true;
        }





        #endregion



        #region Implemented Methos
        public bool EmailExists(string email)
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty(string input)
        {
            throw new NotImplementedException();
        }

        public float IsFloat(string flInn)
        {
            throw new NotImplementedException();
        }

        public int IsInt(string intIn)
        {
            throw new NotImplementedException();
        }

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

        #endregion
    }
}
