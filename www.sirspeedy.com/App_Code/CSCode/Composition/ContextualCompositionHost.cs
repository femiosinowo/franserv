// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// WebForms and MEF Sample
// http://mef.codeplex.com/
// http://mef.codeplex.com/license
// http://mef.codeplex.com/releases/view/44166 
// -----------------------------------------------------------------------


using System;
using System.Collections;
using System.ComponentModel.Composition.Hosting;

namespace Ektron.Composition.WebExtensions
{
	public static class ContextualCompositionHost
	{
		private static readonly string ItemKey = "__container";
		private static Func<IDictionary> _currentContextAccessor;
		private static Func<CompositionContainer> _containerCreator;

		public static void Initialize(Func<IDictionary> currentContextAccessor, Func<CompositionContainer> containerCreator)
		{
			if (currentContextAccessor == null)
				throw new ArgumentNullException("currentContextAccessor");
			if (containerCreator == null)
				throw new ArgumentNullException("containerCreator");

			_currentContextAccessor = currentContextAccessor;
			_containerCreator = containerCreator;
		}

		public static void BoundaryEnter()
		{
			AssertHasAccessors();

			var items = _currentContextAccessor();
			items[ItemKey] = new Lazy<CompositionContainer>(_containerCreator);
		}

		public static void BoundaryExit()
		{
			AssertHasAccessors();

			var items = _currentContextAccessor();
			var lazy = (Lazy<CompositionContainer>) items[ItemKey];

			if (lazy != null && lazy.IsValueCreated)
				lazy.Value.Dispose();
		}

		public static CompositionContainer Container
		{
			get
			{
				var items = _currentContextAccessor();
				var lazy = (Lazy<CompositionContainer>) items[ItemKey];
				if (lazy == null)
					throw new InvalidOperationException("Container not available in this context");

				return lazy.Value;
			}
		}

		public static bool HasContainer
		{
			get
			{
				if (_currentContextAccessor == null) return false;
				return _currentContextAccessor()[ItemKey] != null;
			}
		}

		private static void AssertHasAccessors()
		{
			if (_currentContextAccessor == null)
				throw new InvalidOperationException("currentContextAccessor was not set");

			if (_containerCreator == null)
				throw new InvalidOperationException("containerCreator was not set");
		}
	}
}