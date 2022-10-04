using System.ComponentModel.DataAnnotations;

namespace EcommProject.Validation
{
    public class ValidationModel:ValidationAttribute
    {
        private readonly string allowedDomain;
        private readonly string track;

        public ValidationModel(string allowedDomain,string track)
        {
            this.allowedDomain = allowedDomain;
            this.track = track;
        }
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                switch (track)
                {
                    case "email":
                        string[] strings = value.ToString().Split('@');
                        return strings[1].ToUpper() == allowedDomain.ToUpper();

                    case "name":
                        return value.ToString().Length > 2;

                    default:
                        return false;
                }
            }
            else
                return false;
        }
    }
}
