namespace Breakout;
public static class Points{ 
    public static int tally = 0;
    /// <summary> Resets the score </summary>
    /// <param> Null </param>
    /// <returns> void </returns>
    public static void ResetTally(){
        tally = 0;
    }
    /// <summary> Gets the score </summary>
    /// <param> Null </param>
    /// <returns> int </returns>
    public static int GetTally(){
        return tally;
    }
    /// <summary> Increases the score </summary>
    /// <param> Null </param>
    /// <returns> void</returns>
    public static void IncreaseTally(){
        tally++;
    }
}