namespace MicroShop.Common.Exceptions;

public abstract class DomainStateException : Exception
{
    /// <summary>
    /// لیست پارامتر‌های خطا
    /// در صورتی که پارامتری وجود داشته باشد message را به صورت الگو ارسال کرده و مقادیر پارامتر‌ها در محل مخصوص در الگو قرار می‌گیرند.
    /// </summary>
    public string[] Parameters { get; set; }

    protected DomainStateException(string message, params string[] parameters) : base(message)
    {
        Parameters = parameters;

    }

    public override string ToString()
    {
        if (Parameters?.Length < 1)
        {
            return Message;
        }


        string result = Message;

        for (int i = 0; i < Parameters?.Length; i++)
        {
            string placeHolder = $"{{{i}}}";
            result = Message.Replace(placeHolder, Parameters[i]);
        }

        return result;
    }
}