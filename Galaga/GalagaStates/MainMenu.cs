using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using System.IO;
using System;
using DIKUArcade.Math;

namespace Galaga.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
                MainMenu.instance = new MainMenu();
                MainMenu.instance.ResetState();
            }
            return MainMenu.instance;
        }
        public void ResetState(){}
        public void UpdateState() {}
        public void RenderState() {
            backGroundImage.Image = new Image(Path.Combine("Assets", "Images", "TitleImage.png"));

            backGroundImage.RenderEntity();

            for (int i = 0; i < menuButtons.Length-1; i++) {
                menuButtons[i].RenderText();
            }
        }
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (key) {
                case "KEY_UP": //text
                    break;
                case "KEY_DOWN": //text
                    break;
                case "KEY_ENTER": //text
                    break;
            }
        }
    }
}