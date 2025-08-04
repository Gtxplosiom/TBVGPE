using System.Numerics;
using System.Windows;
using Microsoft.Win32;

namespace TBVGPE.Views
{
    public partial class MenuBar : Window
    {
        private Vector2 _screenDimentions;

        public MenuBar()
        {
            InitializeComponent();

            UpdateDimensions();

            SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
            this.Closed += (s, e) => SystemEvents.DisplaySettingsChanged -= OnDisplaySettingsChanged;
        }

        private void OnDisplaySettingsChanged(object? sender, EventArgs e)
        {
            UpdateDimensions();
        }

        private void UpdateDimensions()
        {
            var width = (int)SystemParameters.PrimaryScreenWidth;
            var height = (int)SystemParameters.PrimaryScreenHeight;
            _screenDimentions = new Vector2(width, height);

            this.Width = _screenDimentions.X;
        }
    }
}
