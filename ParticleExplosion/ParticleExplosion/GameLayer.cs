using System;
using System.Collections.Generic;
using CocosSharp;

namespace ParticleExplosion
{
	public class GameLayer : CCLayerColor
	{
		private readonly MySpriteFactory _factory;
		private float _currentTime;
		private float _nextSpawnTime;

		public GameLayer () : base (CCColor4B.AliceBlue)
		{
			_currentTime = 0.0f;
			_nextSpawnTime = 0.0f;
			_factory = new MySpriteFactory ();
		}

		private void GameLoop(float dt){
			_currentTime += dt;
			if (_currentTime > _nextSpawnTime) {
				AddChild (_factory.Get ());
				_nextSpawnTime = _currentTime + ((float)Constant._random.Next (1, 10)) / 10.0f;
			}

			for (int i = _factory.AliveSprites.Count - 1; i >= 0; i--) {
				_factory.AliveSprites [i].Move ();
			}
		}

		protected override void AddedToScene ()
		{
			base.AddedToScene ();

			Schedule (GameLoop);

			// Use the bounds to layout the positioning of our drawable assets
			CCRect bounds = VisibleBoundsWorldspace;

			// Register for touch events 
			var touchListener = new CCEventListenerTouchAllAtOnce ();
			touchListener.OnTouchesBegan = OnTouchesBegan;
			AddEventListener (touchListener, this);
		}

		void OnTouchesBegan (List<CCTouch> touches, CCEvent touchEvent)
		{
			if (touches.Count > 0) {
				foreach (var sprite in _factory.AliveSprites) {
					if (!sprite.IsDead && sprite.Sprite.BoundingBoxTransformedToWorld.ContainsPoint (touches [0].Location)) {
						sprite.Explode ();
						break;
					}
				}
			}
		}
	}
}
