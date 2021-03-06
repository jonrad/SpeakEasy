namespace SpeakEasy
{
    public class Header
    {
        public Header(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }

        public string Value { get; private set; }

        internal ParsedHeaderValue ParseValue()
        {
            var parser = new HeaderParser(Value);
            return parser.Parse();
        }
    }
}