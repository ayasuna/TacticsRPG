using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Allow data to display properly in inspector*/

[System.Serializable]

/*Point struct to hold coordinate data
 *For purposes of grid, we assume x,y
 *i.e. top-down view
 */

public struct Point
{
    public int x;
    public int y;

    //Constructor

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }



    /*Operator overloading for addition, subtraction and equality*/

    public static Point operator +(Point a, Point b)
    {
        return new Point(a.x + b.x, a.y + b.y);
    }

    public static Point operator -(Point a, Point b)
    {
        return new Point(a.x - b.x, a.y - b.y);
    }

    public static bool operator ==(Point a, Point b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(Point a, Point b)
    {
        return !(a == b);
    }

    /*Implemented == and != override, o have to override Equals and GetHashCode
     *Avoids collisions
     */

    public override bool Equals(object obj)
    {
        if (obj is Point)
        {
            Point p = (Point)obj;
            return x == p.x && y == p.y;
        }
        return false;
    }

    public bool Equals(Point p)
    {
        return x == p.x && y == p.y;
    }

    public override int GetHashCode()
    {
        return x ^ y;
    }

    /*ToString override for printing*/

    public override string ToString()
    {
        return string.Format("({0},{1})", x, y);
    }
}
