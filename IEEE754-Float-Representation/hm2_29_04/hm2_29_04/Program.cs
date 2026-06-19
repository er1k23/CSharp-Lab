using System;

class Program
{
    static void Main()
    {
        int a = int.MaxValue;
        Console.WriteLine(unchecked(a + 1));

        long b = long.MaxValue;
        Console.WriteLine(unchecked(b + 1));

        Console.WriteLine(Add("9999", "1"));
        Console.WriteLine(Subtract("10000", "1"));
        Console.WriteLine(Multiply("123", "456"));
    }

    static string Add(string a, string b)
    {
        if (a == null || a == "")
            a = "0";

        if (b == null || b == "")
            b = "0";

        string answer = "";
        int i = a.Length - 1;
        int j = b.Length - 1;
        int carry = 0;

        while (i >= 0 || j >= 0 || carry > 0)
        {
            int digitA = 0;
            int digitB = 0;

            if (i >= 0)
                digitA = a[i] - '0';

            if (j >= 0)
                digitB = b[j] - '0';

            int sum = digitA + digitB + carry;

            answer = (sum % 10) + answer;
            carry = sum / 10;

            i--;
            j--;
        }

        return RemoveZeros(answer);
    }

    static string Subtract(string a, string b)
    {
        if (a == null || a == "")
            a = "0";

        if (b == null || b == "")
            b = "0";

        string answer = "";
        int i = a.Length - 1;
        int j = b.Length - 1;
        int borrow = 0;

        while (i >= 0)
        {
            int digitA = a[i] - '0' - borrow;
            int digitB = 0;

            if (j >= 0)
                digitB = b[j] - '0';

            if (digitA < digitB)
            {
                digitA = digitA + 10;
                borrow = 1;
            }
            else
            {
                borrow = 0;
            }

            int diff = digitA - digitB;
            answer = diff + answer;

            i--;
            j--;
        }

        return RemoveZeros(answer);
    }

    static string Multiply(string a, string b)
    {
        if (a == null || a == "" || a == "0")
            return "0";

        if (b == null || b == "" || b == "0")
            return "0";

        string finalAnswer = "0";

        int zeroCount = 0;

        for (int i = b.Length - 1; i >= 0; i--)
        {
            int digitB = b[i] - '0';
            int carry = 0;
            string line = "";

            for (int z = 0; z < zeroCount; z++)
                line = line + "0";

            for (int j = a.Length - 1; j >= 0; j--)
            {
                int digitA = a[j] - '0';

                int mul = digitA * digitB + carry;

                line = (mul % 10) + line;
                carry = mul / 10;
            }

            if (carry > 0)
                line = carry + line;

            finalAnswer = Add(finalAnswer, line);
            zeroCount++;
        }

        return RemoveZeros(finalAnswer);
    }

    static string RemoveZeros(string s)
    {
        int index = 0;

        while (index < s.Length - 1 && s[index] == '0')
        {
            index++;
        }

        return s.Substring(index);
    }
}