namespace formulate.app.Persistence.Internal.Sql.Models
{

    // Namespaces.
    using System;

    using NPoco;

    using Umbraco.Core.Persistence;
    using Umbraco.Core.Persistence.DatabaseAnnotations;


    /// <summary>
    /// Model for the submissions database table.
    /// </summary>
    [TableName(nameof(FormulateSubmission))]
    [PrimaryKey(nameof(SequenceId), AutoIncrement = true)]
    public class FormulateSubmission
    {

        #region Properties

        /// <summary>
        /// The sequential ID.
        /// </summary>
        [PrimaryKeyColumn(AutoIncrement = true, Clustered = true)]
        public long SequenceId { get; set; }


        /// <summary>
        /// The ID generated for the submission outside of the database.
        /// </summary>
        [Index(IndexTypes.UniqueNonClustered)]
        public Guid GeneratedId { get; set; }


        /// <summary>
        /// The date the submission was created.
        /// </summary>
        [Index(IndexTypes.NonClustered)]
        public DateTime CreationDate { get; set; }


        /// <summary>
        /// The ID of the form the submission is for.
        /// </summary>
        public Guid FormId { get; set; }


        /// <summary>
        /// The data values.
        /// </summary>
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string DataValues { get; set; }


        /// <summary>
        /// The file values.
        /// </summary>
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string FileValues { get; set; }


        /// <summary>
        /// The URL the form was submitted from.
        /// </summary>
        [Length(4000)]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string Url { get; set; }


        /// <summary>
        /// The ID of the page the form was submitted from.
        /// </summary>
        [NullSetting(NullSetting = NullSettings.Null)]
        public int? PageId { get; set; }

        #endregion

    }

}