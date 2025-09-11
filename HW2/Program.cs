internal class Program
{
    
    private static void Main(string[] args)
    {
        MyDate md1 = new MyDate(DateTime.Now.Day,DateTime.Now.Month, DateTime.Now.Year);
        Console.WriteLine("{0}/{1}/{2}",md1.Day,md1.Month,md1.Year);
        Console.ReadKey();
    }
}