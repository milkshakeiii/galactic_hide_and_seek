using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour 
{
	private bool isSelected;
	public UnityEngine.UI.Text cardTextDisplay;

	void Start()
	{
		cardTextDisplay.text = CardText ();
	}

	void OnMouseDown()
	{
		SetSelected (!IsSelected ());
	}

	public bool IsSelected()
	{
		return isSelected;
	}

	public void SetSelected(bool newSelected)
	{
		isSelected = newSelected;

		UpdateHighlight ();
	}

	private void UpdateHighlight()
	{
		if (IsSelected())
			gameObject.GetComponent<SpriteRenderer> ().color = Color.green;
		else
			gameObject.GetComponent<SpriteRenderer> ().color = Color.white;
	}

	public virtual string CardText()
	{
		return "This card needs text!";
	}
}