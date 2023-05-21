namespace Breakout;
public static class Points{ 
    public static int tally = 0;
    public static void ResetTally(){
        tally = 0;
    }
    public static int GetTally(){
        return tally;
    }
    public static void IncreaseTally(){
        tally++;
    }
}