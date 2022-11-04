using Quix.Core.Models;

namespace Quix.Core.Services
{
    public interface IPalindromeChecker
    {
        List<PalindromeResult> GetPalindromes (string input);
    }
}