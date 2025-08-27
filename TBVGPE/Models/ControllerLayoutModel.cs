using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TBVGPE.Models
{
    public enum ControllerElementType { FaceButtons, LeftStick, RightStick, DPad, Shoulder, Trigger, Button, Custom }

    public class ControllerElementModel : INotifyPropertyChanged
    {
        string _id;
        ControllerElementType _type;
        double _x, _y, _width, _height;

        public string Id { get => _id; set { _id = value; OnPropertyChanged(); } }
        public ControllerElementType Type { get => _type; set { _type = value; OnPropertyChanged(); } }

        // Position on the canvas
        public double X { get => _x; set { _x = value; OnPropertyChanged(); } }
        public double Y { get => _y; set { _y = value; OnPropertyChanged(); } }

        // Size
        public double Width { get => _width; set { _width = value; OnPropertyChanged(); } }
        public double Height { get => _height; set { _height = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }

    public class ControllerLayoutModel
    {
        public string Name { get; set; }
        public ObservableCollection<ControllerElementModel> Elements { get; set; } = new();
    }
}
