/*
 * Amneet Kaur
 * A00296046
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;

namespace GME1003GoblinDanceParty
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        //Declare some variables
        private List<float> _rotationAmount; // list of rotation amount
        private int _numStars;          //how many stars?
        private List<int> _starsX;      //list of star x-coordinates
        private List<int> _starsY;      //list of star y-coordinates

        private Texture2D _starSprite;  //the sprite image for our star
        private Texture2D _background;  //the sprite image for the background
        private Random _rng;            //for all our random number needs
        private List<Color> _starsColor;       //list of star colors - let's have fun with colour!!
        private List<float> _starsScale;       //list of star size
        private List<float> _starsTransparency;//list of star transparency
        private List<float> _starsRotation;    //list of star rotation



        //***This is for the goblin. Ignore it.
        Goblin goblin;
        Song music;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _rng = new Random();        //finish setting up our Randon 
            _numStars = _rng.Next(50,301);  //this would be better as a random number between 100 and 300 ( in the assignment,it says 50,300)
            _rotationAmount = new List<float>(); //the iniatialization of rotation list
            _starsX = new List<int>();  //stars X coordinate
            _starsY = new List<int>();  //stars Y coordinate
            _starsColor = new List<Color>(); // stars color
            _starsScale = new List<float>(); //stars size
            _starsTransparency = new List<float>(); //stars transparency 
            _starsRotation = new List<float>(); // stars rotation
           // _starColor = new Color(128 + _rng.Next(0,129), 128 + _rng.Next(0, 129), 128 + _rng.Next(0, 129));                   //this is a "relatively" easy way to create random colors
            //_starScale = _rng.Next(50, 100) / 200f; //this will affect the size of the stars
            //_starTransparency = _rng.Next(25, 101)/100f;   //star transparency
            //_starRotation = _rng.Next(0, 101) / 100f;       //star rotation

            //use a separate for loop for each list - for practice
            //List of X coordinates
            for (int i = 0; i < _numStars; i++) 
            { 
                _starsX.Add(_rng.Next(0, 801)); //all star x-coordinates are between 0 and 801
            }

            //List of Y coordinates
            for (int i = 0; i < _numStars; i++)
            {
                _starsY.Add(_rng.Next(0, 481)); //all star y-coordinates are between 0 and 480
            }

            //ToDo: List of Colors
            for (int i = 0; i < _numStars; i++)
            {
                _starsColor.Add(new Color(128 + _rng.Next(0, 129), 128 + _rng.Next(0, 129), 128 + _rng.Next(0, 129))); // all stars color 
            }
            //ToDo: List of scale values
            for (int i = 0; i < _numStars; i++)
            {
                _starsScale.Add(_rng.Next(50, 100) / 200f); // all stars size 
            }

            //ToDo: List of transparency values
            for (int i =0; i < _numStars; i++)
            {
                _starsTransparency.Add(_rng.Next(25, 101) / 100f); // all stars transparency 
            }

            //ToDo: List of rotation values
            for (int i = 0; i < _numStars; i++)
            {
                _starsRotation.Add(_rng.Next(0, 101) / 100f); // all stars rotation
            }

            // List of rotation amount
            for (int i = 0; i < _numStars; i++)
            {
                _rotationAmount.Add(_rng.Next(-10, 11) / 1000f ); //the rotation amount
            }

                base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load out star sprite
            _starSprite = Content.Load<Texture2D>("starSprite"); 
            _background = Content.Load<Texture2D>("mbackground"); //load background

            //***This is for the goblin. Ignore it for now.
            goblin = new Goblin(Content.Load<Texture2D>("goblinIdleSpriteSheet"), 400, 400);
            music = Content.Load<Song>("chiptune");
            
            //if you're tired of the music player, comment this out!
            MediaPlayer.Play(music);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < _numStars; i++)
            {
                _starsRotation[i] += _rotationAmount[i]; // add rotation to each star
            }


                //***This is for the goblin. Ignore it for now.
                goblin.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            _spriteBatch.Begin();
            _spriteBatch.Draw(_background, new Rectangle(0, 0, 800, 480), Color.White);
            //it would be great to have a background image here! 
            //you could make that happen with a single Draw statement.

            //this is where we draw the stars...
            for (int i = 0; i < _numStars; i++) 
            {
                _spriteBatch.Draw(_starSprite, 
                    new Vector2(_starsX[i], _starsY[i]),    //set the star position
                    null,                                   //ignore this
                    _starsColor[i] * _starsTransparency[i],         //set colour and transparency
                    _starsRotation[i],                          //set rotation
                    new Vector2(_starSprite.Width / 2, _starSprite.Height / 2), //ignore this
                    new Vector2(_starsScale[i], _starsScale[i]),    //set scale (same number 2x)
                    SpriteEffects.None,                     //ignore this
                    0f);                                    //ignore this
            }
            _spriteBatch.End();



            //***This is for the goblin. Ignore it for now.
            goblin.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
