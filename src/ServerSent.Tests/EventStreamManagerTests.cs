using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace ServerSent.Tests
{
    public class EventStreamManagerTests
    {
        private readonly EventStreamManager _manager;

        public EventStreamManagerTests()
        {
            _manager = new EventStreamManager();
        }

        [Fact]
        public void ShouldStartWithZeroCount()
        {
            Assert.Equal(0, _manager.Count);
        }

        [Fact]
        public void ShouldRegisterStream()
        {
            var stream = new MemoryStream();
            _manager.Register("", stream);
            Assert.Equal(1, _manager.Count);
        }

        [Fact]
        public void ShouldWriteToAllRegisteredStreams()
        {
            const string input = "All of this should be there";
            const string expected = "data: " + input + "\r\n\r\n";
            var expectedBytes = Encoding.UTF8.GetBytes(expected);

            var streams = Enumerable.Range(0, 200).Select(x => new MemoryStream()).ToArray();

            foreach (var stream in streams)
                _manager.Register(Guid.NewGuid().ToString(), stream);

            _manager.WriteToAll(input);

            foreach (var stream in streams)
                Assert.Equal(expectedBytes, stream.ToArray());
        }

        [Fact]
        public void ShouldUnregister()
        {
            var stream = new MemoryStream();

            _manager.Register("stream1", stream);
            _manager.Unregister("stream1");

            Assert.Equal(0, _manager.Count);
        }
    }
}