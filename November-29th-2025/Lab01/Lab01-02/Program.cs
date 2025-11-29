using System.ComponentModel.Design;

namespace Lab
{
    class Student
    {
        private string studentID;
        private string fullName;
        private float averageScore;
        private string faculty;

        public string StudentID { get => studentID; set => studentID = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public float AverageScore { get => averageScore; set => averageScore = value; }
        public string Faculty { get => faculty; set => faculty = value; }

        public Student()
        {

        }

        public Student(string studentID, string fullName, float averageScore, string faculty)
        {
            this.studentID = studentID;
            this.fullName = fullName;
            this.averageScore = averageScore;
            this.faculty = faculty;
        }

        public void Input()
        {
            Console.Write("Nhap MSSV: ");
            StudentID = Console.ReadLine();
            Console.Write("Nhap ho ten Sinh vien: ");
            FullName = Console.ReadLine();
            Console.Write("Nhap diem TB: ");
            AverageScore = float.Parse(Console.ReadLine());

            Console.Write("Nhap khoa: ");
            Faculty = Console.ReadLine();
        }

        public void Show()
        {
            Console.WriteLine("MSSV:{0} Ho ten:{1} Khoa:{2} DiemTB:{3}", this.StudentID, this.FullName, this.Faculty, this.AverageScore);
        }

        static void Main()
        {
            List<Student> studentList = new List<Student>();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("=== MENU ===");
                Console.WriteLine("1. Them sinh vien");
                Console.WriteLine("2. Hien thi danh sach sinh vien");
                Console.WriteLine("3. Hien thi danh sach sinh vien cua 1 khoa");
                Console.WriteLine("4. Hien thi danh sach sinh vien co diem TB lon hon hoac bang X");
                Console.WriteLine("5. Hien thi danh sach sinh vien sap xep theo diem TB tang dan");
                Console.WriteLine("6. Hien thi danh sach sinh vien cua 1 khoa va co diem TB lon hon hoac bang X");
                Console.WriteLine("7. Hien thi danh sach sinh vien cua 1 khoa va co diem TB cao nhat");
                Console.WriteLine("8. Hien thi danh sach sinh vien xep theo loai");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon chuc nang (0-8): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddStudent(studentList);
                        break;
                    case "2":
                        DisplayStudentList(studentList);
                        break;
                    case "3":
                        DisplayStudentsByFaculty(studentList, "CNTT");
                        break;
                    case "4":
                        DisplayStudentsWithHighAverageScore(studentList, 5);
                        break;
                    case "5":
                        SortStudentsByAverageScore(studentList);
                        break;
                    case "6":
                        DisplayStudentsByFacultyAndScore(studentList, "CNTT", 5);
                        break;
                    case "7":
                        DisplayStudentsWithHighestAverageScoreByFaculty(studentList, "CNTT");
                        break;
                    case "8":
                        DisplayStudentGrade(studentList);
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Ket thuc chuong trinh.");
                        break;
                    default:
                        Console.WriteLine("Tuy chon khong hop le, vui long chon lai.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void AddStudent(List<Student> studentList)
        {
            Console.WriteLine("=== Nhap thong tin sinh vien ===");
            Student student = new Student();
            student.Input();
            studentList.Add(student);
            Console.WriteLine("Them sinh vien thanh cong!");
        }

        static void DisplayStudentList(List<Student> studentList)
        {
            Console.WriteLine("=== Danh sach chi tiet thong tin sinh vien ===");
            foreach (Student student in studentList)
            {
                student.Show();
            }
        }

        static void DisplayStudentsByFaculty(List<Student> studentList, string faculty)
        {
            Console.WriteLine("=== Danh sach sinh vien thuoc khoa {0}", faculty);
            var students = studentList.Where(s => s.Faculty.Equals(faculty, StringComparison.OrdinalIgnoreCase));
            DisplayStudentList(studentList);
        }

        static void DisplayStudentsWithHighAverageScore(List<Student> studentList, float minDTB)
        {
            Console.WriteLine("=== Danh sach sinh vien co diem TB >= {0}", minDTB);
            var students = studentList.Where(s => s.AverageScore >= minDTB);
            DisplayStudentList(studentList);
        }

        static void SortStudentsByAverageScore(List<Student> studentList)
        {
            Console.WriteLine("=== Danh sach sinh vien duoc sap xep theo diem trung binh tang dan ===");
            var sortedStudents = studentList.OrderBy(s => s.AverageScore).ToList();
            DisplayStudentList(sortedStudents);
        }

        static void DisplayStudentsByFacultyAndScore(List<Student> studentList, string faculty, float minDTB)
        {
            Console.WriteLine("=== Danh sach sinh vien co diem TB >= {0} va thuoc khoa {1}", minDTB, faculty);
            var students = studentList.Where(s => s.AverageScore >= minDTB && s.Faculty.Equals(faculty, StringComparison.OrdinalIgnoreCase)).ToList();
            DisplayStudentList(students);
        }

        static void DisplayStudentsWithHighestAverageScoreByFaculty(List<Student> studentList, string faculty)
        {
            Console.WriteLine("=== Danh sach sinh vien co diem TB cao nhat va thuoc khoa {0}", faculty);
            var studentFaculty = studentList.Where(s => s.Faculty.Equals(faculty, StringComparison.OrdinalIgnoreCase)).ToList();
            double highestDTB = studentFaculty.Any() ? studentFaculty.Max(s => s.AverageScore) : 0;
            var studentHF = studentFaculty.Where(s => s.AverageScore == highestDTB).ToList();
            DisplayStudentList(studentHF);
        }

        static void DisplayStudentGrade(List<Student> studentList)
        {
            Console.WriteLine("=== Xep loai theo danh sach");
            var studentXuatSac = studentList.Where(s => s.AverageScore >= 9.0 && s.AverageScore <= 10).ToList();
            var studentGioi = studentList.Where(s => s.AverageScore >= 8.0 && s.AverageScore < 9.0).ToList();
            var studentKha = studentList.Where(s => s.AverageScore >= 7.0 && s.AverageScore < 8.0).ToList();
            var studentTrungBinh = studentList.Where(s => s.AverageScore >= 5.0 && s.AverageScore < 7.0).ToList();
            var studentYeu = studentList.Where(s => s.AverageScore >= 4.0 && s.AverageScore < 5.0).ToList();
            var studentKem = studentList.Where(s => s.AverageScore < 4.0).ToList();

            Console.WriteLine("=== Xuat Sac"); DisplayStudentList(studentXuatSac);
            Console.WriteLine("=== Gioi"); DisplayStudentList(studentGioi);
            Console.WriteLine("=== Kha"); DisplayStudentList(studentKha);
            Console.WriteLine("=== Trung Binh"); DisplayStudentList(studentTrungBinh);
            Console.WriteLine("=== Yeu"); DisplayStudentList(studentYeu);
            Console.WriteLine("=== Kem"); DisplayStudentList(studentKem);
        }
    }
}