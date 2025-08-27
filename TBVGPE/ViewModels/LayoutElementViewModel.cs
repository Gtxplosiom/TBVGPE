using TBVGPE.ViewModels;

// para ma push ha viewmodel an value of the position han moved element when editing
public interface ILayoutElement
{
    double X { get; set; }
    double Y { get; set; }
}

public class LayoutElementViewModel : ViewModelBase, ILayoutElement
{
    private double _x;
    private double _y;

    public double X
    {
        get => _x;
        set
        {
            if (_x != value)
            {
                _x = value;
                OnPropertyChanged(nameof(X));
            }
        }
    }

    public double Y
    {
        get => _y;
        set
        {
            if (_y != value)
            {
                _y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
    }
}
