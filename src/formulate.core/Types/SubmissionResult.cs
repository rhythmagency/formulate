namespace formulate.core.Types
{
    
    //  Namespaces.
    using System.Collections.Generic;

    /// <summary>
    /// The result of a form submission.
    /// </summary>
    public class SubmissionResult
    {
        public SubmissionResult()
        {
            ValidationErrors = new List<ValidationError>();
        }
        
        public bool Success { get; set; }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }

}