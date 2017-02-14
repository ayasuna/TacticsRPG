using UnityEngine;
using System.Collections;
using System;

public class Temp : MonoBehaviour {

    void OnEnable()
    {
        InputController.moveEvent += OnMoveEvent;
        InputController.fireEvent += OnFireEvent;
    }

    private void OnFireEvent(object sender, InfoEventArgs<int> e)
    {
        Debug.Log("Fire" + e.info);
    }

    private void OnMoveEvent(object sender, InfoEventArgs<Point> e)
    {
        transform.localPosition = transform.localPosition + new Vector3(e.info.x, 0, e.info.y);
    }

    void OnDisable()
    {
        InputController.moveEvent -= OnMoveEvent;
        InputController.fireEvent -= OnFireEvent;
    }
}
