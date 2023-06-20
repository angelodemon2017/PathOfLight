using Assets.Scripts.Data;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    public static int Power(this List<EntityOfBoard> eobs, Element element) 
    {
        return eobs.Where(x => x.MyElement == element).Sum(x => x.Level);
    }
}