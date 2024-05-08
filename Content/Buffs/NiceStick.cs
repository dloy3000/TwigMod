using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TwigMod.Content.Buffs
{
    public class NiceStick : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.maxRunSpeed += 21f;
            player.runAcceleration *= 2.1f;
            player.GetKnockback<GenericDamageClass>() += 28f;
        }
    }
}