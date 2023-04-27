using System;
using System.Collections.Generic;
using System.IO;

namespace Breakout.Loader{
    public class AsciiReader{

        protected List<string> Map;

        protected List<string> Metadata;

        protected Dictionary<char, string> Legend;

        public AsciiReader(){
            Map = new List<string>();
            Metadata = new List<string>();
            Legend = new Dictionary<char, string>();
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
                        file[j].Replace(")","");
                        file[j].Replace(" ","");
                        char fst = file[j][0];
                        file[j].Replace(Char.ToString(fst),"");
                        string indmad = file[j];
                        Legend.Add(fst,indmad);
                        j++;
                    }
                }
            }
        }

    }
}