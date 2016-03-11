using System.Collections.Generic;
using System.ComponentModel.Composition;
using Ektron.Cms.Common;
using Ektron.Cms.Content.Targeting.Rules;

namespace Ektron.Cms.RuleTemplateCommands
{
	[Export(typeof(IRuleTemplateCommand))]
	public class UserPropertyCommand : IRuleTemplateCommand
	{
		public UserPropertyCommand()
		{
			RuleTemplates = new List<RuleTemplateWithOrder>();
		}

		public ICollection<RuleTemplateWithOrder> RuleTemplates { get; set; }

		public void Execute()
		{
			

			Ektron.Cms.API.User.User userApi = new Ektron.Cms.API.User.User();
			Ektron.Cms.UserCustomPropertyData[] customProperties = userApi.EkUserRef.GetAllCustomProperty("");
			if (customProperties == null) return;

			int stringOrder = 2000;
			int selectListOrder = 2020;
			int booleanOrder = 2040;
			int numericOrder = 2060;
			int dateOrder = 2080;

			foreach (Ektron.Cms.UserCustomPropertyData customProperty in customProperties)
			{
				switch (customProperty.PropertyValueType)
				{
					case EkEnumeration.ObjectPropertyValueTypes.String:
						RuleTemplates.Add(
							new RuleTemplateWithOrder
								{
									Order = stringOrder,
									RuleTemplate = new UserStringPropertyRuleTemplate(customProperty)
								});
						stringOrder++;
						break;

					case EkEnumeration.ObjectPropertyValueTypes.SelectList:
						RuleTemplates.Add(
							new RuleTemplateWithOrder
							{
								Order = selectListOrder,
								RuleTemplate = new UserSelectListPropertyRuleTemplate(customProperty)
							});
						selectListOrder++;
						break;

					case EkEnumeration.ObjectPropertyValueTypes.Boolean:
						RuleTemplates.Add(
							new RuleTemplateWithOrder
							{
								Order = booleanOrder,
								RuleTemplate = new UserBooleanPropertyRuleTemplate(customProperty)
							});
						booleanOrder++;
						break;

					case EkEnumeration.ObjectPropertyValueTypes.Numeric:
						RuleTemplates.Add(
							new RuleTemplateWithOrder
							{
								Order = numericOrder,
								RuleTemplate = new UserNumericPropertyRuleTemplate(customProperty)
							});
						numericOrder++;
						break;

					case EkEnumeration.ObjectPropertyValueTypes.Date:
						RuleTemplates.Add(
							new RuleTemplateWithOrder
							{
								Order = dateOrder,
								RuleTemplate = new UserDatePropertyRuleTemplate(customProperty)
							});
						dateOrder++;
						break;

					default:
						break;
				}
			}
		}
	}
}