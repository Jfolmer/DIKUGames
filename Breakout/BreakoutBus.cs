using DIKUArcade.Events;

namespace Breakout {
    public static class BreakoutBus {
        private static GameEventBus eventBus;
        /// <summary> Get the eventbus </summary>
        /// <param> Null </param>
        /// <returns> GameEventBus </returns>
        public static GameEventBus GetBus() {
            return BreakoutBus.eventBus ?? (BreakoutBus.eventBus = new GameEventBus());
        }
    }
}