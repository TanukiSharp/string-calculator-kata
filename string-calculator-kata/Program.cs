namespace StringCalculatorKata;

public class StringCalculator
{
    private readonly char[] separators = [ ',', '\n' ];

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

        string[] splitEntries = numbers.Split(separators, StringSplitOptions.TrimEntries);

        if (splitEntries.Any(string.IsNullOrEmpty))
        {
            throw new ArgumentException("Empty entry not allowed.", nameof(numbers));
        }

        if (splitEntries.Any(x => int.TryParse(x, out _) == false))
        {
            throw new ArgumentException("Non-integer entry not allowed.", nameof(numbers));
        }

        return splitEntries
            .Select(int.Parse)
            .Sum();
    }
}
