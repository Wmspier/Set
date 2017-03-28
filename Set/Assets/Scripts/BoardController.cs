using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

    private GameObject[,] m_grid;
    private int m_currentWidth;
    private int m_currentHeight;

    public int DefaultWidth;
    public int DefaultHeight;
    public float CardMargin;

    public GameObject CardPrefab;


	void Start () { }
	void Update () { }

    //Generates gird based on default values (also resets the grid)
    public void GenerateGrid()
    {
        RectTransform thisTransform = gameObject.GetComponent<RectTransform>();
        //Kill all the children!
        foreach (Transform child in thisTransform) Destroy(child.gameObject);
        m_grid = new GameObject[DefaultWidth, DefaultHeight];
        m_currentWidth = DefaultWidth;
        m_currentHeight = DefaultHeight;

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
                m_grid[i,j] = newCard;
            }
        }
    }

    //Adds a new column to the grid by copying the 2d array into a fatter 2d array
    public void AddColumn()
    { 
        RectTransform thisTransform = gameObject.GetComponent<RectTransform>();
        GameObject[,] newGrid = new GameObject[++m_currentWidth, m_currentHeight];

        for (int i = 0; i < m_currentWidth; i++)
        {
            for (int j = 0; j < m_currentHeight; j++)
            {
                GameObject newCard;
                if (i == m_currentWidth - 1)
                {
                    newCard = Instantiate(CardPrefab);
                }
                else 
                {
                    newCard = m_grid[i, j];
                }
                RectTransform newCardTransform = newCard.GetComponent<RectTransform>();
                newCardTransform.SetParent(thisTransform, false);
                //Insert card rect into parent rect           //Start from left anchor  //Add a margin // Set the width of the card 
                newCardTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, CardMargin / 2, thisTransform.rect.width / m_currentWidth - CardMargin);
                newCardTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, CardMargin / 2, thisTransform.rect.height / m_currentHeight - CardMargin);
                //Set the position of the card rect relative to this rect
                newCardTransform.position = new Vector2(newCardTransform.position.x + (thisTransform.rect.width / m_currentWidth * i * newCardTransform.lossyScale.x),
                                                        newCardTransform.position.y + (thisTransform.rect.height / m_currentHeight * j * newCardTransform.lossyScale.y));
                newCard.name = "Card_" + (i * DefaultHeight + j);
                newGrid[i, j] = newCard;
            }
        }
        m_grid = newGrid;
    }
}
