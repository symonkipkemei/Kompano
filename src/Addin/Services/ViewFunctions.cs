using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Kompano.src.Addin.Services
{
    public static class ViewFunctions
    {

        public static View3D GetOrCreate3DView(Document doc)
        {
            // Try to find an existing 3D view
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            View3D view3D = collector.OfClass(typeof(View3D))
                                     .Cast<View3D>()
                                     .FirstOrDefault(v => !v.IsTemplate);

            // If no 3D view exists, create a new one
            if (view3D == null)
            {
                ViewFamilyType viewFamilyType = new FilteredElementCollector(doc)
                                                .OfClass(typeof(ViewFamilyType))
                                                .Cast<ViewFamilyType>()
                                                .FirstOrDefault(x => x.ViewFamily == ViewFamily.ThreeDimensional);

                if (viewFamilyType != null)
                {
                    view3D = View3D.CreateIsometric(doc, viewFamilyType.Id);
                }
            }

            return view3D;

        }


        public static void SetView3DSettings(UIApplication uiApp, View3D view3D) 
        {
            // Set view to Isometric
            view3D.SetOrientation(new ViewOrientation3D(new XYZ(0, 0, 0), new XYZ(1, 1, 1), new XYZ(0, 0, 1)));

            // Set detail level to Fine
            view3D.get_Parameter(BuiltInParameter.VIEW_DETAIL_LEVEL).Set((int)ViewDetailLevel.Fine);

            // Set visual style to Consistent Colors
            view3D.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(3); // 3 corresponds to Consistent Colors

            // Set scale to 1:1
            view3D.Scale = 1;

        }

        public static void SetZoom(UIApplication uiApp, View3D view3D)
        {
            IList<UIView> uiViews = uiApp.ActiveUIDocument.GetOpenUIViews();

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
