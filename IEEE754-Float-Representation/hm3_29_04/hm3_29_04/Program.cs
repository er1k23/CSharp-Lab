class Program
{
    static void Main()
    {
        MyList list = new MyList();

        list.Add(10);
        list.Add(20);
        list.Add(30);

        list.AddRange(new int[] { 40, 50 });

        list.Remove(20);

        int value;
        if (list.TryGet(2, out value))
        {
            Console.WriteLine("Value: " + value);
        }

        Console.WriteLine("Contains 30: " + list.Contains(30));
        Console.WriteLine("Index of 50: " + list.IndexOf(50));

        Console.WriteLine("Count: " + list.Count);

        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine(list[i]);
        }
    }
}