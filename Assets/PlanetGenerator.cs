using UnityEngine;
using System.Collections;

public class PlanetGenerator : MonoBehaviour {

	public GameObject planetPrefab;
	public float xSpacing;
	public float ySpacing;
	public int height;
	public int width;
	
	void Start () 
	{
		GeneratePlanets ();
	}
	
	private void GeneratePlanets()
	{
		Planet[,] newPlanets = new Planet[width,height];
		for (int j = 0; j < height; j++)
		{
			for (int i = 0; i < width; i++)
			{
				GameObject newPlanetObject = Instantiate(planetPrefab) as GameObject;
				PositionPlanet(newPlanetObject, i, j);
				Planet newPlanet = newPlanetObject.GetComponent<Planet>();
				newPlanets[i,j] = newPlanet;
				if (i > 0)
				{
					newPlanet.SetLeft(newPlanets[i-1,j]);
					newPlanets[i-1,j].SetRight(newPlanet);
				}
				if (j > 0)
				{
					if (j%2==0)
					{
						newPlanet.SetUpRight(newPlanets[i, j-1]);
						newPlanets[i, j-1].SetDownLeft(newPlanet);
						if (i > 0)
						{
							newPlanet.SetUpLeft(newPlanets[i-1, j-1]);
							newPlanets[i-1, j-1].SetDownRight(newPlanet);
						}
					}
					else
					{
						if (i < width - 1)
						{
							newPlanet.SetUpRight(newPlanets[i+1, j-1]);
							newPlanets[i+1, j-1].SetDownLeft(newPlanet);
						}
						newPlanet.SetUpLeft(newPlanets[i, j-1]);
						newPlanets[i, j-1].SetDownRight(newPlanet);
					}
				}
			}
		}
	}

	private void PositionPlanet(GameObject newPlanet, int iIndex, int jIndex)
	{
		float newY = jIndex * ySpacing;
		float newX = iIndex * xSpacing;
		if (jIndex % 2 == 0)
			newX -= xSpacing / 2f;
		newPlanet.transform.SetParent (gameObject.transform);
		newPlanet.transform.localPosition = new Vector2 (newX, -newY);
	}
}
