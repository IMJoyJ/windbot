using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
    [Deck("Dragunity", "AI_Dragunity")]
    public class DragunityExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int StardustDragonAssaultMode = 61257789;
            public const int DragunityArmaMysletainn = 876330;
            public const int AssaultBeast = 3431737;
            public const int DragunityDux = 28183605;
            public const int DragunityPhalanx = 59755122;
            public const int AssaultTeleport = 29863101;
            public const int CardsOfConsonance = 39701395;
            public const int UpstartGoblin = 70368879;
            public const int DragonsMirror = 71490127;
            public const int Terraforming = 73628505;
            public const int FoolishBurial = 81439173;
            public const int MonsterReborn = 83764718;
            public const int MysticalSpaceTyphoon = 5318639;
            public const int FireFormationTenki = 57103969;
            public const int DragunitySpearOfDestiny = 60004971;
            public const int DragonRavine = 62265044;
            public const int MirrorForce = 44095762;
            public const int StarlightRoad = 58120309;
            public const int DimensionalPrison = 70342110;
            public const int AssaultModeActivate = 80280737;
            public const int FiveHeadedDragon = 99267150;
            public const int CrystalWingSynchroDragon = 50954680;
            public const int ScrapDragon = 76774528;
            public const int StardustDragon = 44508094;
            public const int DragunityKnightGaeDearg = 34116027;
            public const int DragunityKnightVajrayana = 21249921;
        }

        public DragunityExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // Set traps
            this.AddExecutor(ExecutorType.SpellSet, this.DefaultSpellSet);

            // Execute spells
            this.AddExecutor(ExecutorType.Activate, CardId.MysticalSpaceTyphoon, this.DefaultMysticalSpaceTyphoon);
            this.AddExecutor(ExecutorType.Activate, CardId.AssaultTeleport);
            this.AddExecutor(ExecutorType.Activate, CardId.UpstartGoblin);
            this.AddExecutor(ExecutorType.Activate, CardId.DragonRavine, this.DragonRavineField);
            this.AddExecutor(ExecutorType.Activate, CardId.Terraforming, this.Terraforming);
            this.AddExecutor(ExecutorType.Activate, CardId.FoolishBurial, this.FoolishBurial);
            this.AddExecutor(ExecutorType.Activate, CardId.MonsterReborn, this.MonsterReborn);

            // Execute monsters
            this.AddExecutor(ExecutorType.Activate, CardId.ScrapDragon, this.ScrapDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.CrystalWingSynchroDragon, this.CrystalWingSynchroDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.DragunityPhalanx);
            this.AddExecutor(ExecutorType.Activate, CardId.DragunityKnightVajrayana);
            this.AddExecutor(ExecutorType.Activate, CardId.DragunityArmaMysletainn, this.DragunityArmaMysletainnEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.DragunityDux);

            // Summon
            this.AddExecutor(ExecutorType.Activate, CardId.DragonsMirror, this.DragonsMirror);
            this.AddExecutor(ExecutorType.SpSummon, CardId.ScrapDragon, this.ScrapDragonSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.CrystalWingSynchroDragon, this.CrystalWingSynchroDragonSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.StardustDragon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.DragunityKnightVajrayana);
            this.AddExecutor(ExecutorType.SpSummon, CardId.DragunityKnightGaeDearg);
            this.AddExecutor(ExecutorType.Summon, CardId.DragunityPhalanx, this.DragunityPhalanxSummon);
            this.AddExecutor(ExecutorType.SpSummon, CardId.DragunityArmaMysletainn, this.DragunityArmaMysletainn);
            this.AddExecutor(ExecutorType.Summon, CardId.DragunityArmaMysletainn, this.DragunityArmaMysletainnTribute);

            // Use draw effects if we can't do anything else
            this.AddExecutor(ExecutorType.Activate, CardId.CardsOfConsonance);
            this.AddExecutor(ExecutorType.Activate, CardId.DragonRavine, this.DragonRavineEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.FireFormationTenki, this.FireFormationTenki);
            this.AddExecutor(ExecutorType.Activate, CardId.DragunitySpearOfDestiny);

            // Summon
            this.AddExecutor(ExecutorType.Summon, CardId.DragunityDux, this.DragunityDux);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.DragunityPhalanx, this.DragunityPhalanxSet);
            this.AddExecutor(ExecutorType.SummonOrSet, CardId.AssaultBeast);

            // Draw assault mode if we don't have one
            this.AddExecutor(ExecutorType.Activate, CardId.AssaultBeast, this.AssaultBeast);

            // Set useless cards
            this.AddExecutor(ExecutorType.SpellSet, CardId.DragonsMirror, this.SetUselessCards);
            this.AddExecutor(ExecutorType.SpellSet, CardId.Terraforming, this.SetUselessCards);
            this.AddExecutor(ExecutorType.SpellSet, CardId.AssaultTeleport, this.SetUselessCards);
            this.AddExecutor(ExecutorType.SpellSet, CardId.CardsOfConsonance, this.SetUselessCards);

            // Chain traps and monsters
            this.AddExecutor(ExecutorType.Activate, CardId.StardustDragonAssaultMode, this.DefaultStardustDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.StardustDragon, this.DefaultStardustDragonEffect);
            this.AddExecutor(ExecutorType.Activate, CardId.StarlightRoad, this.DefaultTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.MirrorForce, this.DefaultTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.DimensionalPrison, this.DefaultTrap);
            this.AddExecutor(ExecutorType.Activate, CardId.AssaultModeActivate, this.AssaultModeActivate);

            this.AddExecutor(ExecutorType.Repos, this.DefaultMonsterRepos);
        }

        private bool DragonRavineField()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                return this.DefaultField();
            }

            return false;
        }

        private bool DragonRavineEffect()
        {
            if (this.Card.Location != CardLocation.SpellZone)
            {
                return false;
            }

            int tributeId = -1;
            if (this.Bot.HasInHand(CardId.DragunityPhalanx))
            {
                tributeId = CardId.DragunityPhalanx;
            }
            else if (this.Bot.HasInHand(CardId.FireFormationTenki))
            {
                tributeId = CardId.FireFormationTenki;
            }
            else if (this.Bot.HasInHand(CardId.Terraforming))
            {
                tributeId = CardId.Terraforming;
            }
            else if (this.Bot.HasInHand(CardId.DragonRavine))
            {
                tributeId = CardId.DragonRavine;
            }
            else if (this.Bot.HasInHand(CardId.AssaultTeleport))
            {
                tributeId = CardId.AssaultTeleport;
            }
            else if (this.Bot.HasInHand(CardId.AssaultBeast))
            {
                tributeId = CardId.AssaultBeast;
            }
            else if (this.Bot.HasInHand((int) CardId.DragunityArmaMysletainn))
            {
                tributeId = CardId.DragunityArmaMysletainn;
            }
            else
            {
                int count = 0;
                foreach (ClientCard card in this.Bot.Hand)
                {
                    if (card.IsCode(CardId.DragunityDux))
                    {
                        ++count;
                    }
                }
                if (count >= 2)
                {
                    tributeId = CardId.DragunityDux;
                }
            }
            if (tributeId == -1 && this.Bot.HasInHand(CardId.StardustDragonAssaultMode))
            {
                tributeId = CardId.StardustDragonAssaultMode;
            }

            if (tributeId == -1 && this.Bot.HasInHand(CardId.DragunitySpearOfDestiny))
            {
                tributeId = CardId.StardustDragonAssaultMode;
            }

            if (tributeId == -1 && this.Bot.HasInHand(CardId.DragonsMirror)
                && this.Bot.GetMonsterCount() == 0)
            {
                tributeId = CardId.StardustDragonAssaultMode;
            }

            if (tributeId == -1)
            {
                return false;
            }

            int needId = -1;
            if (!this.Bot.HasInMonstersZone(CardId.DragunityPhalanx) &&
                !this.Bot.HasInGraveyard(CardId.DragunityPhalanx))
            {
                needId = CardId.DragunityPhalanx;
            }
            else if (this.Bot.GetMonsterCount() == 0)
            {
                needId = CardId.DragunityDux;
            }
            else
            {
                /*bool hasRealMonster = false;
                foreach (ClientCard card in Bot.GetMonsters())
                {
                    if (!card.IsCode(CardId.AssaultBeast))
                    {
                        hasRealMonster = true;
                        break;
                    }
                }
                if (!hasRealMonster || Util.GetProblematicCard() != null)*/
                needId = CardId.DragunityDux;
            }

            if (needId == -1)
            {
                return false;
            }

            int option;

            if (tributeId == CardId.DragunityPhalanx)
            {
                needId = CardId.DragunityDux;
            }

            int remaining = 3;
            foreach (ClientCard card in this.Bot.Hand)
            {
                if (card.IsCode(needId))
                {
                    remaining--;
                }
            }

            foreach (ClientCard card in this.Bot.Graveyard)
            {
                if (card.IsCode(needId))
                {
                    remaining--;
                }
            }

            foreach (ClientCard card in this.Bot.Banished)
            {
                if (card.IsCode(needId))
                {
                    remaining--;
                }
            }

            if (remaining <= 0)
            {
                return false;
            }

            if (needId == CardId.DragunityPhalanx)
            {
                option = 2;
            }
            else
            {
                option = 1;
            }

            if (this.ActivateDescription != this.Util.GetStringId(CardId.DragonRavine, option))
            {
                return false;
            }

            this.AI.SelectCard(tributeId);
            this.AI.SelectNextCard(needId);

            return true;
        }

        private bool Terraforming()
        {
            if (this.Bot.HasInHand(CardId.DragonRavine))
            {
                return false;
            }

            if (this.Bot.SpellZone[5] != null)
            {
                return false;
            }

            return true;
        }

        private bool SetUselessCards()
        {
            ClientField field = this.Bot;

            if (field.HasInSpellZone(CardId.FireFormationTenki))
            {
                return false;
            }

            if (field.HasInSpellZone(CardId.AssaultTeleport))
            {
                return false;
            }

            if (field.HasInSpellZone(CardId.CardsOfConsonance))
            {
                return false;
            }

            if (field.HasInSpellZone(CardId.DragonsMirror))
            {
                return false;
            }

            return this.Bot.GetSpellCountWithoutField() < 4;
        }

        private bool FireFormationTenki()
        {
            if (this.Card.Location == CardLocation.Hand)
            {
                return this.Bot.GetSpellCountWithoutField() < 4;
            }

            return true;
        }

        private bool FoolishBurial()
        {
            this.AI.SelectCard(
                CardId.DragunityPhalanx,
                CardId.AssaultBeast,
                CardId.StardustDragonAssaultMode
                );
            return true;
        }

        private bool MonsterReborn()
        {
            List<ClientCard> cards = new List<ClientCard>(this.Bot.Graveyard.GetMatchingCards(card => card.IsCanRevive()));
            cards.Sort(CardContainer.CompareCardAttack);
            ClientCard selectedCard = null;
            for (int i = cards.Count - 1; i >= 0; --i)
            {
                ClientCard card = cards[i];
                if (card.Attack < 2000)
                {
                    break;
                }

                if (card.IsCode(CardId.StardustDragonAssaultMode, CardId.FiveHeadedDragon))
                {
                    continue;
                }

                if (card.IsMonster())
                {
                    selectedCard = card;
                    break;
                }
            }
            cards = new List<ClientCard>(this.Enemy.Graveyard.GetMatchingCards(card => card.IsCanRevive()));
            cards.Sort(CardContainer.CompareCardAttack);
            for (int i = cards.Count - 1; i >= 0; --i)
            {
                ClientCard card = cards[i];
                if (card.Attack < 2000)
                {
                    break;
                }

                if (card.IsMonster() && card.HasType(CardType.Normal) && (selectedCard == null || card.Attack > selectedCard.Attack))
                {
                    selectedCard = card;
                    break;
                }
            }
            if (selectedCard != null)
            {
                this.AI.SelectCard(selectedCard);
                return true;
            }
            return false;
        }

        private bool DragonsMirror()
        {
            IList<ClientCard> tributes = new List<ClientCard>();
            int phalanxCount = 0;
            foreach (ClientCard card in this.Bot.Graveyard)
            {
                if (card.IsCode(CardId.DragunityPhalanx))
                {
                    phalanxCount++;
                    break;
                }
                if (card.Race == (int) CardRace.Dragon)
                {
                    tributes.Add(card);
                }

                if (tributes.Count == 5)
                {
                    break;
                }
            }

            // We can tribute one or two phalanx if needed, but only
            // if we have more than one in the graveyard.
            if (tributes.Count < 5 && phalanxCount > 1)
            {
                foreach (ClientCard card in this.Bot.Graveyard)
                {
                    if (card.IsCode(CardId.DragunityPhalanx))
                    {
                        phalanxCount--;
                        tributes.Add(card);
                        if (phalanxCount <= 1)
                        {
                            break;
                        }
                    }
                }
            }

            if (tributes.Count < 5)
            {
                return false;
            }

            this.AI.SelectCard(CardId.FiveHeadedDragon);
            this.AI.SelectNextCard(tributes);
            return true;
        }

        private bool ScrapDragonSummon()
        {
            //if (Util.IsOneEnemyBetterThanValue(2500, true))
            //    return true;
            ClientCard invincible = this.Util.GetProblematicEnemyCard(3000);
            return invincible != null;
        }

        private bool ScrapDragonEffect()
        {
            ClientCard invincible = this.Util.GetProblematicEnemyCard(3000);
            if (invincible == null && !this.Util.IsOneEnemyBetterThanValue(2800 - 1, false))
            {
                return false;
            }

            int tributeId = -1;
            if (this.Bot.HasInSpellZone(CardId.FireFormationTenki))
            {
                tributeId = CardId.FireFormationTenki;
            }
            else if (this.Bot.HasInSpellZone(CardId.Terraforming))
            {
                tributeId = CardId.Terraforming;
            }
            else if (this.Bot.HasInSpellZone(CardId.DragonsMirror))
            {
                tributeId = CardId.DragonsMirror;
            }
            else if (this.Bot.HasInSpellZone(CardId.CardsOfConsonance))
            {
                tributeId = CardId.CardsOfConsonance;
            }
            else if (this.Bot.HasInSpellZone(CardId.AssaultTeleport))
            {
                tributeId = CardId.AssaultTeleport;
            }
            else if (this.Bot.HasInSpellZone(CardId.AssaultModeActivate))
            {
                tributeId = CardId.AssaultModeActivate;
            }
            else if (this.Bot.HasInSpellZone(CardId.DragonRavine))
            {
                tributeId = CardId.DragonRavine;
            }

            List<ClientCard> monsters = this.Enemy.GetMonsters();
            monsters.Sort(CardContainer.CompareCardAttack);

            ClientCard destroyCard = invincible;
            if (destroyCard == null)
            {
                for (int i = monsters.Count - 1; i >= 0; --i)
                {
                    if (monsters[i].IsAttack())
                    {
                        destroyCard = monsters[i];
                        break;
                    }
                }
            }

            if (destroyCard == null)
            {
                return false;
            }

            this.AI.SelectCard(tributeId);
            this.AI.SelectNextCard(destroyCard);

            return true;
        }

        private bool CrystalWingSynchroDragonSummon()
        {
            return !this.Bot.HasInHand(CardId.AssaultModeActivate)
                && !this.Bot.HasInHand(CardId.AssaultBeast)
                && !this.Bot.HasInSpellZone(CardId.AssaultModeActivate);
        }

        private bool CrystalWingSynchroDragonEffect()
        {
            return this.Duel.LastChainPlayer != 0;
        }

        private bool DragunityPhalanxSummon()
        {
            return this.Bot.HasInHand(CardId.DragunityArmaMysletainn);
        }

        private bool DragunityArmaMysletainn()
        {
            if (this.Bot.HasInMonstersZone(CardId.DragunityPhalanx))
            {
                this.AI.SelectCard(CardId.DragunityPhalanx);
                return true;
            }
            if (this.Bot.HasInMonstersZone(CardId.DragunityDux))
            {
                this.AI.SelectCard(CardId.DragunityDux);
                return true;
            }
            return false;
        }

        private bool DragunityArmaMysletainnEffect()
        {
            this.AI.SelectCard(CardId.DragunityPhalanx);
            return true;
        }

        private bool DragunityArmaMysletainnTribute()
        {
            if ((this.Bot.HasInMonstersZone(CardId.AssaultBeast)
                && this.Bot.HasInGraveyard(CardId.DragunityPhalanx))
                || this.Bot.HasInMonstersZone(CardId.DragunityPhalanx)
                || this.Bot.HasInHand(CardId.DragunitySpearOfDestiny))
            {
                List<ClientCard> monster_sorted = this.Bot.GetMonsters();
                monster_sorted.Sort(CardContainer.CompareCardAttack);
                foreach (ClientCard monster in monster_sorted)
                {
                    this.AI.SelectMaterials(monster);
                    return true;
                }
            }
            return false;
        }

        private bool DragunityDux()
        {
            return this.Bot.HasInGraveyard(CardId.DragunityPhalanx) ||
                (this.Bot.GetMonsterCount() == 0 && this.Bot.HasInHand(CardId.DragunityArmaMysletainn) ||
                this.Bot.HasInHand(CardId.DragunitySpearOfDestiny));
        }

        private bool DragunityPhalanxSet()
        {
            return this.Bot.GetMonsterCount() == 0 || !this.Bot.HasInGraveyard(CardId.DragunityPhalanx);
        }

        private bool AssaultBeast()
        {
            if (!this.Bot.HasInSpellZone(CardId.AssaultModeActivate))
            {
                return true;
            }

            return false;
        }

        private bool AssaultModeActivate()
        {
            if (this.Duel.Player == 0 && this.Duel.Phase == DuelPhase.BattleStart)
            {
                List<ClientCard> monsters = this.Bot.GetMonsters();
                foreach (ClientCard monster in monsters)
                {
                    if (monster.IsCode(CardId.StardustDragon) && monster.Attacked)
                    {
                        this.AI.SelectCard(monster);
                        return true;
                    }
                }
            }
            return this.Duel.Player == 1;
        }
    }
}