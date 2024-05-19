using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace TwigMod.Content.Projectiles
{
    public class Ash_Pollen : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = false; // Screw the cultist. 😈
        }

        public override void SetDefaults()
        {
            //Visual Properties.
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.light = 0.6f;
            Projectile.Opacity = 71;

            //Projectile Mechanics.
            Projectile.aiStyle = 0;

            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;

            Projectile.noDropItem = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(232, 86, 7, 0) * Projectile.Opacity;
        }

        public override void AI()
        {
            float maxDetectRadius = 600f;
            float projSpeed = 2.1f;

            Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y - 8f), Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y - 4f), Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.7f, Projectile.velocity.Y * 0.7f);

            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            foreach (var target in Main.ActiveNPCs)
            {
                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            base.OnKill(timeLeft);
        }

        public override void OnSpawn(IEntitySource source)
        {
            Random rand = new Random();

            Projectile.Center = Main.GetPlayerArmPosition(Projectile) + Projectile.velocity;
            Projectile.position = new Vector2(Projectile.position.X, Projectile.position.Y + (float)rand.Next(-24, -8));
            Projectile.velocity = new Vector2((Projectile.velocity.X * ((float)rand.NextDouble() * 2f)) + 0.1f, (Projectile.velocity.Y * ((float)rand.NextDouble() * 2f)) + 0.1f);

            SoundEngine.PlaySound(SoundID.Item20, Projectile.position);

            base.OnSpawn(source);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target?.AddBuff(67, 60 * damageDone); //Burning Effect.

            base.OnHitNPC(target, hit, damageDone);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Player projOwner;
            Projectile.TryGetOwner(out projOwner);

            if (target != projOwner)
            {
                target?.AddBuff(67, 300); //Burning Effect.
            }

            base.OnHitPlayer(target, info);
        }
    }
}