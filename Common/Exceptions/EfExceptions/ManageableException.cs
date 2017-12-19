using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    /// <summary>
    /// Manageable exception that can be caught from DataAccess Layer
    /// </summary>
    public class ManageableException : DataAccessException 
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="e"></param>
        public ManageableException(Exception e)
            : base(e)
        {

        }
    }
}
