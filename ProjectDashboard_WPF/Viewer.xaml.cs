using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using LiveCharts;
using LiveCharts.Wpf;
using System.IO;


namespace ProjectDashboard_WPF
{
    /// <summary>
    /// Interaction logic for Viewer.xaml
    /// </summary>
    public partial class Viewer : Window
    {
        public Document document;
        public UIDocument uidoc;
        public int ovrTotalWarnings, ovrTotalElements;
        public double ovrFileSize;
        public Viewer(Document doc)
        {
            document = doc;
            
            
            InitializeComponent();
            DisplayViews();
            DisplayElementsPerformance();
            
        }

        public void DisplayViews()
        {
            //List<View> views = new List<View>();
            int noOfViews;
            int noOfWorksets;
            int noOfSheets;
            int noOfLinks;
            int noOfRasterImages;
            int noOfCADLink;
            int noOfDesignOptions;
            int noOfWarnings;
            int noOfMasses;
            int noOfModelGroups;
            int noOfDetailGroups;
            int noOFTotalElements;
            int notOnSheets;
            int cadLinksCount = 0;
            int cadImportsCount = 0;
            string path;
            

            FilteredElementCollector totalElements = new FilteredElementCollector(document);
            FilteredElementCollector viewsCollector = new FilteredElementCollector(document);
            FilteredWorksetCollector worksets = new FilteredWorksetCollector(document);
            FilteredElementCollector sheets = new FilteredElementCollector(document);
            FilteredElementCollector links = new FilteredElementCollector(document);
            FilteredElementCollector rasterImages = new FilteredElementCollector(document);
            FilteredElementCollector cadLinks = new FilteredElementCollector(document);
            FilteredElementCollector designOptions = new FilteredElementCollector(document);
            FilteredElementCollector masses = new FilteredElementCollector(document);
            FilteredElementCollector modelGroups = new FilteredElementCollector(document);
            FilteredElementCollector detailGroups = new FilteredElementCollector(document);

           
            ElementCategoryFilter modelGroupFilter = new ElementCategoryFilter(BuiltInCategory.OST_IOSModelGroups);
            ElementCategoryFilter massFilter = new ElementCategoryFilter(BuiltInCategory.OST_Mass);
            ElementCategoryFilter linkFilter = new ElementCategoryFilter(BuiltInCategory.OST_RvtLinks);
            ElementCategoryFilter viewFilter = new ElementCategoryFilter(BuiltInCategory.OST_Views);
            ElementCategoryFilter sheetFilter = new ElementCategoryFilter(BuiltInCategory.OST_Sheets);
            ElementCategoryFilter rasterImagesFilter = new ElementCategoryFilter(BuiltInCategory.OST_RasterImages);
            ElementCategoryFilter DesignOptionsFilter = new ElementCategoryFilter(BuiltInCategory.OST_DesignOptions);
            ElementCategoryFilter detailGroupFilter = new ElementCategoryFilter(BuiltInCategory.OST_IOSDetailGroups);

            //IList<View> views = viewsCollector.WherePasses(viewFilter).OfClass(typeof(View))
            viewsCollector.OfClass(typeof(View));
            noOfViews = viewsCollector.Count();
            TotalViews.Text = noOfViews.ToString();

            #region Get No of links
            var linkList = links.WherePasses(linkFilter).WhereElementIsNotElementType().ToElements();
            noOfLinks = linkList.Count();
            RevitLinks.Text = noOfLinks.ToString();

            #endregion


            #region Set Project Name
            string projectName = document.ProjectInformation.Name;
            ProjectName.Text =$"Project {projectName}" ;

            #endregion

            #region Total Elements
            totalElements.WhereElementIsNotElementType();
            noOFTotalElements = totalElements.Count();
            ovrTotalElements = noOFTotalElements;
            TotalElements.Text = noOFTotalElements.ToString();
            #endregion

            #region Detail Groups

            var detailGroupsList = detailGroups.WherePasses(detailGroupFilter).WhereElementIsNotElementType().ToElements();
            noOfDetailGroups = detailGroupsList.Count();
            DetailGroup.Text = noOfDetailGroups.ToString();
            #endregion


            #region File size
            path = document.PathName;
            FileInfo rvtFile = new FileInfo(path);
            double size = rvtFile.Length;
            double fileSIzeInt = (size / 1048576);
            string fileSize = (size / 1048576).ToString();
            FileSizeText.Text = fileSize.ToString();
            ovrFileSize = fileSIzeInt;
            #endregion

            #region ModelGroups
            var modelGroupList = modelGroups.WherePasses(modelGroupFilter).WhereElementIsNotElementType().ToElements();
            noOfModelGroups = modelGroupList.Count();
            ModelGroups.Text = noOfModelGroups.ToString();
            #endregion

            #region Masses
            var massList = masses.WherePasses(massFilter).WhereElementIsElementType().ToElements();
            noOfMasses = massList.Count();
            InPlaceFamilies.Text = noOfMasses.ToString();
            #endregion

            #region DesignOptions
            var designOptionsList = designOptions.WherePasses(DesignOptionsFilter).WhereElementIsNotElementType().ToElements();
            noOfDesignOptions = designOptionsList.Count();
            Options.Text = noOfDesignOptions.ToString();

            #endregion



            #region Host Not Associated
            int notAssociated = 0;

            FilteredElementCollector electricalFixtureCollector = new FilteredElementCollector(document);
            FilteredElementCollector lightingFixtureCollector = new FilteredElementCollector(document);
            FilteredElementCollector electricalEquipmentCollector = new FilteredElementCollector(document);


            ElementCategoryFilter electricalFixtureFilter = new ElementCategoryFilter(BuiltInCategory.OST_ElectricalFixtures);
            ElementCategoryFilter lightingFixtureFilter = new ElementCategoryFilter(BuiltInCategory.OST_LightingFixtures);
            ElementCategoryFilter electricalEquipmentFilter = new ElementCategoryFilter(BuiltInCategory.OST_ElectricalEquipment);

            var lfList = lightingFixtureCollector.WherePasses(lightingFixtureFilter).WhereElementIsNotElementType().ToElements();
            var elecFixtureList = electricalFixtureCollector.WherePasses(electricalFixtureFilter).WhereElementIsNotElementType().ToElements();
            var electEquipmentList = electricalEquipmentCollector.WherePasses(electricalEquipmentFilter).WhereElementIsNotElementType().ToElements();

            foreach(Element ele in lfList)
            {
                var param = ele.get_Parameter(BuiltInParameter.HOST_ID_PARAM).AsValueString();
                if(param == "null" || param == "<not associated>")
                {
                    notAssociated++;
                }
            }
            foreach (Element ele in elecFixtureList)
            {
                var param = ele.get_Parameter(BuiltInParameter.HOST_ID_PARAM).AsValueString();
                if (param == "null" || param == "<not associated>")
                {
                    notAssociated++;
                }
            }

            foreach (Element ele in electEquipmentList)
            {
                var param = ele.get_Parameter(BuiltInParameter.HOST_ID_PARAM).AsValueString();
                if (ele.LookupParameter("Host")!= null)
                {

                }
                
                if (param == "null" || param == "<not associated>")
                {
                    notAssociated++;
                }
            }

            Duplicates.Text = notAssociated.ToString();


            #endregion
            
            #region Schedules

            FilteredElementCollector schedulesColl = new FilteredElementCollector(document);
            ElementCategoryFilter scheduleFilter = new ElementCategoryFilter(BuiltInCategory.OST_Schedules);
            int noOfSchedules;

            var schedules = schedulesColl.WherePasses(scheduleFilter).WhereElementIsNotElementType().ToElements();
            noOfSchedules = schedules.Count();
            SchedulesBlock.Text = noOfSchedules.ToString();


            #endregion
            #region POint Clouds
            FilteredElementCollector pointClouds = new FilteredElementCollector(document);
            ElementCategoryFilter pointCloudFilter = new ElementCategoryFilter(BuiltInCategory.OST_PointClouds);
            int noOfPointClouds;

            var pointCloudList = pointClouds.WherePasses(pointCloudFilter).WhereElementIsNotElementType().ToElements();
            noOfPointClouds = pointCloudList.Count();
            PointClouds.Text = noOfPointClouds.ToString();

            #endregion


            #region CoordinationModels
            FilteredElementCollector coordinationModels = new FilteredElementCollector(document);
            ElementCategoryFilter coordinationModelFilter = new ElementCategoryFilter(BuiltInCategory.OST_Coordination_Model);
            int noOfCoordinationModels;

            var coordinationModelList = coordinationModels.WherePasses(coordinationModelFilter).WhereElementIsNotElementType().ToElements();
            noOfCoordinationModels = coordinationModelList.Count();
            CoordinationModels.Text = noOfCoordinationModels.ToString();

            #endregion

            #region Warnings
            var warningList = document.GetWarnings();
            noOfWarnings = warningList.Count();
            ovrTotalWarnings = warningList.Count();
            TotalWarnings.Text = noOfWarnings.ToString();
            #endregion

            #region Get and set  worksets
            //worksets.OfClass(typeof(Workset));
            //noOfWorksets = worksets.Count();
            //Worksets.Text = noOfWorksets.ToString();
            worksets.OfKind(WorksetKind.UserWorkset);//Filter
            noOfWorksets = worksets.Count();

            Worksets.Text = noOfWorksets.ToString();
            #endregion

            #region Sheets
            var sheetList = sheets.WherePasses(sheetFilter).WhereElementIsNotElementType().ToElements();
            //List<ElementId> sheetList = sheets.OfCategory(BuiltInCategory.OST_Sheets).ToElementIds().ToList();
            noOfSheets = sheetList.Count();
            Sheets.Text = noOfSheets.ToString();

            #endregion

            #region Views not on sheet
            List<ElementId> placedViews = new List<ElementId>();
            foreach (Element vs in sheetList)
            {
                var nvs = vs as ViewSheet;
                var viewsONSheet = nvs.GetAllPlacedViews();
                foreach(ElementId viewId in viewsONSheet)
                {
                    placedViews.Add(viewId);
                }
            }
            
            notOnSheets = noOfViews - placedViews.Count();
            NotOnSheets.Text = notOnSheets.ToString();
            #endregion

                #region Set CAD Links and Imports
                cadLinks.OfClass(typeof(ImportInstance)).WhereElementIsNotElementType();
            foreach (ImportInstance cad in cadLinks)
            {
                
                if (cad.IsLinked)
                {
                    cadLinksCount++;
                }
                else
                {
                    cadImportsCount++;
                }
            }
            noOfCADLink = cadLinks.Count();
            CADLinksDisplay.Text = cadLinksCount.ToString();
            CadImports.Text = cadLinksCount.ToString();
            #endregion

            #region Raster Images
            var rasterImagesList = rasterImages.WherePasses(rasterImagesFilter).WhereElementIsNotElementType().ToElementIds();
            noOfRasterImages = rasterImagesList.Count();
            RasterImages.Text = noOfRasterImages.ToString();

            #endregion

            //foreach(View v in viewsCollector)
            //{
            //    var eleInView = new FilteredElementCollector(document, v.Id).Cast<Element>().ToList();
            //}
            //var allModelElements = GetFamilyInstanceModelElements(document);
            //String totalElements = allModelElements.Count().ToString();
            //TotalElements.Text = totalElements;



        }

       
        public void DisplayElementsPerformance()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeValueOnClickElements(object sender, RoutedEventArgs e)
        {
            TotalElementsGauge.Value = ovrTotalElements;
            PurgableElementsGauge.Value = 220;
            
            
        }

        private void ChangeValueOnClickProject(object sender, RoutedEventArgs e)
        {
            TotalWarningsGauge.Value = ovrTotalWarnings;
            FileSizeGauge.Value = ovrFileSize;
        }

        /*
        public IList<Element> GetFamilyInstanceModelElements(Document doc)
        {
            ElementClassFilter familyInstanceFilter
              = new ElementClassFilter(
                typeof(FamilyInstance));

            FilteredElementCollector familyInstanceCollector
              = new FilteredElementCollector(doc);

            IList<Element> elementsCollection
              = familyInstanceCollector.WherePasses(
                familyInstanceFilter).ToElements();

            IList<Element> modelElements
              = new List<Element>();

            foreach (Element e in elementsCollection)
            {
                if ((null != e.Category)
                && (null != e.LevelId)
                && (null != e.get_Geometry(new Options())))
                {
                    modelElements.Add(e);
                }
            }
            return modelElements;
        }*/
    }
    
}
