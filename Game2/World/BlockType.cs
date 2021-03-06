﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class BlockType
    {
        public static readonly BlockType GREEN = new BlockType("Green","themes/Normal/blocks/green", true, true, false, false, false);
        public static readonly BlockType STONE = new BlockType("Stone", "themes/Normal/blocks/stone", true, true, false, false, false);
        public static readonly BlockType CLEAN = new BlockType("Clean", "themes/Normal/blocks/clean", true, true, false, false, false);
        public static readonly BlockType SPIKE = new BlockType("Spike", "themes/Normal/blocks/spike", false, false, false, true, false);
        public static readonly BlockType LAVA = new BlockType("Lava", "themes/Normal/blocks/lava", false, true, false, true, true);
        public static readonly BlockType AIR = new BlockType("Air", "themes/Normal/blocks/air", false, true, false, false, false);
        public static readonly BlockType ICE = new BlockType("Ice", "themes/Normal/blocks/ice", true, true, false, false, false);
        public static readonly BlockType STONEDECO = new BlockType("StoneDeco", "themes/Normal/blocks/stonedeco", false, true, true, false, false);
        public static readonly BlockType DECO1 = new BlockType("flower", "themes/Normal/blocks/flower", false, false, false, false, false);

        private string texName;
        private string name;
        private bool collision;
        private Texture2D texture;
        bool connectsToSelf, connectsToOthers;
        bool killing;
        bool illumination;

        public BlockType(string name, string texName, bool collision, bool connectsToSelf, bool connectsToOthers, bool killing, bool illumination)
        {
            this.name = name;
            this.texName = texName;
            this.collision = collision;
            this.connectsToSelf = connectsToSelf;
            this.killing = killing;
            this.illumination = illumination;
            this.connectsToOthers = connectsToOthers;

        }
        public void reloadTexture(ContentManager content, string theme)
        {
            texName = "themes/" + theme + "/blocks/" + name;
            texture = content.Load<Texture2D>(texName);
        }
        public static IEnumerable<BlockType> Values
        {
            get
            {
                yield return GREEN;
                yield return STONE;
                yield return CLEAN;
                yield return SPIKE;
                yield return LAVA;
                yield return ICE;
                yield return STONEDECO;
                yield return DECO1;
            }
        }

        public void setCurrentBlockType()
        {
            EditorGui.currentBlockType = this;
        }

        public static BlockType getBlockTypeByName(string typename)
        {
            foreach(BlockType type in Values)
            {
                if (type.name == typename)
                    return type;
            }
            return null;
        }
        public bool isConnectsToSelf { get { return connectsToSelf; } }
        public bool isConnectsToOthers { get { return connectsToOthers; } }
        public string Name { get { return name; } }
        public string TexName { get { return texName; } }
        public Texture2D Texture { get { return texture; } }
        public bool Collision { get { return collision; } }
        public bool Killing { get { return killing; } }
        public bool Illumination { get { return illumination; } }
    }
}
