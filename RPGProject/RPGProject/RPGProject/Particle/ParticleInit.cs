using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace RPGProject
{
    class ParticleInit
    {
        Random rnd = new Random();
        public ParticleInit(ParticleManager pm , Game game, ContentManager content)
        {

            ParticleSettings settings = new ParticleSettings();

            settings.ParticleName = "Soins";

            settings.TextureName = "Star";

            settings.MaxParticles = 2400;

            settings.Duration = TimeSpan.FromSeconds(2);

            settings.DurationRandomness = 0.51f;

            settings.MinHorizontalVelocity = 1;
            settings.MaxHorizontalVelocity = 1;

            settings.MinVerticalVelocity = -0;
            settings.MaxVerticalVelocity = -2.5f;

            // Set gravity upside down, so the flames will 'fall' upward.
            settings.Gravity = new Vector3(0, 1, 0);

            settings.MinColor = new Color(200, 200, 0, 255);
            settings.MaxColor = new Color(255, 250, 250, 0);

            settings.MinStartSize = 0.5f;
            settings.MaxStartSize = 1f;

            settings.MinEndSize = 0.01f;
            settings.MaxEndSize = 0.01f;

            // Use additive blending.
            settings.BlendState = BlendState.AlphaBlend;
            CustomParticle cp = new CustomParticle(settings, game, content);
            cp.Initialize();
            pm.Add(cp);


            settings = new ParticleSettings();

            settings.ParticleName = "Freeze";

            settings.TextureName = "Snow";

            settings.MaxParticles = 4800;

            settings.Duration = TimeSpan.FromSeconds(4);

            settings.DurationRandomness = 0.51f;

            settings.MinHorizontalVelocity = -1;
            settings.MaxHorizontalVelocity = 1;

            settings.MinVerticalVelocity = -2.5f;
            settings.MaxVerticalVelocity = 2.5f;

            // Set gravity upside down, so the flames will 'fall' upward.
            settings.Gravity = new Vector3(0, 0, 0);

            settings.MinColor = new Color(200, 230, 200, 0);
            settings.MaxColor = new Color(255, 255, 255, 255);

            settings.MinStartSize = 0.01f;
            settings.MaxStartSize = 0.01f;

            settings.MinEndSize = 0.1f;
            settings.MaxEndSize = 0.1f;

            // Use additive blending.
            settings.BlendState = BlendState.AlphaBlend;
             cp = new CustomParticle(settings, game, content);
            cp.Initialize();
            pm.Add(cp);



            settings = new ParticleSettings();

            settings.ParticleName = "Snow";

            settings.TextureName = "Snow";

            settings.MaxParticles = 16000;

            settings.Duration = TimeSpan.FromSeconds(5);

            settings.DurationRandomness = 0f;

            settings.MinHorizontalVelocity = 0;
            settings.MaxHorizontalVelocity = 0f;

            settings.MinVerticalVelocity = 0;
            settings.MaxVerticalVelocity = 00f;
          
            // Set gravity upside down, so the flames will 'fall' upward.
            settings.Gravity = new Vector3(rnd.Next(-10, 10), -40, rnd.Next(-10, 10));

            settings.MinColor = new Color(200, 230, 200, 0);
            settings.MaxColor = new Color(255, 255, 255, 0);

            settings.MinStartSize = 0.25f;
            settings.MaxStartSize = 0.25f;

            settings.MinEndSize = 0.25f;
            settings.MaxEndSize = 0.25f;

            // Use additive blending.
            settings.BlendState = BlendState.AlphaBlend;
            cp = new CustomParticle(settings, game, content);
            cp.Initialize();
            pm.Add(cp);

            settings = new ParticleSettings();

            settings.ParticleName = "Rain";

            settings.TextureName = "Water001";

            settings.MaxParticles = 16000;

            settings.Duration = TimeSpan.FromSeconds(5);

            settings.DurationRandomness = 0f;

            settings.MinHorizontalVelocity = 0;
            settings.MaxHorizontalVelocity = 0f;

            settings.MinVerticalVelocity = 0;
            settings.MaxVerticalVelocity = 00f;

            // Set gravity upside down, so the flames will 'fall' upward.
            settings.Gravity = new Vector3(0, -40, 0);

            settings.MinColor = new Color(200, 230, 200, 0);
            settings.MaxColor = new Color(255, 255, 255, 0);

            settings.MinStartSize = 0.25f;
            settings.MaxStartSize = 0.25f;

            settings.MinEndSize = 0.25f;
            settings.MaxEndSize = 0.25f;

            // Use additive blending.
            settings.BlendState = BlendState.AlphaBlend;
            cp = new CustomParticle(settings, game, content);
            cp.Initialize();
            pm.Add(cp);


        }




    }
}
