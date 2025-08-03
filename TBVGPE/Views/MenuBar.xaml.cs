using System.Numerics;
using System.Windows;

namespace TBVGPE.Views
{
    public partial class MenuBar : Window
    {
        private readonly Vector2 _screenDimentions;

        public MenuBar()
        {
            InitializeComponent();

            _screenDimentions = new Vector2((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight);

            this.Width = _screenDimentions.X;
        }
    }
}
