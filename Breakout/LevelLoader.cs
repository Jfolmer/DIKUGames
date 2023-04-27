using Breakout;
using Breakout.Blocks;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using System.IO;

namespace Breakout.Loader{

    public class LevelLoader{

        public LevelLoader(){}

        public EntityContainer<Entity> ReadLevel(List<string> map, List<string> metadata, Dictionary<char, string> Legend){
            EntityContainer<Entity> output = new EntityContainer<Entity>();
            for (int i = 0; i < map.Count; i++){
                for (int j = 0; j < map[i].Length; j++){
                    if(map[i][j] != '-'){
                        float x = (((float)j)/((float)map[i].Length));
                        float y = 0.25f + 0.75f*(1.0f - ((float)i)/(float)map.Count);
                        Vec2F pos = new Vec2F(x,y);
                        Vec2F ext = new Vec2F((0.085f),(0.031f));
                        string imgPath = Legend[map[i][j]];
                        IBaseImage img = new Image(Path.Combine("Assets", "Images", imgPath));
                        output.AddEntity(new BaseBlock(new DynamicShape(pos,ext),img));
                    }
                }
            }
            return output;
        }
    }
}