using System;
using System.Collections.Generic;
using System.Text;

namespace GSharp.UI.Controls.Tree
{
	public interface IToolTipProvider
	{
		string GetToolTip(TreeNodeAdv node);
	}
}
