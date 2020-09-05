using System;
using System.IO;
using PowerArgs;

namespace BulkReplace.Console.CommandArgs
{
    public class FromFileArgs
    {
        [ArgDescription("list of comma seperated string replacements"), ArgExistingFile, ArgPosition(1), ArgShortcut("r")]
        public string ReplacementsFile { get; set; }

        [ArgDescription("Target file for text replacement"), ArgExistingFile, ArgPosition(2), ArgShortcut("t")]
        public string TargetFile { get; set; }

        [ArgDescription("Output file for text replacement"), ArgPosition(3), ArgShortcut("o")]
        public string OutputFile { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), $"replacement-{DateTime.UtcNow}.csv"); 
    }
}