using System;

namespace BreakoutStates;

    public enum GameStateType {
        GameRunning,
    }
        public class StateTransformer{
            public static GameStateType TransformStringToState(string state){
                switch (state){
                    case "GAME_RUNNING": return GameStateType.GameRunning;

                    default: throw new ArgumentException("INVALID_GAMESTATE");
                }
            }
            public static string TransformStateToString(GameStateType state){
                switch (state){
                    case GameStateType.GameRunning: return "GAME_RUNNING";
                    
                    default: throw new ArgumentException("INVALID_GAMESTATE");
                }
            }
        }
    