using Godot;
using System;

namespace ItemToss
{
    public partial class Utils
    {
        // Maps a float that is between [fromMin, fromMax] to the range [toMin, toMax].
        // For example, MapFloat(10,20, 0,10, 15) => 5
        public static float MapFloat(
            float fromMin,
            float fromMax,
            float toMin,
            float toMax,
            float fromValue
        )
        {
            float percent = (fromValue - fromMin) / (fromMax - fromMin);
            return toMin + (toMax - toMin) * percent;
        }
    }
}
