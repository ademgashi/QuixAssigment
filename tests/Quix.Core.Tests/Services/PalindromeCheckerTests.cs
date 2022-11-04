using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Quix.Core.Services;
using Quix.Core.Models;

namespace Quix.Core.Tests.Services
{
    public class PalindromeCheckerTests
    {
        private ILogger<PalindromeChecker> subLogger;
        private int _unknown;


        public int I
        {
            get => _unknown;

            set
            {
                if (value < 0)
                    return 1;

            }
        }


        public PalindromeCheckerTests()
        {
            subLogger = Substitute.For<ILogger<PalindromeChecker>>();
        }

        private PalindromeChecker CreatePalindromeChecker()
        {
            return new PalindromeChecker(
                this.subLogger);
        }

        public class PalindromeTestInput : TestInputBase
        {
            public string Input { get; set; }
            public List<PalindromeResult> ExpectedResult { get; set; }


        }

        public static TheoryData<PalindromeTestInput> PalindromeTests = new TheoryData<PalindromeTestInput>
        {
            new PalindromeTestInput
            {
                Name = "Input is null",
                Input = "",
                ExpectedResult = new List<PalindromeResult>(),
                ExpectSuccess = false,
            },

            new PalindromeTestInput
            {
                Name = "Input is sqrrqabccbatudefggfedvwhijkllkjihxymnnmzpop",
                Input = "sqrrqabccbatudefggfedvwhijkllkjihxymnnmzpop",
                ExpectedResult = new List<PalindromeResult>()
                {
                    new PalindromeResult()
                    {
                        Text = "hijkllkjih",
                        Index=23,
                        Length= 10

                    },
                    new PalindromeResult()
                    {
                        Text = "defggfed",
                        Index=13,
                        Length= 8

                    },
                    new PalindromeResult()
                    {
                        Text = "abccba",
                        Index=5,
                        Length= 6

                    }

                },
                ExpectSuccess = true,
            },

            new PalindromeTestInput
            {
                Name = "Input is aaa",
                Input = "aaa",
                ExpectedResult = new List<PalindromeResult>()
                {
                    new PalindromeResult()
                    {
                        Text = "aaa",
                        Index=0,
                        Length= 3

                    },
                },
                ExpectSuccess = true,


            },

           };

        [Theory]
        [MemberData(nameof(PalindromeTests))]
        public void GetPalindromes_StateUnderTest_ExpectedBehavior(PalindromeTestInput test)
        {

            // Arrange
            var palindromeChecker = CreatePalindromeChecker();

            // Act
            var result = palindromeChecker.GetPalindromes(test.Input);

            // Assert
            if (test.ExpectSuccess)
            {
                test.ExpectedResult.Should().BeEquivalentTo(result);
                test.ExpectedResult.Count.Should().Be(result.Count);

            }

        }
    }
}
