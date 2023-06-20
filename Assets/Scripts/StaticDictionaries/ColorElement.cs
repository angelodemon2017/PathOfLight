using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StaticDictionaries
{
    public static class ColorElement
    {
        public static Dictionary<Element, Color> ColorByElement = new Dictionary<Element, Color>()
        {
            { Element.none, Color.gray },
            { Element.Fire, Color.red },
            { Element.Grass, Color.green },
            { Element.Water, Color.blue },
            { Element.Light, Color.white },
        };
    }
}
