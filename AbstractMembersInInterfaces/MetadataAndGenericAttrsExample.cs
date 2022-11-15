using System.ComponentModel.DataAnnotations;

namespace Demo.StaticAbstractInterfaceMembers
{
    public class ExampleDto
    {
        [Pattern<Email>]
        [Pattern<PhoneNumber>]
        public string Email { get; set; }

        [Pattern<PhoneNumber>]
        public string PhoneNumber { get; set; }

        [Pattern<OrderId>]
        public string OrderNumber { get; set; }
    }

    public interface IPatternTemplate
    {
        static abstract string ErrorMessage { get; }

        static abstract string Pattern { get; }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)] //Vad tror ni händer vid false?
    public class PatternAttribute<T> : RegularExpressionAttribute where T : IPatternTemplate
    {
        public PatternAttribute() : base(T.Pattern)
        {
            ErrorMessage = T.ErrorMessage;
        }
    }

    public class Email : IPatternTemplate
    {
        public static string ErrorMessage => "Value must be a valid e-mail address";
        public static string Pattern => "\\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,}\\b";
    }

    public class PhoneNumber : IPatternTemplate
    {
        public static string ErrorMessage => "Value must be a valid phone number";
        public static string Pattern => "^([+]46)\\s*(7[0236])\\s*(\\d{4})\\s*(\\d{3})$";
    }

    public class OrderId : IPatternTemplate
    {
        public static string ErrorMessage => "Order Id must be in the format SE-NNNNNN";
        public static string Pattern => "SE-\\d{6}";
    }











    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class OldStylePatternAttribute : RegularExpressionAttribute
    {
        private static readonly Dictionary<Type, Tuple<string, string>> Patterns = new()
        {
            {typeof(Email), new Tuple<string, string>(Email.Pattern, Email.ErrorMessage)},
            {typeof(PhoneNumber), new Tuple<string, string>(PhoneNumber.Pattern, PhoneNumber.ErrorMessage)}
        };

        public OldStylePatternAttribute(Type type) : base(Patterns[type].Item1)
        {
            ErrorMessage = Patterns[type].Item2;
        }

        public OldStylePatternAttribute(string pattern, string errorMessage) : base(pattern)
        {
            ErrorMessage = errorMessage;
        }
    }

}
