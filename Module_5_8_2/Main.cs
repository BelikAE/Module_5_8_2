using Autodesk.Revit.Attributes;
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
            Document doc = uiDoc.Document;

            string exportPath = @"C:\Export\Model.nwc"; // Укажите свой путь

            try
            {
                NWCExporter exporter = new NWCExporter();
                exporter.ExportToNWC(doc, exportPath);
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

    public class NWCExporter
    {
        public void ExportToNWC(Document document, string exportPath)
        {
            // Создаем опции экспорта для Navisworks
            NavisworksExportOptions options = new NavisworksExportOptions();

            // Настройка параметров (пример)
            options.ExportScope = NavisworksExportScope.Model; // Экспорт всей модели
            options.Coordinates = NavisworksCoordinates.Shared; // Использовать общие координаты
            options.ConvertElementProperties = true; // Конвертировать свойства элементов
            options.ExportLinks = true; // Включить связанные файлы

            // Убедитесь, что директория существует
            Directory.CreateDirectory(Path.GetDirectoryName(exportPath));

            // Выполняем экспорт
            document.Export(Path.GetDirectoryName(exportPath),
                            Path.GetFileNameWithoutExtension(exportPath),
                            options);
        }
    }
}
