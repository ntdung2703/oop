using System;

namespace QuanLyKhachSan
{
    // Lớp trừu tượng 
    abstract class Phong
    {
        public int SoNgayThue { get; set; }
        public double DonGia { get; protected set; }

        public virtual void Nhap()
        {
            Console.Write("Nhập số ngày thuê: ");
            SoNgayThue = int.Parse(Console.ReadLine());
        }

        public abstract double TinhTien();

        public abstract void Xuat();
    }

    // Phòng loại A 
    class PhongA : Phong
    {
        public double TienDichVu { get; set; }

        public PhongA()
        {
            DonGia = 80;
        }

        public override void Nhap()
        {
            base.Nhap();
            Console.Write("Nhập tiền dịch vụ: ");
            TienDichVu = double.Parse(Console.ReadLine());
        }

        public override double TinhTien()
        {
            double tong = DonGia * SoNgayThue + TienDichVu;
            if (SoNgayThue >= 5) tong *= 0.9; // giảm 10%
            return tong;
        }

        public override void Xuat()
        {
            Console.WriteLine($"Phòng A - {SoNgayThue} ngày, Tiền DV: {TienDichVu}, Tổng: {TinhTien()} USD");
        }
    }

    // Phòng loại B 
    class PhongB : Phong
    {
        public PhongB()
        {
            DonGia = 60;
        }

        public override double TinhTien()
        {
            double tong = DonGia * SoNgayThue;
            if (SoNgayThue >= 5) tong *= 0.9; // giảm 10%
            return tong;
        }

        public override void Xuat()
        {
            Console.WriteLine($"Phòng B - {SoNgayThue} ngày, Tổng: {TinhTien()} USD");
        }
    }

    // Phòng loại C 
    class PhongC : Phong
    {
        public PhongC()
        {
            DonGia = 40;
        }

        public override double TinhTien()
        {
            return DonGia * SoNgayThue; // không giảm giá
        }

        public override void Xuat()
        {
            Console.WriteLine($"Phòng C - {SoNgayThue} ngày, Tổng: {TinhTien()} USD");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Phong phong = null;

            Console.WriteLine("Chọn loại phòng thuê:");
            Console.WriteLine("1. Phòng A");
            Console.WriteLine("2. Phòng B");
            Console.WriteLine("3. Phòng C");
            Console.Write("Chọn: ");
            int loai = int.Parse(Console.ReadLine());

            switch (loai)
            {
                case 1: phong = new PhongA(); break;
                case 2: phong = new PhongB(); break;
                case 3: phong = new PhongC(); break;
                default: Console.WriteLine("Loại phòng không hợp lệ!"); return;
            }

            phong.Nhap();
            Console.WriteLine("\n--- Kết quả ---");
            phong.Xuat();

            Console.ReadLine();
        }
    }
}
