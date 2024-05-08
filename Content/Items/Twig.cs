using TwigMod.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TwigMod.Content.Items
{
    public class Twig : ModItem
    {
        public override void SetDefaults()
        {
            //Damage stats.
            Item.DamageType = DamageClass.Melee;
            Item.damage = 99;
            Item.crit = 95;
            Item.knockBack = 99;

            //Tool power.
            Item.axe = 84;

            //Visual properties.
            Item.width = 29;
            Item.height = 30;
            Item.scale = 1.72f;

            Item.useTime = 1;
            Item.useAnimation = 4;
            Item.autoReuse = true;

            Item.holdStyle = 5;
            Item.useStyle = ItemUseStyleID.Swing;

            //Misc properties.
            Item.value = Item.buyPrice(copper: 1);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
        }

        public override void HoldItem(Player player)
        {
            player.AddBuff(257, 60); //Lucky.
            player.AddBuff(ModContent.BuffType<NiceStick>(), 60); //Nice stick.

            base.HoldItem(player);
        }
    }
}
