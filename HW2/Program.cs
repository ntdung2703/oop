using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding= Encoding.UTF8;

            // Khởi tạo một mảng Person để lưu thông tin 3 người
            Person[] persons = new Person[3];

            // Nhập thông tin cho 3 người
            for (int i = 0; i < persons.Length; i++)
            {
                Console.WriteLine($"Nhập thông tin cho Người {i + 1}");
                Console.Write("Vui lòng nhập tên: ");
                string name = Console.ReadLine();

                Console.Write("Vui lòng nhập địa chỉ: ");
                string address = Console.ReadLine();

                string salary;
                bool validSalary = false;

                // Kiểm tra và yêu cầu nhập lại lương nếu không hợp lệ
                while (!validSalary)
                {
                    Console.Write("Vui lòng nhập lương: ");
                    salary = Console.ReadLine();

                    // Nhập thông tin người vào mảng
                    persons[i] = Person.Nhap(name, address, salary); // Nhập thông tin của người
                    validSalary = true;  // Nếu lương hợp lệ, thoát khỏi vòng lặp
                }

                // Hiển thị thông tin sau khi nhập
                Person.HienThi(persons[i]);
            }

            // Sắp xếp theo lương
            persons = Person.sortBySalary(persons);  // Gọi phương thức sortBySalary từ lớp Person

            // Hiển thị thông tin sau khi sắp xếp
            Console.WriteLine("\nThông tin sau khi sắp xếp theo Lương:");
            foreach (var person in persons)
            {
                Person.HienThi(person);
            }

            Console.ReadLine();
        }
    }
}

