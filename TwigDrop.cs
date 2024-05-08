using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using TwigMod.Content.Items;


namespace TwigMod
{
    public class TwigDropItem : GlobalItem
    {
        //Drop a Twig when an Item is spawned from a tree.
        public override void OnSpawn(Item item, IEntitySource source)
        {
            TwigDrop.SpawnTwig((int)item.position.X, (int)item.position.Y, source);
        }
    }

    public class TwigDropAnimal : GlobalNPC
    {
        //Drop a Twig when an NPC is spawned from a tree.
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            TwigDrop.SpawnTwig((int)npc.position.X, (int)npc.position.Y, source);
        }
    }

    public static class TwigDrop
    {
        public static void SpawnTwig(int x, int y, IEntitySource source)
        {
            if (source is EntitySource_ShakeTree && source.Context != "shakedStick")
            {
                if (Main.rand.NextBool(999999)) //1 in 1 million chance.
                {
                    int soilX, soilY;
                    WorldGen.GetTreeBottom(x, y, out soilX, out soilY);

                    int soilType = getSoilType(soilX / 16, soilY / 16);

                    TreeTypes treeType = WorldGen.GetTreeType(soilType);
                    EntitySource_ShakeTree shaked = new EntitySource_ShakeTree(x, y, "shakedStick"); //Prevents infinite spawning.

                    //NetworkText debugMsg = NetworkText.FromLiteral(string.Format("Tree Bottom Coords: <x:{0},y:{1}>, tileID:{2}", soilX / 16, soilY / 16, soilType));
                    //ChatHelper.BroadcastChatMessage(debugMsg, new Microsoft.Xna.Framework.Color(255, 255, 255));

                    if (treeType is TreeTypes.Crimson or TreeTypes.PalmCrimson)
                    {
                        Item.NewItem(shaked, new Vector2(x, y), ModContent.ItemType<Twig_Crimson>());
                    }
                    else if (treeType is TreeTypes.Corrupt or TreeTypes.PalmCorrupt)
                    {
                        Item.NewItem(shaked, new Vector2(x, y), ModContent.ItemType<Twig_Corruption>());
                    }
                    else if (treeType is TreeTypes.Hallowed or TreeTypes.PalmHallowed)
                    {
                        Item.NewItem(shaked, new Vector2(x, y), ModContent.ItemType<Twig_Hallow>());
                    }
                    else if (treeType is TreeTypes.Ash)
                    {
                        Item.NewItem(shaked, new Vector2(x, y), ModContent.ItemType<Twig_Ash>());
                    }
                    else if (treeType is not TreeTypes.Mushroom) //Mushroom Trees can't spawn Twigs.
                    {
                        Item.NewItem(shaked, new Vector2(x, y), ModContent.ItemType<Twig>());
                    }
                }
            }
        }

        public static int getSoilType(int posX, int posY)
        {
            int tileType = WorldGen.TileType(posX, posY);
            int tileLim = 30;

            int count = 0;

            while (count <= tileLim &&
            tileType != 23 &&
            tileType != 199 &&
            tileType != 492 &&
            tileType != 633 &&
            tileType != 1112 &&
            tileType != 234 &&
            tileType != 116)
            {
                posY++;
                count++;
                tileType = WorldGen.TileType(posX, posY);
            }

            return tileType;
        }
    }

    public class HitDebug : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            base.KillTile(i, j, type, ref fail, ref effectOnly, ref noItem);

            //NetworkText debugMsg = NetworkText.FromLiteral(string.Format("Hit Coords: <x:{0},y:{1}>", i, j));
            //ChatHelper.BroadcastChatMessage(debugMsg, new Microsoft.Xna.Framework.Color(255, 255, 255));
        }
    }
}