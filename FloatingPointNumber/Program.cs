using System.Text.RegularExpressions;

namespace FloatingPointNumber
{
    class Program
    {
        static void Main(string[] arg)
        {
            // 1. Nhập vào 1 số chấm động, xuất ra định dạng nhị phân Single Floating Point tương ứng:
            //FloatingPointNumberToBinarySingleFloatingPoint.ConvertFloatingPointNumberToBinarySingleFloatingPoint();

            // 2. Nhập vào 1 dãy bit nhị phân (32 bit) ở dạng Single Floating Point xuát ra giá trị thập phân tương ứng:
            SingleFloatingPointToDecimal.ConvertSingleFloatingPointToDecimal();

        }
    }
}

