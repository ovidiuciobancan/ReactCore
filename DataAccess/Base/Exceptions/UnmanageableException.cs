using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Exceptions
{
    /// <summary>
    /// Manageable exception that cannot be caught from DataAccess Layer
    /// </summary>
    public class UnmanageableException : DataAccessException 
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="e"></param>
        public UnmanageableException(Exception e)
            : base(e)
        {

        }
    }
}
