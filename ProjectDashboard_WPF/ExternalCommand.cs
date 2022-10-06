﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;


namespace ProjectDashboard_WPF
{
    [Transaction(TransactionMode.Manual)]
    public class ExternalCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            UIDocument uIDocument = commandData.Application.ActiveUIDocument;

            Viewer viewer = new Viewer(doc);
            
            viewer.ShowDialog();
            return Result.Succeeded;
        }
    }
}
