using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace RPGProject
{
    public enum Sens { None, Gauche, Droite ,Haut,Bas,Rothaut,Rotbas};

	public class Camera
	{
        private float amplificateur = 1; // pour ajutster la vitesse de relévement / abaissement de la tête pour compenser la distance
        private float rayonview = 50;
        public float vit = 1;
        public float angle = 0;
        public float hview = 0;
        public float sensibilite = 5;
		public Vector3 position = new Vector3(0, 50.0f, 500.0f);

		public Vector3 lookat = new Vector3(0, 0, 0); // point vue

		public Vector3 rotation = new Vector3(0, 0, 0);

		public void modposition(Vector3 modi)
		{
			position += modi;

		}

		public void actucamera(List<Sens> ls)
		{
			foreach (Sens sens in ls)
			{
				switch (sens)
				{
					case Sens.Droite: makeviewrot(sens);
						break;
					case Sens.Gauche: makeviewrot(sens);
						break;
					case Sens.Haut: makestep(sens);
						break;
					case Sens.Bas: makestep(sens);
						break;
					case Sens.Rothaut: makeviewbanging(sens);
						break;
					case Sens.Rotbas: makeviewbanging(sens);
						break;

				}
			}
		}

		public Matrix getview()
		{
			return Matrix.CreateLookAt(position, lookat, Vector3.Up);
		}

		public Matrix GetProjection()
		{
			return Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
				Tools.Quick.graphics.GraphicsDevice.Viewport.Width /
				(float)Tools.Quick.graphics.GraphicsDevice.Viewport.Height, 1.0f, 10000.0f);
		}

		public void makestep(Sens sens)
		{

         //   double dirX = rotation.X / (rayonview + position.X);
          //  double dirZ = rotation.Z / (rayonview + position.Z);
            double dirX = Math.Cos(MathHelper.ToRadians(angle));
            double dirZ = Math.Sin(MathHelper.ToRadians(angle));
            if (sens == Sens.Haut)
            {
                position.X = position.X + (float)(dirX * vit);
                position.Z = position.Z + (float)(dirZ * vit);

            }
            else
            {
                position.X = position.X - (float)(dirX * vit);
                position.Z = position.Z - (float)(dirZ * vit);

            }
            actuLookat();
		}

		public void modiposition(Vector3 vec, bool tp)
		{
			if (tp)
			{
				position = vec;
			}
			else
			{
				position += vec;
			}
            actuLookat();
		}

		public void makeviewbanging(Sens sens)
		{
			if (sens == Sens.Rotbas)
			{
                hview -= sensibilite * amplificateur;
                lookat = lookat + new Vector3(0, -sensibilite*amplificateur, 0);
			}
			else
			{
                hview += sensibilite * amplificateur;
                lookat = lookat + new Vector3(0, sensibilite * amplificateur, 0);
			}
		}

		public void makeviewrot(Sens sens)
		{
            float vec = (float)sensibilite;
			if (sens == Sens.Gauche)
			{
				vec = (-vec);
			}
			if (angle <= 0 && vec < 0)
			{
				angle = 360;
			}
			else if (angle == 360 && vec > 0)
			{
				angle = 0;
			}
			else { angle += vec; }

            //rotation.X = (float)((position.X + rayonview) * Math.Cos(MathHelper.ToRadians(angle)));
            //rotation.Z = (float)((position.Z + rayonview) * Math.Sin(MathHelper.ToRadians(angle)));
            rotation.X = (float)((position.X) + rayonview * Math.Cos(MathHelper.ToRadians(angle)));
            rotation.Z = (float)((position.Z) + rayonview * Math.Sin(MathHelper.ToRadians(angle)));
          
            rotation.Y = position.Y + hview;
			lookat = rotation;
			// Console.WriteLine(angle);
			//  Console.WriteLine(lookat.X + "/" + lookat.Z);
			#region comment
			/*
                        if ((rotation.X/1000) < 1 & (rotation.X/1000) > 0 & (rotation.Z/1000) <= 1 & (rotation.Z/1000) > 0)
                        {
                            rotation.X = rotation.X +100 * vec;
                            rotation.Z = rotation.Z - 100 * vec;

                            return;
                        }

                        if ((rotation.X/1000) <= 1 & (rotation.X/1000) > 0 & (rotation.Z/1000) >= -1 & (rotation.Z/1000) <= 0)
                        {
                            rotation.X = rotation.X - 100 * vec;
                            rotation.Z = rotation.Z - 100 * vec;
                            return;
                        }

                        if ((rotation.X/1000) <= 0 & (rotation.X/1000) >= -1 & (rotation.Z/1000) >= -1 & (rotation.Z/1000) <= 0)
                        {
                            rotation.X = rotation.X - 100 * vec;
                            rotation.Z = rotation.Z + 100 * vec;
                            return;
                        }

                        if ((rotation.X/1000) <= 0 & (rotation.X/1000) >= -1 & (rotation.Z/1000) >= 0 & (rotation.Z/1000) <= 1)
                        {
                            rotation.X = rotation.X + 100 * vec;
                            rotation.Z = rotation.Z + 100 * vec;
                            return;
                        }
                        lookat = position + rotation;
                        Console.WriteLine(rotation.X + "/" + rotation.Y + "/" + rotation.Z);
            
                    }
             */
			#endregion
		}

        public void actuLookat()
        {
            rotation.X = (float)((position.X) + rayonview * Math.Cos(MathHelper.ToRadians(angle)));
            rotation.Z = (float)((position.Z) + rayonview * Math.Sin(MathHelper.ToRadians(angle)));
            rotation.Y = position.Y + hview;
            lookat = rotation;
        }
	}
}
