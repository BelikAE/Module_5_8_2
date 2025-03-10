using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Module_5_8_2
{
    public class ViewModel : BaseViewModel
    {
        public event EventHandler CloseRequest;
        public event EventHandler HideRequest;
        public event EventHandler ShowRequest;
        private ExternalCommandData _commandData;
        public DelegateCommand DelCommand { get; }


        public ViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            DelCommand = new DelegateCommand(Command);
        }

        private void Command()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;


            using (var ts = new Transaction(doc, "Transaction name"))
            {
                ts.Start();


                ts.Commit();
            }

            RaiseCloseRequest();
        }

        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

        private void RaiseHideRequest()
        {
            HideRequest?.Invoke(this, EventArgs.Empty);
        }

        private void RaiseShowRequest()
        {
            ShowRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}
