using Misfit.Desktop.Toolkit.Presentation;
using Misfit.Desktop.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Misfit.Desktop.Wpf
{
    /// <summary>
    /// An ICommand implementation displaying a message box.
    /// </summary>
    public class SampleMsgBoxCommand
        : CommandBase
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void OnExecute(object parameter)
        {
            ModernDialog.ShowMessage("A messagebox triggered by selecting a hyperlink", "Messagebox", MessageBoxButton.OK);
        }
    }
}
