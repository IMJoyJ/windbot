using System;
using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using System.Diagnostics;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using System.Linq;

namespace WindBot.Game.AI.Decks
{
    [Deck("MathMech", "AI_Mathmech")]
    public class MathmechExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int MathmechNebla = 53577438;
            public const int MathmechSigma = 27182739;
            public const int MathmechDivision = 89743495;
            public const int MathmechAddition = 80965043;
            public const int MathmechSubtra = 16360142;
            public const int Mathmechdouble = 52354896;
            public const int MathmechFinalSigma = 42632209;
            public const int Mathmechalem = 85692042;
            public const int MathmechMagma = 15248594;
            public const int BalancerLord = 08567955;
            public const int LightDragon = 61399402;

            // spells
            public const int upstartGoblin = 70368879;
            public const int raigeki = 12580477;
            public const int cynetmining = 57160136;
            public const int  PotOfDesires= 35261759;
            public const int lightningStorm =  14532163;
            public const int cosmicCyclone = 08267140;
            public const int foolishBurial = 81439173;
            public const int OneTimePasscode = 93104632;
            public const int mathmechEquation = 14025912;
            //traps
            public const int threanteningRoar = 36361633;
            //tokens
            public const int securitytoken = 93104633;

        }
        public MathmechExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            this.AddExecutor(ExecutorType.Activate, CardId.raigeki , this.when_raigeki);
            this.AddExecutor(ExecutorType.Activate, CardId.upstartGoblin);
            this.AddExecutor(ExecutorType.Activate, CardId.OneTimePasscode);
            this.AddExecutor(ExecutorType.SpellSet, CardId.threanteningRoar);
            this.AddExecutor(ExecutorType.Activate,CardId.cosmicCyclone , this.when_cosmic);
            this.AddExecutor(ExecutorType.Activate,CardId.lightningStorm , this.lightstorm_target);
            this.AddExecutor(ExecutorType.Activate,CardId.foolishBurial, this.foolish_burial_target);
            this.AddExecutor(ExecutorType.Activate,CardId.mathmechEquation, this.mathmech_equation_target);
            this.AddExecutor(ExecutorType.Activate,CardId.PotOfDesires);


            this.AddExecutor(ExecutorType.Summon, CardId.MathmechNebla);
            this.AddExecutor(ExecutorType.Summon,CardId.BalancerLord );
            this.AddExecutor(ExecutorType.Summon, CardId.Mathmechdouble);
            this.AddExecutor(ExecutorType.Summon, CardId.MathmechSubtra);
            this.AddExecutor(ExecutorType.Summon, CardId.MathmechAddition);
            this.AddExecutor(ExecutorType.Summon, CardId.MathmechDivision);
            this.AddExecutor(ExecutorType.Summon, CardId.MathmechDivision);
            this.AddExecutor(ExecutorType.Activate, CardId.MathmechSigma);
            this.AddExecutor(ExecutorType.Activate,CardId.threanteningRoar);

            //xyz summons
            this.AddExecutor(ExecutorType.SpSummon, CardId.Mathmechalem, this.when_Mathmechalem);
            //xyz effects
            this.AddExecutor(ExecutorType.Activate, CardId.Mathmechalem, this.mathchalenEffect);
            //Synchro
            this.AddExecutor(ExecutorType.SpSummon, CardId.MathmechFinalSigma , this.FinalSigmaSummon);

            this.AddExecutor(ExecutorType.Activate, CardId.Mathmechdouble, this.doubleEffect);

            //normal effects
            this.AddExecutor(ExecutorType.Activate, CardId.MathmechNebla, this.NeblaEffect);
            this.AddExecutor(ExecutorType.Activate,CardId.MathmechDivision , this.divisionEffect);
            this.AddExecutor(ExecutorType.Activate,CardId.BalancerLord , this.active_balancer);
            this.AddExecutor(ExecutorType.Activate, CardId.MathmechSubtra , this.whom_subtra);
            this.AddExecutor(ExecutorType.Activate, CardId.MathmechAddition , this.whom_addition);
            //spell effects
            this.AddExecutor(ExecutorType.Activate, CardId.cynetmining , this.how_to_cynet_mine);
            this.AddExecutor(ExecutorType.SpSummon, CardId.MathmechMagma, this.MagmaSummon);
            this.AddExecutor(ExecutorType.Activate,CardId.MathmechFinalSigma);
            this.AddExecutor(ExecutorType.Activate,CardId.MathmechMagma);
            

            //function
            
        }

        public override bool OnSelectHand()
        {
            return false;
        }
        private bool when_cosmic()
        {
            
            if (this.Enemy.GetSpellCount() > 1)
            {
                this.AI.SelectCard(this.Util.GetBestEnemySpell());
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool divisionEffect()
        {
            if (this.Enemy.GetMonsterCount() > 0)
            {
                this.AI.SelectCard(this.Util.GetBestEnemyMonster(canBeTarget:true,onlyFaceup:true));
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool when_raigeki()
        {
            if (this.Enemy.GetMonsterCount() > 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool whom_addition()
        {
            this.AI.SelectCard(this.Util.GetBestBotMonster(onlyATK:true));
            return true;
        }

        private bool whom_subtra()
        {
            try
            {
                this.AI.SelectCard(this.Util.GetBestEnemyMonster(onlyFaceup: true, canBeTarget: true));
                return true;
            }
            catch (Exception e)
            {
                return true;
            }
        }

        private bool active_balancer()
        {
            if (this.Bot.HasInHand(CardId.MathmechNebla))
            {
                this.AI.SelectCard(CardId.MathmechNebla);
                return true;
            }
            else
            {
                return true;
            }
        }
        private bool lightstorm_target()
        {
            if ((this.Enemy.MonsterZone.ToList().Count > this.Enemy.SpellZone.ToList().Count ) && this.Enemy.MonsterZone.ToList().Count>3)
            {
                this.AI.SelectPlace(Zones.MonsterZones);
                return true;
            }
            else
            {
                this.AI.SelectPlace(Zones.SpellZones);
                return true;
            }

        }

        private bool mathmech_equation_target()
        {
            if (this.Bot.HasInGraveyard(CardId.MathmechNebla))
            {
                this.AI.SelectCard(CardId.MathmechNebla);
                return true;
            }
            else
            {
                this.AI.SelectCard((this.Util.GetBestBotMonster(onlyATK: true)));
                return true;
            }
        }

        private bool foolish_burial_target()
        {
            this.AI.SelectCard(CardId.MathmechNebla);
            return true;
        }
        private bool how_to_cynet_mine()
        {
            this.AI.SelectCard(this.Util.GetWorstBotMonster());
            if (!this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.MathmechSigma))
            {
                this.AI.SelectNextCard(CardId.MathmechSigma);
                return true;
            }
            return true;
        }
        private bool when_Mathmechalem()
        {
            if (this.Bot.HasInMonstersZone(CardId.MathmechNebla)){
                return false;
            }
            else if(this.Bot.HasInMonstersZone(CardId.MathmechSigma) && this.Bot.HasInMonstersZone(CardId.Mathmechdouble))
            {
                return false;
            }
            else if (this.Bot.HasInMonstersZone(CardId.Mathmechalem))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool FinalSigmaSummon()
        {
            if (this.Duel.Turn < 1)
            {
                return false;
            }
            if ((this.Bot.HasInMonstersZone(CardId.Mathmechdouble) && ((this.Bot.HasInMonstersZone(CardId.MathmechSigma)) || this.Bot.HasInMonstersZone(CardId.MathmechNebla))))
            {
                this.AI.SelectPosition(CardPosition.Attack);
                try { this.AI.SelectPlace(Zones.ExtraMonsterZones);  }
                catch { }
                
                return true;
            }
            else
            {
                return true;
            }

        }
        private bool NeblaEffect()
        {
            bool a = this.Bot.HasInMonstersZone(CardId.MathmechSubtra) || this.Bot.HasInMonstersZone(CardId.securitytoken) || this.Bot.HasInMonstersZone(CardId.MathmechSigma) || this.Bot.HasInMonstersZone(CardId.MathmechAddition) || this.Bot.HasInMonstersZone(CardId.Mathmechalem) || this.Bot.HasInMonstersZone(CardId.MathmechDivision);
            if (a)
            {
                List<int> cards = new List<int>
                {
                    CardId.MathmechSigma,
                    CardId.MathmechSubtra,
                    CardId.MathmechAddition
                };
                cards.Add(item:CardId.MathmechDivision);
                cards.Add(item:CardId.Mathmechalem);
                cards.Add(CardId.securitytoken);
                int u = 0;
                List<ClientCard> monsters = this.Bot.GetMonstersInMainZone();
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (cards.Contains(monsters[i].Id))
                    {
                        u = monsters[i].Id;
                        break;
                    }
                    else
                    {
                        u = CardId.securitytoken;
                    }
                }
                this.AI.SelectCard(CardId.securitytoken);
                this.AI.SelectNextCard(CardId.Mathmechdouble);
                return true;


            }
            if (this.Card.Location == CardLocation.Grave)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool doubleEffect()
        {
            if (this.Bot.HasInMonstersZone(CardId.MathmechNebla) || this.Bot.HasInMonstersZone(CardId.MathmechSigma))
            {
                return true;
            };
            if (this.Card.Location == CardLocation.Grave )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        
        private bool mathchalenEffect()

        {
            if (this.Duel.Turn < 1)
            {
                return false;
            }
            if ( (this.Bot.HasInHandOrInGraveyard(CardId.MathmechNebla) &&  !this.Bot.HasInMonstersZone(CardId.MathmechNebla)) && (this.Card.Location == CardLocation.FieldZone && this.Card.HasXyzMaterial(0)) )
            {
                this.AI.SelectCard(CardId.Mathmechalem);
                this.AI.SelectNextCard(CardId.MathmechNebla);
                return true;
            }

            if (this.Bot.HasInHandOrInGraveyard(CardId.Mathmechdouble) &&
                (this.Bot.HasInMonstersZone(CardId.MathmechNebla) || this.Bot.HasInMonstersZone(CardId.MathmechSigma)) &&
                this.Card.Location == CardLocation.FieldZone && this.Card.HasXyzMaterial(0))
            {
                this.AI.SelectCard(CardId.Mathmechalem);
                this.AI.SelectNextCard(CardId.Mathmechdouble);
                return true;
            }
            if (!this.Bot.HasInHandOrInGraveyard(CardId.MathmechNebla) && this.Card.HasXyzMaterial(2))
            {
                this.AI.SelectCard(CardId.MathmechNebla);
                this.AI.SelectThirdCard(CardId.MathmechNebla);
                return true;
            }

            if (!this.Bot.HasInHandOrInGraveyard(CardId.MathmechSigma) && this.Card.HasXyzMaterial(2))
            {
                this.AI.SelectCard(CardId.MathmechSigma);
                this.AI.SelectThirdCard(CardId.MathmechSigma);
                return true;
            }
            else
            {
                return false;
            };
            
            
        }

        private bool MagmaSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.MathmechNebla))
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.MathmechSigma) && this.Bot.HasInMonstersZone(CardId.Mathmechdouble))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override int OnSelectPlace(int cardId, int player, CardLocation location, int available)
        {
            if (cardId == CardId.MathmechFinalSigma)
            {
                if ((Zones.ExtraZone1 & available) > 0)
                {
                    return Zones.ExtraZone1;
                }

                if ((Zones.ExtraZone2 & available) > 0)
                {
                    return Zones.ExtraZone2;
                }
            }
            return base.OnSelectPlace(cardId, player, location, available);
        }

    }

}