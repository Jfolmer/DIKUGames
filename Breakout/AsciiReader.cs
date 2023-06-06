using System;
using System.Collections.Generic;
using System.IO;

namespace Breakout.Loader{
    public class AsciiReader{

        private List<string> Map;

        private Dictionary<string,string> Metadata {get;}

        private Dictionary<char, string> Legend {get;}

        public AsciiReader(){
            Map = new List<string>();
            Metadata = new Dictionary<string,string>();
            Legend = new Dictionary<char, string>();
        }
        
        /// <summary>
        ///  Returns the MapData, that the ascii reader has read
        /// </summary>
        /// <param>
        ///  Null
        /// </param>
        /// <returns>
        ///  A list of strings
        /// </returns>
        public List<string> GetMap(){
            return Map;
        }

        /// <summary>
        ///  Returns the MetaData, that the ascii reader has read
        /// </summary>
        /// <param>
        ///  Null
        /// </param>
        /// <returns>
        ///  A disctionary of strings with strings as keys
        /// </returns>
        public Dictionary<string,string> GetMeta(){
            return Metadata;
        }

        /// <summary>
        ///  Returns the LegendData, that the ascii reader has read
        /// </summary>
        /// <param>
        ///  Null
        /// </param>
        /// <returns>
        ///  A disctionary of strings with characters as keys
        /// </returns>
        public Dictionary<char, string> GetLegend(){
            return Legend;
        }

        /// <summary>
        ///  Saves the relevant meta-, legend and mapdata from a .txt file
        /// </summary>
        /// <param>
        ///  A string being the directory path for the .txt file to be read
        /// </param>
        /// <returns>
        ///  Void
        /// </returns>
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
                        int midpoint = file[j].IndexOf(' ');
                        string key = "";
                        string stuff = "";
                        for (int a = 0; a < midpoint; a++){
                            stuff += file[j][a];
                        }
                        for (int b = midpoint + 1; b < file[j].Length; b++){
                            key += file[j][b];
                        }
                        if (stuff == "Time:"){
                            Metadata.Add(stuff,key);
                        } else {
                            Metadata.Add(key,stuff);
                        }
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