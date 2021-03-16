using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("Toadally Awesome", "AI_ToadallyAwesome")]
    public class ToadallyAwesomeExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int CryomancerOfTheIceBarrier = 23950192;
            public const int DewdarkOfTheIceBarrier = 90311614;
            public const int SwapFrog = 9126351;
            public const int PriorOfTheIceBarrier = 50088247;
            public const int Ronintoadin = 1357146;
            public const int DupeFrog = 46239604;
            public const int GraydleSlimeJr = 80250319;

            public const int GalaxyCyclone = 5133471;
            public const int HarpiesFeatherDuster = 18144506;
            public const int Surface = 33057951;
            public const int DarkHole = 53129443;
            public const int CardDestruction = 72892473;
            public const int FoolishBurial = 81439173;
            public const int MonsterReborn = 83764718;
            public const int MedallionOfTheIceBarrier = 84206435;
            public const int Salvage = 96947648;
            public const int AquariumStage = 29047353;

            public const int HeraldOfTheArcLight = 79606837;
            public const int ToadallyAwesome = 90809975;
            public const int SkyCavalryCentaurea = 36776089;
            public const int DaigustoPhoenix = 2766877;
            public const int CatShark = 84224627;

            public const int MysticalSpaceTyphoon = 5318639;
            public const int BookOfMoon = 14087893;
            public const int CallOfTheHaunted = 97077563;
            public const int TorrentialTribute = 53582587;
        }

        public ToadallyAwesomeExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster, this.DefaultHarpiesFeatherDusterFirst);
            this.AddExecutor(ExecutorType.Activate, CardId.GalaxyCyclone, this.DefaultGalaxyCyclone);
            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);

            this.AddExecutor(ExecutorType.Activate, CardId.AquariumStage, this.AquariumStageEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.MedallionOfTheIceBarrier, this.MedallionOfTheIceBarrierEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.FoolishBurial, this.FoolishBurialEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.PriorOfTheIceBarrier);
            this.AddExecutor(ExecutorType.Summon, CardId.GraydleSlimeJr, this.GraydleSlimeJrSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.SwapFrog, this.SwapFrogSpsummon);

            this.AddExecutor(ExecutorType.Activate, CardId.SwapFrog, this.SwapFrogEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.GraydleSlimeJr, this.GraydleSlimeJrEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Ronintoadin, this.RonintoadinEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.PriorOfTheIceBarrier);
            this.AddExecutor(ExecutorType.Activate, CardId.DupeFrog);

            this.AddExecutor(ExecutorType.Activate, CardId.Surface, this.SurfaceEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.MonsterReborn, this.SurfaceEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Salvage, this.SalvageEffect);

            this.AddExecutor(ExecutorType.Summon, CardId.SwapFrog);
            this.AddExecutor(ExecutorType.Summon, CardId.DewdarkOfTheIceBarrier, this.IceBarrierSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.CryomancerOfTheIceBarrier, this.IceBarrierSummon);

            this.AddExecutor(ExecutorType.Activate, CardId.CardDestruction);

            this.AddExecutor(ExecutorType.Summon, CardId.GraydleSlimeJr, this.NormalSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.PriorOfTheIceBarrier, this.NormalSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.Ronintoadin, this.NormalSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.DupeFrog, this.NormalSummon);
            this.AddExecutor(ExecutorType.Summon, CardId.PriorOfTheIceBarrier, this.PriorOfTheIceBarrierSummon);

            this.AddExecutor(ExecutorType.SpSummon, CardId.CatShark, this.CatSharkSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.CatShark, this.CatSharkEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.SkyCavalryCentaurea, this.SkyCavalryCentaureaSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.SkyCavalryCentaurea);
            this.AddExecutor(ExecutorType.SpSummon, CardId.DaigustoPhoenix, this.DaigustoPhoenixSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.DaigustoPhoenix);
            this.AddExecutor(ExecutorType.SpSummon, CardId.ToadallyAwesome);
            this.AddExecutor(ExecutorType.Activate, CardId.ToadallyAwesome, this.ToadallyAwesomeEffect);
            this.AddExecutor(ExecutorType.SpSummon, CardId.HeraldOfTheArcLight, this.HeraldOfTheArcLightSummon);
            this.AddExecutor(ExecutorType.Activate, CardId.HeraldOfTheArcLight);

            this.AddExecutor(ExecutorType.MonsterSet, CardId.GraydleSlimeJr);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.DupeFrog);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.Ronintoadin);

            this.AddExecutor(ExecutorType.Repos, this.Repos);

            // cards got by Toadally Awesome
            this.AddExecutor(ExecutorType.Activate, CardId.MysticalSpaceTyphoon, this.DefaultMysticalSpaceTyphoon);
            this.AddExecutor(ExecutorType.Activate, CardId.BookOfMoon, this.DefaultBookOfMoon);
            this.AddExecutor(ExecutorType.Activate, CardId.CallOfTheHaunted, this.SurfaceEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.TorrentialTribute, this.DefaultTorrentialTribute);
            this.AddExecutor(ExecutorType.Activate, this.OtherSpellEffect);
            this.AddExecutor(ExecutorType.Activate, this.OtherTrapEffect);
            this.AddExecutor(ExecutorType.Activate, this.OtherMonsterEffect);
        }

        public override bool OnSelectHand()
        {
            return true;
        }

        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (!defender.IsMonsterHasPreventActivationEffectInBattle())
            {
                if (attacker.IsCode(CardId.SkyCavalryCentaurea) && !attacker.IsDisabled() && attacker.HasXyzMaterial())
                {
                    attacker.RealPower = this.Bot.LifePoints + attacker.Attack;
                }
            }
            return base.OnPreBattleBetween(attacker, defender);
        }

        private bool MedallionOfTheIceBarrierEffect()
        {
            if (this.Bot.HasInHand(new[]
                {
                    CardId.CryomancerOfTheIceBarrier,
                    CardId.DewdarkOfTheIceBarrier
                }) || this.Bot.HasInMonstersZone(new[]
                {
                    CardId.CryomancerOfTheIceBarrier,
                    CardId.DewdarkOfTheIceBarrier
                }))
            {
                this.AI.SelectCard(CardId.PriorOfTheIceBarrier);
            }
            else
            {
                this.AI.SelectCard(
                    CardId.CryomancerOfTheIceBarrier,
                    CardId.DewdarkOfTheIceBarrier
                    );
            }
            return true;
        }

        private bool SurfaceEffect()
        {
            this.AI.SelectCard(
                CardId.ToadallyAwesome,
                CardId.HeraldOfTheArcLight,
                CardId.SwapFrog,
                CardId.DewdarkOfTheIceBarrier,
                CardId.CryomancerOfTheIceBarrier,
                CardId.DupeFrog,
                CardId.Ronintoadin,
                CardId.GraydleSlimeJr
                );
            return true;
        }

        private bool AquariumStageEffect()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return this.SurfaceEffect();
            }
            return true;
        }

        private bool FoolishBurialEffect()
        {
            if (this.Bot.HasInHand(CardId.GraydleSlimeJr) && !this.Bot.HasInGraveyard(CardId.GraydleSlimeJr))
            {
                this.AI.SelectCard(CardId.GraydleSlimeJr);
            }
            else if (this.Bot.HasInGraveyard(CardId.Ronintoadin) && !this.Bot.HasInGraveyard(CardId.DupeFrog))
            {
                this.AI.SelectCard(CardId.DupeFrog);
            }
            else if (this.Bot.HasInGraveyard(CardId.DupeFrog) && !this.Bot.HasInGraveyard(CardId.Ronintoadin))
            {
                this.AI.SelectCard(CardId.Ronintoadin);
            }
            else
            {
                this.AI.SelectCard(
                    CardId.GraydleSlimeJr,
                    CardId.Ronintoadin,
                    CardId.DupeFrog,
                    CardId.CryomancerOfTheIceBarrier,
                    CardId.DewdarkOfTheIceBarrier,
                    CardId.PriorOfTheIceBarrier,
                    CardId.SwapFrog
                    );
            }

            return true;
        }

        private bool SalvageEffect()
        {
            this.AI.SelectCard(
                CardId.SwapFrog,
                CardId.PriorOfTheIceBarrier,
                CardId.GraydleSlimeJr
                );
            return true;
        }

        private bool SwapFrogSpsummon()
        {
            if (this.Bot.GetCountCardInZone(this.Bot.Hand, CardId.GraydleSlimeJr)>=2 && !this.Bot.HasInGraveyard(CardId.GraydleSlimeJr))
            {
                this.AI.SelectCard(CardId.GraydleSlimeJr);
            }
            else if (this.Bot.HasInGraveyard(CardId.Ronintoadin) && !this.Bot.HasInGraveyard(CardId.DupeFrog))
            {
                this.AI.SelectCard(CardId.DupeFrog);
            }
            else if (this.Bot.HasInGraveyard(CardId.DupeFrog) && !this.Bot.HasInGraveyard(CardId.Ronintoadin))
            {
                this.AI.SelectCard(CardId.Ronintoadin);
            }
            else
            {
                this.AI.SelectCard(
                    CardId.Ronintoadin,
                    CardId.DupeFrog,
                    CardId.CryomancerOfTheIceBarrier,
                    CardId.DewdarkOfTheIceBarrier,
                    CardId.PriorOfTheIceBarrier,
                    CardId.GraydleSlimeJr,
                    CardId.SwapFrog
                    );
            }

            return true;
        }

        private bool SwapFrogEffect()
        {
            if (this.ActivateDescription == -1)
            {
                return this.FoolishBurialEffect();
            }
            else
            {
                if (this.Bot.HasInHand(CardId.DupeFrog))
                {
                    this.AI.SelectCard(
                        CardId.PriorOfTheIceBarrier,
                        CardId.GraydleSlimeJr,
                        CardId.SwapFrog
                        );
                    return true;
                }
            }
            return false;
        }

        private bool GraydleSlimeJrSummon()
        {
            return this.Bot.HasInGraveyard(CardId.GraydleSlimeJr);
        }

        private bool GraydleSlimeJrEffect()
        {
            this.AI.SelectCard(CardId.GraydleSlimeJr);
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            this.AI.SelectNextCard(
                CardId.SwapFrog,
                CardId.CryomancerOfTheIceBarrier,
                CardId.DewdarkOfTheIceBarrier,
                CardId.Ronintoadin,
                CardId.DupeFrog,
                CardId.PriorOfTheIceBarrier,
                CardId.GraydleSlimeJr
                );
            return true;
        }

        private bool RonintoadinEffect()
        {
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }

        private bool NormalSummon()
        {
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.Level==2)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IceBarrierSummon()
        {
            return this.Bot.GetCountCardInZone(this.Bot.Hand, CardId.PriorOfTheIceBarrier) > 0;
        }

        private bool PriorOfTheIceBarrierSummon()
        {
            return this.Bot.GetCountCardInZone(this.Bot.Hand, CardId.PriorOfTheIceBarrier) >= 2;
        }

        private bool ToadallyAwesomeEffect()
        {
            if (this.Duel.CurrentChain.Count > 0)
            {
                // negate effect, select a cost for it
                List<ClientCard> monsters = this.Bot.GetMonsters();
                IList<int> suitableCost = new[] {
                    CardId.SwapFrog,
                    CardId.Ronintoadin,
                    CardId.GraydleSlimeJr,
                    CardId.CryomancerOfTheIceBarrier,
                    CardId.DewdarkOfTheIceBarrier
                };
                foreach (ClientCard monster in monsters)
                {
                    if (monster.IsCode(suitableCost))
                    {
                        this.AI.SelectCard(monster);
                        return true;
                    }
                }
                if (!this.Bot.HasInSpellZone(CardId.AquariumStage, true))
                {
                    foreach (ClientCard monster in monsters)
                    {
                        if (monster.IsCode(CardId.DupeFrog))
                        {
                            this.AI.SelectCard(monster);
                            return true;
                        }
                    }
                }
                List<ClientCard> hands = this.Bot.Hand.GetMonsters();
                if (this.Bot.GetCountCardInZone(this.Bot.Hand, CardId.GraydleSlimeJr) >= 2)
                {
                    foreach (ClientCard monster in hands)
                    {
                        if (monster.IsCode(CardId.GraydleSlimeJr))
                        {
                            this.AI.SelectCard(monster);
                            return true;
                        }
                    }
                }
                if (this.Bot.HasInGraveyard(CardId.Ronintoadin) && !this.Bot.HasInGraveyard(CardId.DupeFrog) && !this.Bot.HasInGraveyard(CardId.SwapFrog))
                {
                    foreach (ClientCard monster in hands)
                    {
                        if (monster.IsCode(CardId.DupeFrog))
                        {
                            this.AI.SelectCard(monster);
                            return true;
                        }
                    }
                }
                foreach (ClientCard monster in hands)
                {
                    if (monster.IsCode(CardId.Ronintoadin, CardId.DupeFrog))
                    {
                        this.AI.SelectCard(monster);
                        return true;
                    }
                }
                foreach (ClientCard monster in hands)
                {
                    this.AI.SelectCard(monster);
                    return true;
                }
                return true;
            }
            else if (this.Card.Location == CardLocation.Grave)
            {
                if (!this.Bot.HasInExtra(CardId.ToadallyAwesome))
                {
                    this.AI.SelectCard(CardId.ToadallyAwesome);
                }
                else
                {
                    this.AI.SelectCard(
                        CardId.SwapFrog,
                        CardId.PriorOfTheIceBarrier,
                        CardId.GraydleSlimeJr
                        );
                }
                return true;
            }
            else if (this.Duel.Phase == DuelPhase.Standby)
            {
                this.SelectXYZDetach(this.Card.Overlays);
                if (this.Duel.Player == 0)
                {
                    this.AI.SelectNextCard(
                        CardId.SwapFrog,
                        CardId.CryomancerOfTheIceBarrier,
                        CardId.DewdarkOfTheIceBarrier,
                        CardId.Ronintoadin,
                        CardId.DupeFrog,
                        CardId.GraydleSlimeJr
                        );
                }
                else
                {
                    this.AI.SelectNextCard(
                        CardId.DupeFrog,
                        CardId.SwapFrog,
                        CardId.Ronintoadin,
                        CardId.GraydleSlimeJr,
                        CardId.CryomancerOfTheIceBarrier,
                        CardId.DewdarkOfTheIceBarrier
                        );
                    this.AI.SelectPosition(CardPosition.FaceUpDefence);
                }
                return true;
            }
            return true;
        }

        private bool CatSharkSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.ToadallyAwesome)
                && ((this.Util.IsOneEnemyBetter(true)
                    && !this.Bot.HasInMonstersZone(new[]
                        {
                            CardId.CatShark,
                            CardId.SkyCavalryCentaurea
                        }, true, true))
                    || !this.Bot.HasInExtra(CardId.ToadallyAwesome)))
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;
        }

        private bool CatSharkEffect()
        {
            List<ClientCard> monsters = this.Bot.GetMonsters();
            foreach (ClientCard monster in monsters)
            {
                if (monster.IsCode(CardId.ToadallyAwesome) && monster.Attack <= 2200)
                {
                    this.SelectXYZDetach(this.Card.Overlays);
                    this.AI.SelectNextCard(monster);
                    return true;
                }
            }
            foreach (ClientCard monster in monsters)
            {
                if (monster.IsCode(CardId.SkyCavalryCentaurea) && monster.Attack <= 2000)
                {
                    this.SelectXYZDetach(this.Card.Overlays);
                    this.AI.SelectNextCard(monster);
                    return true;
                }
            }
            foreach (ClientCard monster in monsters)
            {
                if (monster.IsCode(CardId.DaigustoPhoenix) && monster.Attack <= 1500)
                {
                    this.SelectXYZDetach(this.Card.Overlays);
                    this.AI.SelectNextCard(monster);
                    return true;
                }
            }
            return false;
        }

        private bool SkyCavalryCentaureaSummon()
        {
            int num = 0;
            foreach (ClientCard monster in this.Bot.GetMonsters())
            {
                if (monster.Level ==2)
                {
                    num++;
                }
            }
            return this.Util.IsOneEnemyBetter(true)
                   && this.Util.GetBestAttack(this.Enemy) > 2200
                   && num < 4
                   && !this.Bot.HasInMonstersZone(new[]
                        {
                            CardId.SkyCavalryCentaurea
                        }, true, true);
        }

        private bool DaigustoPhoenixSummon()
        {
            if (this.Duel.Turn != 1)
            {
                int attack = 0;
                int defence = 0;
                foreach (ClientCard monster in this.Bot.GetMonsters())
                {
                    if (!monster.IsDefense())
                    {
                        attack += monster.Attack;
                    }
                }
                foreach (ClientCard monster in this.Enemy.GetMonsters())
                {
                    defence += monster.GetDefensePower();
                }
                if (attack - 2000 - defence > this.Enemy.LifePoints && !this.Util.IsOneEnemyBetter(true))
                {
                    return true;
                }
            }
            return false;
        }

        private bool HeraldOfTheArcLightSummon()
        {
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }

        private bool Repos()
        {
            if (this.Card.IsFacedown())
            {
                return true;
            }

            if (this.Card.IsDefense() && !this.Util.IsAllEnemyBetter(true) && this.Card.Attack >= this.Card.Defense)
            {
                return true;
            }

            return false;
        }

        private bool OtherSpellEffect()
        {
            foreach (CardExecutor exec in this.Executors)
            {
                if (exec.Type == this.ExecType && exec.CardId == this.Card.Id)
                {
                    return false;
                }
            }
            return this.Card.IsSpell();
        }

        private bool OtherTrapEffect()
        {
            foreach (CardExecutor exec in this.Executors)
            {
                if (exec.Type == this.ExecType && exec.CardId == this.Card.Id)
                {
                    return false;
                }
            }
            return this.Card.IsTrap() && this.DefaultTrap();
        }

        private bool OtherMonsterEffect()
        {
            foreach (CardExecutor exec in this.Executors)
            {
                if (exec.Type == this.ExecType && exec.CardId == this.Card.Id)
                {
                    return false;
                }
            }
            return this.Card.IsMonster();
        }

        private void SelectXYZDetach(List<int> Overlays)
        {
            if (Overlays.Contains(CardId.GraydleSlimeJr) && this.Bot.HasInHand(CardId.GraydleSlimeJr) && !this.Bot.HasInGraveyard(CardId.GraydleSlimeJr))
            {
                this.AI.SelectCard(CardId.GraydleSlimeJr);
            }
            else if (Overlays.Contains(CardId.DupeFrog) && this.Bot.HasInGraveyard(CardId.Ronintoadin) && !this.Bot.HasInGraveyard(CardId.DupeFrog))
            {
                this.AI.SelectCard(CardId.DupeFrog);
            }
            else if (Overlays.Contains(CardId.Ronintoadin) && this.Bot.HasInGraveyard(CardId.DupeFrog) && !this.Bot.HasInGraveyard(CardId.Ronintoadin))
            {
                this.AI.SelectCard(CardId.Ronintoadin);
            }
            else
            {
                this.AI.SelectCard(
                    CardId.GraydleSlimeJr,
                    CardId.Ronintoadin,
                    CardId.DupeFrog,
                    CardId.CryomancerOfTheIceBarrier,
                    CardId.DewdarkOfTheIceBarrier,
                    CardId.PriorOfTheIceBarrier,
                    CardId.SwapFrog
                    );
            }
        }
    }
}
