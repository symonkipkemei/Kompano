using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kompano.src.Addin.Services
{
    public static class ViewFunctions
    {
        public static View3D Activate3DView( Document doc)
        {
            // check if the view already exists

            View3D view3D = new FilteredElementCollector(doc)
                .OfClass(typeof(View3D))
                .Cast<View3D>()
                .FirstOrDefault(v => v.Name.Equals("{3D}", StringComparison.OrdinalIgnoreCase));


            if (view3D == null)
            {
                MessageBox.Show("View3D is null");
            }
 
  
            return view3D;
        }

       

        public static void SetView3DSettings( View3D view3D) 
        {

            // Set view to Isometric
            //XYZ eyePosition = new XYZ(1, 1, 1); // Camera position
            //XYZ upDirection = new XYZ(0, 0, 1); // Up direction (Z-axis)
            //XYZ forwardDirection = new XYZ(-1, -1, 0); // Forward direction (must be orthogonal to upDirection)
            ViewOrientation3D currentOrientation = view3D.GetOrientation();
            XYZ eyePosition = currentOrientation.EyePosition;
            XYZ upDirection = currentOrientation.UpDirection;
            XYZ forwardDirection = currentOrientation.ForwardDirection;

            MessageBox.Show($"Eyeposition: {eyePosition}, Updrirection {upDirection} Forwarddirection {forwardDirection}");

            view3D.SetOrientation(new ViewOrientation3D(eyePosition, upDirection, forwardDirection));


            // Set detail level to Fine
            view3D.get_Parameter(BuiltInParameter.VIEW_DETAIL_LEVEL).Set((int)ViewDetailLevel.Fine);

            // Set visual style to Consistent Colors
            view3D.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(3); // 3 corresponds to Consistent Colors

            //Set scale to 1:1
            view3D.Scale = 1;

        }

        public static void SetZoom(UIDocument familyUiDoc, View3D view3D)
        {
            IList<UIView> uiViews = familyUiDoc.GetOpenUIViews();

            foreach (UIView uiView in uiViews)
            {
                if (uiView.ViewId.Equals(view3D.Id))
                {
                    Rectangle viewRectangle = uiView.GetWindowRectangle();
                    XYZ cornerView1 = new XYZ(viewRectangle.Left, viewRectangle.Bottom, 0);
                    XYZ cornerView2 = new XYZ(viewRectangle.Right, viewRectangle.Top, 0);
                    uiView.ZoomAndCenterRectangle(cornerView1,cornerView2);
                }
            }
        }

    }
}
