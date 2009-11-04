﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Axelerate.SlkCourseManagerLogicalLayer.Adapters
{
    /// <summary>
    /// Exception thrown when there are no Learners on a Course. 
    /// </summary>
    public class NoLearnersOnSiteException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public NoLearnersOnSiteException():base()
        { 
        }

        /// <summary>
        /// Constructor with an specific error message.
        /// </summary>
        /// <param name="errorMessage"></param>
        public NoLearnersOnSiteException(string errorMessage): base(errorMessage)
        {
        }

        /// <summary>
        /// Constructor with an specific error message and an inner exception
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="innerEx"></param>
        public NoLearnersOnSiteException(string errorMessage, Exception innerEx)
            : base(errorMessage, innerEx) 
        {
        }
    }
}