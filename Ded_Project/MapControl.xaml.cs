using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ded_Project
{
    /// <summary>
    /// Логика взаимодействия для MapControl.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        public bool flag = true;
        public MapControl()
        {
            InitializeComponent();
        }
        public void LoadMap(double x, double y)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            mapView.MapProvider = GMapProviders.GoogleMap;
            mapView.Position = new PointLatLng(x, y);
            mapView.MinZoom = 5;
            mapView.MaxZoom = 19;

            GMapMarker marker = new GMapMarker(mapView.Position);
            marker.Shape = new Ellipse
            {
                Width = 20,
                Height = 20,
                Stroke = Brushes.Red,
                StrokeThickness = 5,
                Fill = Brushes.White           
            };
            mapView.Markers.Add(marker);
            
            mapView.Zoom = 17;
            if (flag)
            {
                // lets the map use the mousewheel to zoom
                mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
                // lets the user drag the map
                mapView.CanDragMap = true;
                // lets the user drag the map with the left mouse button
                mapView.DragButton = MouseButton.Right;            
            }
        }
    }
}
