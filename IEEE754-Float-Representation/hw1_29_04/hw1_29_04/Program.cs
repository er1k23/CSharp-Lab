using System;
using System.Text;

namespace MyFirstApp
{
using System;
using System.Text;

class Program
{
    static void Main()
    {
        float x = 12.375f;

        string ieee = ToIEEE754Float(x);
        Console.WriteLine("Float to IEEE 754:");
        Console.WriteLine(ieee);

        float back = FromIEEE754Float(ieee);
        Console.WriteLine("Back to float:");
        Console.WriteLine(back);
    }

    static string ToIEEE754Float(float x)
    {
        int signBit;

        if (x < 0)
        {
            signBit = 1;
            x = -x;
        }
        else
        {
            signBit = 0;
        }

        int power = 0;

        // приводим число к виду 1.xxx * 2^power
        while (x >= 2)
        {
            x = x / 2;
            power++;
        }

        while (x < 1)
        {
            x = x * 2;
            power--;
        }

        int expWithShift = power + 127;
        string expBits = MakeBinary(expWithShift, 8);

        // первая 1 не записывается, поэтому убираем ее
        float partAfterOne = x - 1;

        string mantissaBits = "";

        for (int i = 0; i < 23; i++)
        {
            partAfterOne = partAfterOne * 2;

            if (partAfterOne >= 1)
            {
                mantissaBits += "1";
                partAfterOne = partAfterOne - 1;
            }
            else
            {
                mantissaBits += "0";
            }
        }

        return signBit + " " + expBits + " " + mantissaBits;
    }

    static float FromIEEE754Float(string code)
    {
        code = code.Replace(" ", "");

        int sign;

        if (code[0] == '1')
            sign = -1;
        else
            sign = 1;

        string expStr = code.Substring(1, 8);
        string mantissaStr = code.Substring(9, 23);

        int exp = BinaryToDecimal(expStr) - 127;

        float mantissa = 1;

        for (int i = 0; i < mantissaStr.Length; i++)
        {
            if (mantissaStr[i] == '1')
            {
                mantissa = mantissa + MyPow(2, -(i + 1));
            }
        }

        float answer = sign * mantissa * MyPow(2, exp);
        return answer;
    }

    static string MakeBinary(int number, int size)
    {
        string result = "";

        while (number > 0)
        {
            int rest = number % 2;
            result = rest + result;
            number = number / 2;
        }

        while (result.Length < size)
        {
            result = "0" + result;
        }

        return result;
    }

    static int BinaryToDecimal(string binary)
    {
        int result = 0;

        for (int i = 0; i < binary.Length; i++)
        {
            result = result * 2;

            if (binary[i] == '1')
            {
                result = result + 1;
            }
        }

        return result;
    }

    static float MyPow(float number, int degree)
    {
        float result = 1;

        if (degree > 0)
        {
            for (int i = 0; i < degree; i++)
                result = result * number;
        }
        else if (degree < 0)
        {
            for (int i = 0; i < -degree; i++)
                result = result / number;
        }

        return result;
    }
}
}