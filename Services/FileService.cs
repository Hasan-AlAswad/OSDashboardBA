namespace OSDashboardBA.Services
{
    public class FileService
    {
        public string WriteFile(string directory, string fileExtension, string content) // overloading
        {
            //Path.GetRandomFileName();  --> layerId
            string path = Path.Combine(directory, $"{Path.GetRandomFileName()}.{fileExtension}");
            //Console.WriteLine(path);
            Directory.CreateDirectory(directory);
            File.WriteAllText(path, content);

            return path;
        }
        public string EditFile(string path, string content) // overloading
        {
            //Console.WriteLine(path);
            File.WriteAllText(path, content);

            return path;
        }
        public string ReadFile(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
