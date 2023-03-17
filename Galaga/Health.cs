using DIKUArcade.Math;
using DIKUArcade.Graphics;

public class Health {
    private int hitpoints;
    private Text display;
    private Vec2F thisposition;
    private Vec2F thisextent;
    public Health (Vec2F position, Vec2F extent) {
        hitpoints = 3;
        display = new Text ("HP Remaining:" + hitpoints.ToString(), position, extent);
        thisextent = extent;
        thisposition = position;
    }
    
    /// <summary> Loses health, if health would be reduced below zero instead it does nothing. This was done
    ///           to avoid some graphic glitches with negative integers being displayed properly </summary>
    /// <returns> A Void </returns>
    public void LoseHealth () {
        if (hitpoints > 0){
            hitpoints -= 1;
        }
    }
    public int GetHealth(){
        return hitpoints;
    }

    public Text GetDisplay(){
        return display;
    }
    
    public void SetHealth(int input){
        hitpoints = input;
    }

    public void UpdateHP(){
        display.SetText("HP Remaining:" + hitpoints.ToString());
    }

    public void RenderHealth () {
        display.RenderText();
    } 
}