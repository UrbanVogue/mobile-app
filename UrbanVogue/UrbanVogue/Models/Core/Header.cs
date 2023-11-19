using System.Collections;

namespace UrbanVogue.Models.Core
{
    public class Header : IEnumerable
    {
        private readonly Dictionary<string, string> _dictionary;

        public Header()
        {
            _dictionary = new Dictionary<string, string>();
        }

        public Header(Dictionary<string, string> headers)
        {
            _dictionary = headers;
        }

        public void Add(string key, string value)
        {
            _dictionary.Add(key, value);
        }

        public void Remove(string key)
        {
            _dictionary.Remove(key);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public int Count()
        {
            return _dictionary.Count;
        }   

        public Dictionary<string, string> GetHeaders()
        {
            return _dictionary;
        }

        public string GetHeader(string key)
        {
            return _dictionary[key];
        }
        public IEnumerator GetEnumerator()
            => _dictionary.GetEnumerator();


        public static implicit operator Dictionary<string, string>(Header header) => header._dictionary;


        public static implicit operator KeyValuePair<string, string>[](Header header) => header._dictionary.ToArray();

        public static explicit operator Header(Dictionary<string, string> v)
        {
            throw new NotImplementedException();
        }
    }
}
