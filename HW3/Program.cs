using System;
using System.Collections.Generic;

namespace BillCalculation
{
    class Employee
    {
        public string Name { get; private set; }
        public Employee(string name) { Name = name; }
    }
    class Item
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public double Discount { get; private set; }

        public Item(string name, double price, double discount)
        {
            Name = name;
            Price = price;
            Discount = discount;
        }
    }
    class GroceryBill
    {
        protected Employee Clerk;
        protected List<Item> items;

        public GroceryBill(Employee clerk)
        {
            Clerk = clerk;
            items = new List<Item>();
        }

        public void Add(Item i) { items.Add(i); }

        public virtual double GetTotal()
        {
            double sum = 0;
            foreach (var i in items) sum += i.Price;
            return sum;
        }

        public virtual void PrintReceipt()
        {
            Console.WriteLine("----- Receipt (GroceryBill) -----");
            Console.WriteLine("Clerk: " + Clerk.Name);
            foreach (var i in items)
            {
                Console.WriteLine($"{i.Name,-12}  Price: {i.Price,6:F2}  Discount: {i.Discount,5:F2}");
            }
            Console.WriteLine($"Total: {GetTotal():F2}");
            Console.WriteLine("---------------------------------\n");
        }
    }
    class DiscountBill : GroceryBill
    {
        private bool preferred;

        public DiscountBill(Employee clerk, bool preferred) : base(clerk)
        {
            this.preferred = preferred;
        }

        public override double GetTotal()
        {
            if (!preferred) return base.GetTotal();
            double sum = 0;
            foreach (var i in items) sum += i.Price - i.Discount;
            return sum;
        }

        public int GetDiscountCount()
        {
            if (!preferred) return 0;
            int cnt = 0;
            foreach (var i in items) if (i.Discount > 0) cnt++;
            return cnt;
        }

        public double GetDiscountAmount()
        {
            if (!preferred) return 0;
            double d = 0;
            foreach (var i in items) d += i.Discount;
            return d;
        }

        public double GetDiscountPercent()
        {
            if (!preferred) return 0;
            double before = base.GetTotal();
            if (before == 0) return 0;
            return (GetDiscountAmount() / before) * 100;
        }

        public override void PrintReceipt()
        {
            Console.WriteLine("----- Receipt (DiscountBill) -----");
            Console.WriteLine("Clerk: " + Clerk.Name);
            Console.WriteLine("Preferred customer: " + (preferred ? "YES" : "NO"));
            foreach (var i in items)
            {
                if (preferred && i.Discount > 0)
                {
                    double after = i.Price - i.Discount;
                    Console.WriteLine($"{i.Name,-12}  After: {after,6:F2}  (Orig: {i.Price,5:F2}, Disc: {i.Discount,4:F2})");
                }
                else
                {
                    Console.WriteLine($"{i.Name,-12}  Price: {i.Price,6:F2} (No discount)");
                }
            }
            Console.WriteLine($"Total: {GetTotal():F2}");
            Console.WriteLine($"Discount count: {GetDiscountCount()}");
            Console.WriteLine($"Discount amount: {GetDiscountAmount():F2}");
            Console.WriteLine($"Discount percent: {GetDiscountPercent():F2}%");
            Console.WriteLine("----------------------------------\n");
        }
    }
    class BillLine
    {
        public Item Item { get; private set; }
        public int Quantity { get; private set; }

        public void SetItem(Item item) { Item = item; }
        public void SetQuantity(int q) { Quantity = q; }

        public double GetLineTotal() => Item.Price * Quantity;
        public double GetLineDiscountTotal() => Item.Discount * Quantity;
    }
    class GroceryBillV2
    {
        protected Employee Clerk;
        protected List<BillLine> lines;

        public GroceryBillV2(Employee clerk)
        {
            Clerk = clerk;
            lines = new List<BillLine>();
        }

        public void Add(BillLine bl) { lines.Add(bl); }

        public virtual double GetTotal()
        {
            double sum = 0;
            foreach (var bl in lines) sum += bl.GetLineTotal();
            return sum;
        }

        public virtual void PrintReceipt()
        {
            Console.WriteLine("----- Receipt (GroceryBillV2) -----");
            Console.WriteLine("Clerk: " + Clerk.Name);
            foreach (var bl in lines)
            {
                Console.WriteLine($"{bl.Quantity}x {bl.Item.Name,-10}  Line: {bl.GetLineTotal(),6:F2} (Unit: {bl.Item.Price,5:F2})");
            }
            Console.WriteLine($"Total: {GetTotal():F2}");
            Console.WriteLine("-----------------------------------\n");
        }
    }
    class DiscountBillV2 : GroceryBillV2
    {
        private bool preferred;

        public DiscountBillV2(Employee clerk, bool preferred) : base(clerk)
        {
            this.preferred = preferred;
        }

        public override double GetTotal()
        {
            if (!preferred) return base.GetTotal();
            double sum = 0;
            foreach (var bl in lines) sum += bl.GetLineTotal() - bl.GetLineDiscountTotal();
            return sum;
        }

        public int GetDiscountCount()
        {
            if (!preferred) return 0;
            int cnt = 0;
            foreach (var bl in lines) if (bl.Item.Discount > 0) cnt += bl.Quantity;
            return cnt;
        }

        public double GetDiscountAmount()
        {
            if (!preferred) return 0;
            double d = 0;
            foreach (var bl in lines) d += bl.GetLineDiscountTotal();
            return d;
        }

        public double GetDiscountPercent()
        {
            if (!preferred) return 0;
            double before = base.GetTotal();
            if (before == 0) return 0;
            return (GetDiscountAmount() / before) * 100;
        }

        public override void PrintReceipt()
        {
            Console.WriteLine("----- Receipt (DiscountBillV2) -----");
            Console.WriteLine("Clerk: " + Clerk.Name);
            Console.WriteLine("Preferred customer: " + (preferred ? "YES" : "NO"));
            foreach (var bl in lines)
            {
                if (preferred && bl.Item.Discount > 0)
                {
                    double after = (bl.Item.Price - bl.Item.Discount) * bl.Quantity;
                    Console.WriteLine($"{bl.Quantity}x {bl.Item.Name,-10}  After: {after,6:F2} (Orig/unit: {bl.Item.Price,5:F2}, Disc/unit: {bl.Item.Discount,4:F2})");
                }
                else
                {
                    Console.WriteLine($"{bl.Quantity}x {bl.Item.Name,-10}  Line: {bl.GetLineTotal(),6:F2} (Unit: {bl.Item.Price,5:F2})");
                }
            }
            Console.WriteLine($"Total: {GetTotal():F2}");
            Console.WriteLine($"Discount count: {GetDiscountCount()}");
            Console.WriteLine($"Discount amount: {GetDiscountAmount():F2}");
            Console.WriteLine($"Discount percent: {GetDiscountPercent():F2}%");
            Console.WriteLine("------------------------------------\n");
        }
    }
    
