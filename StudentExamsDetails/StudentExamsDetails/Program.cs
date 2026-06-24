using System;

namespace StudentResultsSystem
{
    class Program
    {
        const int MAX_STUDENTS = 3;
        const int NUM_COURSES = 5;

        static string[] courseNames = new string[NUM_COURSES]
        {
            "Programming with C#",
            "Database Systems",
            "Computer Networks",
            "Web Development",
            "Mathematics for Computing"
        };

        static string[] studentNames = new string[MAX_STUDENTS];
        static string[] studentIDs = new string[MAX_STUDENTS];
        static string[] studentProgrammes = new string[MAX_STUDENTS];
        static string[] studentLevels = new string[MAX_STUDENTS];
        static double[,] studentScores = new double[MAX_STUDENTS, NUM_COURSES];

        static bool resultsEntered = false;

        static void Main(string[] args)
        {
            int choice;

            do
            {
                DisplayMenu();
                choice = GetMenuChoice();

                switch (choice)
                {
                    case 1:
                        EnterStudentResults();
                        break;
                    case 2:
                        ViewStudentReport();
                        break;
                    case 3:
                        Console.WriteLine("\nThank you for using the Student Results Processing System.");
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please choose 1, 2, or 3.");
                        break;
                }

            } while (choice != 3);
        }

        //  MENU
        static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("==========================================");
            Console.WriteLine("   STUDENT RESULTS PROCESSING SYSTEM     ");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Enter Student Results");
            Console.WriteLine("2. View Student Report");
            Console.WriteLine("3. Exit");
            Console.WriteLine("------------------------------------------");
            Console.Write("Choose an option: ");
        }

        static int GetMenuChoice()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.Write("Please enter a valid number (1-3): ");
            }
            return choice;
        }

        // ENTER RESULTS

        static void EnterStudentResults()
        {
            Console.WriteLine("\n--- Enter Student Results ---\n");

            for (int i = 0; i < MAX_STUDENTS; i++)
            {
                Console.WriteLine($"Enter details for Student {i + 1}");
                Console.WriteLine("------------------------------------------");

                Console.Write("Enter full name:   ");
                studentNames[i] = Console.ReadLine();

                Console.Write("Enter student ID:  ");
                studentIDs[i] = Console.ReadLine();

                Console.Write("Enter programme:   ");
                studentProgrammes[i] = Console.ReadLine();

                Console.Write("Enter level:       ");
                studentLevels[i] = Console.ReadLine();

                Console.WriteLine();

                // Collect scores for each course
                for (int j = 0; j < NUM_COURSES; j++)
                {
                    studentScores[i, j] = GetValidScore(courseNames[j]);
                }

                Console.WriteLine($"\nResults for Student {i + 1} saved successfully!\n");
            }

            resultsEntered = true;
        }

        // Validates that a score is between 0 and 100
        static double GetValidScore(string courseName)
        {
            double score;
            while (true)
            {
                Console.Write($"Enter score for {courseName}: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out score) && score >= 0 && score <= 100)
                {
                    return score;
                }
                else
                {
                    Console.WriteLine("Invalid score. Score must be between 0 and 100.");
                }
            }
        }

        // VIEW REPORT

        static void ViewStudentReport()
        {
            if (!resultsEntered)
            {
                Console.WriteLine("\nNo results found. Please enter student results first (Option 1).");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("==========================================");
            Console.WriteLine("         STUDENT RESULTS REPORT          ");
            Console.WriteLine("==========================================");

            for (int i = 0; i < MAX_STUDENTS; i++)
            {
                double total = CalculateTotal(i);
                double average = total / NUM_COURSES;
                string grade = GetGrade(average);
                string status = GetStatus(average);

                Console.WriteLine();
                Console.WriteLine($"Student Name : {studentNames[i]}");
                Console.WriteLine($"Student ID   : {studentIDs[i]}");
                Console.WriteLine($"Programme    : {studentProgrammes[i]}");
                Console.WriteLine($"Level        : {studentLevels[i]}");
                Console.WriteLine();

                // Print individual course scores
                for (int j = 0; j < NUM_COURSES; j++)
                {
                    Console.WriteLine($"  {courseNames[j],-35}: {studentScores[i, j]}");
                }

                Console.WriteLine();
                Console.WriteLine($"  Total Score  : {total}");
                Console.WriteLine($"  Average Score: {average:F2}");
                Console.WriteLine($"  Grade        : {grade}");
                Console.WriteLine($"  Status       : {status}");
                Console.WriteLine("------------------------------------------");
            }

            DisplayClassSummary();
        }

        // CALCULATIONS

        static double CalculateTotal(int studentIndex)
        {
            double total = 0;
            for (int j = 0; j < NUM_COURSES; j++)
            {
                total += studentScores[studentIndex, j];
            }
            return total;
        }

        static string GetGrade(double average)
        {
            if (average >= 80) return "A";
            if (average >= 70) return "B";
            if (average >= 60) return "C";
            if (average >= 50) return "D";
            return "F";
        }

        static string GetStatus(double average)
        {
            return average >= 50 ? "Passed" : "Failed";
        }

        static void DisplayClassSummary()
        {
            double classTotal = 0;
            double bestAverage = -1;
            double lowestAverage = 101;
            string bestStudent = "";
            string lowestStudent = "";

            for (int i = 0; i < MAX_STUDENTS; i++)
            {
                double avg = CalculateTotal(i) / NUM_COURSES;
                classTotal += avg;

                if (avg > bestAverage)
                {
                    bestAverage = avg;
                    bestStudent = studentNames[i];
                }

                if (avg < lowestAverage)
                {
                    lowestAverage = avg;
                    lowestStudent = studentNames[i];
                }
            }

            double classAverage = classTotal / MAX_STUDENTS;

            Console.WriteLine();
            Console.WriteLine("==========================================");
            Console.WriteLine("             CLASS SUMMARY               ");
            Console.WriteLine("==========================================");
            Console.WriteLine($"  Class Average    : {classAverage:F2}");
            Console.WriteLine($"  Best Student     : {bestStudent} ({bestAverage:F2})");
            Console.WriteLine($"  Lowest Average   : {lowestStudent} ({lowestAverage:F2})");
            Console.WriteLine("==========================================");
            Console.WriteLine();
        }
    }
}