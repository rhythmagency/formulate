﻿namespace formulate.app.Helpers
{

    // Namespaces.
    using System;
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// Helps with GUID operations.
    /// </summary>
    public class GuidHelper
    {

        #region Methods

        /// <summary>
        /// Converts a GUID to a string.
        /// </summary>
        /// <param name="guid">
        /// The GUID.
        /// </param>
        /// <returns>
        /// The string representation of the GUID.
        /// </returns>
        /// <remarks>
        /// This ensures all GUIDs are in the expected format.
        /// </remarks>
        public static string GetString(Guid guid)
        {
            return guid.ToString("N");
        }


        /// <summary>
        /// Converts a GUID collection to an array of strings.
        /// </summary>
        /// <param name="guids">
        /// The GUID collection.
        /// </param>
        /// <returns>
        /// The string representations of the GUID's.
        /// </returns>
        /// <remarks>
        /// This ensures all GUIDs are in the expected format.
        /// </remarks>
        public static string[] GetStrings(IEnumerable<Guid> guids)
        {
            return guids.Select(x => GetString(x)).ToArray();
        }


        /// <summary>
        /// Converts a string GUID to a GUID instance.
        /// </summary>
        /// <param name="value">The string GUID.</param>
        /// <returns>
        /// The GUID instance, or the empty GUID.
        /// </returns>
        /// <remarks>
        /// This only parses GUIDs in the expected format.
        /// </remarks>
        public static Guid GetGuid(string value)
        {
            var guid = default(Guid);
            if (Guid.TryParseExact(value, "N", out guid))
            {
                return guid;
            }
            else
            {
                return Guid.Empty;
            }
        }

        #endregion

    }

}

//TODO: Get rid of static functions.