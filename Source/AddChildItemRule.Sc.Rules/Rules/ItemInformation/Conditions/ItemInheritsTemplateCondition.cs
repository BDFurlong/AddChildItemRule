using AddChildItemRule.Sc.Rules.Extensions;
using Sitecore.Data;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using Sitecore.StringExtensions;

namespace AddChildItemRule.Sc.Rules.Rules.ItemInformation.Conditions
{
    public class ItemInheritsTemplateCondition<T> : WhenCondition<T> where T : RuleContext
    {
        public string TemplateId { get; set; }

        protected override bool Execute(T ruleContext)
        {
            if (TemplateId.IsNullOrEmpty())
            {
                return false;
            }

            return ruleContext.Item.IsDerived(new TemplateID(ID.Parse(TemplateId)));
        }
    }
}