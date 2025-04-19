using System.Collections.Generic;
using UnityEngine;
public class InteractionResolver
{
	private List<IInteractionRule> rules = new();

	public void RegisterRule(IInteractionRule rule)
		=> rules.Add(rule);

	public void Resolve(ISelectable selected, IInteractable clicked)
	{
		foreach (var rule in rules)
		{
			if (rule.CanHandle(selected, clicked))
			{
				rule.Execute(selected, clicked);
				return;
			}
		}

		Debug.Log("No valid interaction found.");
	}
}
