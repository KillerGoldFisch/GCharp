/* ****************************************************************************
 *  RuntimeObjectEditor
 * 
 * Copyright (c) 2005 Corneliu I. Tusnea
 * 
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the author be held liable for any damages arising from 
 * the use of this software.
 * Permission to use, copy, modify, distribute and sell this software for any 
 * purpose is hereby granted without fee, provided that the above copyright 
 * notice appear in all copies and that both that copyright notice and this 
 * permission notice appear in supporting documentation.
 * 
 * Corneliu I. Tusnea (corneliutusnea@yahoo.com.au)
 * www.acorns.com.au
 * ****************************************************************************/


using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;
using GSharp.UI.Controls.RuntimeProperty.Utils;

namespace GSharp.UI.Controls.RuntimeProperty.Tabs.Events
{
	/// <summary>
	/// InstanceEventsTab 
	/// </summary>
	public class InstanceEventsTab : PropertyTab
	{
		public InstanceEventsTab()
		{
		}

		public override Bitmap Bitmap
		{
			get { return (Bitmap) Bitmap.FromStream(GetType().Assembly.GetManifestResourceStream("GSharp.UI.Controls.RuntimePropertys.Resources.Events.bmp")); }
		}

		public override string TabName
		{
			get { return "Events"; }
		}

		public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
		{
			return GetProperties(null, component, attributes);
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
		{
			EventInfoConverter eventConverter = component as EventInfoConverter;
			if (eventConverter != null)
			{
				return eventConverter.GetProperties();
			}
			else
			{
				return PropertyDescriptorUtils.GetInstanceEvents(component);
			}
		}
	}
}