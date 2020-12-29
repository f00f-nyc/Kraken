using Godot;
using kraken.src.kraken.segments;
using System;
using System.Collections.Generic;

namespace kraken.src
{
    public class AlwaysPrint { }

    public class Debug
    {
        public static Dictionary<Type, bool> OutputTurnedOnFor = new Dictionary<Type, bool>
        {
            { typeof(AlwaysPrint), true },
            { typeof(tentacle), true },
            { typeof(SegmentPosition), false }
        };

        public static void Print<T>(params object[] args)
        {
            if (Debug.OutputTurnedOnFor.TryGetValue(typeof(T), out var print) && print)
            {
                GD.Print(args);
            }
        }
    }
}
