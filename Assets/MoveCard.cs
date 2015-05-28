using UnityEngine;
using System.Collections;

public class MoveCard : Card
{
	Planet sourcePlanet;

	void OnEnable()
	{
		Planet.onPlanetClicked += OnPlanetClicked;
	}

	void OnDisable()
	{
		Planet.onPlanetClicked -= OnPlanetClicked;
	}

	private void OnPlanetClicked(Planet clickedPlanet)
	{
		if (IsSelected())
		{
			if (sourcePlanet == null)
			{
				if (clickedPlanet.InhabitantAvailable())
					sourcePlanet = clickedPlanet;
				else
					SetSelected(false);
			}
			else
			{
				clickedPlanet.AddInhabitant(sourcePlanet.PopInhabitant());
				sourcePlanet = null;
				SetSelected (false);
				TurnTracker.ActionTaken ();
			}
		}
	}

	public override string CardText ()
	{
		return "Move!";
	}
}