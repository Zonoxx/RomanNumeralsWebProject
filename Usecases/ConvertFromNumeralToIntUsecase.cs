
namespace RomanNumeralsWebProject.UseCases
{
    public class ConvertFromNumeraltoIntUsecase
    {
        public string? UserInput;
        public bool IsInputValid;
        private readonly Dictionary<char, int> Numerals = new()
    {
        {'I', 1},
        {'V', 5},
        {'X', 10},
        {'L', 50},
        {'C', 100},
        {'D', 500},
        {'M', 1000}
    };



        public int convertRomanNumeralToInt(string input)
        {
            UserInput = input;
            int result = 0;
            int previousKeyValue = 0;

            foreach (char numeral in UserInput)
            {
                var keyValue = Numerals[numeral];
                if (previousKeyValue != 0 && keyValue > previousKeyValue)
                {
                    result -= previousKeyValue * 2;
                }

                result += keyValue;
                previousKeyValue = keyValue;
            }
            return result;
        }

        public string getUserInput()
        {
            Console.WriteLine("Welcome to the Roman Numerals Converter.\nThis converter converts your Roman Numerals into number values.\nPlease enter a numeral between 1 and 3999.");
            while (UserInput == null || UserInput == "")
            {
                askForValidInput();
                while (!IsInputValid)
                {
                    Console.WriteLine("You entered an invalid Numeral!");
                    askForValidInput();
                }
            }
            UserInput = UserInput.ToUpper();
            return UserInput;
        }

        public void askForValidInput()
        {
            Console.WriteLine("Please enter your numeral: ");
            UserInput = Console.ReadLine();
            IsInputValid = checkForValidInput(UserInput!.ToUpper());
        }

        public bool checkForValidInput(string input)
        {
            UserInput = input;

            var syntax = syntaxCheck();
            if (!syntax)
            {
                return false;
            }

            var semantics = semanticsCheck();
            if (!semantics)
            {
                return false;
            }
            return true;
        }

        private bool semanticsCheck()
        {
            if (UserInput!.Length > 1)
            {
                int previousNumeralValue = 0;
                foreach (char numeral in UserInput!)
                {
                    var numeralValue = Numerals[numeral];
                    var numeralCount = UserInput!.Count(n => n == numeral);
                    //check that V,L,D are not used for substraction
                    if (numeralValue >= previousNumeralValue && (previousNumeralValue.ToString().Contains(5.ToString())))
                    {
                        return false;
                    }
                    //check that I,X,C are only used for subtracting at the two next highest numerals
                    else if (previousNumeralValue != 0 && previousNumeralValue * 10 < numeralValue)
                    {
                        return false;
                    }
                    //check for more than 3 uses of I,X,C,M
                    else if (numeralCount > 3)
                    {
                        return false;
                    }
                    previousNumeralValue = numeralValue;
                }
            }
            return true;
        }



        private bool syntaxCheck()
        {
            foreach (char numeral in UserInput!)
            {
                if (!Numerals.ContainsKey(numeral))
                {
                    return false;
                }
            }
            return true;
        }

        public static void Run()
        {
            var converter = new ConvertFromNumeraltoIntUsecase();
            var input = converter.getUserInput();
            var result = converter.convertRomanNumeralToInt(input);
            Console.WriteLine($"The Numeral you entered is {result} in decimal numbers");
        }
    }
}
