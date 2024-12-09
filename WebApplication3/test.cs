using Xunit;

namespace WebApplication3
{
    public class test
    {
        [Fact]
        public void Test_Addition()
        {
            int result = 2 + 2;
            Assert.Equal(4, result);
        }
    }

}

