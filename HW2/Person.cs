using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2
{
    internal class Person
    {
        private string name;
        private string address;
        private double salary;

        // Constructor
        public Person(string name, string address, double salary)
        {
            this.name = name;
            this.address = address;
            this.salary = salary;
        }

        public string Name 
        { 
            get { return name; } 
            set { name = value; } 
        }
        public string Address 
        { 
            get { return address; } 
            set { address = value; } 
        }
        public double Salary 
        { 
            get { return salary; } 
            set { salary = value; } 
        }

        // Phương thức nhập thông tin người
        public static Person Nhap(string name, string address, string sSalary)
        {
            double salary = 0;
            bool validSalary = false;

            // Kiểm tra tính hợp lệ của lương
            while (!validSalary)
            {
                // Sử dụng double.TryParse để kiểm tra lương có phải là số hợp lệ không
                if (double.TryParse(sSalary, out salary))
                {
                    // Kiểm tra lương có lớn hơn 0 không
                    if (salary <= 0)
                    {
                        Console.WriteLine("Lương phải lớn hơn 0.");
                    }
                    else
                    {
                        validSalary = true;
                    }
                }
                else
                {
                    Console.WriteLine("Bạn phải nhập số.");
                }

                // Nếu lương không hợp lệ, yêu cầu nhập lại
                if (!validSalary)
                {
                    Console.Write("Vui lòng nhập lại lương: ");
                    sSalary = Console.ReadLine();
                }
            }

            return new Person(name, address, salary);
        }

        // Phương thức hiển thị thông tin người
        public static void HienThi(Person person)
        {
            Console.WriteLine($"\nThông tin người bạn đã nhập");
            Console.WriteLine($"Tên: {person.Name}");
            Console.WriteLine($"Địa chỉ: {person.Address}");
            Console.WriteLine($"Lương: {person.Salary}");
        }

        // Phương thức sắp xếp theo lương (BubbleSort)
        public static Person[] sortBySalary(Person[] persons)
        {
            for (int i = 0; i < persons.Length - 1; i++)
            {
                for (int j = 0; j < persons.Length - i - 1; j++)
                {
                    if (persons[j].Salary > persons[j + 1].Salary)
                    {
                        // Hoán đổi vị trí nếu lương của person[j] lớn hơn person[j+1]
                        Person temp = persons[j];
                        persons[j] = persons[j + 1];
                        persons[j + 1] = temp;
                    }
                }
            }
            return persons;
        }
    }
}
