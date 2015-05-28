using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	public delegate void PlanetClicked(Planet clickedPlanet);
	public static event PlanetClicked onPlanetClicked;

	private Planet left;
	private Planet right;
	private Planet upLeft;
	private Planet upRight;
	private Planet downLeft;
	private Planet downRight;

	public Queue hidingInhabitants = new Queue();
	public Queue seekingInhabitants = new Queue();

	void OnMouseDown()
	{
		onPlanetClicked (this);
	}

	public void SetLeft(Planet newLeft)
	{
		left = newLeft;
	}

	public void SetRight(Planet newRight)
	{
		right = newRight;
	}

	public void SetUpLeft(Planet newUpLeft)
	{
		upLeft = newUpLeft;
	}

	public void SetUpRight(Planet newUpRight)
	{
		upRight = newUpRight;
	}

	public void SetDownLeft(Planet newDownLeft)
	{
		downLeft = newDownLeft;
	}

	public void SetDownRight(Planet newDownRight)
	{
		downRight = newDownRight;
	}

	public void AddInhabitant(GameObject inhabitant)
	{
		Vector3 intendedLocalPosition = inhabitant.transform.localPosition;
		inhabitant.transform.parent = this.gameObject.transform;
		inhabitant.transform.localPosition = intendedLocalPosition;
		if (TurnTracker.HiderTurn())
		{
			hidingInhabitants.Enqueue(inhabitant);
		}
		else
		{
			seekingInhabitants.Enqueue(inhabitant);
		}
	}

	public bool InhabitantAvailable()
	{
		if (TurnTracker.HiderTurn())
		{
			return hidingInhabitants.Count > 0;
		}
		else
		{
			return seekingInhabitants.Count > 0;
		}
	}

	public GameObject PopInhabitant()
	{
		if (TurnTracker.HiderTurn())
		{
			return hidingInhabitants.Dequeue() as GameObject;
		}
		else
		{
			return seekingInhabitants.Dequeue() as GameObject;
		}
	}
}