// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// WebForms and MEF Sample
// http://mef.codeplex.com/
// http://mef.codeplex.com/license
// http://mef.codeplex.com/releases/view/44166 
// -----------------------------------------------------------------------


using System;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.ComponentModel.Composition.Hosting;

namespace Ektron.Composition.WebExtensions
{
	public class ComposeContainerHttpModule : IHttpModule
	{
		/// <summary>
		/// You will need to configure this module in the web.config file of your
		/// web and register it with IIS before being able to use it. For more information
		/// see the following link: http://go.microsoft.com/?linkid=8101007
		/// </summary>
		#region IHttpModule Members

		public void Init(HttpApplication context)
		{
			context.PreRequestHandlerExecute += ContextPreRequestHandlerExecute;
		}

		private void ContextPreRequestHandlerExecute(object sender, EventArgs e)
		{
			Page page = HttpContext.Current.CurrentHandler as Page;
			if (page != null)
			{
				page.PreInit += Page_PreInit;
				page.Init += Page_Init;
			}
		}

		private void Page_Init(object sender, EventArgs e)
		{
			Page handler = sender as Page;

			if (handler != null)
			{
				try
				{
					CompositionBatch batch = new CompositionBatch();
					batch = ComposeWebPartsUtils.BuildUpControls(batch, handler.Controls);
					ContextualCompositionHost.Container.Compose(batch);
				}
				catch (ReflectionTypeLoadException)
				{
				}
			}
		}

		private void Page_PreInit(object sender, EventArgs e)
		{
			Page handler = sender as Page;

			if (handler != null)
			{
				try
				{
					CompositionBatch batch = new CompositionBatch();
					batch = ComposeWebPartsUtils.BuildUp(batch, handler);
					batch = ComposeWebPartsUtils.BuildUpUserControls(batch, handler.Controls);
					batch = ComposeWebPartsUtils.BuildUpMaster(batch, handler.Master);
					ContextualCompositionHost.Container.Compose(batch);
				}
				catch (ReflectionTypeLoadException)
				{
				}
			}
		}

		public void Dispose()
		{
		}

		#endregion
	}
}
