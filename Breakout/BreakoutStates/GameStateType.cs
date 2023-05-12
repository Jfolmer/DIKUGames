using System;

namespace BreakoutStates{
    public enum GameStateType{
        GameRunning,
        MainMenu,
        GamePaused,
        LevelSelector
    }

    public static class StateTransformer{
        public static GameStateType TransformStringToState(string state){
            return state switch{
                "GAME_RUNNING" => GameStateType.GameRunning,
                "MAIN_MENU" => GameStateType.MainMenu,
                "GAME_PAUSED" => GameStateType.GamePaused,
                "LEVEL_SELECTOR" => GameStateType.LevelSelector,
                _ => throw new ArgumentException("INVALID"),
            };
        }
        public static string TransformStateToString(GameStateType state){
            return state switch{
                GameStateType.GameRunning => "GAME_RUNNING",
                GameStateType.MainMenu => "MAIN_MENU",
                GameStateType.GamePaused => "GAME_PAUSED",
                GameStateType.LevelSelector => "LEVEL_SELECTOR",
                _ => throw new ArgumentException("INVALID"),
            };
        }
    }
}