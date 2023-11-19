using System.Collections;

namespace UrbanVogue.Models.Core
{
    public class ContentBody : IEnumerable
    {
        public Dictionary<string, string> Dictionary { get; private set; }

        public ContentBody(Dictionary<string, string> values)
        {
            Dictionary = new Dictionary<string, string>(values);
        }

        public ContentBody()
        {
            Dictionary = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
            => Dictionary.Add(key, value);


        public void Clear()
            => Dictionary.Clear();

        public IEnumerator GetEnumerator() => Dictionary.GetEnumerator();
    }
}
