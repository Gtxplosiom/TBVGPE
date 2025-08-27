namespace TBVGPE.ViewModels
{
    public interface IControllerLayoutManagerInterface
    {
        // serves as a way to access the SaveLayout methods in each code-behind
        void SaveLayout(string path);
        void LoadLayout(string path);
        void LoadDefault();
    }
}
