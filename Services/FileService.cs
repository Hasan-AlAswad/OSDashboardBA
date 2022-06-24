namespace OSDashboardBA.Services
{
    public class FileService
    {
        public string WriteFile(string directory, string fileExtension, string content) // overloading
        {
            //Path.GetRandomFileName();
            string path = Path.Combine(directory, Path.GetRandomFileName() + "." + fileExtension);
            File.WriteAllText(path, content);

            return path;
        }
    }
}
