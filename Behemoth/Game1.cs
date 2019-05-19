using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using System;

namespace Behemoth
{

    public enum Dir
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
        Texture2D playerDownCharging;
        Texture2D playerUpCharging;
        Texture2D playerLeftCharging;
        Texture2D playerRightCharging;
        Texture2D playerRightDownCharging;
        Texture2D playerRightUpCharging;
        Texture2D playerLeftDownCharging;
        Texture2D playerLeftUpCharging;
        Texture2D playerDownSwing;
        Texture2D playerUpSwing;
        Texture2D playerLeftSwing;
        Texture2D playerRightSwing;
        Texture2D playerRightDownSwing;
        Texture2D playerRightUpSwing;
        Texture2D playerLeftDownSwing;
        Texture2D playerLeftUpSwing;

        //Texture2D testHitSPrite;

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

            //testHitSPrite = Content.Load<Texture2D>("Misc/testHit");

            playerUp = Content.Load<Texture2D>("Player/BehemothUp");
            playerDown = Content.Load<Texture2D>("Player/BehemothDown");
            playerLeft = Content.Load<Texture2D>("Player/BehemothLeft");
            playerRight = Content.Load<Texture2D>("Player/BehemothRight");
            playerRightDown = Content.Load<Texture2D>("Player/BehemothRightDown");
            playerRightUp = Content.Load<Texture2D>("Player/BehemothRightUp");
            playerLeftDown = Content.Load<Texture2D>("Player/BehemothLeftDown");
            playerLeftUp = Content.Load<Texture2D>("Player/BehemothLeftUp");

            playerUpCharging = Content.Load<Texture2D>("Player/BehemothUpCharging");
            playerDownCharging = Content.Load<Texture2D>("Player/BehemothDownCharging");
            playerLeftCharging = Content.Load<Texture2D>("Player/BehemothLeftCharging");
            playerRightCharging = Content.Load<Texture2D>("Player/BehemothRightCharging");
            playerRightDownCharging = Content.Load<Texture2D>("Player/BehemothRightDownCharging");
            playerRightUpCharging = Content.Load<Texture2D>("Player/BehemothRightUpCharging");
            playerLeftDownCharging = Content.Load<Texture2D>("Player/BehemothLeftDownCharging");
            playerLeftUpCharging = Content.Load<Texture2D>("Player/BehemothLeftUpCharging");

            playerUpSwing = Content.Load<Texture2D>("Player/BehemothUpSwing");
            playerDownSwing = Content.Load<Texture2D>("Player/BehemothDownSwing");
            playerLeftSwing = Content.Load<Texture2D>("Player/BehemothLeftSwing");
            playerRightSwing = Content.Load<Texture2D>("Player/BehemothRightSwing");
            playerRightDownSwing = Content.Load<Texture2D>("Player/BehemothRightDownSwing");
            playerRightUpSwing = Content.Load<Texture2D>("Player/BehemothRightUpSwing");
            playerLeftDownSwing = Content.Load<Texture2D>("Player/BehemothLeftDownSwing");
            playerLeftUpSwing = Content.Load<Texture2D>("Player/BehemothLeftUpSwing");


            player.animations[0][0] = new AnimatedSprite(playerRight, 1, 4);
            player.animations[0][1] = new AnimatedSprite(playerRightUp, 1, 4);
            player.animations[0][2] = new AnimatedSprite(playerUp, 1, 4);
            player.animations[0][3] = new AnimatedSprite(playerLeftUp, 1, 4);
            player.animations[0][4] = new AnimatedSprite(playerLeft, 1, 4);
            player.animations[0][5] = new AnimatedSprite(playerLeftDown, 1, 4);
            player.animations[0][6] = new AnimatedSprite(playerDown, 1, 4);
            player.animations[0][7] = new AnimatedSprite(playerRightDown, 1, 4);

            player.animations[1][0] = new AnimatedSprite(playerRightCharging, 1, 4);
            player.animations[1][1] = new AnimatedSprite(playerRightUpCharging, 1, 4);
            player.animations[1][2] = new AnimatedSprite(playerUpCharging, 1, 4);
            player.animations[1][3] = new AnimatedSprite(playerLeftUpCharging, 1, 4);
            player.animations[1][4] = new AnimatedSprite(playerLeftCharging, 1, 4);
            player.animations[1][5] = new AnimatedSprite(playerLeftDownCharging, 1, 4);
            player.animations[1][6] = new AnimatedSprite(playerDownCharging, 1, 4);
            player.animations[1][7] = new AnimatedSprite(playerRightDownCharging, 1, 4);

            player.animations[2][0] = new AnimatedSprite(playerRightSwing, 1, 1);
            player.animations[2][1] = new AnimatedSprite(playerRightUpSwing, 1, 1);
            player.animations[2][2] = new AnimatedSprite(playerUpSwing, 1, 1);
            player.animations[2][3] = new AnimatedSprite(playerLeftUpSwing, 1, 1);
            player.animations[2][4] = new AnimatedSprite(playerLeftSwing, 1, 1);
            player.animations[2][5] = new AnimatedSprite(playerLeftDownSwing, 1, 1);
            player.animations[2][6] = new AnimatedSprite(playerDownSwing, 1, 1);
            player.animations[2][7] = new AnimatedSprite(playerRightDownSwing, 1, 1);

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
                player.Swing.Update(gameTime, player);
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
                //spriteBatch.Draw(testHitSPrite, new Vector2(player.Swing.Position.X-16, player.Swing.Position.Y-16), null, Color.White, 0f, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), (float)((player.Swing.Position.Y + 16) / myMap.HeightInPixels));
            }
            foreach (Obstacle ob in Obstacle.obstacles)
            {
                spriteBatch.Draw(ob.Texture, ob.Position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(1, 1), new SpriteEffects(), (float)((ob.HitPos.Y - ob.DrawSort) / myMap.HeightInPixels));
                //spriteBatch.Draw(_texture, ob.HitBox, Color.White);
            }


            

            player.anim.Draw(spriteBatch, new Vector2(player.Position.X - 32, player.Position.Y - 32), myMap.WidthInPixels);
            //spriteBatch.Draw(_texture, player.HitBox, Color.White);

            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Sta: " + Math.Floor(player.Stamina), new Vector2(3,3), Color.White);
            spriteBatch.DrawString(font, "Str : " + Math.Floor(player.Strength), new Vector2(3, 45), Color.White);
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
