string relativePath = @"C:\temp\..\temp\.\.\text\..\test.txt";
string absolutePath = @"C:\temp\test.txt";

Console.WriteLine(GetAbsolutePath(relativePath));

string GetAbsolutePath(string relativePath)
{
    List<string> relativeWords = relativePath.Split(@"\").ToList();
    relativeWords.RemoveAll(e => e.Equals("."));

    Stack<string> resultStack = new Stack<string>();

    for (int i = 0; i < relativeWords.Count; i++)
    {
        if (relativeWords[i].Equals(".."))
        {
            resultStack.Pop();
        }

        else
        {
            resultStack.Push(relativeWords[i]);
        }
    }

    return string.Join(@"\", resultStack.Reverse());
}
