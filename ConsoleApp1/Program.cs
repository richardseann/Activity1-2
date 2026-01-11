using System;
using System.Collections.Generic;

namespace StudentGradeManager
{
    class Program
    {
        static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("    STUDENT GRADE MANAGEMENT SYSTEM");
            Console.WriteLine("==========================================");
            Console.WriteLine();

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ViewAllStudents();
                        break;
                    case "3":
                        SearchStudent();
                        break;
                    case "4":
                        CalculateClassAverage();
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("\nThank you for using the system!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n--- MAIN MENU ---");
            Console.WriteLine("1. Add New Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. Search Student");
            Console.WriteLine("4. Calculate Class Average");
            Console.WriteLine("5. Exit");
            Console.Write("\nEnter your choice: ");
        }

        static void AddStudent()
        {
            Console.WriteLine("\n--- ADD NEW STUDENT ---");
            
            Console.Write("Enter Student ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Grade (0-100): ");
            double grade;
            while (!double.TryParse(Console.ReadLine(), out grade) || grade < 0 || grade > 100)
            {
                Console.Write("Invalid grade. Please enter a number between 0 and 100: ");
            }

            Student student = new Student(id, name, grade);
            students.Add(student);

            Console.WriteLine($"\nStudent {name} added successfully!");
            Console.WriteLine($"Remark: {student.GetRemark()}");
        }

        static void ViewAllStudents()
        {
            Console.WriteLine("\n--- ALL STUDENTS ---");
            
            if (students.Count == 0)
            {
                Console.WriteLine("No students found in the system.");
                return;
            }

            Console.WriteLine(String.Format("{0,-10} {1,-25} {2,-10} {3,-10}", 
                "ID", "Name", "Grade", "Remark"));
            Console.WriteLine(new string('-', 60));

            foreach (var student in students)
            {
                student.DisplayInfo();
            }

            Console.WriteLine($"\nTotal Students: {students.Count}");
        }

        static void SearchStudent()
        {
            Console.WriteLine("\n--- SEARCH STUDENT ---");
            Console.Write("Enter Student ID: ");
            string searchId = Console.ReadLine();

            Student found = students.Find(s => s.StudentId == searchId);

            if (found != null)
            {
                Console.WriteLine("\nStudent Found:");
                Console.WriteLine(String.Format("{0,-10} {1,-25} {2,-10} {3,-10}", 
                    "ID", "Name", "Grade", "Remark"));
                Console.WriteLine(new string('-', 60));
                found.DisplayInfo();
            }
            else
            {
                Console.WriteLine($"\nNo student found with ID: {searchId}");
            }
        }

        static void CalculateClassAverage()
        {
            Console.WriteLine("\n--- CLASS AVERAGE ---");
            
            if (students.Count == 0)
            {
                Console.WriteLine("No students found in the system.");
                return;
            }

            double total = 0;
            foreach (var student in students)
            {
                total += student.Grade;
            }

            double average = total / students.Count;
            Console.WriteLine($"Class Average: {average:F2}");
            Console.WriteLine($"Total Students: {students.Count}");

            int passed = students.FindAll(s => s.Grade >= 75).Count;
            int failed = students.Count - passed;

            Console.WriteLine($"Passed: {passed}");
            Console.WriteLine($"Failed: {failed}");
        }
    }

    class Student
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public double Grade { get; set; }

        public Student(string studentId, string name, double grade)
        {
            StudentId = studentId;
            Name = name;
            Grade = grade;
        }

        public string GetRemark()
        {
            if (Grade >= 90) return "Excellent";
            else if (Grade >= 85) return "Very Good";
            else if (Grade >= 80) return "Good";
            else if (Grade >= 75) return "Fair";
            else return "Failed";
        }

        public void DisplayInfo()
        {
            Console.WriteLine(String.Format("{0,-10} {1,-25} {2,-10:F2} {3,-10}", 
                StudentId, Name, Grade, GetRemark()));
        }
    }
}