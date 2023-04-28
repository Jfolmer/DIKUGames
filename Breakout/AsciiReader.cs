using System;
using System.Collections.Generic;
using System.IO;

namespace Breakout.Loader{
    public class AsciiReader{

        private List<string> Map;

        private List<string> Metadata {get;}

        private Dictionary<char, string> Legend {get;}

        public AsciiReader(){
            Map = new List<string>();
            Metadata = new List<string>();
            Legend = new Dictionary<char, string>();
        }
        public List<string> GetMap(){
            return Map;
        }

        public List<string> GetMeta(){
            return Metadata;
        }

        public Dictionary<char, string> GetLegend(){
            return Legend;
        }

        public void Read(string filename){

            string[] file = File.ReadAllLines(filename);
            
            for (int i = 0; i < file.Length; i++){

                if (file[i].StartsWith("Map:")){
                    int j = i + 1;
                    while (!file[j].StartsWith("Map/")){
                        Map.Add(file[j]);
                        j++;
                    }
                }

                if (file[i].StartsWith("Meta:")){
                    int j = i + 1;
                    while (!file[j].StartsWith("Meta/")){
                        Metadata.Add(file[j]);
                        j++;
                    }
                }

                if (file[i].StartsWith("Legend:")){
                    int j = i + 1;
                    while (!file[j].StartsWith("Legend/")){
                        file[j] = file[j].Replace(")","");
                        file[j] = file[j].Replace(" ","");
                        char fst = file[j][0];
                        string indmad = "";
                        for (int a = 1; a < file[j].Length; a++){
                            indmad = indmad + file[j][a];
                        }
                        Legend.Add(fst,indmad);
                        j++;
                    }
                }
            }
        }

    }
}