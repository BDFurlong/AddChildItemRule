using System;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Events;
using Sitecore.Rules;

namespace AddChildItemRule.Sc.Rules.Handlers.ItemAdded
{
    public class ItemAddedRulesHandler
    {
        public void OnItemAdded(object sender, EventArgs args)
        {
            var addedItem = ExtractItem(args);

            if (addedItem == null)
            {
                return;
            }

            var rulesFolderId = ID.Parse(ItemAddedRulesConstants.ItemAddedRules.ItemId);
            var itemAddedRules = addedItem.Database.GetItem(rulesFolderId);

            if (itemAddedRules == null)
            {
                return;
            }

            var ruleContext = new RuleContext();
            ruleContext.Item = addedItem;

            RuleList<RuleContext> rules = RuleFactory.GetRules<RuleContext>(itemAddedRules, "Rule");

            if (rules.Count > 0)
            {
                rules.Run(ruleContext);
            }
        }

        private Item ExtractItem(EventArgs args)
        {
            Assert.IsNotNull(args, "Added Item is Null");
            var item = Event.ExtractParameter(args, 0) as Item;

            return item;
        }
    }
}
