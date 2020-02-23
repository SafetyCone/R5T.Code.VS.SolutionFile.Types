using System;
using System.Collections.Generic;


namespace R5T.Code.VisualStudio.Model
{
    /// <summary>
    /// A generic solution file global section whose data is structured simply as (string) lines.
    /// </summary>
    public class GenericSolutionFileGlobalSection : SolutionFileGlobalSectionBase
    {
        #region Static

        public static GenericSolutionFileGlobalSection New(string name, PreOrPostSolution preOrPostSolution)
        {
            var output = new GenericSolutionFileGlobalSection
            {
                Name = name,
                PreOrPostSolution = preOrPostSolution,
            };
            return output;
        }

        #endregion


        public List<string> Lines { get; } = new List<string>();

        public override IEnumerable<string> ContentLines => this.Lines;
    }
}
