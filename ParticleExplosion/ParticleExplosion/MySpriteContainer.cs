using System;
using CocosSharp;
using System.Threading.Tasks;

namespace ParticleExplosion
{
	public class MySpriteContainer : CCNode
	{
		private readonly MySpriteFactory _factory;
		public readonly CCSprite Sprite = new MySprite (){Visible = false};
		public float dX = -1;
		public bool IsDead = false;

		public MySpriteContainer (MySpriteFactory factory)
		{
			_factory = factory;
			AddChild (Sprite);
		}

		public void Move(){
			if (IsDead) {
				return;
			}

			PositionX += dX;
			if ( PositionX < 100 ) {
				Explode ();
			}
		}

		public void Reset(){
			IsDead = false;
			Position = new CCPoint (Constant.Width + 100, (Constant.Height / 2 + Constant._random.Next (Constant.Height / -2, Constant.Height / 2)));
			dX = -1 * Constant._random.Next (1, 10);
			Sprite.Visible = true;
		}

		public void Explode(){
			IsDead = true;
			Sprite.Visible = false;

			var explosion = new CCParticleExplosion (new CCPoint(0,0));
			explosion.TotalParticles = 10;
			AddChild (explosion);
			ScheduleOnce (dt => {
				RemoveChild (explosion);
				RemoveFromParent (false);
				_factory.Release (this);
			}, 3000);
		}
	}
}

