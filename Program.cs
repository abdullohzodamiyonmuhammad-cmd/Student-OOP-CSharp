using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ConsoleApplication1
{
    internal class Student
    {
        private string firstName;
        private string lastName;
        private int age;
        private List<int> grades;

        public Student(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            grades = new List<int>();
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ном холӣ буда наметавонад.");
                firstName = value.Trim();
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Насаб холӣ буда наметавонад.");
                lastName = value.Trim();
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value < 14 || value > 100)
                    throw new ArgumentException("Синну сол бояд аз 14 то 100 бошад.");
                age = value;
            }
        }

        public string FullName
        {
            get { return firstName + " " + lastName; }
        }

        public int GradesCount
        {
            get { return grades.Count; }
        }

        public void AddGrade(int grade)
        {
            if (grade < 0 || grade > 100)
                throw new ArgumentException("Баҳо бояд аз 0 то 100 бошад.");
            grades.Add(grade);
        }

        public double GetAverage()
        {
            if (grades.Count == 0)
                return 0;

            int sum = 0;
            for (int i = 0; i < grades.Count; i++)
            {
                sum += grades[i];
            }
            return (double)sum / grades.Count;
        }

        public int GetMax()
        {
            if (grades.Count == 0)
                return 0;

            int max = grades[0];
            for (int i = 1; i < grades.Count; i++)
            {
                if (grades[i] > max)
                    max = grades[i];
            }
            return max;
        }

        public int GetMin()
        {
            if (grades.Count == 0)
                return 0;

            int min = grades[0];
            for (int i = 1; i < grades.Count; i++)
            {
                if (grades[i] < min)
                    min = grades[i];
            }
            return min;
        }

        public string GetGradesText()
        {
            if (grades.Count == 0)
                return "(баҳо нест)";

            return string.Join(", ", grades);
        }

        public override string ToString()
        {
            return FullName + ", " + age + " сола, баҳои миёна: " +
                GetAverage().ToString("0.00", CultureInfo.InvariantCulture);
        }
    }

    internal class Program
    {
        private static List<Student> students = new List<Student>();

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                choice = (choice == null) ? "" : choice.Trim();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;

                    case "2":
                        AddGradeToStudent();
                        break;

                    case "3":
                        ShowStudentInfo();
                        break;

                    case "4":
                        ShowAll();
                        break;

                    case "0":
                        Console.WriteLine("Хайр!");
                        return;

                    default:
                        Console.WriteLine("Хато: интихоби нодуруст.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("=== Идоракунии донишҷӯён ===");
            Console.WriteLine("1. Илова кардани донишҷӯ");
            Console.WriteLine("2. Илова кардани баҳо");
            Console.WriteLine("3. Маълумоти пурраи донишҷӯ");
            Console.WriteLine("4. Рӯйхати ҳамаи донишҷӯён");
            Console.WriteLine("0. Баромадан");
            Console.Write("Интихоб: ");
        }

        private static void AddStudent()
        {
            string firstName = ReadText("Ном: ");
            string lastName = ReadText("Насаб: ");
            int age = ReadInt("Синну сол (14-100): ", 14, 100);

            Student student = new Student(firstName, lastName, age);
            students.Add(student);

            Console.WriteLine("Донишҷӯ илова шуд: " + student.FullName);
        }

        private static void AddGradeToStudent()
        {
            Student student = SelectStudent();
            if (student == null)
                return;

            int grade = ReadInt("Баҳо (0-100): ", 0, 100);
            student.AddGrade(grade);

            Console.WriteLine("Баҳо илова шуд. Баҳои миёнаи нав: " +
                student.GetAverage().ToString("0.00", CultureInfo.InvariantCulture));
        }

        private static void ShowStudentInfo()
        {
            Student student = SelectStudent();
            if (student == null)
                return;

            Console.WriteLine();
            Console.WriteLine("--- " + student.FullName + " ---");
            Console.WriteLine("Синну сол:        " + student.Age);
            Console.WriteLine("Шумораи баҳоҳо:   " + student.GradesCount);
            Console.WriteLine("Баҳоҳо:           " + student.GetGradesText());
            Console.WriteLine("Баҳои миёна:      " + student.GetAverage().ToString("0.00", CultureInfo.InvariantCulture));
            Console.WriteLine("Баландтарин:      " + student.GetMax());
            Console.WriteLine("Пасттарин:        " + student.GetMin());
        }

        private static void ShowAll()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Рӯйхат холӣ аст.");
                return;
            }

            Console.WriteLine("--- Рӯйхати донишҷӯён (" + students.Count + ") ---");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + students[i]);
            }
        }

        private static Student SelectStudent()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Рӯйхат холӣ аст. Аввал донишҷӯ илова кунед.");
                return null;
            }

            ShowAll();
            int number = ReadInt("Рақами донишҷӯ: ", 1, students.Count);
            return students[number - 1];
        }

        private static string ReadText(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                input = (input == null) ? "" : input.Trim();

                if (input.Length > 0)
                    return input;

                Console.WriteLine("Хато: майдон холӣ буда наметавонад.");
            }
        }

        private static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                int value;
                if (int.TryParse(input, out value) && value >= min && value <= max)
                    return value;

                Console.WriteLine("Хато: адади бутун аз " + min + " то " + max + " ворид кунед.");
            }
        }
    }
}