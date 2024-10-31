using System.Text;

namespace ConsoleApp2
{
    
    public static class Logger
    {
        // Стандартное название для файла логирования
        private static string _filename = "history.log";
        private enum Codes { OK, ERROR, WARNING };

        public static void setFilename(string filename)
        {
            _filename = filename;
        }
        
        public static async void WriteLog(string input, int code)
        {
            var status_code = (Codes)code;
            string output = $"[{status_code.ToString()}] [{DateTime.Now}] {input}\n";

            byte[] encodedText = Encoding.Unicode.GetBytes(output);

            using (FileStream sourceStream = new FileStream(_filename,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }
    }
}
