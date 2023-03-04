using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSchool_SimplePasswordGenerator
{
    public class PasswordGenerator
    {
        private const string LowercaseCharacters = "abcdefghijklmnopqrstuvwxyz";
        private const string UppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Numbers = "1234567890";
        private const string SpecialCharacters = "!@#$%^&*_-=+";
        private int _lenght = 0;
        private bool _isUserInputValid = true;

        public void ReadInputs()
        {
            Console.WriteLine("*********************************************************");
            Console.WriteLine("Welcome to the B E S T   P A S S W O R D   M A N A G E R ");
            Console.WriteLine("*********************************************************");
            Console.WriteLine("");

            Console.WriteLine("Do you want to include numbers?(Y/N)");
            var includeNumbers = Console.ReadLine()?.ToLower() == "y";

            Console.WriteLine("\n Do you want to include lowercase characters?(y/n)");
            var includeLowercaseCharacters = Console.ReadLine()?.ToLower() == "y";

            Console.WriteLine("\n Do you want to include uppercase characters?(y/n)");
            var includeUppercaseCharacters = Console.ReadLine()?.ToLower() == "y";

            Console.WriteLine("\n Do you want to include special characters?(y/n)");
            var includeSpecialCharacters = Console.ReadLine()?.ToLower() == "y";

            Console.WriteLine("\n How long do you want to keep your password length?");

            var passwordlenght = Console.ReadLine();

            if (!int.TryParse(passwordlenght, out var length)
                ||
                !includeNumbers && !includeLowercaseCharacters && !includeUppercaseCharacters && !includeSpecialCharacters)

            {
                isUserInputValid = false;
                return;
            }
            _isUserInputValid = true;
        }

        private string _lastGeneratedPassword;
        private bool isUserInputValid;
        private readonly Random _random;
        private readonly StringBuilder _passwordBuilder;
        private readonly StringBuilder _charSetBuilder;

        public PasswordGenerator()
        {
            _random = new Random();
            _lastGeneratedPassword=String.Empty;
            _passwordBuilder = new StringBuilder();
            _charSetBuilder = new StringBuilder();
        }

        public bool IncludeLowercaseCharacters { get;set; }
        public bool IncludeUppercaseCharacters { get; set; }
        public bool IncludeNumbers { get; set; }
        public bool IncludeSpecialCharacters { get; set; }

        
        public void Generate()
        {
            if (IncludeNumbers) _charSetBuilder.Append(Numbers);
            if (IncludeLowercaseCharacters) _charSetBuilder.Append(LowercaseCharacters);
            if (IncludeUppercaseCharacters) _charSetBuilder.Append(UppercaseCharacters);
            if (IncludeSpecialCharacters) _charSetBuilder.Append(SpecialCharacters);

            var charSet = _charSetBuilder.ToString();

            for (int i = 0; i < _lenght; i++)
            {
                var randomIndex = _random.Next(charSet.Length);

                _passwordBuilder.Append(charSet[randomIndex]);
            }

            _lastGeneratedPassword = _passwordBuilder.ToString();

            _charSetBuilder.Clear();
            _passwordBuilder.Clear();

        }

        public string GetLatestGeneratedPassword() => _lastGeneratedPassword;

        public void WriteLatestGeneratedPassword() => WriteFormattedPassword(_lastGeneratedPassword);

        private void WriteFormattedPassword (string? password)
        {
            Console.WriteLine("*********************************************************");
            Console.WriteLine(password);
            Console.WriteLine("*********************************************************");
        }


    }

}
