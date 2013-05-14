using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Management;


namespace RPGProject
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
    #region variable

        public Texture2D backmenu;
        public   Menu menuactif;
        public GameConsole gameconsole = new GameConsole();
        ActionMouse mousy;
		public Camera camera = new Camera();
		DrawThings display = new DrawThings();
		BasicEffect effect;
		GraphicsDeviceManager graphics;
		public SpriteBatch spriteBatch;
		Ground actualGround;
		Matrix viewMatrix;
		Matrix projectionMatrix;
		Texture2D texture;
		public float x = 0;
		public float y = 0;
		public float z = 0;
		List<Sens> ls = new List<Sens>();
		 PhysicsGest phgest;
		Model myModel;
		// ParticleSimple ps;
        ActionKeyboard actkey = new ActionKeyboard();
		SpriteFont arial;
        Random rnd = new Random();
		PlayerInterface pi;
		Vector2 mouselastpos = Vector2.Zero;
        Menu menu;
		ParticleManager particleManager;

        public InventoryInterface inf;

        Texture2D testgivre;
        bool indoor = false;
        Dungeon d;

        Water water;
        SkyBox skybox;
        Trees trees;

        #endregion

        public Game1()
		{
			
            //Map.MAPSIZE = 513;
			//  ParticleSimple ps;
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			//camera =  new Camera(this);
           
			ls.Add(Sens.Droite);
			ls.Add(Sens.Gauche);
			camera.actucamera(ls);
			//graphics.ToggleFullScreen();
			graphics.PreferredBackBufferWidth = Tools.Quick.screen.Bounds.Width;
			graphics.PreferredBackBufferHeight = Tools.Quick.screen.Bounds.Height;
            Tools.Quick.bounds.X = graphics.PreferredBackBufferWidth;
            Tools.Quick.bounds.Y = graphics.PreferredBackBufferHeight;
			particleManager = new ParticleManager(this);
            Tools.Quick.game = this;
            mousy = new ActionMouse(this, camera);
            Tools.Quick.actmouse = mousy;
            //water = new Water(this);
            skybox = new SkyBox(this);
            trees = new Trees(this);
        }

        protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			//camera.Initialize();


            //ParticleSystem fireParticle = new FireParticleSystem(this, Content);
            //ParticleSystem fireSmokeParticle = new SmokePlumeParticleSystem(this, Content);

            //fireParticle.Initialize();
            //fireSmokeParticle.Initialize();

            //fireParticle.DrawOrder = 200;
            //fireSmokeParticle.DrawOrder = 100;


            new ParticleInit(particleManager, this, this.Content);

            //particleManager.Add(fireParticle);
            //particleManager.Activate("Fire");
            //particleManager.Add(fireSmokeParticle);
            //particleManager.Activate("SmokePlume");
            //particleManager.Activate("Snow");
            //Components.Add(water);
            Components.Add(skybox);
            Components.Add(trees);
            

			base.Initialize();

		}

        void UpdateFire()
		{
			const int fireParticlesPerFrame = 20;

			// Create a number of fire particles, randomly positioned around a circle.
			for (int i = 0; i < fireParticlesPerFrame; i++)
			{
				particleManager.AddParticle("Fire", RandomPointOnCircle(), Vector3.Zero);
			}

			// Create one smoke particle per frmae, too.
			particleManager.AddParticle("SmokePlume", RandomPointOnCircle(), Vector3.Zero);
		}

        Vector3 RandomPointOnCircle()
		{
			Random random = new Random();

			const float radius = 5;
			const float height = 8;

			double angle = random.NextDouble() * Math.PI * 2;

			float x = (float)Math.Cos(angle);
			float y = (float)Math.Sin(angle);

			return new Vector3(x * radius, y * radius + height, 0);
		}

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.IsMouseVisible = true;
            //  graphics.ToggleFullScreen();
            Tools.Quick.content = Content;
            Tools.Quick.device = graphics.GraphicsDevice;
            Tools.Quick.graphics = graphics;
            Tools.Quick.init();
            Tools.Quick.spriteBatch = spriteBatch;
            Map map = new Map();
            map.GenMap();

            //MapGen.MapConverter.Init();

            //MapGen.MapVoronoi v = new MapGen.MapVoronoi(245);
            //v.Generate();
            //v.Convert();

            effect = new BasicEffect(graphics.GraphicsDevice);
            actualGround = LoadMap.loadground(map);
            texture = Tools.Quick.groundTexture[BiomeType.SubtropicalDesert];
            testgivre = Content.Load<Texture2D>("givre");
            //water.SetGround(actualGround);
            trees.SetGround(actualGround);
            trees.CreateTrees();

            //element a supprimer cause de test
            myModel = Content.Load<Model>("Models\\sapin");
            // myModel = Content.Load<Model>("Models\\untitled");
            display.init();
            camera.modiposition(actualGround.tiles[1].v2, true);
            phgest = new PhysicsGest(actualGround, new List<Object>());
            camera.modiposition(actualGround.tiles[0].v1, true);
            // TODO: use this.Content to load your game content here
            //ps = new ParticleSimple(Content, this, Components);
            //psinit();
            //psLoadContent(Content);


            arial = Content.Load<SpriteFont>("Arial");
            // menu = new Menu(arial, TypeMenu.Principal);

            Tools.Quick.player = new Personnage("Test", true, 0, new Inventory(), myModel);
            //d = new Dungeon();
            inf = new InventoryInterface(Content.Load<Texture2D>("InterfaceComponent\\Cadre"), Content.Load<Texture2D>("InterfaceComponent\\cadre bois"), Content.Load<Texture2D>("Test\\Mannequin"), Content.Load<Texture2D>("InterfaceComponent\\fond bois"), this);
            //d.setVisible(false);
            //Components.Add(d);
            base.LoadContent();
        }

	
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		protected override void Update(GameTime gameTime)
		{
			ls = new List<Sens>();
            Tools.Quick.ks = Keyboard.GetState();
            Tools.Quick.ms = Mouse.GetState();
			x = y = z = 0;
			// Allows the game to exit
          
                actkey.valider();


                if (menuactif == null)
                {
                    #region out menu

                    if (Tools.Quick.ks.IsKeyDown(Keys.Left))
                    {
                        ls.Add(Sens.Gauche);

                        // x -= 100f;
                    }
                    if (Tools.Quick.ks.IsKeyDown(Keys.Right))
                    {
                        ls.Add(Sens.Droite);
                        // x+=100f;
                    }
                    if (Tools.Quick.ks.IsKeyDown(Keys.Up))
                    {
                        ls.Add(Sens.Haut);
                    }
                    if (Tools.Quick.ks.IsKeyDown(Keys.Down))
                    {
                        ls.Add(Sens.Bas);
                    }

                    if (Tools.Quick.ks.IsKeyDown(Keys.Z))
                    {
                        ls.Add(Sens.Rothaut);
                    }

                    if (Tools.Quick.ks.IsKeyDown(Keys.S))
                    {
                        ls.Add(Sens.Rotbas);
                    }

                    if (Tools.Quick.ks.IsKeyDown(Keys.PageUp))
                    {
                        camera.modiposition(new Vector3(0, 10, 0), false);

                    }
                    if (Tools.Quick.ks.IsKeyDown(Keys.PageDown))
                    {

                        camera.modiposition(new Vector3(0, -10, 0), false);
                    }
                    
                    phgest.onGround(camera.position, camera);

                    float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    for (int k = 0; k < 40; k++)
                    {
                        particleManager.AddParticle("Snow", new Vector3((float)rnd.Next(0, Map.MAPSIZE), 125, (float)rnd.Next(0, Map.MAPSIZE)), new Vector3(0, 00, 0));
                     
                    }
                   
                    #endregion
                }
                else
                {
                    #region in menu

                   
                    #endregion


                }
			if (!(mouselastpos.X == Tools.Quick.ms.X && mouselastpos.Y == Tools.Quick.ms.Y))
			{
				mouselastpos = new Vector2(Tools.Quick.ms.X, Tools.Quick.ms.Y);
                if (menuactif != null)
                {
                    foreach (ClickableArea lca in menuactif.menuclick)
                    {
                        lca.onItem(new Vector2(Tools.Quick.ms.X, Tools.Quick.ms.Y), Tools.Quick.ms.LeftButton.Equals(ButtonState.Pressed));

                    }
                }
			//	
			}
			if (Tools.Quick.ms.LeftButton.Equals(ButtonState.Pressed))
			{
                if (menuactif != null)
                {
                    foreach (ClickableArea lca in menuactif.menuclick)
                    {
                        lca.onItem(new Vector2(Tools.Quick.ms.X, Tools.Quick.ms.Y), Tools.Quick.ms.LeftButton.Equals(ButtonState.Pressed));

                    }
                }
			}

			UpdateFire();
            mousy.actualise();
			base.Update(gameTime);

		}

        protected override void Draw(GameTime gameTime)
		{

            camera.actucamera(ls);

            //water.DrawRefractionMap(camera);
            //water.DrawReflectionMap(camera, skybox);
		
			
			GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 5.0f, 0);
			spriteBatch.Begin();
            if (menuactif == null)
            {
                //water.Visible = true;
                trees.Visible = true;
                #region out menu
                skybox.SetCamera(camera.getview(), camera.GetProjection(), camera.position);
                skybox.Draw(gameTime);
                this.IsMouseVisible = false;
                if (actualGround.Visible) { actualGround.DrawGround(camera.lookat, camera.position, display, camera); }
                display.drawMesh(myModel, new Vector3(020, 0, 0), 0.0f, camera);

                trees.SetCamera(camera.getview(), camera.GetProjection(), camera.position);
                //trees.Draw(gameTime);
                //Tools.Quick.player.getInterface().Draw(gameTime);
                // spriteBatch.Draw(testgivre,new Rectangle(0,0,graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight),Color.White);

                // display.drawMesh(Tools.Quick.player.getModel(), camera.position, camera.angle, camera);


                particleManager.SetCamera(camera.getview(), camera.GetProjection());

                #endregion

            }
            else
            {

                #region in menu
                //water.Visible = false;
                trees.Visible = false;
                //  spriteBatch.Draw(backmenu, new Vector2(0, 0), Color.White) ;
                // menuactif.Draw(gameTime);

                #endregion
            }

            if (gameconsole.onA)
            {
                spriteBatch.DrawString(Tools.Quick.dicoFont[Tools.TypeFont.Texte], gameconsole.historique, new Vector2(0, graphics.PreferredBackBufferHeight - 550), Color.White);
                spriteBatch.DrawString(Tools.Quick.dicoFont[Tools.TypeFont.Texte], gameconsole.text, new Vector2(0, graphics.PreferredBackBufferHeight - 50), Color.White);
               
            }

            spriteBatch.End();
			base.Draw(gameTime);

		}

        public void switchMenu(TypeMenu menu)
        {
            if (menuactif != null)
            {
                if (menuactif.type == menu)
                {
                    this.IsMouseVisible = false;
                    menuactif.setVisibility(false);
                    menuactif = null;
                    Tools.Quick.player.getInterface().setVisibility(true);
                    
                }

                
            }
            else
            {

                Tools.Quick.player.getInterface().setVisibility(false);
               // backmenu.SaveAsJpeg((System.IO.Stream)(new System.IO.FileStream("menu.jpg", System.IO.FileMode.OpenOrCreate)), backmenu.Width, backmenu.Height);
                this.IsMouseVisible = true; ;
                menuactif = new Menu(arial, menu);
                menuactif.setVisibility(true);

                

            }
        }

		private void SetUpCamera()
		{
			viewMatrix = Matrix.Identity;
			projectionMatrix = Matrix.CreateOrthographic(Window.ClientBounds.Width, Window.ClientBounds.Height, -1.0f, 1.0f);
		}

        public void switchinoutdoor()
        {
            indoor = !indoor;
            if (indoor)
            {

                actualGround.setVisible(false);
              //  d.setVisible(true);


            }
            else
            {
                actualGround.setVisible(true);
              //  d.setVisible(false);
            }
        }

        public void switchPhysics()
        {
            phgest.switchactivate();
        }
	}
}
