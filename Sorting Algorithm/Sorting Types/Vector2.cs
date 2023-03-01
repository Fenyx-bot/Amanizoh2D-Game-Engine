using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting_Algorithm.Sorting_Types
{
    public class Vector2
    {
        public float x { get; set; }
        public float y { get; set; }

        public Vector2()
        {
            x = Zero().x;
            y = Zero().y;
        }

        public Vector2(float X, float Y)
        {
            this.x = X;
            this.y = Y;
        }

        public static Vector2 Zero()
        {
            return new Vector2(0, 0);
        }
    }
}
