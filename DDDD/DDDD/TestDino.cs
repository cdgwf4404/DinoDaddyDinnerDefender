using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDD
{
    class TestDino : AnimatedSprite

    {
        public TestDino (Vector2 position) : base (position)
        {
            FramesPerSecond = 7; 
        }

        public void LoadContent(ContentManager content)
        {
            spriteTexture = content.Load<Texture2D>("dino");
            AddAnimation(22);
        }
    }
}
