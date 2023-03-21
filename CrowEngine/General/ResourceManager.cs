using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Text.Json;

namespace CrowEngine
{

    public class Config
    {
        public Dictionary<uint, string> Items { get; set; }
        public Dictionary<uint, string> Entities { get; set; }
    }

    public class SmeltableConfig
    {
        public Dictionary<uint, uint> Items { get; set; }
    }

    public class RawToSmeltedConfig
    {
        public Dictionary<uint, uint> Items { get; set; }
    }

    public class FocusedItemAnimationConfig
    {
        public Dictionary<uint, string> Swingables { get; set; }
    }

    /// <summary>
    /// The Resource Manager is a singleton that should be accesible from anywhere. Handles loading fonts, textures, songs, and sound effects
    /// </summary>
    /// This may change in the future for optimization, only loading in resources
    /// that are useful on the screens they are useful... However, that change will happen later if needed
    public static class ResourceManager
    {


        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, Song> music = new Dictionary<string, Song>();

        public static ContentManager manager { get; set; }


        public static SpriteFont GetFont(string fontName)
        {
            if (!fonts.ContainsKey(fontName))
            {
                throw new Exception($"{fontName} doesn't exist in the current resource manager");
            }

            return fonts[fontName];
        }

        public static Texture2D GetTexture(string textureName)
        {
            if (!textures.ContainsKey(textureName))
            {
                throw new Exception($"{textureName} doesn't exist in the current resource manager");
            }

            return textures[textureName];
        }

        public static Song GetSong(string songName)
        {
            if (!music.ContainsKey(songName))
            {
                throw new Exception($"{songName} doesn't exist in the current resource manager");
            }

            return music[songName];
        }

        public static SoundEffect GetSoundEffect(string soundEffectName)
        {
            if (!soundEffects.ContainsKey(soundEffectName))
            {
                throw new Exception($"{soundEffectName} doesn't exist in the current resource manager");
            }
            return soundEffects[soundEffectName];
        }

        public static void RegisterFont(string pathToFont, string fontName)
        {
            if (!fonts.ContainsKey(fontName))
            {
                fonts.Add(fontName, manager.Load<SpriteFont>(pathToFont));
            }
        }

        public static void RegisterTexture(string pathToTexture, string textureName)
        {
            if (!textures.ContainsKey(textureName))
            {
                textures.Add(textureName, manager.Load<Texture2D>(pathToTexture));
            }
        }

        public static void RegisterTexture(Texture2D texture, string textureName)
        {
            if (!textures.ContainsKey(textureName))
            {
                textures.Add(textureName, texture);
            }
        }

        public static void RegisterSoundEffect(string pathToSoundEffect, string soundEffectName)
        {
            if (!soundEffects.ContainsKey(pathToSoundEffect))
            {
                soundEffects.Add(soundEffectName, manager.Load<SoundEffect>(pathToSoundEffect));
            }
        }

        public static void RegisterSong(string pathToSong, string songName)
        {
            if (!music.ContainsKey(songName))
            {
                music.Add(songName, manager.Load<Song>(pathToSong));
            }
        }
    }
}
