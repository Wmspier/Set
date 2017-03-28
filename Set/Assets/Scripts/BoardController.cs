using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    private GameObject[][] m_grid;

    public int DefaultWidth;
    public int DefaultHeight;
    public float CardMargin;

    public GameObject CardPrefab;


	void Start () 
    {
        GenerateGrid();
    
    }
	void Update () { }


    void GenerateGrid()
    {
        RectTransform thisTransform = gameObject.GetComponent<RectTransform>();

        for (int i = 0; i < DefaultWidth; i++)
        {
            for (int j = 0; j < DefaultHeight; j++)
            {
                GameObject newCard = Instantiate(CardPrefab);
                RectTransform newCardTransform = newCard.GetComponent<RectTransform>();
                newCardTransform.SetParent(thisTransform, false);
                //Insert card rect into parent rect           //Start from left anchor  //Add a margin // Set the width of the card 
                newCardTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, CardMargin/2, thisTransform.rect.width / DefaultWidth - CardMargin);
                newCardTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, CardMargin/2, thisTransform.rect.height / DefaultHeight - CardMargin);
                //Set the position of the card rect relative to this rect
                newCardTransform.position = new Vector2(newCardTransform.position.x + (thisTransform.rect.width / DefaultWidth * i * newCardTransform.lossyScale.x),
                                                        newCardTransform.position.y + (thisTransform.rect.height / DefaultHeight * j * newCardTransform.lossyScale.y));
                newCard.name = "Card_" + (i*DefaultHeight + j);
            }
        }
    }
}
