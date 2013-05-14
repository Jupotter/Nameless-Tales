using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RPGProject
{
	class CustomParticle:ParticleSystem
	{
		ParticleSettings customSettings;

		public CustomParticle(ParticleSettings settings, Game game, ContentManager content)
            : base(game, content)
        {
			customSettings = settings;
		}

		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.ParticleName = customSettings.ParticleName;

			settings.TextureName = customSettings.TextureName;

			settings.MaxParticles = customSettings.MaxParticles;

			settings.Duration = customSettings.Duration;

			settings.DurationRandomness = customSettings.DurationRandomness;

			settings.MinHorizontalVelocity = customSettings.MinHorizontalVelocity;
			settings.MaxHorizontalVelocity = customSettings.MaxHorizontalVelocity;

			settings.MinVerticalVelocity = customSettings.MinVerticalVelocity;
			settings.MaxVerticalVelocity = customSettings.MaxVerticalVelocity;

			settings.Gravity = customSettings.Gravity;

			settings.MinColor = customSettings.MinColor;
			settings.MaxColor = customSettings.MaxColor;

			settings.MinStartSize = customSettings.MinStartSize;
			settings.MaxStartSize = customSettings.MaxStartSize;

			settings.MinEndSize = customSettings.MinEndSize;
			settings.MaxEndSize = customSettings.MaxEndSize;

			settings.BlendState = customSettings.BlendState;
		}
	}
}
