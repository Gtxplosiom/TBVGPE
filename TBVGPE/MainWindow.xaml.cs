using System.Numerics;
using System.Windows;

namespace TBVGPE
{
    public partial class MainWindow : Window
    {
        private readonly Vector2 _screenDimentions;

        public MainWindow()
        {
            InitializeComponent();

            _screenDimentions = new Vector2((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight);

            this.Width = _screenDimentions.X;
            this.Height = _screenDimentions.Y;
        }
    }
}

// TODO: do something about that * 2 in the setleft
// * 2 because when i don't the button is cut in half and the rest is outside the screen
