using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ServerSent
{
    public class EventStreamManager
    {
        private readonly IDictionary<string, EventStreamWriter> _writers = new ConcurrentDictionary<string, EventStreamWriter>();

        public int Count { get { return _writers.Count; } }

        public void Register(string key, Stream stream)
        {
            _writers.Add(key, new EventStreamWriter(stream));
        }

        public void WriteToAll(string body, string eventName = null)
        {
            _writers.Values
               .AsParallel()
               .ForAll(x => x.Write(body, eventName));
        }

        public void Unregister(string key)
        {
            _writers.Remove(key);
        }
    }
}