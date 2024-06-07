using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    // Завдання 1: Перевірка, чи є число числом Фібоначчі
    public static bool IsFibonacci(this int n)
    {
        if (n < 0) return false;
        if (n == 0 || n == 1) return true;

        int a = 0;
        int b = 1;
        while (b < n)
        {
            int temp = a + b;
            a = b;
            b = temp;
        }
        return b == n;
    }

    // Завдання 2: Підрахунок кількості слів у рядку
    public static int WordCount(this string str)
    {
        if (string.IsNullOrWhiteSpace(str)) return 0;

        string[] words = str.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }

    // Завдання 3: Підрахунок довжини останнього слова у рядку
    public static int LengthOfLastWord(this string str)
    {
        if (string.IsNullOrWhiteSpace(str)) return 0;

        string[] words = str.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return words.Length == 0 ? 0 : words[words.Length - 1].Length;
    }

    // Завдання 4: Перевірка валідності розставляння дужок у рядку
    public static bool IsValidBrackets(this string str)
    {
        Stack<char> stack = new Stack<char>();
        Dictionary<char, char> brackets = new Dictionary<char, char>
        {
            { '(', ')' },
            { '{', '}' },
            { '[', ']' }
        };

        foreach (char ch in str)
        {
            if (brackets.ContainsKey(ch))
            {
                stack.Push(ch);
            }
            else if (brackets.ContainsValue(ch))
            {
                if (stack.Count == 0 || brackets[stack.Pop()] != ch)
                {
                    return false;
                }
            }
        }

        return stack.Count == 0;
    }

    // Завдання 5: Фільтрація елементів масиву
    public static int[] Filter(this int[] array, Predicate<int> predicate)
    {
        return array.Where(x => predicate(x)).ToArray();
    }
}

public record DailyTemperature(int High, int Low);
public record StudentGrades(string Subject, int Grade);

public class Program
{
    public static void Main()
    {
        // Тестування Завдання 1
        int number = 13;
        Console.WriteLine($"{number} is a Fibonacci number: {number.IsFibonacci()}");
        number = 14;
        Console.WriteLine($"{number} is a Fibonacci number: {number.IsFibonacci()}");

        // Тестування Завдання 2
        string text = "Hello, world! This is a test.";
        Console.WriteLine($"Word count: {text.WordCount()}");
        text = "Another test, with more words.";
        Console.WriteLine($"Word count: {text.WordCount()}");

        // Тестування Завдання 3
        text = "Hello, world! This is a test.";
        Console.WriteLine($"Length of last word: {text.LengthOfLastWord()}");
        text = "Another test, with more words.";
        Console.WriteLine($"Length of last word: {text.LengthOfLastWord()}");

        // Тестування Завдання 4
        string testString = "{}[]";
        Console.WriteLine($"{testString} is valid: {testString.IsValidBrackets()}");
        testString = "[{]}";
        Console.WriteLine($"{testString} is valid: {testString.IsValidBrackets()}");

        // Тестування Завдання 5
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Predicate<int> isEven = x => x % 2 == 0;
        int[] evenNumbers = numbers.Filter(isEven);
        Console.WriteLine($"Even numbers: {string.Join(", ", evenNumbers)}");

        Predicate<int> isOdd = x => x % 2 != 0;
        int[] oddNumbers = numbers.Filter(isOdd);
        Console.WriteLine($"Odd numbers: {string.Join(", ", oddNumbers)}");

        // Тестування Завдання 6
        DailyTemperature[] temperatures =
        {
            new DailyTemperature(15, 5),
            new DailyTemperature(20, 10),
            new DailyTemperature(25, 15),
            new DailyTemperature(30, 20),
            new DailyTemperature(35, 25)
        };

        var dayWithMaxDifference = temperatures
            .Select((temp, index) => new { Day = index + 1, Difference = temp.High - temp.Low })
            .OrderByDescending(x => x.Difference)
            .First();

        Console.WriteLine($"Day with maximum temperature difference: Day {dayWithMaxDifference.Day} with a difference of {dayWithMaxDifference.Difference}°C");

        // Тестування Завдання 7
        StudentGrades[] grades =
        {
            new StudentGrades("Math", 85),
            new StudentGrades("Science", 90),
            new StudentGrades("Literature", 78),
            new StudentGrades("History", 88)
        };

        var maxGradeStudent = grades.OrderByDescending(x => x.Grade).First();
        var averageGrade = grades.Average(x => x.Grade);

        Console.WriteLine($"Student with the highest grade: {maxGradeStudent.Subject} with grade {maxGradeStudent.Grade}");
        Console.WriteLine($"Average grade: {averageGrade}");
    }
}
