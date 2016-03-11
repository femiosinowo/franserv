// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// WebForms and MEF Sample
// http://mef.codeplex.com/
// http://mef.codeplex.com/license
// http://mef.codeplex.com/releases/view/44166 
// -----------------------------------------------------------------------


using System;
using System.Linq;
using System.ComponentModel.Composition.Hosting;
using System.Web;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition;
using System.Web.UI;

namespace Ektron.Composition
{
public static class ComposeWebPartsUtils
{
		public static CompositionBatch BuildUp(CompositionBatch batch, Object o)
		{
			ComposablePart part = AttributedModelServices.CreatePart(o);

			// any imports?
			if (part.ImportDefinitions.Any())
			{
				if (part.ExportDefinitions.Any())
					// exports are not allowed
					throw new Exception(string.Format("'{0}': Modules, Pages, Handlers, MasterPages and Controls cannot be exportable", o.GetType().FullName));

				batch.AddPart(part);
			}

			return batch;
		}

		public static CompositionBatch BuildUpUserControls(CompositionBatch batch, ControlCollection controls)
		{
			foreach (Control c in controls)
			{
				if (c is UserControl)
					batch = ComposeWebPartsUtils.BuildUp(batch, c);
				batch = BuildUpUserControls(batch, c.Controls);
			}

			return batch;
		}

		public static CompositionBatch BuildUpControls(CompositionBatch batch, ControlCollection controls)
		{
			foreach (Control c in controls)
			{
				batch = ComposeWebPartsUtils.BuildUp(batch, c);
				batch = BuildUpControls(batch, c.Controls);
			}

			return batch;
		}

		public static CompositionBatch BuildUpModules(HttpApplication app)
		{
			CompositionBatch batch = new CompositionBatch();

			for (int i = 0; i < app.Modules.Count - 1; i++)
				batch = BuildUp(batch, app.Modules.Get(i));

			return batch;
		}

		public static CompositionBatch BuildUpMaster(CompositionBatch batch, MasterPage master)
		{
			if (master != null)
				batch = BuildUpMaster(ComposeWebPartsUtils.BuildUp(batch, master), master.Master);

			return batch;
		}

	}
}
