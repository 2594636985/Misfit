using System;

namespace Misfit.AddIn.Utility
{
    public static class ConsoleUtil
    {
        // Methods

        //public static void PrintComandError(string itemName,
        //                                    SourceItemResult result)
        //{
        //    switch (result)
        //    {
        //        case SourceItemResult.E_AlreadyConflicted:
        //            Console.WriteLine("error: {0} is already in a conflicted state", itemName);
        //            break;

        //        case SourceItemResult.E_AlreadyUnderSourceControl:
        //            Console.WriteLine("error: {0} is already under source control", itemName);
        //            break;

        //        case SourceItemResult.E_ChildDeleteFailure:
        //            Console.WriteLine("warning: {0} will not be deleted because child items could not be deleted", itemName);
        //            break;

        //        case SourceItemResult.E_HasLocalModifications:
        //            Console.WriteLine("error: {0} has local modifications", itemName);
        //            break;

        //        case SourceItemResult.E_NotInAWorkingFolder:
        //            Console.WriteLine("error: {0} is not in a working folder", itemName);
        //            break;

        //        case SourceItemResult.E_NotUnderSourceControl:
        //            Console.WriteLine("error: {0} is not under source control", itemName);
        //            break;

        //        case SourceItemResult.E_PathNotFound:
        //            Console.WriteLine("error: Path not found: {0}", itemName);
        //            break;

        //        case SourceItemResult.E_WontClobberLocalItem:
        //            Console.WriteLine("error: Won't clobber writable {0}", itemName);
        //            break;

        //        case SourceItemResult.E_WontDeleteFileWithModifications:
        //            Console.WriteLine("error: Won't delete modified {0}", itemName);
        //            break;

        //        case SourceItemResult.E_FileNotFound:
        //            Console.WriteLine("error: File not found: {0}", itemName);
        //            break;

        //        case SourceItemResult.E_DirectoryNotFound:
        //            Console.WriteLine("error: Directory not found: {0}", itemName);
        //            break;

        //        case SourceItemResult.E_AccessDenied:
        //            Console.WriteLine("error: Access denied: {0}", itemName);
        //            break;

        //        default:
        //            Console.WriteLine("Unknown error {0} for {1}", result, itemName);
        //            break;
        //    }
        //}

        public static string ReadLine(bool echoInput)
        {
            string result = "";

            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);

                switch (cki.KeyChar)
                {
                    case '\r':
                        Console.WriteLine();
                        return result;

                    case '\b':
                        if (result.Length > 0)
                        {
                            result = result.Substring(0, result.Length - 1);
                            Console.Write("\b \b");
                        }
                        break;

                    default:
                        result += cki.KeyChar;
                        if (echoInput)
                            Console.Write(cki.KeyChar);
                        else
                            Console.Write("*");
                        break;
                }
            }
        }
    }
}