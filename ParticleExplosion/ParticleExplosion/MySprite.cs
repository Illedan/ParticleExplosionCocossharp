using System;
using CocosSharp;

namespace ParticleExplosion
{
	public class MySprite : CCSprite
	{
		private static CCTexture2D _texture = new CCTexture2D("Bird.png");
		public MySprite () : base(_texture)
		{
			Scale = 0.3f;
			ScaleX *= -1.0f;
		}
	}
}

