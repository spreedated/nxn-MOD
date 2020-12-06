using System;

namespace neXn.MOD
{
    public static class HelperFunctions
    {
        public static int Map(int value, int fromMin, int fromMax, int toMin, int toMax)
        {
            // NewValue = (((OldValue - OldMin) * (NewMax - NewMin)) / (OldMax - OldMin)) + NewMin
            return (int)Math.Round(((((float)value - fromMin) * (toMax - toMin)) / (fromMax - fromMin)) + toMin,0);
        }
    }
}
