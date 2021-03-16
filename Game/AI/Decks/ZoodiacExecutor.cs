using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("Zoodiac", "AI_Zoodiac")]
    class ZoodiacExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int JizukirutheStarDestroyingKaiju = 63941210;
            public const int GadarlatheMysteryDustKaiju = 36956512;
            public const int GamecieltheSeaTurtleKaiju = 55063751;
            public const int RadiantheMultidimensionalKaiju = 28674152;
            public const int KumongoustheStickyStringKaiju = 29726552;
            public const int PhotonThrasher = 65367484;
            public const int Thoroughblade = 77150143;
            public const int Whiptail = 31755044;
            public const int Ratpier = 78872731;
            public const int AleisterTheInvoker = 86120751;

            public const int HarpiesFeatherDuster = 18144506;
            public const int DarkHole = 53129443;
            public const int Terraforming = 73628505;
            public const int Invocation = 74063034;
            public const int MonsterReborn = 83764718;
            public const int InterruptedKaijuSlumber = 99330325;
            public const int ZoodiacBarrage = 46060017;
            public const int FireFormationTenki = 57103969;
            public const int MagicalMeltdown = 47679935;
            public const int ZoodiacCombo = 73881652;

            public const int InvokedMechaba = 75286621;
            public const int InvokedMagellanica = 48791583;
            public const int NumberS39UtopiatheLightning = 56832966;
            public const int Number39Utopia = 84013237;
            public const int DaigustoEmeral = 581014;
            public const int Tigermortar = 11510448;
            public const int Chakanine = 41375811;
            public const int Drident = 48905153;
            public const int Broadbull = 85115440;
        }

        bool TigermortarSpsummoned = false;
        bool ChakanineSpsummoned = false;
        bool BroadbullSpsummoned = false;
        int WhiptailEffectCount = 0;

        public ZoodiacExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // Quick spells
            this.AddExecutor(ExecutorType.Activate, CardId.HarpiesFeatherDuster);
            this.AddExecutor(ExecutorType.Activate, CardId.InterruptedKaijuSlumber, this.DefaultInterruptedKaijuSlumber);
            this.AddExecutor(ExecutorType.Activate, CardId.DarkHole, this.DefaultDarkHole);

            this.AddExecutor(ExecutorType.SpSummon, CardId.GamecieltheSeaTurtleKaiju, this.DefaultKaijuSpsummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.KumongoustheStickyStringKaiju, this.DefaultKaijuSpsummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.GadarlatheMysteryDustKaiju, this.DefaultKaijuSpsummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.RadiantheMultidimensionalKaiju, this.DefaultKaijuSpsummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.JizukirutheStarDestroyingKaiju, this.DefaultKaijuSpsummon);

            this.AddExecutor(ExecutorType.Activate, CardId.Terraforming);
            this.AddExecutor(ExecutorType.Activate, CardId.MagicalMeltdown);
            this.AddExecutor(ExecutorType.Activate, CardId.FireFormationTenki, this.FireFormationTenkiEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.ZoodiacBarrage, this.ZoodiacBarrageEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.DaigustoEmeral, this.DaigustoEmeralEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.PhotonThrasher, this.PhotonThrasherSummon);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Number39Utopia, this.DefaultNumberS39UtopiaTheLightningSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.NumberS39UtopiatheLightning);
            this.AddExecutor(ExecutorType.Activate, CardId.NumberS39UtopiatheLightning, this.DefaultNumberS39UtopiaTheLightningEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.InvokedMechaba, this.DefaultTrap);

            this.AddExecutor(ExecutorType.Activate, this.RatpierMaterialEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.Drident, this.DridentEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Broadbull, this.BroadbullEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Tigermortar, this.TigermortarEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.Chakanine, this.ChakanineEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Chakanine, this.ChakanineSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Tigermortar, this.TigermortarSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Broadbull, this.BroadbullSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.Drident, this.DridentSummon);

            this.AddExecutor(ExecutorType.Summon, CardId.Ratpier);
            this.AddExecutor(ExecutorType.Activate, CardId.Ratpier, this.RatpierEffect);
            this.AddExecutor(ExecutorType.Summon, CardId.Thoroughblade);
            this.AddExecutor(ExecutorType.Activate, CardId.Thoroughblade, this.RatpierEffect);
            this.AddExecutor(ExecutorType.Summon, CardId.AleisterTheInvoker);
            this.AddExecutor(ExecutorType.Activate, CardId.AleisterTheInvoker, this.AleisterTheInvokerEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.DaigustoEmeral, this.DaigustoEmeralSummon);

            this.AddExecutor(ExecutorType.SpSummon, CardId.Broadbull, this.BroadbullXYZSummon);

            this.AddExecutor(ExecutorType.Activate, CardId.MonsterReborn, this.MonsterRebornEffect);

            this.AddExecutor(ExecutorType.SpSummon, CardId.PhotonThrasher);
            this.AddExecutor(ExecutorType.Summon, CardId.Whiptail);

            this.AddExecutor(ExecutorType.Activate, CardId.Invocation, this.InvocationEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.Whiptail, this.WhiptailEffect);

            this.AddExecutor(ExecutorType.Activate, CardId.ZoodiacCombo, this.ZoodiacComboEffect);

            this.AddExecutor(ExecutorType.SpellSet, CardId.ZoodiacCombo);

            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);
        }

        public override bool OnSelectHand()
        {
            // go first
            return true;
        }

        public override void OnNewTurn()
        {
            // reset
            this.TigermortarSpsummoned = false;
            this.ChakanineSpsummoned = false;
            this.BroadbullSpsummoned = false;
            this.WhiptailEffectCount = 0;
        }

        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (!defender.IsMonsterHasPreventActivationEffectInBattle())
            {
                if (attacker.HasType(CardType.Fusion) && this.Bot.HasInHand(CardId.AleisterTheInvoker))
                {
                    attacker.RealPower = attacker.RealPower + 1000;
                }
            }
            return base.OnPreBattleBetween(attacker, defender);
        }

        private bool PhotonThrasherSummon()
        {
            return this.Bot.HasInHand(CardId.AleisterTheInvoker)
                && !this.Bot.HasInHand(CardId.Ratpier)
                && !this.Bot.HasInHand(CardId.Thoroughblade);
        }

        private bool AleisterTheInvokerEffect()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                if (!(this.Duel.Phase == DuelPhase.BattleStep
                    || this.Duel.Phase == DuelPhase.BattleStart
                    || this.Duel.Phase == DuelPhase.Damage))
                {
                    return false;
                }

                return this.Duel.Player==0
                    || this.Util.IsOneEnemyBetter();
            }
            return true;
        }

        private bool InvocationEffect()
        {
            if (this.Card.Location == CardLocation.Grave)
            {
                return true;
            }

            IList<ClientCard> materials0 = this.Bot.Graveyard;
            IList<ClientCard> materials1 = this.Enemy.Graveyard;
            IList<ClientCard> mats = new List<ClientCard>();
            ClientCard aleister = this.GetAleisterInGrave();
            if (aleister != null)
            {
                mats.Add(aleister);
            }
            ClientCard mat = null;
            foreach (ClientCard card in materials0)
            {
                if (card.HasAttribute(CardAttribute.Light))
                {
                    mat = card;
                    break;
                }
            }
            foreach (ClientCard card in materials1)
            {
                if (card.HasAttribute(CardAttribute.Light))
                {
                    mat = card;
                    break;
                }
            }
            if (mat != null)
            {
                mats.Add(mat);
                this.AI.SelectCard(CardId.InvokedMechaba);
                this.AI.SelectMaterials(mats);
                this.AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
            }
            foreach (ClientCard card in materials0)
            {
                if (card.HasAttribute(CardAttribute.Earth))
                {
                    mat = card;
                    break;
                }
            }
            foreach (ClientCard card in materials1)
            {
                if (card.HasAttribute(CardAttribute.Earth))
                {
                    mat = card;
                    break;
                }
            }
            if (mat != null)
            {
                mats.Add(mat);
                this.AI.SelectCard(CardId.InvokedMagellanica);
                this.AI.SelectMaterials(mats);
                this.AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
            }
            return false;
        }

        private ClientCard GetAleisterInGrave()
        {
            foreach (ClientCard card in this.Enemy.Graveyard)
            {
                if (card.IsCode(CardId.AleisterTheInvoker))
                {
                    return card;
                }
            }
            foreach (ClientCard card in this.Bot.Graveyard)
            {
                if (card.IsCode(CardId.AleisterTheInvoker))
                {
                    return card;
                }
            }
            return null;
        }

        private bool ChakanineSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.Ratpier) && !this.ChakanineSpsummoned)
            {
                this.AI.SelectMaterials(CardId.Ratpier);
                this.AI.SelectYesNo(true);
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                this.ChakanineSpsummoned = true;
                return true;
            }
            if (this.Bot.HasInMonstersZone(CardId.Broadbull) && !this.ChakanineSpsummoned)
            {
                this.AI.SelectMaterials(CardId.Broadbull);
                this.AI.SelectYesNo(true);
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                this.ChakanineSpsummoned = true;
                return true;
            }
            return false;
        }

        private bool ChakanineEffect()
        {
            if (this.Bot.HasInGraveyard(CardId.Whiptail) || this.Bot.HasInGraveyard(CardId.Thoroughblade))
            {
                this.AI.SelectCard(
                    CardId.Broadbull,
                    CardId.Tigermortar,
                    CardId.Chakanine,
                    CardId.Thoroughblade,
                    CardId.Ratpier,
                    CardId.Whiptail
                    );
                this.AI.SelectNextCard(
                    CardId.Whiptail,
                    CardId.Thoroughblade
                    );
                return true;
            }
            return false;
        }

        private bool TigermortarSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.Chakanine) && !this.TigermortarSpsummoned)
            {
                this.AI.SelectMaterials(CardId.Chakanine);
                this.AI.SelectYesNo(true);
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                this.TigermortarSpsummoned = true;
                return true;
            }
            if (this.Bot.HasInMonstersZone(CardId.Ratpier) && !this.TigermortarSpsummoned)
            {
                this.AI.SelectMaterials(CardId.Ratpier);
                this.AI.SelectYesNo(true);
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                this.TigermortarSpsummoned = true;
                return true;
            }
            if (this.Bot.HasInMonstersZone(CardId.Thoroughblade) && !this.TigermortarSpsummoned
                && this.Bot.HasInGraveyard(new[]
                {
                    CardId.Whiptail,
                    CardId.Ratpier
                }))
            {
                this.AI.SelectMaterials(CardId.Thoroughblade);
                this.AI.SelectYesNo(true);
                this.TigermortarSpsummoned = true;
                return true;
            }
            if (this.Bot.HasInMonstersZone(CardId.Whiptail) && !this.TigermortarSpsummoned
                && this.Bot.HasInGraveyard(CardId.Ratpier))
            {
                this.AI.SelectMaterials(CardId.Whiptail);
                this.AI.SelectYesNo(true);
                this.TigermortarSpsummoned = true;
                return true;
            }
            return false;
        }

        private bool TigermortarEffect()
        {
            //if (Card.HasXyzMaterial(CardId.Ratpier) || !Bot.HasInGraveyard(CardId.Ratpier))
            //    return false;
            this.AI.SelectCard(CardId.Chakanine);
            this.AI.SelectNextCard(CardId.Tigermortar);
            this.AI.SelectThirdCard(CardId.Ratpier, CardId.Whiptail, CardId.Thoroughblade);
            return true;
        }

        private bool BroadbullSummon()
        {
            if (this.Bot.HasInMonstersZone(CardId.Tigermortar) && !this.BroadbullSpsummoned)
            {
                this.AI.SelectMaterials(CardId.Tigermortar);
                this.AI.SelectYesNo(true);
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                this.BroadbullSpsummoned = true;
                return true;
            }
            if (this.Bot.HasInMonstersZone(CardId.Chakanine) && !this.BroadbullSpsummoned)
            {
                this.AI.SelectMaterials(CardId.Chakanine);
                this.AI.SelectYesNo(true);
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                this.BroadbullSpsummoned = true;
                return true;
            }
            if (this.Bot.HasInMonstersZone(CardId.Ratpier) && !this.BroadbullSpsummoned)
            {
                this.AI.SelectMaterials(CardId.Ratpier);
                this.AI.SelectYesNo(true);
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                this.BroadbullSpsummoned = true;
                return true;
            }
            if (this.Bot.HasInMonstersZone(CardId.Thoroughblade) && !this.BroadbullSpsummoned)
            {
                this.AI.SelectMaterials(CardId.Thoroughblade);
                this.AI.SelectYesNo(true);
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                this.BroadbullSpsummoned = true;
                return true;
            }
            return false;
        }

        private bool BroadbullEffect()
        {
            this.AI.SelectCard(
                CardId.Tigermortar,
                CardId.Chakanine,
                CardId.Drident,
                CardId.AleisterTheInvoker,
                CardId.PhotonThrasher
                );
            if (this.Bot.HasInHand(CardId.Whiptail) && !this.Bot.HasInHand(CardId.Ratpier))
            {
                this.AI.SelectNextCard(CardId.Ratpier);
            }
            else
            {
                this.AI.SelectNextCard(CardId.Whiptail);
            }

            return true;
        }

        private bool BroadbullXYZSummon()
        {
            this.AI.SelectYesNo(false);
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            this.AI.SelectMaterials(new[]
                {
                    CardId.Ratpier,
                    CardId.PhotonThrasher,
                    CardId.Whiptail,
                    CardId.AleisterTheInvoker
                });
            return true;
        }

        private bool DridentSummon()
        {
            this.AI.SelectMaterials(new[]
                {
                    CardId.Broadbull,
                    CardId.Tigermortar,
                    CardId.Chakanine,
                    CardId.Thoroughblade
                });
            return true;
        }

        private bool RatpierMaterialEffect()
        {
            if (this.ActivateDescription == this.Util.GetStringId(CardId.Ratpier, 1))
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;
        }

        private bool WhiptailEffect()
        {
            if (this.Duel.Phase == DuelPhase.Main1 || this.Duel.Phase == DuelPhase.Main2)
            {
                return false;
            }

            if (this.Card.IsDisabled() || this.WhiptailEffectCount >= 3)
            {
                return false;
            }

            ClientCard target = null;
            List<ClientCard> monsters = this.Bot.GetMonsters();
            foreach (ClientCard monster in monsters)
            {
                if (monster.IsFaceup() && monster.IsCode(CardId.Drident) && !monster.HasXyzMaterial())
                {
                    target = monster;
                    break;
                }
            }
            /*if (target == null)
            {
                foreach (ClientCard monster in monsters)
                {
                    if (monster.IsFaceup() && monster.Type == (int)CardType.Xyz && !monster.IsCode(CardId.DaigustoEmeral) && !monster.HasXyzMaterial())
                    {
                        target = monster;
                        break;
                    }
                }
            }*/
            if (target == null)
            {
                this.AI.SelectCard(new[]
                    {
                        CardId.Drident
                    });
            }
            this.WhiptailEffectCount++;
            return true;
        }

        private bool RatpierEffect()
        {
            this.AI.SelectCard(
                CardId.ZoodiacCombo,
                CardId.Thoroughblade,
                CardId.ZoodiacBarrage
                );
            return true;
        }

        private bool DridentEffect()
        {
            if (this.Duel.LastChainPlayer == 0)
            {
                return false;
            }

            ClientCard target = this.Util.GetBestEnemyCard(true);
            if (target == null)
            {
                return false;
            }

            this.AI.SelectCard(
                CardId.Broadbull,
                CardId.Tigermortar,
                CardId.Chakanine,
                CardId.Thoroughblade,
                CardId.Ratpier,
                CardId.Whiptail
                );
            this.AI.SelectNextCard(target);
            return true;
        }

        private bool DaigustoEmeralSummon()
        {
            this.AI.SelectMaterials(new[]
                {
                    CardId.PhotonThrasher,
                    CardId.AleisterTheInvoker
                });
            return this.Bot.GetGraveyardMonsters().Count >= 3;
        }

        private bool DaigustoEmeralEffect()
        {
            this.AI.SelectCard(
                CardId.Ratpier,
                CardId.AleisterTheInvoker,
                CardId.Whiptail
                );
            this.AI.SelectNextCard(
                CardId.Ratpier,
                CardId.DaigustoEmeral
                );
            return true;
        }

        private bool FireFormationTenkiEffect()
        {
            if (this.Bot.HasInHand(CardId.ZoodiacBarrage)
               || this.Bot.HasInSpellZone(CardId.ZoodiacBarrage)
               || this.Bot.HasInHand(CardId.Ratpier))
            {
                this.AI.SelectCard(CardId.Whiptail);
            }
            else
            {
                this.AI.SelectCard(CardId.Ratpier);
            }
            this.AI.SelectYesNo(true);
            return true;
        }

        private bool ZoodiacBarrageEffect()
        {
            foreach (ClientCard spell in this.Bot.GetSpells())
            {
                if (spell.IsCode(CardId.ZoodiacBarrage) && !this.Card.Equals(spell))
                {
                    return false;
                }
            }
            this.AI.SelectCard(
                CardId.FireFormationTenki,
                CardId.MagicalMeltdown,
                CardId.ZoodiacBarrage
                );
            this.AI.SelectNextCard(
                CardId.Ratpier,
                CardId.Whiptail,
                CardId.Thoroughblade
                );
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }

        private bool ZoodiacComboEffect()
        {
            if (this.Duel.CurrentChain.Count > 0)
            {
                return false;
            }

            if (this.Card.Location != CardLocation.Grave)
            {
                this.AI.SelectCard(CardId.Drident);
                this.AI.SelectNextCard(
                    CardId.Whiptail,
                    CardId.Ratpier,
                    CardId.Thoroughblade
                    );
            }
            return true;
        }

        private bool MonsterRebornEffect()
        {
            this.AI.SelectCard(
                CardId.Ratpier,
                CardId.Whiptail,
                CardId.InvokedMechaba,
                CardId.JizukirutheStarDestroyingKaiju,
                CardId.InvokedMagellanica,
                CardId.Tigermortar,
                CardId.Chakanine,
                CardId.Broadbull
                );
            return true;
        }

        private bool MonsterRepos()
        {
            if (this.Card.IsCode(CardId.NumberS39UtopiatheLightning) && this.Card.IsAttack())
            {
                return false;
            }

            return base.DefaultMonsterRepos();
        }
    }
}
