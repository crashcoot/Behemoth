using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using System;

namespace Behemoth
{

    enum Dir
    {
        Right,
        RightUp,
        Up,
        LeftUp,
        Left,
        LeftDown,
        Down,
        RightDown
    }
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;
        
        Texture2D playerDown;
        Texture2D playerUp;
        Texture2D playerLeft;
        Texture2D playerRight;
        Texture2D playerRightDown;
        Texture2D playerRightUp;
        Texture2D playerLeftDown;
        Texture2D playerLeftUp;

        Texture2D testHitSPrite;

        Texture2D treeSprite;
        Texture2D bushSprite;
        Texture2D boulderSprite;

        Color[] data = new Color[80 * 30];
        

        TiledMapRenderer mapRenderer;
        TiledMap myMap;

        Camera2D cam;

        Player player = new Player();

        Texture2D _texture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        
        protected override void Initialize()
        {
            mapRenderer = new TiledMapRenderer(GraphicsDevice);

            cam = new Camera2D(GraphicsDevice);
            cam.Zoom = 1.4f;

            


            base.Initialize();
        }

        
        protected override void LoadContent()
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _texture = new Texture2D(GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.DarkSlateGray });

            font = Content.Load<SpriteFont>("Misc/spaceFont");

            testHitSPrite = Content.Load<Texture2D>("Misc/testHit");

            playerUp = Content.Load<Texture2D>("Player/BehemothUp");
            playerDown = Content.Load<Texture2D>("Player/BehemothDown");
            playerLeft = Content.Load<Texture2D>("Player/BehemothLeft");
            playerRight = Content.Load<Texture2D>("Player/BehemothRight");
            playerRightDown = Content.Load<Texture2D>("Player/BehemothRightDown");
            playerRightUp = Content.Load<Texture2D>("Player/BehemothRightUp");
            playerLeftDown = Content.Load<Texture2D>("Player/BehemothLeftDown");
            playerLeftUp = Content.Load<Texture2D>("Player/BehemothLeftUp");

            player.animations[0] = new AnimatedSprite(playerRight, 1, 4);
            player.animations[1] = new AnimatedSprite(playerRightUp, 1, 4);
            player.animations[2] = new AnimatedSprite(playerUp, 1, 4);
            player.animations[3] = new AnimatedSprite(playerLeftUp, 1, 4);
            player.animations[4] = new AnimatedSprite(playerLeft, 1, 4);
            player.animations[5] = new AnimatedSprite(playerLeftDown, 1, 4);
            player.animations[6] = new AnimatedSprite(playerDown, 1, 4);
            player.animations[7] = new AnimatedSprite(playerRightDown, 1, 4);

            myMap = Content.Load<TiledMap>("Misc/gameMap");

            treeSprite = Content.Load<Texture2D>("Obstacles/Tree");
            bushSprite = Content.Load<Texture2D>("Obstacles/bush");
            boulderSprite = Content.Load<Texture2D>("Obstacles/boulder");
            generateForest(new Vector2(0,0), myMap.WidthInPixels, myMap.HeightInPixels, 25, 25);


        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            int mapW = myMap.WidthInPixels;
            int mapH = myMap.HeightInPixels;

            player.Update(gameTime, mapW, mapH);
            if (player.Swing != null)
            {
                player.Swing.Update(gameTime, player.Position);
                foreach(Obstacle ob in Obstacle.obstacles)
                {
                    int sum = player.Swing.Radius + ob.Radius;
                    if (Vector2.Distance(player.Swing.Position, ob.HitPos) < sum)
                    {
                        ob.OnHit(player.Position, 15);
                    }
                }
            }
            foreach(Obstacle ob in Obstacle.obstacles)
            {
                ob.Update();
            }

            Obstacle.obstacles.RemoveAll(ob => ob.Dead);

            float tmpX = player.Position.X;
            float tmpY = player.Position.Y;

            float camW = graphics.PreferredBackBufferWidth / cam.Zoom;
            float camH = graphics.PreferredBackBufferHeight / cam.Zoom;

            if (tmpX < camW / 2)
            {
                tmpX = camW / 2;
            }
            if (tmpY < camH / 2 + 64)
            {
                tmpY = camH / 2 + 64;
            }
            

            if (tmpX > mapW - camW / 2)
            {
                tmpX = mapW - camW / 2;
            }
            if (tmpY > mapH - camH / 2)
            {
                tmpY = mapH - camH / 2;
            }

            cam.LookAt(new Vector2(tmpX, tmpY));

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);



            spriteBatch.Begin(transformMatrix: cam.GetViewMatrix(), sortMode: SpriteSortMode.FrontToBack);
            mapRenderer.Draw(myMap, cam.GetViewMatrix());
            if (player.Swing != null) 
            {
                spriteBatch.Draw(testHitSPrite, new Vector2(player.Swing.Position.X-16, player.Swing.Position.Y-16), null, Color.White, 0f, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), (float)((player.Swing.Position.Y + 16) / myMap.HeightInPixels));
            }
            foreach (Obstacle ob in Obstacle.obstacles)
            {
                spriteBatch.Draw(ob.Texture, ob.Position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), (float)((ob.HitPos.Y - ob.DrawSort) / myMap.HeightInPixels));
                spriteBatch.Draw(_texture, ob.HitBox, Color.White);
            }


            

            player.anim.Draw(spriteBatch, new Vector2(player.Position.X - 32, player.Position.Y - 32), myMap.WidthInPixels);
            spriteBatch.Draw(_texture, player.HitBox, Color.White);

            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Stamina: " + Math.Floor(player.Stamina), new Vector2(3,3), Color.White);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void generateForest(Vector2 topLeft, int width, int height, int columns, int rows)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    Obstacle.obstacles.Add(new Tree(new Vector2(rnd.Next(x * width / columns + 16, (x + 1) * width / columns) - 16, rnd.Next(y * height / rows + 16, (y + 1) * height / rows - 16)), treeSprite));
                }
            }
            for (int x = 0; x < columns; x += 2)
            {
                for (int y = 0; y < rows; y += 2)
                {
                    Bush tempBush = new Bush(new Vector2(rnd.Next(x * width / columns + 16, (x + 2) * width / columns), rnd.Next(y * height / rows + 16, (y + 1) * height / rows - 16)), bushSprite);
                    if (Obstacle.didCollide(tempBush.HitBox) == null)
                    {
                        Obstacle.obstacles.Add(tempBush);
                    }
                }
            }

            for (int x = 0; x < columns; x += 3)
            {
                for (int y = 0; y < rows; y += 3)
                {
                    Boulder tempBoulder = new Boulder(new Vector2(rnd.Next(x * width / columns + 16, (x + 2) * width / columns), rnd.Next(y * height / rows + 16, (y + 1) * height / rows - 16)), boulderSprite);
                    if (Obstacle.didCollide(tempBoulder.HitBox) == null)
                    {
                        Obstacle.obstacles.Add(tempBoulder);
                    }
                }
            }
        }
    }
}
