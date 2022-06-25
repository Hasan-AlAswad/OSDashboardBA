namespace OSDashboardBA.Services
{
    public class FileService
    {
        public string WriteFile(string directory, string fileExtension, string content, int layerId) // overloading
        {
            //Path.GetRandomFileName();  --> layerId
            string path = Path.Combine(directory, $"{layerId}.{fileExtension}");
            //Console.WriteLine(path);
            Directory.CreateDirectory(directory);
            File.WriteAllText(path, content);

            return path;
        }
        public string ReadFile(string path)
        {
            return File.ReadAllText(path);
            
        }
    }
}
