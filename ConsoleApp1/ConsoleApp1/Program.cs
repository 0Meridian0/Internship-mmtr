using System.IO.Hashing;
using System.Text;

byte[] b = new byte[3];

while (true)
{
    Console.WriteLine("Введите строку: ");
    byte a = byte.Parse(Console.ReadLine());


    Console.WriteLine("Добавить в массив?");
    var flag = Console.ReadLine();
    if (flag == "y")
    {
        b.Append(a);
        var crc = Crc32.Hash(b);
        Console.WriteLine(crc);
    }

    if(flag == "d")
    {
        b = null;
    }

    flag = string.Empty;
}