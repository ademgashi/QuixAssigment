using Microsoft.Extensions.Logging;
using Quix.Core.Models;

namespace Quix.Core.Services;

public class PalindromeChecker : IPalindromeChecker
{

    #region Private fields

    private readonly ILogger<PalindromeChecker> _log;
    private const int MinimumPalindromeLength = 2;
    private const int UniquePalindromes = 3;

    #endregion

    #region Constructor
    public PalindromeChecker(ILogger<PalindromeChecker> log)
    {
        _log = log;
    }
    #endregion

    #region Public Methods
    public List<PalindromeResult> GetPalindromes(string input)
    {
        _log.LogTrace("GetPalindromes");

        if (string.IsNullOrEmpty(input))
        {
            _log.LogDebug("Input was null or empty");
            return new List<PalindromeResult>();
        }

        string? current;
        List<string?> palindromes = new List<string?>();

        char[] inputStrArr = input.ToCharArray();

        for (int i = 0; i < inputStrArr.Length; i++)
        {
            for (int j = i + 1; j < inputStrArr.Length; j++)
            {
                current = input.Substring(i, j - i + 1);

                if (_IsPalindrome(current) && current.Length > MinimumPalindromeLength)
                {
                    _log.LogDebug($"Found: {current}");

                    palindromes.Add(current);
                }
            }
        }

        var allPalindromes = palindromes.Where(s => s.Length >= MinimumPalindromeLength).
            OrderByDescending(str => str.Length).ToList();

        var repeatedPalindromes = _RepeatedPalindromes(allPalindromes);

        var unique = allPalindromes.Except(repeatedPalindromes).ToList().Take(UniquePalindromes);

        var palindromesOutput = new List<PalindromeResult>();

        foreach (var item in unique)
        {
            var palindromeDisplay = new PalindromeResult()
            {
                Index = input.IndexOf(item, StringComparison.Ordinal),
                Length = item.Length,
                Text = item
            };

            palindromesOutput.Add(palindromeDisplay);

            _log.LogInformation(palindromeDisplay.ToString());

        }

        return palindromesOutput;
    }
    #endregion

    #region Implemenation Details
    private List<string> _RepeatedPalindromes(List<string?> allPalindromes)
    {
        List<string> repeatedPalindromes = new List<string>();

        foreach (var item in allPalindromes)
        {
            _log.LogDebug($" palindrome {item}");

            foreach (var item2 in allPalindromes.Where(s => s != item))
            {
                if (item != null && item.Contains(item2))
                {
                    repeatedPalindromes.Add(item2);
                }
            }
        }

        return repeatedPalindromes;
    }
    private bool _IsPalindrome(string? pal)
    {
        for (var i = 0; i < pal.Length / 2; i++)
        {
            if (pal[i].ToString() != pal[^(i + 1)].ToString())
            {
                return false;

            }

            if (i == (pal.Length / 2) - 1)
            {
                return true;

            }
        }
        return true;
    }
    #endregion
}
