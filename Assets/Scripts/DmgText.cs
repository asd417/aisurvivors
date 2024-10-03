using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DmgText : MonoBehaviour
{
    public int life = 500;
    TextMeshProUGUI textComponent;

    public void SetDmgPos(int DmgVal, Vector3 worldPos)
    {
        Camera currentCamera = Camera.main;
        Vector3 screenPos = currentCamera.WorldToScreenPoint(worldPos);
        textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.text = DmgVal.ToString();
        textComponent.rectTransform.anchoredPosition = new Vector2(screenPos.x, screenPos.y);
        //Debug.Break();
    }

    // Update is called once per frame
    void Update()
    {
        life -= 1;
        textComponent.rectTransform.anchoredPosition = textComponent.rectTransform.anchoredPosition + new Vector2(0, 1);
        if (life < 0)
        {
            Destroy(gameObject);
        }
    }


}
