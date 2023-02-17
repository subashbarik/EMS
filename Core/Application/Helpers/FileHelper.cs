namespace Application.Helpers
{
    public static class FileHelper
    {
        public static bool DirExists(string path)
        {
            return Directory.Exists(path);
        }
        public static bool CreateDir(string path)
        {
            bool dirCreated = false;
            try
            {
                Directory.CreateDirectory(path);
                dirCreated = true;
            }
            catch(Exception ex) 
            { 
                throw new Exception(ex.Message, ex);
            }
            return dirCreated;
        }
        public static bool ContainsFileExtension(string fileName,string extension)
        {
            if(fileName.Contains(extension))
            {
                return true;
            }
            else
            { 
                return false;
            }
        }
    }
}
