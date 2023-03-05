using System;
namespace FloatingPointNumber
{
    public class SingleFloatingPointToDecimal
    {
        public static void ConvertSingleFloatingPointToDecimal()
        {
            Console.Write("Input a single precision floating point BINARY: ");
            string floatingPointBinaryString = Console.ReadLine();

            string signBit = floatingPointBinaryString.Substring(0, 1);
            string exponentContentPart = floatingPointBinaryString.Substring(1, 8);
            string significandContentPart = floatingPointBinaryString.Substring(9, 23);

            string sign = signBit == "1" ? "-" : "+";
            int exponentValue = ExponentBinToDecAlgorithm(exponentContentPart);
            string significandContent = RetrieveSignificand(significandContentPart);

            if (exponentValue == -127 && int.Parse(significandContent) == 0)
            {
                Console.WriteLine($"The original real number of single-precision-floating-point {floatingPointBinaryString} is Zero");
                Console.Read();
                return;
            }

            if (exponentValue == -127 && int.Parse(significandContent) != 0)
            {
                string denormalizedRepresentation = $"{sign}0.{significandContent} * 2^-126";
                Console.WriteLine($"The original real number of single-precision-floating-point {floatingPointBinaryString} is a De-normalized Number: {denormalizedRepresentation}");
                Console.Read();
                return;
            }

            if (exponentValue == 128 && int.Parse(significandContent) == 0)
            {
                Console.WriteLine($"The original real number of single-precision-floating-point {floatingPointBinaryString} is Infinity");
                Console.Read();
                return;
            }

            if (exponentValue == 128 && int.Parse(significandContent) != 0)
            {
                Console.WriteLine($"The original real number of single-precision-floating-point {floatingPointBinaryString} is NaN - Not a Number");
                Console.Read();
                return;
            }

            Console.WriteLine($"signBit: {signBit}");
            Console.WriteLine($"exponentContentPart: {exponentContentPart}");
            Console.WriteLine($"significandContentPart: {significandContentPart}");



            string OneDotForm = $"{sign}1.{significandContent} * 2^{exponentValue}";
            Console.WriteLine($"The +/- 1.F * 2^E representation for {floatingPointBinaryString} is: {OneDotForm}");

            string originalBinaryFloatingPoint = OriginalBinaryFloatingPoint(exponentValue, significandContent);
            Console.WriteLine($"The original binary floating point is: {sign}{originalBinaryFloatingPoint}");

            string originalRealNumber = ConvertOriginalBinaryFloatingPointToDecimal(sign, originalBinaryFloatingPoint);
            Console.WriteLine($"The original real number of single-precision-floating-point {floatingPointBinaryString} is {originalRealNumber}");
            Console.Read();
        }

        public static int ExponentBinToDecAlgorithm(string binaryString)
        {
            int sum = 0;
            string[] binaryElements = FloatingPointNumberToBinarySingleFloatingPoint.SplitStringInToArrayOfStrings(binaryString);
            Array.Reverse(binaryElements);
            for (int i = 0; i < binaryElements.Length; i++)
            {
                if (binaryElements[i] == "1")
                {
                    sum += (int)Math.Pow(2, i);
                }
            }
            return sum - 127;
        }

        public static string RetrieveSignificand(string binSignificandPart)
        {
            string[] binSignificandElements = FloatingPointNumberToBinarySingleFloatingPoint.SplitStringInToArrayOfStrings(binSignificandPart);
            int lastBit1Index = Array.LastIndexOf(binSignificandElements, "1");
            string significandContent = binSignificandPart.Substring(0, lastBit1Index + 1);
            return significandContent.Length > 0 ? significandContent : "0";
        }

        public static string OriginalBinaryFloatingPoint(int exponentValue, string significandContent)
        {
            string integerBinaryPart = "";
            string decimalBinaryPart = "";
            string shiftingContent = "";
            if (exponentValue < 0)
            {
                integerBinaryPart += "0";
                shiftingContent += "1";
                shiftingContent.PadLeft(exponentValue, '0');
                decimalBinaryPart += shiftingContent.Concat(significandContent);
            }

            if (exponentValue > 0)
            {
                integerBinaryPart += "1";
                shiftingContent += significandContent.Substring(0, exponentValue);
                integerBinaryPart += shiftingContent;
                decimalBinaryPart += significandContent.Substring(exponentValue);
            }

            return $"{integerBinaryPart}.{decimalBinaryPart}";
        }

        public static string ConvertOriginalBinaryFloatingPointToDecimal(string sign, string originalBinaryFloatingPoint)
        {
            string[] originalBinaryFloatingPointElements = originalBinaryFloatingPoint.Split(".");
            string integerBinaryPart = originalBinaryFloatingPointElements[0];
            string decimalBinaryPart = originalBinaryFloatingPointElements[1];

            int integerPart = IntegerBinaryPartToInteger(integerBinaryPart);
            float decimalPart = DecimalBinaryPartToDecimalPart(decimalBinaryPart);

            return $"{sign}{integerPart + decimalPart}";
        }

        public static int IntegerBinaryPartToInteger(string integerBinPart)
        {
            string[] binElements = FloatingPointNumberToBinarySingleFloatingPoint.SplitStringInToArrayOfStrings(integerBinPart);
            Array.Reverse(binElements);
            int sum = 0;
            for (int i = 0; i < binElements.Length; i++)
            {
                if (binElements[i] == "1")
                {
                    sum += (int)Math.Pow(2, i);
                }
            }
            return sum;
        }
        public static float DecimalBinaryPartToDecimalPart(string decimalBinaryPart)
        {
            string[] binElements = FloatingPointNumberToBinarySingleFloatingPoint.SplitStringInToArrayOfStrings(decimalBinaryPart);
            float sum = 0;
            for (int i = 0; i < binElements.Length; i++)
            {
                if (binElements[i] == "1")
                {
                    sum += (float)Math.Pow(2, -(i + 1));
                }
            }
            return sum;
        }
    }
}

