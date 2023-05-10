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

        public EntityContainer<BaseBlock> ReadLevel(List<string> map, Dictionary<string, string> metadata, Dictionary<char, string> Legend){
            EntityContainer<BaseBlock> output = new EntityContainer<BaseBlock>();
            for (int i = 0; i < map.Count; i++){
                for (int j = 0; j < map[i].Length; j++){
                    if(map[i][j] != '-'){
                        float x = (((float)j)/((float)map[i].Length));
                        float y = 0.25f + 0.75f*(1.0f - ((float)i)/(float)map.Count);
                        Vec2F pos = new Vec2F(x,y);
                        Vec2F ext = new Vec2F((0.085f),(0.031f));
                        string imgPath = Legend[map[i][j]];
                        IBaseImage img = new Image(Path.Combine("Assets", "Images", imgPath));
                        if (!metadata.ContainsKey(char.ToString(map[i][j]))){
                            output.AddEntity(new BaseBlock(new DynamicShape(pos,ext),img));
                        } else switch (metadata[char.ToString(map[i][j])]){
                            case "Hardened:":
                                HardenedBlock TomHardy = new HardenedBlock(new DynamicShape(pos,ext),img);
                                TomHardy.ImagePath = Path.Combine("Assets", "Images", imgPath);
                                output.AddEntity(TomHardy);
                                break;
                            case "Unbreakable:":
                                output.AddEntity(new UnbreakableBlock(new DynamicShape(pos,ext),img));
                                break;
                            default:
                                output.AddEntity(new BaseBlock(new DynamicShape(pos,ext),img));
                                break;
                        }
                    }
                }
            }
            return output;
        }
    }
}