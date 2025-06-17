using Xunit;
using HelloWorldApp;
 
namespace HelloWorldApp.Tests
{
    public class HelloTests
    {
        //[Fact]
        public void GetMessage_ReturnsCorrectMessage()
        {
            string expected = "Hello, World from .NET!";
            string actual = Program.GetMessage();
            Assert.Equal(expected, actual);
        }
    }
}
