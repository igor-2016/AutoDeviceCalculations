using Serilog;
using System.Reflection;

namespace CalcStatistics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            try
            {

                var assembly = Assembly.GetExecutingAssembly();

                var dataFilePath = new FileInfo(assembly.Location).DirectoryName + @"\Data\data.txt";

                Log.Information("Loading data file: {filePath}", dataFilePath);

                try
                {
                    Calculator.Calc(dataFilePath, new CalcSettings()
                    {
                        Window = 21,
                        ReaderType = ReaderTypes.LineByLine,
                        ParserType = ParserTypes.Split,
                    });
                    Log.Information("Done.");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error. Calculation went wrong...");
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error. Loading went wrong...");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            //Console.ReadLine();
        }
    }
}