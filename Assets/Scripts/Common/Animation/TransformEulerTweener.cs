using UnityEngine;
using System.Collections;

public class TransformEulerTweener : Vector3Tweener
{
    protected override void OnUpdate(object sender, System.EventArgs e)
    {
        base.OnUpdate(sender, e);
        transform.eulerAngles = currentValue;
    }
}
