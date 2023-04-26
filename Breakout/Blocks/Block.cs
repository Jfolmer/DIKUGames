using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout;

namespace Breakout.Blocks{

    public interface Block {
        Entity entity {get;}
        DynamicShape shape {get;}
        float Y {get;}
        float X {get;}
        int HP {get;set;}
        void Hit(){}
    }

}