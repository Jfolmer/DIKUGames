using DIKUArcade.Math;
using DIKUArcade.Graphics;

public class Health {
    private int hitpoints;
    private Text display;
    public Health (Vec2F position, Vec2F extent) {
        hitpoints = 3;
        display = new Text (hitpoints.ToString(), position, extent);
    }
    // Remember to explaination your choice as to what happens
    //when losing health.
    public void LoseHealth () {
        hitpoints -= 1;
    }
    public void RenderHealth () {
        display.RenderText();
    } 
}