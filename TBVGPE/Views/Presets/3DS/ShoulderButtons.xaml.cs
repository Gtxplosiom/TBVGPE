using Microsoft.Win32;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace TBVGPE.Views.Presets._3DS
{
    public partial class ShoulderButtons : UserControl
    {
        private Vector2 _screenDimentions;

        public ShoulderButtons()
        {
            InitializeComponent();

            UpdateDimensions();

            SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
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
