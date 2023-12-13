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
            { "1,2", 3 },
        };
    
    public static TheoryData<string, int> Step2Data =
        new()
        {
            { "", 0 },
            { "1", 1 },
            { "1,2", 3 },
            { "1,2,3", 6 },
            { "1,2,1,3,5,6", 18 },
        };
    
    [Theory]
    [MemberData(nameof(FirstData))]
    public void InitialTests(string input, int output)
    {
        new StringCalculator().Add(input).Should().Be(output);
    }
    
    [Theory]
    [MemberData(nameof(Step2Data))]
    public void Step2Test(string input, int output)
    {
        new StringCalculator().Add(input).Should().Be(output);
    }
    
    [Fact]
    public void Step2RandomTest()
    {
        var randomInt = new Random().Next(100);
        string input = "";

        for (int i = 0; i < randomInt-1; i++)
        {
            input += "1,";
        }

        input += 1;
        
        new StringCalculator().Add(input).Should().Be(randomInt);
    }

    [Fact]
    public void Step3ValidStringTest()
    {
        new StringCalculator().Add("1\n2").Should().Be(3);
    }
    
    [Fact]
    public void Step3InValidStringTest()
    {
        var res = () => new StringCalculator().Add("1,\n2");
        res.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData('-')]
    [InlineData('+')]
    [InlineData(';')]
    public void Step4SimpleDelimiterTest(char delimiter)
    {
        var input = $"//{delimiter}\n1{delimiter}2\n3{delimiter}1";

        new StringCalculator().Add(input).Should().Be(7);
    }
    
    [Fact]
    public void Step4InValidStringTest()
    {
        var res = () => new StringCalculator().Add("//+\n1,2");
        res.Should().Throw<ArgumentException>();
    }
}
