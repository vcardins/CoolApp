#region credits
// ***********************************************************************
// Assembly	: TaskForceManager
// Author	: Rod Johnson
// Created	: 03-17-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using TaskForceManager.Metadata.Attributes;

namespace TaskForceManager.Extensions.HtmlHelpers
{
    #region

    

    #endregion

    public class BootstrapInputOptions
    {
        public string Type { get; set; }

        public TextboxSize Size { get; set; }

        public string Placeholder { get; set; }

        public bool Multiline { get; set; }

        public string Mask { get; set; }

        public FocusBehavior FocusBehavior { get; set; }
    }
}