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
    public class Label2D
    {
        public string text = "";
        public Font font = null;
        public uint FontSize = 1;
        public Vector2 Position = null;
        public string Tag = "";
        public bool centered;
        public Color color = Color.White;
 

        public Label2D(string TEXT, Font FONT, uint FONTSIZE, Vector2 POSITION, Color COLOR,string TAG, bool CENTERED)
        {
            this.text = TEXT;
            this.FontSize = FONTSIZE;
            this.font = FONT;
            this.Position = POSITION;
            this.Tag = TAG;
            this.centered = CENTERED;
            this.color = COLOR;

            Log.Info($"[LABEL2D]({Tag}) - Has been registered!");
            Amanizoh2D.RegisterLabel(this);
        }

        public void DestroySelf()
        {
            Log.Info($"[LABEL2D]({Tag}) - Has been Destoryed!");
            Amanizoh2D.UnRegisterLabel(this);
        }
    }
}
