using ElvUI_Updater.Implementations;

namespace ElvUI_Updater
{
    public class Factory
    {
        public WebHelper GetWebHelper()
        {
            return new WebHelper();
        }
        public FileSystemHelper GetFileSystemHelper()
        {
            return new FileSystemHelper();
        }
    }
}
