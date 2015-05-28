using UnityEngine;
using System.Collections;

public class PlaceCard : Card 
{
	public GameObject seekerPiecePrefab;
	public GameObject hiderPiecePrefab;

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
			GameObject placedPiece;
			if (TurnTracker.HiderTurn())
				placedPiece = Instantiate(hiderPiecePrefab) as GameObject;
			else
				placedPiece = Instantiate(seekerPiecePrefab) as GameObject;
			clickedPlanet.AddInhabitant(placedPiece);
			SetSelected (false);
			TurnTracker.ActionTaken ();
		}
	}
	
	public override string CardText ()
	{
		return "Place";
	}
}
