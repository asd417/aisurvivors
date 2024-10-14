using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player class to control different aspects of player prefab with script
    [SerializeField]
    public Color playerColor = Color.white;
    public Color highlightColor = Color.green;

    private void Start()
    {
        setHighlighted(false);
    }

    public void setHighlighted(bool highlighted)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (highlighted)
        {
            sr.color = highlightColor;
        }
        else
        {
            sr.color = playerColor;
        }
    }
}
