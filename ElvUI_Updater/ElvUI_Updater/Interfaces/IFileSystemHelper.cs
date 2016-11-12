namespace ElvUI_Updater.Interfaces
{
    public interface IFileSystemHelper
    {
        string LocateWorldOfWarcraftInstallation();
        void DirectoryCopy(string source, string destination);
    }
}
