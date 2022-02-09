using Xunit;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void FizzBuzzGeneratorReturnsTheArrayWithRigthLength(int numItems)
        {
            var result = FizzBuzzGenerator.GetFizzBuzzArray(numItems);

            Assert.Equal(numItems, result.Length);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void FizzBuzzGeneratorWithParamLessZeroThrowsException(int numItems)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => FizzBuzzGenerator.GetFizzBuzzArray(numItems));
        }

        [Theory]
        [InlineData(2, "2")]
        [InlineData(3, "Fizz")]
        [InlineData(5, "Buzz")]
        [InlineData(6, "Fizz")]
        [InlineData(7, "7")]
        [InlineData(15, "FizzBuzz")]
        public void FizzBuzzGeneratorWithRightParamReturnsTheRightTheLastValue(int numItems, string lastValue)
        {
            var result = FizzBuzzGenerator.GetFizzBuzzArray(numItems);
            Assert.Equal(result[numItems - 1], lastValue);
        }

        [Theory]
        [InlineData(7, new string[] { "1", "2", "Fizz", "4", "Buzz", "Fizz", "7" })]
        public void FizzBuzzGeneratorReturnsTheExpectedArray(int numItems, string[] expectedResult)
        {
            var result = FizzBuzzGenerator.GetFizzBuzzArray(numItems);
            Assert.Equal(result, expectedResult);
        }

    }
}