using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{ 
    public GameObject _SymbolPrefab;

    public GameObject[] Symbols;

    public void GenerateSymbols(int[] attributes)
    {
        //  Shouldn't attributes be an int[]? 
        int num = attributes[(int)CardModel.eAttribute.NUMBER] + 1; //  UGHHHHH SINCE we randomize the count from [0-2], we need to +1 for a correct num
        Symbols = new GameObject[num];

        for(int i = 0; i < num; ++i)
        {
            GameObject newSymbolObj = GameObject.Instantiate(_SymbolPrefab);

            SetUpColor(newSymbolObj, attributes[(int)CardModel.eAttribute.COLOR]);
            SetUpFill(newSymbolObj, attributes[(int)CardModel.eAttribute.FILL]);
            SetUpShape(newSymbolObj, attributes[(int)CardModel.eAttribute.SHAPE]);

            newSymbolObj.transform.SetParent(this.transform);
            Symbols[i] = newSymbolObj;
        }
    }

    void SetUpColor(GameObject symbol, int colorValue)
    {
        Image symbolImage = symbol.GetComponent<Image>();
        if(symbolImage == null)
        {
            Debug.LogError("CardView::SetUpColor -- no image on this gameobject!");
            return;
        }

        switch((CardModel.eColor)colorValue)
        {
            case CardModel.eColor.RED:
                symbolImage.color = Color.red;
                break;
            case CardModel.eColor.BLUE:
                symbolImage.color = Color.blue;
                break;
            case CardModel.eColor.YELLOW:
                symbolImage.color = Color.yellow;
                break;
            case CardModel.eColor.GREEN:
                symbolImage.color = Color.green;
                break;
            default:
                {
                    Debug.LogError("CardView::SetUpColor -- given value is not defined!");
                }
                break;
        }
    }

    void SetUpFill(GameObject symbol, int fillValue)
    {
        Image symbolImage = symbol.GetComponent<Image>();
        if (symbolImage == null)
        {
            Debug.LogError("CardView::SetUpFill -- no image on this gameobject!");
            return;
        }

        //  todo -- change material!

        switch((CardModel.eFill)fillValue)
        {
            case CardModel.eFill.FILL:
                break;
            case CardModel.eFill.NONE:
                break;
            case CardModel.eFill.STRIPED:
                break;
            default:
                {
                    Debug.LogError("CardView::SetUpFill -- given value is not defined!");
                }
                break;
        }

    }

    void SetUpShape(GameObject symbol, int shapeValue)
    {
        Image symbolImage = symbol.GetComponent<Image>();
        if (symbolImage == null)
        {
            Debug.LogError("CardView::SetUpShape -- no image on this gameobject!");
            return;
        }

        //  todo -- change image/texture!

        switch((CardModel.eShape)shapeValue)
        {
            case CardModel.eShape.CIRCLE:
                break;
            case CardModel.eShape.DIAMOND:
                break;
            case CardModel.eShape.SQUARE:
                break;
            default:
                {
                    Debug.LogError("CardView::SetUpShape -- given value is not defined!");
                }
                break;
        }

    }
}
