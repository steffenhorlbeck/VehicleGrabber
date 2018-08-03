using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using VehicleGrabberCore.DataObjects;
using VehicleGrabberCore.Exporter;

namespace VehicleGrabberCore
{
    public class VGCore
    {
        ImporterBase importer = null;

        public VGCore()
        {            
        }


        public void Import(int importType = 0)
        {
            if (importType == (int)ImporterBase.ImporterType.ADAC)
            {
                importer = new ADACImporter();
            } else if (importType == (int)ImporterBase.ImporterType.AUTOMOBILIO)
            {
                importer = new AutomobilioImporter();
            }

            importer.StartImport();
        }



        public void ExportToCSV()
        {
            CSVExporter exporter = new CSVExporter(importer.MakersList, importer.modelsList, importer.modelTypesList, importer.carDetailsList);
            exporter.ExportModels();
            exporter.ExportModelTypes();
        }



        public void ExportToMySQL()
        {
            MySQLExporter exporter = new MySQLExporter(importer.MakersList, importer.modelsList, importer.modelTypesList, importer.carDetailsList);
            exporter.HandleMakers();
            exporter.HandleModels();
        }

        public string GetPageContent()
        {
            string result = string.Empty;

            if(importer != null)
            {
                result = importer.GetPageContent();
            }

            return result;
        }

        public string GetBaseUrl()
        {
            string result = string.Empty;

            if (importer != null)
            {
                result = importer.GetBaseUrl();
            }

            return result;
        }


    }
}
