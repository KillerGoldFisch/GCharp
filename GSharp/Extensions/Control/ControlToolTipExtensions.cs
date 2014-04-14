using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ErhardtAbt.Extensions.ControlEx {
    public static class ControlToolTipExtensions {
        /// <summary>Sets the tooltip for controls found in composite containers to match their parents tooltip</summary>
        /// <param name="aParent">The container parent control</param>
        /// <param name="aTip">The tooltip control to update</param>
        /// <remarks>
        /// By default, the .NET ToolTip class will not display a tooltip when the mouse is over a child control in a 
        /// composite control, such as a UserControl derived class. Use this method to apply the composite controls
        /// tooltip to child controls.
        /// </remarks>
        public static void SetContainerTooltips(this Control aParent, ToolTip aTip, string tip = null) {
            if (aParent == null || aParent.Controls == null || aParent.Controls.Count <= 0)
                return;

            foreach (Control lCtrl in aParent.Controls) {
                if (lCtrl.Controls == null || aParent.Controls.Count <= 0)
                    continue;

                string lTip = tip == null ? aTip.GetToolTip(lCtrl) : tip;
                foreach (Control lChildCtrl in lCtrl.Controls)
                    aTip.SetToolTip(lChildCtrl, lTip);
            }
        }

        /// <summary>Extension method to sets the tooltip for controls found in composite containers to match their parents tooltip</summary>
        /// <param name="aTip">The tooltip control to update (extension)</param>
        /// <param name="aParent">The container parent control</param>
        /// <remarks>
        /// By default, the .NET ToolTip class will not display a tooltip when the mouse is over a child control in a 
        /// composite control, such as a UserControl derived class. Use this method to apply the composite controls
        /// tooltip to child controls.
        /// </remarks>
        public static void SetContainerTooltips(this ToolTip aTip, Control aParent, string tip = null) {
            SetContainerTooltips(aParent, aTip, tip);
        }
    }
}
