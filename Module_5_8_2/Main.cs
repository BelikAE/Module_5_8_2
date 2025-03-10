using Autodesk.Revit.Attributes;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_5_8_2
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Autodesk.Revit.DB.Document doc = uiDoc.Document;

            try
            {
                NavisworksExportOptions options = new NavisworksExportOptions();

                // Настройка параметров 
                options.ExportScope = NavisworksExportScope.Model; // Экспорт всей модели
                options.Coordinates = NavisworksCoordinates.Shared; // Использовать общие координаты
                options.ConvertElementProperties = true; // Конвертировать свойства элементов
                options.ExportLinks = true; // Включить связанные файлы

                // Выполняем экспорт
                doc.Export(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                                "Model.nwc",
                                options);

                TaskDialog.Show("Экспорт", "Модель успешно экспортирована в NWC!");
                return Result.Succeeded;

            }
            catch (Exception ex)
            {
                TaskDialog.Show("Ошибка", $"Не удалось экспортировать модель: {ex.Message}");
                return Result.Failed;
            }
        }
    }
}
