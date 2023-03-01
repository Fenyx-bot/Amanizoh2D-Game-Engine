using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Sorting_Algorithm.Sorting_Types
{
    public abstract class Amanizoh2D
    {
        public uint height = 500;
        public uint width = 500;
        public string title = "Amanizoh 2D";
        public Color windowColor = new Color(0, 192, 255);
        public RenderWindow app;

        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Label2D> AllLabels = new List<Label2D>();

        private void OnClose(object sender, EventArgs e)
        {
            //Close the window when OnClose event is received
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        public Amanizoh2D(uint WIDTH, uint HEIGHT, string TITLE, Color WINDOWCOLOR)
        {
            this.height = HEIGHT;
            this.width = WIDTH;
            this.title = "Amanizoh 2D";
            this.windowColor = WINDOWCOLOR;
            app = new RenderWindow(new VideoMode(width, height), title, style: Styles.Close);
            app.KeyPressed += App_KeyPressed;
            app.KeyReleased += App_KeyReleased;
            app.Closed += new EventHandler(OnClose);
            //Start the game loop
            GameLoop();
        }

        private void App_KeyReleased(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        private void App_KeyPressed(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        public static void RegisterShape(Shape2D shape)
        {
            AllShapes.Add(shape);
        }

        public static void UnRegisterShape(Shape2D shape)
        {
            AllShapes.Remove(shape);
        }

        public static void RegisterLabel(Label2D label)
        {
            AllLabels.Add(label);
        }

        public static void UnRegisterLabel(Label2D label)
        {
            AllLabels.Remove(label);
        }

        void GameLoop()
        {
            OnLoad();
            while (app.IsOpen)
            {
                app.DispatchEvents();
                Renderer();
                app.Display();
                OnUpdate();
                Thread.Sleep(10);
            }
        }

        public void Renderer()
        {
            app.Clear(windowColor);
            foreach(Shape2D shape in AllShapes)
            {
                RectangleShape graphics = new RectangleShape(new Vector2f(shape.Scale.x, -shape.value * 3));
                graphics.Position = new Vector2f(shape.Position.x, shape.Position.y);
                graphics.FillColor = shape.Color;
                graphics.OutlineColor = shape.OutLineColor;
                graphics.OutlineThickness = 0.5f;
                app.Draw(graphics);
            }

            foreach(Label2D label in AllLabels)
            {
                Text text = new Text(label.text, label.font, label.FontSize);
                if (label.centered)
                {
                    FloatRect textRect = text.GetLocalBounds();
                    text.Origin = new Vector2f(textRect.Left + textRect.Width / 2.0f, textRect.Top + textRect.Height / 2.0f);
                }
                text.Position = new Vector2f(label.Position.x, label.Position.y);
                text.Color = label.color;
                app.Draw(text);
            }
            
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);
    }
}
