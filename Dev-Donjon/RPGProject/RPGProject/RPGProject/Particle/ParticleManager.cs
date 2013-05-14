using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RPGProject
{
	public class ParticleManager : GameComponent
	{
		Dictionary<string, ParticleSystem> particleList;
		HashSet<string> activeParticle;

		public ParticleManager(Game game)
			: base(game)
		{
			particleList = new Dictionary<string, ParticleSystem>();
			activeParticle = new HashSet<string>();
		}

		public bool AddParticle(string name, Vector3 position, Vector3 velocity)
		{
			ParticleSystem part;
			if (particleList.TryGetValue(name, out part))
			{
				part.AddParticle(position, velocity);
				return true;
			}
			return false;
		}

		public bool Activate(string name)
		{
			ParticleSystem part;
			if (particleList.TryGetValue(name, out part))
			{
				activeParticle.Add(name);
				Game.Components.Add(part);
				return true;
			}
			return false;
		}

		public bool Desactivate(string name)
		{
			ParticleSystem part;
			if (particleList.TryGetValue(name, out part))
			{
				activeParticle.Remove(name);
				Game.Components.Remove(part);
				return true;
			}
			return false;
		}

		public override void Update(GameTime gameTime)
		{
			ParticleSystem part;
			foreach (string s in activeParticle)
			{
				if (particleList.TryGetValue(s, out part))
				{
					part.Update(gameTime);
				}
			}
		}

		public void SetCamera(Matrix view, Matrix projection)
		{
			ParticleSystem part;
			foreach (string s in activeParticle)
			{
				if (particleList.TryGetValue(s, out part))
				{
					part.SetCamera(view, projection);
				}
			}
		}

		#region DictionnaryWrapper

		public void Add(ParticleSystem particle)
		{
			if (particleList.ContainsKey(particle.Name))
				throw new Exception("Key collision");
			particleList.Add(particle.Name, particle);
		}

		public bool TryGetValue(string name, out ParticleSystem value)
		{
			return particleList.TryGetValue(name, out value);
		}

		public bool Remove(string name)
		{
			return particleList.Remove(name);
		}

		#endregion
	}
}
