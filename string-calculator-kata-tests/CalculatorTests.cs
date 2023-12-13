using FluentAssertions;
using StringCalculatorKata;

namespace StringCalculatorKataTests;

public class CalculatorTests
{
    public static TheoryData<string, int> FirstData =
        new()
        {
            { "", 0 },
            { "1", 1 },
            { "1, 2", 3 },
        };

    
    [Theory]
    [MemberData(nameof(FirstData))]
    public void InitialTests(string input, int output)
    {
        new StringCalculator().Add(input).Should().Be(output);
    }
}
