// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// WebForms and MEF Sample
// http://mef.codeplex.com/
// http://mef.codeplex.com/license
// http://mef.codeplex.com/releases/view/44166 
// -----------------------------------------------------------------------


using System.Web;
using System.ComponentModel.Composition.Hosting;

namespace Ektron.Composition.WebExtensions
{
	public class ComposableWebHandlerFactory : SimpleWebHandlerFactory
	{
		public override IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
		{
			IHttpHandler handler = base.GetHandler(context, requestType, virtualPath, path);

			if (handler != null)
			{
				CompositionBatch batch = new CompositionBatch();
				batch = ComposeWebPartsUtils.BuildUp(batch, handler);
				ContextualCompositionHost.Container.Compose(batch);
			}

			return handler;
		}

	}
}
