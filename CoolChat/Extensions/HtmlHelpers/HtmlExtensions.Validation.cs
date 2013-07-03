using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using TaskForceManager.Metadata.Attributes;

namespace TaskForceManager.Extensions.HtmlHelpers
{
	public static partial class HtmlExtensions
	{
		public static MvcHtmlString ValidatedEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			if (htmlHelper.ViewData.ModelMetadata.ModelType == null)
			{
				return new MvcHtmlString(String.Empty);
			}

			var tagBuilder = new TagBuilder("input");
			var name = ExpressionHelper.GetExpressionText(expression);
			var validation = String.Empty;
            var rules = new List<string>();

			//Try to get the attributes for the property
			Object[] objects = typeof(TModel).GetProperty(name).GetCustomAttributes(true);
		    
			foreach (Attribute attribute in objects)
			{
				if (attribute.GetType() == typeof(RequiredAttribute))
				{
                    rules.Add("required");
				}
				if (attribute.GetType() == typeof(RangeAttribute))
				{
					var min = ((RangeAttribute)attribute).Minimum;
					var max = ((RangeAttribute)attribute).Maximum;
                    rules.Add( String.Format("min[{0}],max[{1}]", min, max) );
				}
                if (attribute.GetType() == typeof(EqualToAttribute))
                {
                    var otherFieldName = ((EqualToAttribute)attribute).OtherPropertyDisplayName;
                    rules.Add( String.Format("equals[{0}]", otherFieldName));
                }
                if (attribute is EmailAddressAttribute)
                {
                    rules.Add("custom[email]");
                }
                if (attribute is UrlAttribute)
                {
                    rules.Add("custom[url]");
                }

				if (attribute.GetType() == typeof(StringLengthAttribute))
				{
					var minimumLength = ((StringLengthAttribute)attribute).MinimumLength;
					var maximumLength = ((StringLengthAttribute)attribute).MaximumLength;
					string validator = String.Format("maxSize[{0}]", maximumLength);

					if (minimumLength >= 0)
					{
						validator += String.Format(",minSize[{0}]", minimumLength);
					}
                    rules.Add(validator);
				}
			}

           validation += String.Format("validate[{0}]", String.Join(",", rules.ToArray()));

			tagBuilder.GenerateId(name);
			tagBuilder.AddCssClass(validation);
			return new MvcHtmlString(tagBuilder.ToString());
		}
	}
}