namespace TenderApp.Services
{
    public class EnumDisplay<T>(T value, string display)
    {
        public T Value { get; set; } = value;
        public string Display { get; set; } = display;

        public override string ToString() => Display;
    }
}
