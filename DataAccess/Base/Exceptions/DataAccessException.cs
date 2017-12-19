using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Exceptions
{
    /// <summary>
    /// Base Wrapper Exception occurred in Data Access Layer  
    /// </summary>
    public class DataAccessException : Exception
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="e"></param>
        public DataAccessException(Exception e)
            : base(e.Message, e.InnerException)
        {
            
        }
    }
}
