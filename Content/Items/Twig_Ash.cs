using TwigMod.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TwigMod.Content.Projectiles;

namespace TwigMod.Content.Items
{
    public class Twig_Ash : ModItem
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
            Item.width = 30;
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

            Item.shoot = ModContent.ProjectileType<Ash_Pollen>();
            Item.shootSpeed = 2.1f;
            Item.noMelee = false;
            Item.channel = true;
        }

        public override void HoldItem(Player player)
        {
            player.AddBuff(257, 60); //Lucky.
            player.AddBuff(ModContent.BuffType<NiceStick>(), 60); //Nice stick.

            base.HoldItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target?.AddBuff(67, 300); //Burning Effect.

            base.OnHitNPC(player, target, hit, damageDone);
        }

        public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
        {
            target?.AddBuff(67, 300); //Burning Effect.

            base.OnHitPvp(player, target, hurtInfo);
        }
    }
}
