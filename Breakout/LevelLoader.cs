using Breakout;
using Breakout.Blocks;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using System.IO;

namespace Breakout.Loader{

    public class LevelLoader{

        public List<Block> ReadLevel(List<string> map, List<string> metadata, Dictionary<char, string> Legend){
            List<Block> output = new List<Block>();
            for (int i = 0; i < map.Count; i++){
                for (int j = 0; j < map[i].Length; j++){
                    if(map[i][j] != '-'){
                        float x = ((float)j/(float)map[i].Length);
                        float y = ((1.0f - (float)i)/(float)map.Count);
                        Vec2F pos = new Vec2F(x,y);
                        Vec2F ext = new Vec2F((0.1f),(0.1f));
                        IBaseImage img = new Image(Path.Combine("Assets", "Images", Legend[map[i][j]]));
                        output.Add(new BaseBlock(new DynamicShape(pos,ext),img));
                    }
                }
            }
            return output;
        }
    }
}