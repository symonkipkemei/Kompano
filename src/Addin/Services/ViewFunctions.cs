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
                .FirstOrDefault(v => v.Name.Equals("Kompano", StringComparison.OrdinalIgnoreCase));


            if (view3D == null)
            {
                // Get the 3D view type in the docs
                ViewFamilyType viewFamilyType = new FilteredElementCollector(doc)
                    .OfClass(typeof(ViewFamilyType))
                    .Cast<ViewFamilyType>()
                    .FirstOrDefault(x => x.ViewFamily == ViewFamily.ThreeDimensional);

                if (viewFamilyType != null)
                {
                    using( Transaction trans = new Transaction(doc, "Create a 3D view"))
                    {
                        trans.Start();
                        //Create the 3D view
                        view3D = View3D.CreateIsometric(doc, viewFamilyType.Id);
                        view3D.Name = "Kompano";

                        trans.Commit();


                    }
                    
                }

                else
                {
                    MessageBox.Show("{3D} Could not be created. View family type 3D is missing");

                }
                
            }
 
  
            return view3D;
        }

       

        public static void SetView3DSettings( View3D view3D) 
        {

            // Set view to Isometric
            //XYZ eyePosition = new XYZ(1, 1, 1); // Camera position
            //XYZ upDirection = new XYZ(0, 0, 1); // Up direction (Z-axis)
            //XYZ forwardDirection = new XYZ(-1, -1, 0); // Forward direction (must be orthogonal to upDirection)


            Dictionary<string, (XYZ eyePosition, XYZ upDirection, XYZ forwardDirection)>  orientation3D = new Dictionary<string, (XYZ eyePosition, XYZ upDirection, XYZ forwardDirection)>();

            orientation3D["Orientation1"] = (new XYZ(8.261403115, -3.573411012, 3.164970061), new XYZ(-0.408248290, 0.408248290, 0.816496581), new XYZ(-0.577350269, 0.577350269, -0.577350269));
            orientation3D["Orientation2"] = (new XYZ(8.261403115, 3.364421511, 3.164970061), new XYZ(-0.408248290, -0.408248290, 0.816496581), new XYZ(-0.577350269, -0.577350269, -0.577350269));
            orientation3D["Orientation3"] = (new XYZ(1.323570591, 3.364421511, 3.164970061), new XYZ(0.408248290, -0.408248290, 0.816496581), new XYZ(0.577350269, -0.577350269, -0.577350269));
            orientation3D["Orientation4"] = (new XYZ(1.323570591, -3.573411012, 3.164970061), new XYZ(0.408248290, 0.408248290, 0.816496581), new XYZ(0.577350269, 0.577350269, -0.577350269));

 
            (XYZ eyePosition, XYZ upDirection, XYZ forwardDirection)  selectedOrientation = orientation3D[App.OrientationKey ?? "Orientation1"];

            XYZ eyePosition = selectedOrientation.eyePosition;
            XYZ upDirection = selectedOrientation.upDirection;
            XYZ forwardDirection = selectedOrientation.forwardDirection;

            view3D.SetOrientation(new ViewOrientation3D(eyePosition, upDirection, forwardDirection));


            // Set detail level to Fine
            view3D.get_Parameter(BuiltInParameter.VIEW_DETAIL_LEVEL).Set((int)ViewDetailLevel.Fine);

            // Set visual style to Consistent Colors
            view3D.DisplayStyle = DisplayStyle.Shading;

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
                    uiView.ZoomToFit();
                }
            }
        }

    }
}
