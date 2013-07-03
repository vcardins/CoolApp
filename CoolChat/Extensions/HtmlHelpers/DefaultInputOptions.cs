#region credits
// ***********************************************************************
// Assembly	: Deten
// Author	: Momentum
// Created	: 03-17-2013
// 
// Last Modified By : Momentum
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using Deten.Web.Metadata.Attributes;

namespace Deten.Web.Extensions.HtmlHelpers
{
    #region

    

    #endregion

    public class DefaultInputOptions
    {
        public string Type { get; set; }

        public TextboxSize Size { get; set; }

        public string Placeholder { get; set; }

        public bool Multiline { get; set; }

        public string Mask { get; set; }

        public FocusBehavior FocusBehavior { get; set; }
    }
}