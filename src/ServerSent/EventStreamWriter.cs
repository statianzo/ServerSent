using System;
using System.IO;

namespace ServerSent
{
    public class EventStreamWriter : IDisposable
    {
        StreamWriter _streamWriter;
        bool _alreadyDisposed;
        
        public EventStreamWriter(Stream stream)
        {
            _streamWriter = new StreamWriter(stream);
        }
        
        public void Write(string content, string eventName = null)
        {
            if (_alreadyDisposed)
                throw new ObjectDisposedException("EventStreamWriter");
                
            var lines = content.Split(new []{'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            
            if (eventName != null)
                _streamWriter.Write("event: {0}\r\n", eventName);
            
            foreach (var line in lines)
                _streamWriter.Write("data: {0}\r\n", line);
            
            _streamWriter.Write("\r\n");
            _streamWriter.Flush();
        }

        public void Dispose()
        {
            if (_alreadyDisposed)
                return;
                
            _alreadyDisposed = true;
            _streamWriter.Dispose();
            _streamWriter = null;
        }
    }
}

