using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*Generic EventArgs class containing a single field of data*/

public class InfoEventArgs<T> : EventArgs
{

    public T info;

    public InfoEventArgs()
    {
        info = default(T);
    }

    public InfoEventArgs(T info)
    {
        this.info = info;
    }
}
