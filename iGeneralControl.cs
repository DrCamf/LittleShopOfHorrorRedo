
namespace LittleShopOfHorrorRedo
{
    /// <summary>
    /// Interface for the general control of input if integer, float or if the ipnut is null
    /// </summary>
    interface IGenerelControl
    {
        /// <summary>
        /// This control if the string can be converted to float
        /// </summary>
        /// <param name="flInn">Input is a string</param>
        /// <returns>returns a float</returns>
        float IsFloat(string flInn);

        bool IsStrFloat(string str);

        /// <summary>
        /// This controls if the string can be converted to an int
        /// </summary>
        /// <param name="intIn">String input</param>
        /// <returns>Integer output</returns>
        int IsInt(string intIn);

        bool IsStrInt(string str);

        /// <summary>
        /// This Controls if the string is null
        /// </summary>
        /// <param name="input">Input string</param>
        bool IsEmpty(string input);
    }
}
