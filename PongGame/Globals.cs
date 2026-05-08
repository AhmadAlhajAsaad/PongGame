using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame;

public static class Globals
{
    public static float Time { get; set; }
    public static ContentManager Content { get; set; }
    public static SpriteBatch SpriteBatch { get; set; }
    public static GraphicsDevice GraphicsDevice { get; set; }
    public static System.Drawing.Point WindowSize { get; set; }
    public static Texture2D Texture { get; set; }

    public static void Update(GameTime gt)
    {
        Time = (float)gt.ElapsedGameTime.TotalSeconds;
    }
}
