// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// WebForms and MEF Sample
// http://mef.codeplex.com/
// http://mef.codeplex.com/license
// http://mef.codeplex.com/releases/view/44166 
// -----------------------------------------------------------------------


using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Web;
using System.IO;

namespace Ektron.Composition.WebExtensions
{
	public class ScopedContainerHttpModule : IHttpModule
	{
		private const string ModeKey = "Mode";
		private HttpApplication _app;
		private ComposablePartCatalog _catalog;
		private CompositionContainer _container;

		public void Init(HttpApplication app)
		{
			app.BeginRequest += OnBeginRequest;
			app.EndRequest += OnEndRequest;
			this._app = app;

			CreateAppLevelContainer();
			SetUpRequestLevelContainerCreation();

			CallbackAppToAllowContainerCustomization();

			ComposeModules(app);
		}

		private void ComposeModules(HttpApplication app)
		{
			CompositionBatch batch = ComposeWebPartsUtils.BuildUpModules(app);
			_container.Compose(batch);
		}

		private void CallbackAppToAllowContainerCustomization()
		{
			var method = this._app.GetType().GetMethod("ConfigureContainer", BindingFlags.Public | BindingFlags.Instance);

			if (method == null)
				return;

			method.Invoke(this._app, new object[] { _container });
		}

		public void Dispose()
		{
			try
			{
				this._app.BeginRequest -= OnBeginRequest;
				this._app.EndRequest -= OnEndRequest;
			}
			catch (InvalidOperationException) { }
		}

		private void CreateAppLevelContainer()
		{
			var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
			
			_catalog = new DirectoryCatalog(path);

			_container = new CompositionContainer(
					new FilteredCatalog(_catalog, def => GetAllWithinAppScope(def)), true);

			CompositionHost.Initialize(_container);
		}

		private void SetUpRequestLevelContainerCreation()
		{
			ContextualCompositionHost.Initialize(
					() => HttpContext.Current.Items, CreateRequestContainer);
		}

		private void OnBeginRequest(object sender, EventArgs e)
		{
			ContextualCompositionHost.BoundaryEnter();
		}

		private void OnEndRequest(object sender, EventArgs e)
		{
			ContextualCompositionHost.BoundaryExit();
		}

		private CompositionContainer CreateRequestContainer()
		{
			return new CompositionContainer(
					new FilteredCatalog(_catalog, def => GetAllWithinRequestScope(def)), _container);
		}

		private static bool GetAllWithinAppScope(ComposablePartDefinition def)
		{
			return def.ExportDefinitions.
					Any(ed =>
							!ed.Metadata.ContainsKey(ModeKey) ||
							(ed.Metadata.ContainsKey(ModeKey) && ((WebScopeMode)ed.Metadata[ModeKey]) == WebScopeMode.Application));
		}

		private static bool GetAllWithinRequestScope(ComposablePartDefinition def)
		{
			return def.ExportDefinitions.
					Any(ed =>
							(ed.Metadata.ContainsKey(ModeKey) && ((WebScopeMode)ed.Metadata[ModeKey]) == WebScopeMode.Request));
		}
	}
}
