using System;
using System.Collections;
using System.Collections.Generic;

namespace ParticleExplosion
{
	public class MySpriteFactory
	{
		private IList<MySpriteContainer> _freeSprites{ get; }

		public MySpriteFactory ()
		{
			AliveSprites = new List<MySpriteContainer> ();
			_freeSprites = new List<MySpriteContainer> ();
		}

		public MySpriteContainer Get(){
			if (_freeSprites.Count == 0) {
				_freeSprites.Add (new MySpriteContainer (this));
			}

			var instance = _freeSprites [0];
			_freeSprites.RemoveAt (0);
			AliveSprites.Add (instance);

			instance.Reset ();
			return instance;
		}

		public void Release(MySpriteContainer sprite){
			AliveSprites.Remove (sprite);
			_freeSprites.Add (sprite);
		}

		public IList<MySpriteContainer> AliveSprites{ get; }
	}
}

