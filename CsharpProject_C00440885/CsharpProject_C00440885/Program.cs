// Devin Skipps
// C00440885
// CMPS 358 .NET/C# Programming
// CsharpProject1
List<booktype> collection = new List<booktype>();
booktype book = new booktype();

//run all the commands
ReadFile();
displaybooks();

//reads the file
void ReadFile()
{
    //needs a list of lines to hold every line 
    List<string> lines = new List<string>();
    using (var location = new StreamReader("../../../../LeavesOfGrassWhitman.txt"))
    {
        string Cline;
        //whilt cline has a line and not null
        while ((Cline = location.ReadLine()) != null)
        {
            lines.Add(Cline);
        }
    }
    // for each line being read in lines if the line had book make a new chapter
    //if title start new chapter else add the line to body
    foreach (string line in lines)
    {
        if (line.StartsWith("BOOK"))
        {
            collection.Add(book);
            book = new booktype { chapter = line, body = new List<string>() };

        }
        else if (line.StartsWith("LEAVES OF GRASS"))
        {
            collection.Add(book);
            book = new booktype { chapter = "TITLE " + line, body = new List<string>() };
        }
        else
        {
            book.body.Add(line);

        }
    }
    //if the body isnt null and is finished add it
    if (book.body != null)
    {
        collection.Add(book);
    }
    
}
//displays book
void displaybooks()
{
    Console.WriteLine("Enter number to view chapter");
    Console.WriteLine("U - up, D - down, X - exit");
    //start initializes to the beginning of the list
    int start = 1;
    //while true is always true but x is an exit
    while (true)
    {
        //use math.min to take the smaller number to determin the end
        int end = Math.Min(start + 20, collection.Count);
        //if start is greater than collection list then it is the end in order to print the same thing subtract
        if (start > collection.Count)
        {
            start -= 20;
        }
        //for loop to read the 20 lines or if less then the less
        for (int i = start; i < end; i++)
        {
            Console.WriteLine($"{i}. {collection[i].chapter}");

        }
        Console.WriteLine("U - up, D - down, X - exit");
        string? input = Console.ReadLine()?.ToUpper();
        //if x exit if d go to next 20 or less if u go back 20 or less
            if (input == "X")
            { 
                Environment.Exit(0);
            }
                
            else if (input == "D")
            {
                start += 20;
            }
            
            else if(input =="U")
            {
                start -= 20;
                //catches if start - 20 gives less than 1
                if (start < 1)
                {
                    start = 1;
                }
            }
            //if a number is inputed then read it
            else if (int.TryParse(input, out int number))
            {
                if (number >= 0 && number <= collection.Count - 1)
                {
                    Read(number);
                }
            }
            //anything else is not needed
            else
            {
                Console.WriteLine("invalid");
            }
            Console.Clear();
        }
        
        
    }

// reads the book 
void Read(int chapternumber)
{
   
        booktype specificbook = collection[chapternumber];
        int size = 20;
        // read as long as the book that you are reading has a body count greater than i
        for (int i = 0; i < specificbook.body.Count; i += size)
        {
            //want the lowest number
            int end = Math.Min(i + size, specificbook.body.Count);
            for (int j = i; j < end; j++)
            {
                Console.WriteLine(specificbook.body[j]);
            }
            Console.WriteLine("Enter number to view chapter");
            Console.WriteLine("U - up, D - down, X - exit");
            string input = Console.ReadLine().ToUpper();
            //basicly the same as the displaybooks
            if (input == "X")
            {
                return;
            }
            else if (input == "U")
            {
                i -= size * 2;
                if (i < 0)
                {
                    i = -1;
                }
            }
            else if (input == "D")
            {
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

    
}

public struct booktype
{
    public string chapter;
    public List<string> body;
}