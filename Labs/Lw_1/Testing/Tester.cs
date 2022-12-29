using System;
using System.Diagnostics;

namespace TestProgramm
{
    internal class Test
    {
        public static void Main()
        {
            //Console.WriteLine("Путь до текстового файла:");
            string pathFile = @"C:\OP_labs\Tests\Labs\Lw_1\Tests.txt";
            if (!File.Exists(pathFile))
            {
                Console.WriteLine("Error");
                return;
            }
            Console.WriteLine("Ожидайте...");
            StreamReader Input = new(pathFile);
            using StreamWriter Output = new(@"C:\OP_labs\Tests\Labs\Lw_1\Results.txt");
            string InputArguments, Arguments, Expectation, Result, OutputResult;
            while ((InputArguments = Input.ReadLine()) != null)
            {
                Arguments = InputArguments.Substring(0, InputArguments.IndexOf('=') - 1);
                Expectation = InputArguments.Substring(InputArguments.IndexOf('=') + 2);

                var startInfo = new ProcessStartInfo
                {
                    FileName = @"C:\OP_labs\Tests\Labs\Lw_1\bin\Debug\net6.0\triangle.exe",
                    Arguments = Arguments,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                };
                using Process process = Process.Start(startInfo);
                Result = process.StandardOutput.ReadLine();
                process.WaitForExit();
                if (Expectation == Result)
                    OutputResult = "success";
                else
                    OutputResult = "error";
                Output.WriteLine(OutputResult);
                Output.Flush();
            }
        }
    }
}