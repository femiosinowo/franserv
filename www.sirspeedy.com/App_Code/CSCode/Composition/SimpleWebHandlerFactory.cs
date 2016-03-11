// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// WebForms and MEF Sample
// http://mef.codeplex.com/
// http://mef.codeplex.com/license
// http://mef.codeplex.com/releases/view/44166 
// -----------------------------------------------------------------------


using System;
using System.Web;

namespace Ektron.Composition.WebExtensions
{
	public class SimpleWebHandlerFactory : IHttpHandlerFactory
	{
		public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
		{
			Type type = WebHandlerParser.GetCompiledType(context, virtualPath, path);
			if (!(typeof(IHttpHandler).IsAssignableFrom(type)))
				throw new HttpException("Type does not implement IHttpHandler: " + type.FullName);

			return Activator.CreateInstance(type) as IHttpHandler;
		}

		public virtual void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}
