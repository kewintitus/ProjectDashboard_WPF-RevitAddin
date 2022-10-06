# ProjectDashboard_WPF-RevitAddin
![image](https://user-images.githubusercontent.com/96645509/194328689-0dfbcec7-782f-4882-a956-1b0ebb2acb39.png)
(UI designed in WPF - Windows Presentation Foundation)\
This addin can be used to have a quick look on the model data

## NuGet Packages used
1. Live charts


## Parameters that can be viewed
| Parameters | Functions    | Status    |
| :-----: | :---: | :---: |
| Model Group | Displays the number of model groups created in the model   | Functioning   |
| Detail Groups | Displays the number of detail groups created in the model   | Functioning   |
| In-Place Families | Displays the number of in-place families created in the model   | Functioning   |
| Duplicates | Displays the number of duplicate elements in the model(mulitple elements in the same location)   | Not Functioning   |
| CAD Imports | Displays the number of CAD files imported in the model   | Functioning   |
| Co-ordination Models | Displays the number of Co-ordination Models linked in the model   | Functioning   |
| Point Clouds | Displays the number of Point Clouds linked in the model   | Functioning   |
| Raster Images | Displays the number of Raster Images imported in the model   | Functioning   |
| Total Views | Displays the number of Views created in the model   | Functioning   |
| No of Schedules | Displays the number of Schedules created in the model   | Functioning   |
| Not on Sheets | Displays the number of views not placed on sheets in the model   | Functioning   |
| Sheets | Displays the number of sheets in the model   | Functioning   |
| Revit links | Displays the totla number of links in the model   | Functioning   |
| CAD links | Displays the number of CAD files linkes to the model   | Functioning   |
| Worksets | Displays the number of worksets created in the model   | Functioning   |
| Options | Displays the number of design options in the model   | Functioning   |
| Total Warnings | Displays the number of Warnings in the model(Press update button for output )   | Functioning   |
| File Size | Displays the revit file size (Press update button for output )    | Functioning   |
| Total Elements | Displays the total number of elements in the model(Press update button for output )    | Functioning   |
| Purgable Elements | Displays the purgable elements in the model   | Not Functioning   |   

(Note - For project name to be displayed ensure it is available in revit in project description)

## Installation details

1.	Goto the location where you’ve extracted the Zip file and copy the path of the PointCloudToggle.dll file pressing “Shift” and right clicking on the ObjExport.dll file.  
2.	Open the addin file <ProjectDashboard_WPF.addin> and paste the location instead of the location mentioned within the assembly tag in the addin file.
![image](https://user-images.githubusercontent.com/96645509/194313058-a429f946-536d-440b-acb7-6861c9b7924e.png)

3.	copy the .addin file 

4.	Paste the addin file in ProgramData and in the addin folder with respect to the Revit version in which you want to access the addin.
 







