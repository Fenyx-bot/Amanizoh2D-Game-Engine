using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Sorting_Algorithm.Sorting_Types
{
    public class Shape2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Tag = "";
        public Color Color = Color.White;
        public Color OutLineColor = Color.White;
        public int value;
        public Shape2D(Vector2 position, Vector2 scale, string tag, Color color, Color outLineColor, int value)
        {
            this.Position = position;
            this.Scale = scale;
            this.Tag = tag;
            this.Color = color;
            this.OutLineColor = outLineColor;
            this.value = value;

            Log.Info($"[SHAPE2D]({Tag}) - Has been registered!");
            Amanizoh2D.RegisterShape(this);
        }

        public void DestroySelf()
        {
            Log.Info($"[SHAPE2D]({Tag}) - Has been Destoryed!");
            Amanizoh2D.UnRegisterShape(this);
        }
    }
}
