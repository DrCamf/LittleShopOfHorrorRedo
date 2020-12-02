using System;
using System.Collections.Generic;
using System.Text;

namespace LittleShopOfHorrorRedo
{

    /// <summary>
    /// The interface for more person concentrated inputs as if information already exists in the database
    /// </summary>
    interface ISpecifigUserControl
    {

        /// <summary>
        /// A simple control if the input email already exists in the database
        /// </summary>
        /// <param name="email">input is a string the email in question</param>
        /// <returns>Output a boolean </returns>
        bool EmailExists(string email);

        /// <summary>
        /// This is a control to see if the input phonenumber is allowed in the danish phone system
        /// </summary>
        /// <param name="nbr">Input is an integer the phonenumber</param>
        /// <returns>output a boolean</returns>
        bool IsRealPhoneNbr(int nbr);
    }
}
