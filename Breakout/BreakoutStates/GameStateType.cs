using System;

namespace BreakoutStates;

    public enum GameStateType {
        GameRunning,
        MainMenu,
        GamePaused,
        LevelSelector,
    }
        public class StateTransformer{
            public static GameStateType TransformStringToState(string state){
                switch (state){
                    case "GAME_RUNNING": return GameStateType.GameRunning;
                    case "MAIN_MENU": return GameStateType.MainMenu;
                    case "GAME_PAUSED": return GameStateType.GamePaused;
                    case "LEVEL_SELECTOR": return GameStateType.LevelSelector;

                    default: throw new ArgumentException("INVALID_GAMESTATE");
                }
            }
            public static string TransformStateToString(GameStateType state){
                switch (state){
                    case GameStateType.GameRunning: return "GAME_RUNNING";
                    case GameStateType.MainMenu: return "MAIN_MENU";
                    case GameStateType.GamePaused: return "GAME_PAUSED";
                    case GameStateType.LevelSelector: return "LEVEL_SELECTOR";
                    
                    default: throw new ArgumentException("INVALID_GAMESTATE");
                }
            }
        }
    