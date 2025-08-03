using System.Numerics;
using System.Windows;
using System.Windows.Controls;

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

            Canvas.SetTop(toggleMenuBtn, 40);
            Canvas.SetLeft(toggleMenuBtn, _screenDimentions.X - (toggleMenuBtn.Width * 2));
        }
    }
}

// TODO: do something about that * 2 in the setleft
// * 2 because when i don't the button is cut in half and the rest is outside the screen
