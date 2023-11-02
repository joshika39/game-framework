using System.Windows.Media;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.UI.WPF;

namespace GameFramework.ManualTests.Desktop.WPF.GameCanvas.TestUnitVisuals
{
    public class TestUnitView : AUnitTile
    {
        public TestUnitView(IPosition2D position, IConfigurationService2D configurationService) : base(position, configurationService)
        {
            Fill = new SolidColorBrush(Colors.Blue);
        }
        
        public override void SteppedOn(IUnit2D unit2D)
        {
            
        }
    }
}
