namespace LittleShopOfHorrorRedo
{
    /// <summary>
    /// The person class is the 
    /// </summary>
    public abstract class Person
    {
        /// <summary>
        ///  the Basic ID of the person which basically comes from the database
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// The first name can be set or read
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The sir name can be read or set
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The email can be set or read
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The phone number can be read or set
        /// </summary>
        public int tlfNr { get; set; }

        /// <summary>
        /// A method that deletes a person object, which can be a customer or sales person, from the database via the persons ID
        /// </summary>
        /// <param name="id">Input is an integer the person ID</param>
        /// <returns>Output a boolean</returns>
        public abstract bool DeletePerson(int id);
    }
}
