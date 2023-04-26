using System;

namespace BreakoutStates;

    public enum GameStateType {
        GameRunning,
        MainMenu,
    }
        public class StateTransformer{
            public static GameStateType TransformStringToState(string state){
                switch (state){
                    case "GAME_RUNNING": return GameStateType.GameRunning;
                    case "MAIN_MENU": return GameStateType.MainMenu;

                    default: throw new ArgumentException("INVALID_GAMESTATE");
                }
            }
            public static string TransformStateToString(GameStateType state){
                switch (state){
                    case GameStateType.GameRunning: return "GAME_RUNNING";
                    case GameStateType.MainMenu: return "MAIN_MENU";
                    
                    default: throw new ArgumentException("INVALID_GAMESTATE");
                }
            }
        }
    