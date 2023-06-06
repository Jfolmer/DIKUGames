using System;

namespace BreakoutStates{
    public enum GameStateType{
        GameRunning,
        MainMenu,
        GamePaused,
        LevelSelector,
        GameOver,
        ScoreScreen
    }

    public static class StateTransformer{

        /// <summary>
        ///  Translates a relevant string into the corrosponding gamestatetype
        /// </summary>
        /// <param>
        ///  A string representing a GameStateType eg. 'GAME_RUNNING'
        /// </param>
        /// <returns>
        ///  A GameStateType eg. 'GameStateType.MainMenu'
        /// </returns>
        public static GameStateType TransformStringToState(string state){
            return state switch{
                "GAME_RUNNING" => GameStateType.GameRunning,
                "MAIN_MENU" => GameStateType.MainMenu,
                "GAME_PAUSED" => GameStateType.GamePaused,
                "LEVEL_SELECTOR" => GameStateType.LevelSelector,
                "GAME_OVER" => GameStateType.GameOver,
                "SCORE_SCREEN" => GameStateType.ScoreScreen,
                _ => throw new ArgumentException("INVALID"),
            };
        }

        /// <summary>
        ///  Translates a gamestatetype into the corrosponding string representation of said gamestatetype
        /// </summary>
        /// <param>
        ///  A GameStateType eg. 'GameStateType.MainMenu'
        /// </param>
        /// <returns>
        ///  A string representing a GameStateType eg. 'GAME_RUNNING' 
        /// </returns>
        public static string TransformStateToString(GameStateType state){
            return state switch{
                GameStateType.GameRunning => "GAME_RUNNING",
                GameStateType.MainMenu => "MAIN_MENU",
                GameStateType.GamePaused => "GAME_PAUSED",
                GameStateType.LevelSelector => "LEVEL_SELECTOR",
                GameStateType.GameOver => "GAME_OVER",
                GameStateType.ScoreScreen => "SCORE_SCREEN",
                _ => throw new ArgumentException("INVALID"),
            };
        }
    }
}