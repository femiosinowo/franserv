﻿// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// WebForms and MEF Sample
// http://mef.codeplex.com/
// http://mef.codeplex.com/license
// http://mef.codeplex.com/releases/view/44166 
// -----------------------------------------------------------------------

using System;
using System.Web.UI;
using System.Web;

namespace Ektron.Composition.WebExtensions
{
	internal class WebHandlerParser : SimpleWebHandlerParser
	{
		internal WebHandlerParser(HttpContext context, string virtualPath, string physicalPath)
			: base(context, virtualPath, physicalPath)
		{
		}

		public static Type GetCompiledType(HttpContext context, string virtualPath, string physicalPath)
		{
			WebHandlerParser parser = new WebHandlerParser(context, virtualPath, physicalPath);
			Type type = parser.GetCompiledTypeFromCache();
			if (type != null)
				return type;
			
			throw new HttpException(string.Format("File '{0}' is not a web handler.", virtualPath));
		}
	
		protected override string DefaultDirectiveName
		{
			get
			{
				return "webhandler";
			}
		}
	}

}
