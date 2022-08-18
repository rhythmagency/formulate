namespace Formulate.Core.DataValues.UsStates
{
    // Namespaces.
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A data values definition for providing US states and territories.
    /// </summary>
    public sealed class UsStatesDataValuesDefinition : DataValuesDefinition
    {
        /// <summary>
        /// Constants related to <see cref="UsStatesDataValuesDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "6CFFC488670E4CC7B965A5F3676BA333";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "List of States and Territories in the United States";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-dynamic-data-values";

            /// <summary>
            /// The icon.
            /// </summary>
            public const string Icon = "icon-formulate-globe";
        }

        /// <inheritdoc />
        public override Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public override string Name => Constants.Name;

        /// <inheritdoc />
        public override string Directive => Constants.Directive;

        /// <inheritdoc />
        public override string Icon => Constants.Icon;

        /// <inheritdoc />
        public override IDataValues CreateDataValues(IDataValuesSettings settings)
        {
            var items = new KeyValuePair<string, string>[]
            {
                new("Alabama", "AL"),
                new("Alaska", "AK"),
                new("American Samoa", "AS"),
                new("Arizona", "AZ"),
                new("Arkansas", "AR"),
                new("California", "CA"),
                new("Colorado", "CO"),
                new("Connecticut", "CT"),
                new("Delaware", "DE"),
                new("Florida", "FL"),
                new("Georgia", "GA"),
                new("Guam", "GU"),
                new("Hawaii", "HI"),
                new("Idaho", "ID"),
                new("Illinois", "IL"),
                new("Indiana", "IN"),
                new("Iowa", "IA"),
                new("Kansas", "KS"),
                new("Kentucky", "KY"),
                new("Louisiana", "LA"),
                new("Maine", "ME"),
                new("Maryland", "MD"),
                new("Massachusetts", "MA"),
                new("Michigan", "MI"),
                new("Minnesota", "MN"),
                new("Mississippi", "MS"),
                new("Missouri", "MO"),
                new("Montana", "MT"),
                new("Nebraska", "NE"),
                new("Nevada", "NV"),
                new("New Hampshire", "NH"),
                new("New Jersey", "NJ"),
                new("New Mexico", "NM"),
                new("New York", "NY"),
                new("North Carolina", "NC"),
                new("North Dakota", "ND"),
                new("Northern Mariana Islands", "MP"),
                new("Ohio", "OH"),
                new("Oklahoma", "OK"),
                new("Oregon", "OR"),
                new("Pennsylvania", "PA"),
                new("Puerto Rico", "PR"),
                new("Rhode Island", "RI"),
                new("South Carolina", "SC"),
                new("South Dakota", "SD"),
                new("Tennessee", "TN"),
                new("Texas", "TX"),
                new("U.S. Virgin Islands", "VI"),
                new("Utah", "UT"),
                new("Vermont", "VT"),
                new("Virginia", "VA"),
                new("Washington", "WA"),
                new("Washington, D.C.", "DC"),
                new("West Virginia", "WV"),
                new("Wisconsin", "WI"),
                new("Wyoming", "WY")
            };

            return new DataValues(settings, items);
        }

        public override object GetBackOfficeConfiguration(IDataValuesSettings settings)
        {
            return default;
        }
    }
}