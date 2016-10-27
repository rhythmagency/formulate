namespace formulate.app.DataValues.Suppliers.Kinds
{

    // Namespaces.
    using core.Types;
    using System.Collections.Generic;


    /// <summary>
    /// Supplies a list of US states.
    /// </summary>
    public class UsStateSupplier : ISupplyValueAndLabelCollection
    {

        #region Public Properties

        /// <summary>
        /// The name of this supplier.
        /// </summary>
        public string Name { get; } = "List of States in the United States";

        #endregion


        #region Private Properties

        /// <summary>
        /// The US states.
        /// </summary>
        private List<ValueAndLabel> AllStates { get; } = new List<ValueAndLabel>()
            {
                new ValueAndLabel()
                {
                    Value = "AL",
                    Label = "Alabama"
                },
                new ValueAndLabel()
                {
                    Value = "AK",
                    Label = "Alaska"
                },
                new ValueAndLabel()
                {
                    Value = "AS",
                    Label = "American Samoa"
                },
                new ValueAndLabel()
                {
                    Value = "AZ",
                    Label = "Arizona"
                },
                new ValueAndLabel()
                {
                    Value = "AR",
                    Label = "Arkansas"
                },
                new ValueAndLabel()
                {
                    Value = "CA",
                    Label = "California"
                },
                new ValueAndLabel()
                {
                    Value = "CO",
                    Label = "Colorado"
                },
                new ValueAndLabel()
                {
                    Value = "CT",
                    Label = "Connecticut"
                },
                new ValueAndLabel()
                {
                    Value = "DE",
                    Label = "Delaware"
                },
                new ValueAndLabel()
                {
                    Value = "FL",
                    Label = "Florida"
                },
                new ValueAndLabel()
                {
                    Value = "GA",
                    Label = "Georgia"
                },
                new ValueAndLabel()
                {
                    Value = "GU",
                    Label = "Guam"
                },
                new ValueAndLabel()
                {
                    Value = "HI",
                    Label = "Hawaii"
                },
                new ValueAndLabel()
                {
                    Value = "ID",
                    Label = "Idaho"
                },
                new ValueAndLabel()
                {
                    Value = "IL",
                    Label = "Illinois"
                },
                new ValueAndLabel()
                {
                    Value = "IN",
                    Label = "Indiana"
                },
                new ValueAndLabel()
                {
                    Value = "IA",
                    Label = "Iowa"
                },
                new ValueAndLabel()
                {
                    Value = "KS",
                    Label = "Kansas"
                },
                new ValueAndLabel()
                {
                    Value = "KY",
                    Label = "Kentucky"
                },
                new ValueAndLabel()
                {
                    Value = "LA",
                    Label = "Louisiana"
                },
                new ValueAndLabel()
                {
                    Value = "ME",
                    Label = "Maine"
                },
                new ValueAndLabel()
                {
                    Value = "MD",
                    Label = "Maryland"
                },
                new ValueAndLabel()
                {
                    Value = "MA",
                    Label = "Massachusetts"
                },
                new ValueAndLabel()
                {
                    Value = "MI",
                    Label = "Michigan"
                },
                new ValueAndLabel()
                {
                    Value = "MN",
                    Label = "Minnesota"
                },
                new ValueAndLabel()
                {
                    Value = "MS",
                    Label = "Mississippi"
                },
                new ValueAndLabel()
                {
                    Value = "MO",
                    Label = "Missouri"
                },
                new ValueAndLabel()
                {
                    Value = "MT",
                    Label = "Montana"
                },
                new ValueAndLabel()
                {
                    Value = "NE",
                    Label = "Nebraska"
                },
                new ValueAndLabel()
                {
                    Value = "NV",
                    Label = "Nevada"
                },
                new ValueAndLabel()
                {
                    Value = "NH",
                    Label = "New Hampshire"
                },
                new ValueAndLabel()
                {
                    Value = "NJ",
                    Label = "New Jersey"
                },
                new ValueAndLabel()
                {
                    Value = "NM",
                    Label = "New Mexico"
                },
                new ValueAndLabel()
                {
                    Value = "NY",
                    Label = "New York"
                },
                new ValueAndLabel()
                {
                    Value = "NC",
                    Label = "North Carolina"
                },
                new ValueAndLabel()
                {
                    Value = "ND",
                    Label = "North Dakota"
                },
                new ValueAndLabel()
                {
                    Value = "MP",
                    Label = "Northern Mariana Islands"
                },
                new ValueAndLabel()
                {
                    Value = "OH",
                    Label = "Ohio"
                },
                new ValueAndLabel()
                {
                    Value = "OK",
                    Label = "Oklahoma"
                },
                new ValueAndLabel()
                {
                    Value = "OR",
                    Label = "Oregon"
                },
                new ValueAndLabel()
                {
                    Value = "PA",
                    Label = "Pennsylvania"
                },
                new ValueAndLabel()
                {
                    Value = "PR",
                    Label = "Puerto Rico"
                },
                new ValueAndLabel()
                {
                    Value = "RI",
                    Label = "Rhode Island"
                },
                new ValueAndLabel()
                {
                    Value = "SC",
                    Label = "South Carolina"
                },
                new ValueAndLabel()
                {
                    Value = "SD",
                    Label = "South Dakota"
                },
                new ValueAndLabel()
                {
                    Value = "TN",
                    Label = "Tennessee"
                },
                new ValueAndLabel()
                {
                    Value = "TX",
                    Label = "Texas"
                },
                new ValueAndLabel()
                {
                    Value = "VI",
                    Label = "U.S. Virgin Islands"
                },
                new ValueAndLabel()
                {
                    Value = "UT",
                    Label = "Utah"
                },
                new ValueAndLabel()
                {
                    Value = "VT",
                    Label = "Vermont"
                },
                new ValueAndLabel()
                {
                    Value = "VA",
                    Label = "Virginia"
                },
                new ValueAndLabel()
                {
                    Value = "WA",
                    Label = "Washington"
                },
                new ValueAndLabel()
                {
                    Value = "DC",
                    Label = "Washington, D.C."
                },
                new ValueAndLabel()
                {
                    Value = "WV",
                    Label = "West Virginia"
                },
                new ValueAndLabel()
                {
                    Value = "WI",
                    Label = "Wisconsin"
                },
                new ValueAndLabel()
                {
                    Value = "WY",
                    Label = "Wyoming"
                }
            };

        #endregion


        #region Methods

        /// <summary>
        /// Returns the states.
        /// </summary>
        /// <returns>
        /// The US states.
        /// </returns>
        public IEnumerable<ValueAndLabel> GetValues()
        {
            return AllStates;
        }

        #endregion

    }

}