using System.Windows.Media;
using GameFramework.Configuration;
using GameFramework.Core.Position;
using GameFramework.UI.WPF.Tiles.Static;
using Color = System.Drawing.Color;

namespace GameFramework.ManualTests.Desktop.WPF.GameCanvas
{
    public class TestTileView : AHoverableTileView
    {
        public TestTileView(IPosition2D position, IConfigurationService2D configurationService) : base(position, configurationService, Color.Chocolate, false)
        { }

        public override void OnHovered()
        {
            base.OnHovered();
            Fill = new SolidColorBrush(Colors.Coral);
        }
        
        public override void OnHoverLost()
        {
            base.OnHoverLost();
            Fill = new SolidColorBrush(Colors.Chocolate);
        }
    }
}
