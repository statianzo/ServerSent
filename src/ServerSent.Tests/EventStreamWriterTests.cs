using System;
using Xunit;
using System.IO;
using System.Text;

namespace ServerSent.Tests
{
    public class EventStreamWriterTests
    {
        readonly EventStreamWriter _writer;
        readonly MemoryStream _stream;
        
        public EventStreamWriterTests()
        {
            _stream = new MemoryStream();
            _writer = new EventStreamWriter(_stream);
        }
    
        [Fact]
        public void ShouldWriteToStream()
        {
            const string input = "All of this should be there";
            const string expected = "data: " + input + "\r\n\r\n";
            var expectedBytes = Encoding.UTF8.GetBytes(expected);
            
            _writer.Write(input);
            
            var bytes = _stream.ToArray();
            
            
            Assert.Equal(expectedBytes, bytes);
        }
        
        [Fact]
        public void ShouldWriteNamedEventToStream()
        {
            const string input = "Body of the event";
            const string name = "dude";
            const string expected = "event: " + name + "\r\n" +
                                    "data: " + input + "\r\n\r\n";
            var expectedBytes = Encoding.UTF8.GetBytes(expected);
            
            _writer.Write(input, name);
            
            var bytes = _stream.ToArray();
            
            
            Assert.Equal(expectedBytes, bytes);
        }
        
        [Fact]
        public void ShouldWriteMultiLineEvent()
        {
            const string input = "Body\r\nof\r\nthe\r\nevent";
            const string expected = "data: Body\r\n" +
                                    "data: of\r\n" +
                                    "data: the\r\n" +
                                    "data: event\r\n\r\n";
                                    
            var expectedBytes = Encoding.UTF8.GetBytes(expected);
            
            _writer.Write(input);
            
            var bytes = _stream.ToArray();
            
            
            Assert.Equal(expectedBytes, bytes);
        }
        
        [Fact]
        public void DisposeDisposesInnerStream()
        {
            _writer.Dispose();
            Assert.False(_stream.CanWrite);
        }
        
        [Fact]
        public void ThrowsWhenUsedAfterDisposal()
        {
            _writer.Dispose();
            Assert.Throws<ObjectDisposedException>(() => _writer.Write(""));
        }
    }
}

