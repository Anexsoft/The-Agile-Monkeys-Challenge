using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CRM.Common.Validator
{
    /// <summary>
    /// Basic XSS validator
    /// </summary>
    public class CrossSiteScriptingAttribute : ValidationAttribute
    {
        public CrossSiteScriptingAttribute() 
        {
            ErrorMessage = "Cross-Site Scripting attack is detected.";
        }

        public override bool IsValid(object value)
        {
            var strValue = value as string;

            if (!string.IsNullOrEmpty(strValue))
            {
                return !Regex.Match(strValue, "<[^>]*?>", RegexOptions.IgnoreCase).Success;
            }

            return true;
        }
    }
}
