using Quix.Core.Models;
using Quix.Core.Services;

namespace Quix.Console;

public class App
{
    private readonly IPalindromeChecker _palindromes;

    public App(IPalindromeChecker palindromes)
    {
        _palindromes = palindromes;
    }

    public void Run(string[] args)
    {
        string input = "";

        foreach (var t in args)
        {
            if (t.ToLower().StartsWith("palindrome="))
            {
                input = t.Substring(11);
                break;
            }
        }

        List<PalindromeResult> output = _palindromes.GetPalindromes(input);

        foreach (var item in output)
        {
            System.Console.WriteLine(item);
        }
        System.Console.ReadLine();
    }
}
