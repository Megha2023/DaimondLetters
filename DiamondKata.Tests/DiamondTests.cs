using System.Text;

namespace DiamondKata.Tests
{
    public class DiamondTests
    {
        private readonly IDiamond _diamond;

        // Used this approach to simulate dependency injection
        public DiamondTests() : this(new Diamond())
        {
        }

        private DiamondTests(IDiamond diamond)
        {
            _diamond = diamond;
        }

        [Theory]
        [InlineData("", "Invalid character supplied - must be a single alphabetical character")]
        [InlineData("£", "Invalid character supplied - must be a single alphabetical character")]
        [InlineData("£E", "Invalid character supplied - must be a single alphabetical character")]
        [InlineData("A2", "Invalid character supplied - must be a single alphabetical character")]
        [InlineData("Ef", "Invalid character supplied - must be a single alphabetical character")]
        public void Invalid_Supplied_Character_Throws_Argument_Exception(string character, string errorMessage)
        {
            var result = Assert.Throws<ArgumentException>(() => _diamond.PlotDiamond(character));
            
            Assert.Equal(errorMessage, result.Message);
        }

        [Fact]
        public void Null_Character_Throws_Argument_Null_Exception()
        {
            var result = Assert.Throws<ArgumentNullException>(() => _diamond.PlotDiamond(null));
            
            Assert.Equal("Null character supplied - must supply a single alphabetical character (Parameter 'character')", result.Message);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("A")]
        public void Character_A_Returns_Correct_Result(string character)
        {
            var expectedResult = new StringBuilder();

            expectedResult.Append('A');
            expectedResult.AppendLine(string.Empty);

            var result = _diamond.PlotDiamond(character);

            Assert.Equal(expectedResult.ToString(), result);
        }

        [Theory]
        [InlineData("d", "D")]
        [InlineData("b", "B")]
        public void Lowercase_And_Uppercase_Characters_Should_Produce_Same_Diamond(string lowercase, string uppercase)
        {
            var resultLowercase = _diamond.PlotDiamond(lowercase);
            var resultUppercase = _diamond.PlotDiamond(uppercase);

            Assert.Equal(resultLowercase, resultUppercase);
        }

        [Theory]
        [InlineData("e")]
        [InlineData("E")]
        public void Character_E_Returns_Correct_Result(string character)
        {
            var expectedResult = new StringBuilder();

            expectedResult.AppendLine("----A----");
            expectedResult.AppendLine("---B-B---");
            expectedResult.AppendLine("--C---C--");
            expectedResult.AppendLine("-D-----D-");
            expectedResult.AppendLine("E-------E");
            expectedResult.AppendLine("-D-----D-");
            expectedResult.AppendLine("--C---C--");
            expectedResult.AppendLine("---B-B---");
            expectedResult.AppendLine("----A----");

            var result = _diamond.PlotDiamond(character);

            Assert.Equal(expectedResult.ToString(), result);
        }
    }
}