using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Rules;
using Sitecore.Rules.Actions;
using Sitecore.StringExtensions;

namespace AddChildItemRule.Sc.Rules.Rules.ItemAdded.Actions
{
    public class AddChildItemAction<T> : RuleAction<T> where T : RuleContext
    {
        public string TemplateId { get; set; }
        public string DisplayName { get; set; }

        public override void Apply([NotNull] T ruleContext)
        {
            if (TemplateId.IsNullOrEmpty() || DisplayName.IsNullOrEmpty())
            {
                return;
            }

            var addedItem = ruleContext.Item;
            var templateId = new TemplateID(ID.Parse(TemplateId));

            if (addedItem.Children.Any(i => i.TemplateID == templateId.ID))
            {
                return;
            }

            if (addedItem.TemplateID != templateId)
            {
                addedItem.Add(DisplayName, templateId);
            }
        }
    }
}