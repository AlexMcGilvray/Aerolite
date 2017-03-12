using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems
{
    public class AeTextureManager
    {
        private int proceduralObjectCounter = 0;
        private const string DEFAULT_TEX_GROUP = "Default";
        private const string DEFAULT_PROCEDURAL_GROUP = "Procedural";
        Dictionary<string, Dictionary<string, Texture2D>> textureLib = new Dictionary<string, Dictionary<string, Texture2D>>();
        AeGame gameReference;

        Texture2D _fillTexture;

        public AeTextureManager(AeGame myGame)
        {
            //create a default group for people who don't want to manage separate texture groups. 

            textureLib.Add(DEFAULT_TEX_GROUP, new Dictionary<string, Texture2D>());
            textureLib.Add(DEFAULT_PROCEDURAL_GROUP, new Dictionary<string, Texture2D>());

            _fillTexture = CreateFilledRectangle(1, 1, Color.White);

            gameReference = myGame;
        }

        public Texture2D GetFillTexture()
        {
            return _fillTexture;
        }

        public Texture2D CreateFilledRectangle(int width, int height, Color color)
        {
            string uniqueName = "ProceduralObject" + "00" + proceduralObjectCounter++;
            Color[] myColor = new Color[width * height];
            Texture2D tex = new Texture2D(AeEngine.Singleton().GameReference.GraphicsDevice, width, height, false, SurfaceFormat.Color);

            for (int i = 0; i < myColor.Length; i++)
            {
                myColor[i] = color;
            }

            tex.SetData<Color>(myColor);

            return LoadProceduralTexture(uniqueName, tex);
        }

        /// <summary>
        /// This is for loading and keeping track of procedurally generated shapes. You could load a 
        /// texture from file using this but that is not recommended because you aren't garaunteed a 
        /// unique name. 
        /// 
        /// This method is kept private to prevent miuse. CreateFilledRectangle uses it.
        /// </summary>
        /// <param name="texName"></param>
        /// <param name="tex"></param>
        /// <returns></returns>
        private Texture2D LoadProceduralTexture(string texName, Texture2D tex)
        {
            if (!textureLib[DEFAULT_PROCEDURAL_GROUP].ContainsKey(texName))
            {
                textureLib[DEFAULT_PROCEDURAL_GROUP].Add(texName, tex);
            }
            return textureLib[DEFAULT_PROCEDURAL_GROUP][texName];
        }

        /// <summary>
        /// Adds a texture to the default group. Use this if you have no interest in creating and managing texture 
        /// dictionaries. 
        /// </summary>
        /// <param name="texName">path</param>
        public Texture2D LoadTexture(string texPath)
        {
            if (!textureLib[DEFAULT_TEX_GROUP].ContainsKey(texPath))
            {
                textureLib[DEFAULT_TEX_GROUP].Add(texPath, gameReference.Content.Load<Texture2D>(texPath));
            }
            return textureLib[DEFAULT_TEX_GROUP][texPath];
        }

        /// <summary>
        /// Adds a texture to an existing texture dictionary. 
        /// </summary>
        /// <param name="collectionName">Name of the dictionary you wish to add the texture to.</param>
        /// <param name="texPath">The path to the texture. </param>
        public Texture2D LoadTexture(string collectionName, string texPath)
        {
            //Make an editor mode, then load images through .NET and copy their pixels to a texture2D so you can open
            //images without having to add them tot he content pieline
            if (textureLib.ContainsKey(collectionName))
            {
                textureLib[collectionName].Add(texPath, gameReference.Content.Load<Texture2D>(texPath));
            }
            else
            {
                textureLib.Add(collectionName, new Dictionary<string, Texture2D>());
                textureLib[collectionName].Add(texPath, gameReference.Content.Load<Texture2D>(texPath));
            }
            return textureLib[collectionName][texPath];

        }

        /// <summary>
        /// Gets a texture from the default texture dictionary.
        /// </summary>
        /// <param name="texPath"></param>
        /// <returns></returns>
        public Texture2D GetTexture(String texPath)
        {
            if (textureLib[DEFAULT_TEX_GROUP].ContainsKey(texPath))
            {
                return textureLib[DEFAULT_TEX_GROUP][texPath];
            }
            else
                throw new Exception("Texture doesn't exist.");
        }

        /// <summary>
        /// Gets a texture from a specified texture collection.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="texPath"></param>
        /// <returns></returns>
        public Texture2D GetTexture(String collectionName, String texPath)
        {
            if (textureLib.ContainsKey(collectionName))
            {
                if (textureLib[collectionName].ContainsKey(texPath))
                {
                    return textureLib[collectionName][texPath];
                }
                else
                    throw new Exception("Texture doesn't exist.");
            }
            else
                throw new Exception("Collection doesn't exist.");
        }
    }
}
