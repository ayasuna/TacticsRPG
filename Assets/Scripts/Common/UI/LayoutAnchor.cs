﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*provide an easy way to move a RectTransform in relationship to its parent RectTransform, 
 * as easily as I am able to via the inspector.*/

[RequireComponent(typeof(RectTransform))]
public class LayoutAnchor : MonoBehaviour
{
    RectTransform myRT;
    RectTransform parentRT;
    // Use this for initialization
    void Awake()
    {
        myRT = transform as RectTransform;
        parentRT = transform.parent as RectTransform;
        if (parentRT == null)
        {
            Debug.Log("This component requires a RectTransform parent to work.", gameObject);
        }	
	}

    //When positioning, need to know the general offsets to use based on the location of the anchor we want and the size of the RectTransform’s rect

    Vector2 GetPosition(RectTransform rt, TextAnchor anchor)
    {
        Vector2 retValue = Vector2.zero;

        switch (anchor)
        {
            case TextAnchor.LowerCenter:
            case TextAnchor.MiddleCenter:
            case TextAnchor.UpperCenter:
                retValue.x += rt.rect.width * 0.5f;
                break;
            case TextAnchor.LowerRight:
            case TextAnchor.MiddleRight:
            case TextAnchor.UpperRight:
                retValue.x += rt.rect.width;
                break;
        }

        switch (anchor)
        {
            case TextAnchor.MiddleLeft:
            case TextAnchor.MiddleCenter:
            case TextAnchor.MiddleRight:
                retValue.y += rt.rect.height * 0.5f;
                break;
            case TextAnchor.UpperLeft:
            case TextAnchor.UpperCenter:
            case TextAnchor.UpperRight:
                retValue.y += rt.rect.height;
                break;
        }

        return retValue;
    }

    //find the value you would use to make a RectTransform appear in the correct place based on the anchor points you specify.

    public Vector2 AnchorPosition(TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
    {
        Vector2 myOffset = GetPosition(myRT, myAnchor);
        Vector2 parentOffset = GetPosition(parentRT, parentAnchor);
        Vector2 anchorCenter = new Vector2(Mathf.Lerp(myRT.anchorMin.x, myRT.anchorMax.x, myRT.pivot.x), Mathf.Lerp(myRT.anchorMin.y, myRT.anchorMax.y, myRT.pivot.y));
        Vector2 myAnchorOffset = new Vector2(parentRT.rect.width * anchorCenter.x, parentRT.rect.height * anchorCenter.y);
        Vector2 myPivotOffset = new Vector2(myRT.rect.width * myRT.pivot.x, myRT.rect.height * myRT.pivot.y);
        Vector2 pos = parentOffset - myAnchorOffset - myOffset + myPivotOffset + offset;
        pos.x = Mathf.RoundToInt(pos.x);
        pos.y = Mathf.RoundToInt(pos.y);
        return pos;
    }

    //Convenience method to determine where to place our RectTransform, 

    public void SnapToAnchorPosition(TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
    {
        myRT.anchoredPosition = AnchorPosition(myAnchor, parentAnchor, offset);
    }

    //animate moving the RectTransform into position.

    public Tweener MoveToAnchorPosition(TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset)
    {
        return myRT.AnchorTo(AnchorPosition(myAnchor, parentAnchor, offset));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
