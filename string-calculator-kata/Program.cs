using System.Text.RegularExpressions;

namespace StringCalculatorKata;

public partial class StringCalculator
{
    private readonly char[] separators = [ '\n' ];
    private readonly char defaultSeparator = ',';

    private readonly Regex SeparatorFinderRegex = CreateSeparatorFinderRegex();

    public int Add(string numbers)
    {
        if (numbers is null)
        {
            throw new ArgumentNullException(nameof(numbers));
        }

        numbers = numbers.Trim();

        if (numbers.Length == 0)
        {
            return 0;
        }

        Match separatorMatch = SeparatorFinderRegex.Match(numbers);

        char separator = defaultSeparator;

        if (separatorMatch.Success)
        {
            separator = separatorMatch.Groups[1].Value[0];
            numbers = numbers[separatorMatch.Groups[0].Value.Length..];
        }

        string[] splitEntries = numbers.Split(
            separators.Concat([separator]).ToArray(),
            StringSplitOptions.TrimEntries
        );

        if (splitEntries.Any(string.IsNullOrEmpty))
        {
            throw new ArgumentException("Empty entry not allowed.", nameof(numbers));
        }

        if (splitEntries.Any(x => int.TryParse(x, out _) == false))
        {
            throw new ArgumentException($"Non-integer entry not allowed. ({numbers})", nameof(numbers));
        }

        return splitEntries
            .Select(int.Parse)
            .Sum();
    }

    [GeneratedRegex(@"^//(?<sep>.)\n")]
    private static partial Regex CreateSeparatorFinderRegex();
}
