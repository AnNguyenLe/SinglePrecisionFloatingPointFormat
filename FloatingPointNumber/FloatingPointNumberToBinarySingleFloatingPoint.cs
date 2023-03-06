using System;
using System.Text.RegularExpressions;

namespace FloatingPointNumber
{
    public class FloatingPointNumberToBinarySingleFloatingPoint
    {
        public static void ConvertFloatingPointNumberToBinarySingleFloatingPoint()
        {
            Console.Write("Input a floating point number: ");
            string floatingPointNumberString = Console.ReadLine();

            if (floatingPointNumberString.Contains('.'))
            {
                string[] splittedFloat = floatingPointNumberString.Split(".", StringSplitOptions.RemoveEmptyEntries);
                int integerPart = int.Parse(splittedFloat[0]);
                string decimalPart = splittedFloat[1];

                string signBit = integerPart < 0 ? "1" : "0";
                string sign = integerPart < 0 ? "-" : "+";

                if (int.Parse(decimalPart) == 0)
                {
                    ConvertIntergerToBinarySingleFloatingPoint(splittedFloat[0]);
                }


                string integerPartInBinary = DecToBinAlgorithm(integerPart);
                string decimalPartInBinary = DecimalPartOfANumberToBinAlgorithm(decimalPart);
                string binaryStringAfterStep1 = $"{integerPartInBinary}.{decimalPartInBinary}";
                Console.WriteLine($"Step 1: Binary representation of {floatingPointNumberString} is {sign}{binaryStringAfterStep1}");

                string[] oneDotFormElements = Calculate1DotFMul2PowE(integerPartInBinary, decimalPartInBinary);
                string significand = oneDotFormElements[0];
                string exponent = oneDotFormElements[1];
                Console.WriteLine($"Step 2: The form of (+/- 1.F * 2^E) representation for {floatingPointNumberString} is {sign}{significand}*2^{exponent}");

                string finalBinaryFloatingPoint = CalculateFinalBinaryFloatingPoint(signBit, exponent, significand);
                Console.WriteLine($"Step 3: Final binary floating point: {finalBinaryFloatingPoint}");

                Console.Read();
                return;
            }


            ConvertIntergerToBinarySingleFloatingPoint(floatingPointNumberString);

        }

        static public string DecToBinAlgorithm(int decNumber)
        {
            int BINARY_VALUE = 2;

            // Early return in case of decimal number is either 0 or 1
            if (decNumber == 0 || decNumber == 1)
            {
                return decNumber.ToString();
            }

            // Otherwise, calculate the binary representation of absolute value of input decimal number
            int absValueOfDecNum = Math.Abs(decNumber);

            List<string> binaryElements = new List<string>();

            int dividedDecNum = absValueOfDecNum;

            while (dividedDecNum > 0)
            {
                string remainder = (dividedDecNum % BINARY_VALUE == 0) ? "0" : "1";
                binaryElements.Add(remainder);
                dividedDecNum /= BINARY_VALUE;
            }

            binaryElements.Reverse();

            // We now have the correct order of absolute value of decimal number in binary representation
            string[] binaryElementsArray = binaryElements.ToArray();

            return string.Join("", binaryElementsArray);
        }

        public static string DecimalPartOfANumberToBinAlgorithm(string inputDecimalPartOfANumber)
        {
            List<string> binaryElements = new List<string>();
            float floatValue = float.Parse($"0.{inputDecimalPartOfANumber}");

            while (floatValue != 1.0)
            {
                floatValue *= 2;

                if (floatValue < 1)
                {
                    binaryElements.Add("0");
                    continue;
                }

                binaryElements.Add("1");

                if (floatValue == 1.0)
                {
                    continue;
                }

                --floatValue;
            }
            return $"{string.Join("", binaryElements.ToArray())}";
        }

        public static string[] SplitStringInToArrayOfStrings(string rawString)
        {
            return Regex.Split(rawString.Trim(), string.Empty).Where(item => item != "").ToArray();
        }

        public static string[] Calculate1DotFMul2PowE(string integerPartInBinary, string decimalPartInBinary)
        {
            int exponentValue = 0;
            string[] returnValues = new string[2];

            if (SingleFloatingPointToDecimal.IntegerBinaryPartToInteger(integerPartInBinary) < 1)
            {
                string[] decimalPartElements = SplitStringInToArrayOfStrings(decimalPartInBinary);
                int firstBitOnePosition = Array.FindIndex(decimalPartElements, (item) => item == "1");
                exponentValue = -(firstBitOnePosition + 1);
                int lastBitOnePosition = Array.FindLastIndex(decimalPartElements, (item) => item == "1");
                string significandValue = decimalPartInBinary.Substring(firstBitOnePosition, lastBitOnePosition - firstBitOnePosition + 1);
                List<string> oneDotFormElements = SplitStringInToArrayOfStrings(decimalPartInBinary).ToList();
                oneDotFormElements.Insert(Math.Abs(exponentValue), ".");
                returnValues[0] = string.Join("", oneDotFormElements.Where(item => item != "0").ToArray());
                returnValues[1] = exponentValue.ToString();
                return returnValues;
            }

            List<string> integerPartElements = SplitStringInToArrayOfStrings(integerPartInBinary).ToList();
            integerPartElements.Insert(1, ".");
            returnValues[0] = string.Join("", integerPartElements.ToArray()) + decimalPartInBinary;
            returnValues[1] = (integerPartInBinary.Length - 1).ToString();
            return returnValues;
        }

        public static string CalculateFinalBinaryFloatingPoint(string signBit, string exponent, string significand, int kBiasedValue = 127, int precision = 23)
        {
            string kBiasedBinaryOfExponent = DecToBinAlgorithm(int.Parse(exponent) + kBiasedValue);
            string rightPaddedZerosOfSignificand = significand.Split(".", StringSplitOptions.RemoveEmptyEntries)[1].PadRight(precision, '0');
            return $"{signBit} {kBiasedBinaryOfExponent} {rightPaddedZerosOfSignificand}";
        }

        public static void ConvertIntergerToBinarySingleFloatingPoint(string integer)
        {
            int integerPart = int.Parse(integer);
            string signBit = integerPart < 0 ? "1" : "0";
            string sign = integerPart < 0 ? "-" : "+";
            string integerPartInBinary = DecToBinAlgorithm(integerPart);

            string decimalPartInBinary = "";
            string binaryStringAfterStep1 = $"{integerPartInBinary}";
            Console.WriteLine($"Step 1: Binary representation of {integer} is {sign}{binaryStringAfterStep1}");

            string[] oneDotFormElements = Calculate1DotFMul2PowE(integerPartInBinary, decimalPartInBinary);
            string significand = oneDotFormElements[0];
            string exponent = oneDotFormElements[1];
            Console.WriteLine($"Step 2: The form of (+/- 1.F * 2^E) representation for {integer} is {sign}{significand}*2^{exponent}");

            string finalBinaryFloatingPoint = CalculateFinalBinaryFloatingPoint(signBit, exponent, significand);
            Console.WriteLine($"Step 3: Final binary floating point: {finalBinaryFloatingPoint}");

            Console.Read();
            return;
        }
    }
}

