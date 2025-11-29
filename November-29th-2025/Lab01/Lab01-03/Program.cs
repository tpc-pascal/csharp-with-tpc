using System.ComponentModel.Design;

namespace Lab
{
    class Person
    {
        protected string personID;
        protected string fullName;

        public string PersonID { get => personID; set => personID = value; }
        public string FullName { get => fullName; set => fullName = value; }

        public Person()
        {

        }

        public Person(string personID, string fullName)
        {
            this.personID = personID;
            this.fullName = fullName;
        }

        public virtual void Input()
        {
            Console.Write("Nhap MS: ");
            PersonID = Console.ReadLine();
            Console.Write("Nhap ho ten: ");
            FullName = Console.ReadLine();
        }

        public virtual void Show()
        {
            Console.WriteLine("MS:{0} Ho ten:{1} ", this.PersonID, this.FullName);
        }
    }

    class Student : Person
    {
        private float averageScore;
        private string faculty;

        public float AverageScore { get => averageScore; set => averageScore = value; }
        public string Faculty { get => faculty; set => faculty = value; }

        public Student()
        {

        }

        public Student(string personID, string fullName, float averageScore, string faculty) : base(personID, fullName)
        {
            this.averageScore = averageScore;
            this.faculty = faculty;
        }

        public override void Input()
        {
            base.Input();
            FullName = Console.ReadLine();
            Console.Write("Nhap diem TB: ");
            AverageScore = float.Parse(Console.ReadLine());
            Console.Write("Nhap khoa: ");
            Faculty = Console.ReadLine();
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine("Khoa:{0} DiemTB:{1}", this.Faculty, this.AverageScore);
        }
    }

    class Teacher : Person
    {
        private string homeAddress;

        public string HomeAddress { get => homeAddress; set => homeAddress = value; }

        public Teacher()
        {

        }

        public Teacher(string personID, string fullName, string homeAddress) : base(personID, fullName)
        {
            this.homeAddress = homeAddress;
        }

        public override void Input()
        {
            base.Input();
            Console.Write("Nhap dia chi nha: ");
            HomeAddress = Console.ReadLine();
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine("DiaChiNha:{0}", this.HomeAddress);
        }
    }

    class Program
    {
        static void Main()
        {
            List<Student> studentList = new List<Student>();
            List<Teacher> teacherList = new List<Teacher>();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("=== MENU ===");
                Console.WriteLine("1. Them sinh vien");
                Console.WriteLine("2. Them giao vien");
                Console.WriteLine("3. Xuat danh sach sinh vien");
                Console.WriteLine("4. Xuat danh sach giao vien");
                Console.WriteLine("5. So luong sinh vien, giao vien");
                Console.WriteLine("6. Xuat danh sach sinh vien cua 1 khoa");
                Console.WriteLine("7. Xuat danh sach giao vien co dia chi chua thong tin quan giong nhau");
                Console.WriteLine("8. Xuat danh sach sinh vien cua 1 khoa va co diem TB cao nhat");
                Console.WriteLine("9. Xuat danh sach sinh vien xep theo loai");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon chuc nang (0-9): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddStudent(studentList);
                        break;
                    case "2":
                        AddTeacher(teacherList);
                        break;
                    case "3":
                        DisplayStudentList(studentList);
                        break;
                    case "4":
                        DisplayTeacherList(teacherList);
                        break;
                    case "5":
                        SoLuongTungLoai(studentList, teacherList);
                        break;
                    case "6":
                        DisplayStudentByFaculty(studentList, "CNTT");
                        break;
                    case "7":
                        DisplayTeacherByHomeAddress(teacherList, "Quan 9");
                        break;
                    case "8":
                        DisplayStudentsWithHighestAverageScoreByFaculty(studentList, "CNTT");
                        break;
                    case "9":
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

        static void AddTeacher(List<Teacher> teacherList)
        {
            Console.WriteLine("=== Nhap thong tin giao vien ===");
            Teacher teacher = new Teacher();
            teacher.Input();
            teacherList.Add(teacher);
            Console.WriteLine("Them giao vien thanh cong!");
        }

        static void DisplayStudentList(List<Student> studentList)
        {
            Console.WriteLine("=== Danh sach chi tiet thong tin sinh vien ===");
            foreach (Student student in studentList)
            {
                student.Show();
            }
        }

        static void DisplayTeacherList(List<Teacher> teacherList)
        {
            Console.WriteLine("=== Danh sach chi tiet thong tin giao vien ===");
            foreach (Teacher teacher in teacherList)
            {
                teacher.Show();
            }
        }

        static void SoLuongTungLoai(List<Student> studentList, List<Teacher> teacherList)
        {
            Console.WriteLine("=== So luong sinh vien la {0}, giao vien la {1}", studentList.Count, teacherList.Count);
        }

        static void DisplayStudentByFaculty(List<Student> studentList, string faculty)
        {
            Console.WriteLine("=== Danh sach cac sinh vien thuoc khoa {0}", faculty);
            var studentFaculty = studentList.Where(s => s.Faculty.Equals(faculty, StringComparison.OrdinalIgnoreCase)).ToList();
            DisplayStudentList(studentFaculty);
        }

        static void DisplayTeacherByHomeAddress(List<Teacher> teacherList, string addr)
        {
            Console.WriteLine("=== Danh sach cac giao vien o {0}", addr);
            var teacherHomeAddress = teacherList.Where(s => s.HomeAddress.Contains(s.HomeAddress)).ToList();
            DisplayTeacherList(teacherHomeAddress);
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