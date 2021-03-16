using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using System.Linq;

namespace WindBot.Game.AI.Decks
{
    [Deck("PureWinds", "AI_PureWinds")]
    // Made by Pluani (AniHelp) and Szefo
    class PureWindsExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int SpeedroidTerrortop = 81275020;
            public const int WindwitchIceBell = 43722862;
            public const int PilicaDescendantOfGusto = 71175527;
            public const int SpeedroidTaketomborg = 53932291;
            public const int WindaPriestessOfGusto = 54455435;
            public const int WindwitchGlassBell = 71007216;

            public const int GustoGulldo = 65277087;
            public const int GustoEgul = 91662792;
            public const int WindwitchSnowBell = 70117860;
            public const int SpeedroidRedEyedDice = 16725505;
            public const int Raigeki = 12580477;
            public const int MonsterReborn = 83764719;
            public const int Reasoning = 58577036;
            public const int ElShaddollWinda = 94977269;

            public const int QuillPenOfGulldos = 27980138;
            public const int CosmicCyclone = 8267140;
            public const int EmergencyTeleport = 67723438;

            public const int ForbiddenChalice = 25789292;
            public const int SuperTeamBuddyForceUnite = 8608979;
            public const int KingsConsonance = 24590232;
            public const int GozenMatch = 53334471;
            public const int SolemnStrike = 40605147;
            public const int SolemnWarning = 84749824;

            public const int MistWurm = 27315304;
            public const int CrystalWingSynchroDragon = 50954680;
            public const int ClearWingSynchroDragon = 82044279;
            public const int WindwitchWinterBell = 14577226;

            public const int StardustChargeWarrior = 64880894;
            public const int DaigustoSphreez = 29552709;
            public const int DaigustoGulldos = 84766279;

            public const int HiSpeedroidChanbara = 42110604;
            public const int OldEntityHastorr = 70913714;
            public const int WynnTheWindCharmerVerdant = 30674956;
            public const int GreatFly = 90512490;
            public const int KnightmareIblee = 10158145;
            public const int ChaosMax = 55410871;
            public const int SkillDrain = 82732705;
            public const int SoulDrain = 73599290;
            public const int Rivalry = 90846359;
            public const int OnlyOne = 24207889;
        }

        readonly List<int> ReposTargets = new List<int>
        { CardId.GustoGulldo,
            CardId.WindaPriestessOfGusto,
            CardId.GustoEgul,
            CardId.PilicaDescendantOfGusto,
            CardId.DaigustoGulldos
        };
        readonly List<int> taketomborgSpList = new List<int>
        {   CardId.WindwitchGlassBell,
            CardId.GustoGulldo,
            CardId.GustoEgul,
            CardId.SpeedroidRedEyedDice,
            CardId.WindwitchSnowBell,
            CardId.SpeedroidTerrortop
        };
        readonly List<int> level1 = new List<int>
        { CardId.GustoEgul,
            CardId.SpeedroidRedEyedDice,
            CardId.WindwitchSnowBell
        };
        readonly List<int> Pilica = new List<int>
        { 
            CardId.ClearWingSynchroDragon,
            CardId.WindwitchWinterBell,
            CardId.StardustChargeWarrior
        };
        readonly List<int> level3 = new List<int>
        { CardId.PilicaDescendantOfGusto,
            CardId.WindwitchIceBell,
            CardId.SpeedroidTaketomborg
        };
        readonly List<int> KeepSynchro = new List<int>
        { CardId.DaigustoSphreez,
            CardId.CrystalWingSynchroDragon,
            CardId.ClearWingSynchroDragon,
            CardId.WindwitchWinterBell,
            CardId.GreatFly,
            CardId.WynnTheWindCharmerVerdant
        };
        readonly List<int> KeepSynchro2 = new List<int>
        { 
            CardId.CrystalWingSynchroDragon,
            CardId.DaigustoSphreez,
            CardId.ClearWingSynchroDragon,
            CardId.WindwitchWinterBell
        };
        readonly List<int> reborn = new List<int>
        { CardId.ClearWingSynchroDragon,
            CardId.DaigustoSphreez,
            CardId.WindwitchWinterBell,
            CardId.PilicaDescendantOfGusto,
            CardId.OldEntityHastorr,
            CardId.HiSpeedroidChanbara,
            CardId.DaigustoGulldos
        };
        readonly List<int> Gulldosulist = new List<int>
        { CardId.CrystalWingSynchroDragon,
            CardId.MistWurm,
            CardId.ClearWingSynchroDragon,
            CardId.WindwitchWinterBell,
            CardId.ClearWingSynchroDragon,
            CardId.StardustChargeWarrior
        };
        readonly List<int> Gulldosulist2 = new List<int>
        {
            CardId.SpeedroidTerrortop,
            CardId.PilicaDescendantOfGusto,
            CardId.WindaPriestessOfGusto,
            CardId.WindwitchIceBell,
            CardId.SpeedroidTaketomborg,
            CardId.OldEntityHastorr,
            CardId.HiSpeedroidChanbara,
            CardId.DaigustoGulldos,
            CardId.DaigustoSphreez
        };
        readonly List<int> EgulsuList = new List<int>
        {
            CardId.SpeedroidTerrortop,
            CardId.PilicaDescendantOfGusto,
            CardId.WindaPriestessOfGusto,
            CardId.WindwitchIceBell,
            CardId.SpeedroidTaketomborg,
            CardId.OldEntityHastorr,
            CardId.HiSpeedroidChanbara,
            CardId.DaigustoGulldos,
            CardId.DaigustoSphreez,
            CardId.StardustChargeWarrior,
            CardId.WindwitchWinterBell,
            CardId.ClearWingSynchroDragon
        };
        readonly List<int> SynchroList = new List<int>
        {
            CardId.SpeedroidTerrortop,
            CardId.PilicaDescendantOfGusto,
            CardId.WindwitchIceBell,
            CardId.SpeedroidTaketomborg,
            CardId.OldEntityHastorr,
            CardId.HiSpeedroidChanbara,
            CardId.DaigustoGulldos,
            CardId.DaigustoSphreez,
            CardId.StardustChargeWarrior,
            CardId.WindwitchWinterBell,
            CardId.ClearWingSynchroDragon,
            CardId.CrystalWingSynchroDragon,
            CardId.MistWurm
        };
        readonly List<int> SynchroFull = new List<int>
        {
            CardId.OldEntityHastorr,
            CardId.HiSpeedroidChanbara,
            CardId.DaigustoGulldos,
            CardId.DaigustoSphreez,
            CardId.StardustChargeWarrior,
            CardId.WindwitchWinterBell,
            CardId.ClearWingSynchroDragon,
            CardId.CrystalWingSynchroDragon,
            CardId.MistWurm
        };
        readonly List<int> LinkList = new List<int>
        {
            CardId.WynnTheWindCharmerVerdant,
            CardId.GreatFly
        };
        readonly List<int> tuner = new List<int>
        {
            CardId.GustoGulldo,
            CardId.GustoEgul,
            CardId.SpeedroidRedEyedDice,
            CardId.WindwitchGlassBell,
            CardId.WindwitchSnowBell
        };
        readonly List<int> gusto = new List<int>
        {
            CardId.GustoGulldo,
            CardId.GustoEgul,
            CardId.WindaPriestessOfGusto,
            CardId.PilicaDescendantOfGusto,
            CardId.DaigustoGulldos,
            CardId.DaigustoSphreez
        };
        readonly List<int> ET = new List<int>
        {
            CardId.ClearWingSynchroDragon,
            CardId.WindwitchWinterBell
        };

        private bool WindwitchGlassBelleff_used;
        private bool Summon_used;
        private bool Pilica_eff;
        private bool plan_A;
        private int SnowBell_count = 0;
        //TODO: reset the flags when they should reset ( public override void OnNewTurn() )
        public PureWindsExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            //counter
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnWarning, base.DefaultSolemnWarning);
            this.AddExecutor(ExecutorType.Activate, CardId.ForbiddenChalice, this.ForbiddenChaliceeff);
            this.AddExecutor(ExecutorType.Activate, CardId.CrystalWingSynchroDragon, this.CrystalWingSynchroDragoneff);
            this.AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, base.DefaultSolemnStrike);
            this.AddExecutor(ExecutorType.Activate, CardId.GustoGulldo, this.GustoGulldoeff);
            this.AddExecutor(ExecutorType.Activate, CardId.GustoEgul, this.GustoEguleff);
            this.AddExecutor(ExecutorType.Activate, CardId.WindaPriestessOfGusto, this.WindaPriestessOfGustoeff);
            this.AddExecutor(ExecutorType.Activate, CardId.PilicaDescendantOfGusto, this.PilicaDescendantOfGustoeff);
            this.AddExecutor(ExecutorType.Activate, CardId.OldEntityHastorr, this.OldEntityHastorreff);
            this.AddExecutor(ExecutorType.Activate, CardId.WynnTheWindCharmerVerdant, this.WynnTheWindCharmerVerdanteff);
            this.AddExecutor(ExecutorType.Activate, CardId.GreatFly, this.GreatFlyeff);
            this.AddExecutor(ExecutorType.Activate, CardId.QuillPenOfGulldos, this.QuillPenOfGulldoseff);
            this.AddExecutor(ExecutorType.Activate, CardId.CosmicCyclone, this.CosmicCycloneeff);
            this.AddExecutor(ExecutorType.Activate, CardId.MonsterReborn, this.Reborneff);
            //plan A             
            this.AddExecutor(ExecutorType.Activate, CardId.WindwitchIceBell, this.WindwitchIceBelleff);
            this.AddExecutor(ExecutorType.Activate, CardId.WindwitchGlassBell, this.WindwitchGlassBelleff);
            this.AddExecutor(ExecutorType.Activate, CardId.WindwitchSnowBell, this.WindwitchSnowBellsp);
            this.AddExecutor(ExecutorType.Activate, CardId.StardustChargeWarrior);
            this.AddExecutor(ExecutorType.Activate, CardId.WindwitchWinterBell, this.WindwitchWinterBelleff);
            this.AddExecutor(ExecutorType.Activate, CardId.ClearWingSynchroDragon, this.ClearWingSynchroDragoneff);
            this.AddExecutor(ExecutorType.Activate, CardId.DaigustoSphreez, this.DaigustoSphreezeff);
            this.AddExecutor(ExecutorType.Activate, CardId.SpeedroidTerrortop, this.SpeedroidTerrortopeff);
            this.AddExecutor(ExecutorType.Activate, CardId.SpeedroidTaketomborg, this.SpeedroidTaketomborgeff);
            this.AddExecutor(ExecutorType.Activate, CardId.SpeedroidRedEyedDice, this.SpeedroidRedEyedDiceeff);
            this.AddExecutor(ExecutorType.Activate, CardId.MistWurm, this.MistWurmeff);
            this.AddExecutor(ExecutorType.Activate, CardId.DaigustoGulldos, this.DaigustoGulldoseff);
            this.AddExecutor(ExecutorType.SpSummon, CardId.WindwitchWinterBell, this.WindwitchWinterBellsp);

            this.AddExecutor(ExecutorType.SpSummon, CardId.CrystalWingSynchroDragon, this.CrystalWingSynchroDragonsp);
            // if fail
            this.AddExecutor(ExecutorType.SpSummon, CardId.ClearWingSynchroDragon, this.ClearWingSynchroDragonsp);
            // if fail
            this.AddExecutor(ExecutorType.SpSummon, CardId.DaigustoSphreez, this.DaigustoSphreezsp);
            // plan B
            this.AddExecutor(ExecutorType.SpSummon, CardId.SpeedroidTerrortop);
            this.AddExecutor(ExecutorType.SpSummon, CardId.SpeedroidTaketomborg, this.SpeedroidTaketomborgsp);
            //summon
            this.AddExecutor(ExecutorType.Summon, CardId.PilicaDescendantOfGusto, this.PilicaDescendantOfGustosu);
            this.AddExecutor(ExecutorType.Summon, CardId.GustoGulldo, this.GustoGulldosu);
            this.AddExecutor(ExecutorType.Summon, CardId.GustoEgul, this.GustoEgulsu);
            this.AddExecutor(ExecutorType.Summon, CardId.WindaPriestessOfGusto, this.WindaPriestessOfGustosu);
            this.AddExecutor(ExecutorType.Summon, CardId.SpeedroidRedEyedDice, this.SpeedroidRedEyedDicesu);
            //other thing
            this.AddExecutor(ExecutorType.SpSummon, CardId.MistWurm);
            this.AddExecutor(ExecutorType.SpSummon, CardId.DaigustoGulldos);
            this.AddExecutor(ExecutorType.SpSummon, CardId.HiSpeedroidChanbara);
            this.AddExecutor(ExecutorType.SpSummon, CardId.StardustChargeWarrior);
            this.AddExecutor(ExecutorType.SpSummon, CardId.OldEntityHastorr);
            this.AddExecutor(ExecutorType.SpSummon, CardId.GreatFly, this.GreatFlysp);
            this.AddExecutor(ExecutorType.SpSummon, CardId.WynnTheWindCharmerVerdant, this.WynnTheWindCharmerVerdantsp);
            this.AddExecutor(ExecutorType.Activate, CardId.Raigeki);
            this.AddExecutor(ExecutorType.Activate, CardId.GozenMatch);
            this.AddExecutor(ExecutorType.Activate, CardId.KingsConsonance, this.KingsConsonanceeff);
            //trap set
            this.AddExecutor(ExecutorType.SpellSet, CardId.KingsConsonance);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnStrike);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SolemnWarning);
            this.AddExecutor(ExecutorType.SpellSet, CardId.ForbiddenChalice);
            this.AddExecutor(ExecutorType.SpellSet, CardId.SuperTeamBuddyForceUnite);
            this.AddExecutor(ExecutorType.SpellSet, CardId.GozenMatch);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.GustoGulldo, this.gulldoset);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.GustoEgul, this.egulset);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.WindaPriestessOfGusto, this.windaset);
            this.AddExecutor(ExecutorType.Summon, CardId.WindwitchGlassBell, this.WindwitchGlassBellsummonfirst);
            this.AddExecutor(ExecutorType.Summon, CardId.WindwitchGlassBell, this.WindwitchGlassBellsummon);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.SpeedroidRedEyedDice, this.SpeedroidRedEyedDiceset);
            this.AddExecutor(ExecutorType.MonsterSet, CardId.WindwitchSnowBell, this.WindwitchSnowBellset);
            this.AddExecutor(ExecutorType.Activate, CardId.EmergencyTeleport, this.EmergencyTeleporteff);
            this.AddExecutor(ExecutorType.Activate, CardId.Reasoning, this.Reasoningeff);
            this.AddExecutor(ExecutorType.Activate, CardId.SuperTeamBuddyForceUnite, this.SuperTeamBuddyForceUniteeff);

            this.AddExecutor(ExecutorType.Repos, this.MonsterRepos);
        }

        public override void OnNewTurn()
        {
            this.WindwitchGlassBelleff_used = false;
            this.Summon_used = false;
            this.Pilica_eff = false;
            this.plan_A = false;
            this.SnowBell_count = 0;
            base.OnNewTurn();
        }
        private bool windaset()
        {
            if (this.Enemy.HasInMonstersZoneOrInGraveyard(CardId.ChaosMax))
            {
                return false;
            }

            return true;
        }
        private bool egulset()
        {
            if (this.Enemy.HasInMonstersZoneOrInGraveyard(CardId.ChaosMax))
            {
                return false;
            }

            return true;
        }
        private bool gulldoset()
        {
            if (this.Enemy.HasInMonstersZoneOrInGraveyard(CardId.ChaosMax))
            {
                return false;
            }

            return true;
        }

        private bool Reasoningeff()
        {
            if ((this.Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                this.Bot.HasInMonstersZone(CardId.MistWurm)) &&
                (this.Util.GetBotAvailZonesFromExtraDeck() == 0))
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(this.level3) &&
                this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell) &&
                this.Bot.HasInHand(CardId.WindwitchSnowBell))
            {
                return false;
            }

            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
        private bool KingsConsonanceeff()
        {
            this.AI.SelectCard(CardId.CrystalWingSynchroDragon,
                CardId.DaigustoSphreez,
                CardId.ClearWingSynchroDragon,
                CardId.HiSpeedroidChanbara,
                CardId.OldEntityHastorr);
            return true;
        }
        private bool Reborneff()
        {
            if (this.Bot.HasInGraveyard(this.KeepSynchro2))
            {
                this.AI.SelectCard(this.KeepSynchro2);
                return true;
            }
            if (!this.Util.IsOneEnemyBetter(true))
            {
                return false;
            }

            if (!this.Bot.HasInGraveyard(this.reborn))
            {
                return false;
            }
            this.AI.SelectCard(this.reborn);
            return true;
        }

        private bool SpeedroidRedEyedDiceset()
        {
            if (this.Enemy.HasInMonstersZone(CardId.ChaosMax))
            {
                return false;
            }

            if ((this.Bot.GetMonstersInMainZone().Count + this.Bot.GetMonstersInExtraZone().Count) == 0)
            {
                return true;
            }

            return false;
        }

        private bool WindwitchSnowBellset()
        {
            if (this.Enemy.HasInMonstersZone(CardId.ChaosMax))
            {
                return false;
            }

            if ((this.Bot.GetMonstersInMainZone().Count + this.Bot.GetMonstersInExtraZone().Count) == 0)
            {
                return true;
            }

            return false;
        }

        private bool GreatFlysp()
        {
            if (this.Bot.HasInMonstersZone(this.KeepSynchro))
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.HiSpeedroidChanbara))
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant))
            {
                return false;
            }

            return true;
        }
        private bool WynnTheWindCharmerVerdantsp()
        { 
            if (this.Bot.HasInMonstersZone(this.KeepSynchro))
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.HiSpeedroidChanbara))
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.GreatFly))
            {
                return false;
            }

            return true;
        }
        private bool MistWurmeff()
        {
            this.AI.SelectCard(this.Util.GetBestEnemyCard(false, true));
            if (this.Util.GetBestEnemyCard(false, true) != null)
            {
                Logger.DebugWriteLine("*************SelectCard= " + this.Util.GetBestEnemyCard(false, true).Id);
            }

            this.AI.SelectNextCard(this.Util.GetBestEnemyCard(false, true));
            if (this.Util.GetBestEnemyCard(false, true) != null)
            {
                Logger.DebugWriteLine("*************SelectCard= " + this.Util.GetBestEnemyCard(false, true).Id);
            }

            this.AI.SelectThirdCard(this.Util.GetBestEnemyCard(false, true));
            if (this.Util.GetBestEnemyCard(false, true) != null)
            {
                Logger.DebugWriteLine("*************SelectCard= " + this.Util.GetBestEnemyCard(false, true).Id);
            }

            return true;
        }
        private bool GustoGulldosu()
        {
            if (this.Bot.HasInMonstersZone(this.Gulldosulist) &&
                (this.Util.GetBotAvailZonesFromExtraDeck() == 0))
            {
                return false;
            }
            else if (this.Bot.HasInMonstersZone(CardId.DaigustoSphreez) ||
                this.Bot.HasInHand(CardId.EmergencyTeleport))
            {
                this.Summon_used = true;
                return true;
            }
            else if (this.Bot.HasInMonstersZone(this.Gulldosulist2) || this.Bot.HasInHand(CardId.SpeedroidTaketomborg))
            {
                this.Summon_used = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool GustoEgulsu()
        {
            if (this.Bot.HasInMonstersZone(CardId.DaigustoSphreez) &&
                !this.Bot.HasInHand(CardId.GustoGulldo))
            {
                this.Summon_used = true;
                return true;
            }
            else if ((this.Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                this.Bot.HasInMonstersZone(CardId.MistWurm)) &&
                (this.Util.GetBotAvailZonesFromExtraDeck() == 0))
            {
                return false;
            }
            else if (this.Bot.HasInMonstersZone(this.EgulsuList) || this.Bot.HasInHand(CardId.SpeedroidTaketomborg))
            {
                this.Summon_used = true;
                return true;
            }
            return false;
        }
        private bool WindaPriestessOfGustosu()
        {
            if (this.Bot.HasInMonstersZone(CardId.DaigustoSphreez) &&
                !this.Bot.HasInHand(CardId.GustoGulldo) &&
                !this.Bot.HasInHand(CardId.GustoEgul))
            {
                this.Summon_used = true;
                return true;
            }
            else if (this.Bot.HasInMonstersZone(CardId.GustoGulldo) ||
                this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell) ||
                ((this.Bot.HasInMonstersZone(this.level3) || this.Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto)) &&
                this.Bot.HasInMonstersZone(this.tuner)) ||
                (this.Bot.HasInMonstersZone(CardId.OldEntityHastorr) && this.Bot.HasInMonstersZone(this.level1)) &&
                (this.Util.GetBotAvailZonesFromExtraDeck() >= 1))
            {
                this.Summon_used = true;
                return true;
            }
            return false;
        }
        private bool SpeedroidRedEyedDicesu()
        {
            if ((this.Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                this.Bot.HasInMonstersZone(CardId.MistWurm) ||
                this.Bot.HasInMonstersZone(CardId.DaigustoSphreez)) &&
                (this.Util.GetBotAvailZonesFromExtraDeck() == 0))
            {
                return false;
            }
            else if (this.Bot.HasInMonstersZone(this.EgulsuList))
            {
                this.Summon_used = true;
                return true;
            }
            return false;
        }
        private bool PilicaDescendantOfGustosu()
        {
            if ((this.Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                this.Bot.HasInMonstersZone(CardId.MistWurm)) &&
                (this.Util.GetBotAvailZonesFromExtraDeck() == 0))
            {
                return false;
            }
            else if (this.Bot.HasInMonstersZone(this.Pilica) &&
                !this.Bot.HasInGraveyard(this.level1) &&
                (this.Util.GetBotAvailZonesFromExtraDeck() == 0))
            {
                return false;
            }
            else if (!this.Bot.HasInMonstersZoneOrInGraveyard(this.tuner))
            {
                return false;
            }
            else {
                this.Summon_used = true;
                return true;
            }
        }
        private bool EmergencyTeleporteff()
        {
            if ((this.Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                this.Bot.HasInMonstersZone(CardId.MistWurm)) &&
                (this.Util.GetBotAvailZonesFromExtraDeck() == 0))
            {
                return false;
            }
            else if (this.Bot.HasInMonstersZone(this.level3) &&
                this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell) &&
                this.Bot.HasInHand(CardId.WindwitchSnowBell))
            {
                return false;
            }
            else if (this.Bot.HasInMonstersZone(this.tuner) && this.Bot.HasInMonstersZone(this.level3))
            {
                return false;
            }
            else if (!this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(this.tuner))
            {
                return false;
            }
            else if (!this.Bot.HasInHandOrInMonstersZoneOrInGraveyard(this.level1) && this.Bot.HasInMonstersZone(this.ET))
            {
                return false;
            }

            if (this.Pilica_eff == true)
            {
                return false;
            }

            this.AI.SelectCard(CardId.PilicaDescendantOfGusto);
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
        private bool SpeedroidRedEyedDiceeff()
        {
            if (this.Bot.HasInMonstersZone(CardId.SpeedroidTerrortop))
            {
                this.AI.SelectCard(CardId.SpeedroidTerrortop);
                this.AI.SelectNumber(6);
                return true;
            }
            else if (this.Bot.HasInMonstersZone(CardId.SpeedroidTaketomborg))
            {
                this.AI.SelectCard(CardId.SpeedroidTaketomborg);
                this.AI.SelectNumber(6);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DaigustoGulldoseff()
        {
            this.AI.SelectCard();
            this.AI.SelectNextCard(this.Util.GetBestEnemyMonster());
            return true;
        }
        private bool SpeedroidTaketomborgeff()
        {
            if ((this.Bot.GetRemainingCount(CardId.SpeedroidRedEyedDice, 1) >= 1) &&
                this.Bot.HasInMonstersZone(CardId.SpeedroidTerrortop))
            {
                this.AI.SelectCard(CardId.SpeedroidRedEyedDice);
                return true;
            }
            return false;
        }

        private bool QuillPenOfGulldoseff()
        {
            var gyTargets = this.Bot.Graveyard.Where(x => x.Attribute == (int)CardAttribute.Wind).Select(x => x.Id).ToArray();
            if (gyTargets.Count() >= 2)
            {
                this.AI.SelectCard(gyTargets);
                if (this.Bot.HasInSpellZone(CardId.OldEntityHastorr))
                {
                    this.AI.SelectNextCard(CardId.OldEntityHastorr);
                }
                else if (this.Util.GetProblematicEnemyCard() != null)
                {

                    this.AI.SelectNextCard(this.Util.GetProblematicEnemyCard());
                }
                else
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        private bool WindwitchIceBelleff()
        {
            if (this.Enemy.HasInMonstersZone(CardId.ElShaddollWinda))
            {
                return false;
            }

            if (this.WindwitchGlassBelleff_used && !this.Bot.HasInHand(CardId.WindwitchSnowBell))
            {
                return false;
            }
            //AI.SelectPlace(Zones.z2, 1);
            if (this.Bot.GetRemainingCount(CardId.WindwitchGlassBell, 3) >= 1)
            {
                this.AI.SelectCard(CardId.WindwitchGlassBell);
            }
            else if (this.Bot.HasInHand(CardId.WindwitchGlassBell))
            {
                this.AI.SelectCard(CardId.WindwitchSnowBell);
            }
            this.AI.GetSelectedPosition();
            if (this.Card.Location == CardLocation.Hand)
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
            }
            return true;
        }
        private bool SpeedroidTaketomborgsp()
        {
            if (this.Util.GetBotAvailZonesFromExtraDeck() == 0)
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(this.taketomborgSpList))
            {
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;

        }
        private bool WindwitchGlassBelleff()
        {
            if ((this.Bot.HasInHandOrHasInMonstersZone(CardId.WindwitchIceBell) ||
                this.Bot.HasInHandOrHasInMonstersZone(CardId.SpeedroidTaketomborg) ||
                this.Bot.HasInMonstersZone(CardId.PilicaDescendantOfGusto)) &&
                !this.Bot.HasInHand(CardId.WindwitchSnowBell))
            {
                this.AI.SelectCard(CardId.WindwitchSnowBell);
                this.WindwitchGlassBelleff_used = true;
                return true;
            }
            this.AI.SelectCard(CardId.WindwitchIceBell);
            this.WindwitchGlassBelleff_used = true;
            return true;
        }

        private bool OldEntityHastorreff()
        {
            this.AI.SelectCard(this.Util.GetBestEnemyMonster());
            return true;
        }

        private bool WynnTheWindCharmerVerdanteff()
        {
            this.AI.SelectCard(CardId.PilicaDescendantOfGusto, CardId.WindwitchIceBell, CardId.SpeedroidTerrortop, CardId.GustoGulldo, CardId.GustoEgul, CardId.WindaPriestessOfGusto);
            return true;
        }
        private bool SpeedroidTerrortopeff()
        {
            this.AI.SelectCard(CardId.SpeedroidTaketomborg, CardId.SpeedroidRedEyedDice);
            return true;
        }
        private bool GreatFlyeff()
        {
            this.AI.SelectCard(CardId.PilicaDescendantOfGusto, CardId.WindwitchIceBell, CardId.SpeedroidTerrortop, CardId.GustoGulldo, CardId.GustoEgul, CardId.WindaPriestessOfGusto);
            return true;
        }

        private bool PilicaDescendantOfGustoeff()
        {
            this.AI.SelectCard(CardId.GustoGulldo, CardId.WindwitchGlassBell, CardId.WindwitchSnowBell, CardId.GustoEgul, CardId.SpeedroidRedEyedDice);
            this.Pilica_eff = true;
            return true;
        }

        private bool SuperTeamBuddyForceUniteeff()
        {
            foreach (ClientCard card in this.Duel.CurrentChain)
            {
                if (card.IsCode(CardId.SuperTeamBuddyForceUnite))
                {
                    return false;
                }
            }

            if (this.Bot.HasInGraveyard(CardId.PilicaDescendantOfGusto) && this.Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                this.AI.SelectCard(CardId.SuperTeamBuddyForceUnite, CardId.DaigustoSphreez, CardId.PilicaDescendantOfGusto);
                this.AI.SelectPosition(CardPosition.Attack);
                return true;
            }

            if (this.Bot.HasInGraveyard(CardId.WindaPriestessOfGusto) && this.Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                this.AI.SelectCard(CardId.SuperTeamBuddyForceUnite, CardId.DaigustoSphreez, CardId.WindaPriestessOfGusto);
                this.AI.SelectPosition(CardPosition.Attack);
                return true;
            }

            if (this.Bot.HasInGraveyard(CardId.DaigustoSphreez) && this.Bot.HasInMonstersZone(CardId.PilicaDescendantOfGusto))
            {
                this.AI.SelectCard(CardId.SuperTeamBuddyForceUnite, CardId.PilicaDescendantOfGusto, CardId.DaigustoSphreez);
                this.AI.SelectPosition(CardPosition.Attack);
                return true;
            }

            if (this.Bot.HasInGraveyard(CardId.DaigustoSphreez) && this.Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto))
            {
                this.AI.SelectCard(CardId.SuperTeamBuddyForceUnite, CardId.WindaPriestessOfGusto, CardId.DaigustoSphreez);
                this.AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            if (this.Bot.HasInGraveyard(CardId.DaigustoGulldos) && this.Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto))
            {
                this.AI.SelectCard(CardId.SuperTeamBuddyForceUnite, CardId.WindaPriestessOfGusto, CardId.DaigustoGulldos);
                this.AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            if (this.Bot.HasInGraveyard(CardId.DaigustoGulldos) && this.Bot.HasInMonstersZone(CardId.PilicaDescendantOfGusto))
            {
                this.AI.SelectCard(CardId.SuperTeamBuddyForceUnite, CardId.DaigustoGulldos, CardId.PilicaDescendantOfGusto);
                this.AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            if (this.Bot.HasInGraveyard(CardId.DaigustoSphreez) && this.Bot.HasInMonstersZone(CardId.DaigustoGulldos))
            {
                this.AI.SelectCard(CardId.DaigustoGulldos, CardId.DaigustoSphreez);
                this.AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            if (this.Bot.HasInGraveyard(CardId.CrystalWingSynchroDragon))
            {
                this.AI.SelectCard(CardId.CrystalWingSynchroDragon);
                this.AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            if (this.Bot.HasInGraveyard(CardId.CrystalWingSynchroDragon))
            {
                this.AI.SelectCard(CardId.ClearWingSynchroDragon);
                this.AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            if (this.Bot.HasInGraveyard(this.SynchroList))
            {
                this.AI.SelectCard(this.SynchroList);
                this.AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            if (this.Bot.HasInGraveyard(CardId.PilicaDescendantOfGusto) && this.Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto))
            {
                this.AI.SelectCard(CardId.WindaPriestessOfGusto, CardId.PilicaDescendantOfGusto);
                this.AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            if (this.Util.GetBotAvailZonesFromExtraDeck() >= 1)
            {
                if ((this.Bot.HasInMonstersZone(CardId.SpeedroidTerrortop) ||
                this.Bot.HasInMonstersZone(CardId.SpeedroidRedEyedDice) ||
                this.Bot.HasInMonstersZone(CardId.HiSpeedroidChanbara)) &&
                !this.Bot.HasInHand(CardId.SpeedroidTaketomborg))
                {
                    this.AI.SelectCard(CardId.SpeedroidRedEyedDice, CardId.SpeedroidTerrortop, CardId.SpeedroidTaketomborg);
                    return true;
                }
                if ((this.Bot.HasInMonstersZone(CardId.SpeedroidTerrortop) ||
                    this.Bot.HasInMonstersZone(CardId.SpeedroidRedEyedDice) ||
                    this.Bot.HasInMonstersZone(CardId.HiSpeedroidChanbara)) &&
                    this.Bot.HasInHand(CardId.SpeedroidTaketomborg))
                {
                    return false;
                }
            }
            if (this.Bot.HasInGraveyard(CardId.SuperTeamBuddyForceUnite))
            {
                this.AI.SelectCard(CardId.SuperTeamBuddyForceUnite);
                return true;
            }
            return false;
        }

        private bool WindwitchSnowBellsp()
        {
            if (this.SnowBell_count >= 5)
            {
                return false;
            }

            if ((this.Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                this.Bot.HasInMonstersZone(CardId.DaigustoSphreez) ||
                this.Bot.HasInMonstersZone(CardId.MistWurm)) &&
                (this.Util.GetBotAvailZonesFromExtraDeck() == 0))
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(this.level3) &&
                this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell) &&
                this.Bot.HasInMonstersZone(this.level1))
            {
                return false;
            }

            if ((this.Bot.HasInMonstersZone(CardId.ClearWingSynchroDragon) ||
                this.Bot.HasInMonstersZone(CardId.WindwitchWinterBell)) &&
                this.Bot.HasInMonstersZone(CardId.WindwitchSnowBell) &&
                (this.Util.GetBotAvailZonesFromExtraDeck() == 0))
            {
                return false;
            }

            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            this.SnowBell_count++;
            return true;
        }
        private bool DaigustoSphreezsp()
        {
            //AI.SelectPlace(Zones.z5, Zones.ExtraMonsterZones);
            this.AI.SelectCard(CardId.WindwitchSnowBell, CardId.PilicaDescendantOfGusto, CardId.WindaPriestessOfGusto);
            this.AI.SelectCard(CardId.SpeedroidRedEyedDice, CardId.PilicaDescendantOfGusto, CardId.WindaPriestessOfGusto);
            this.AI.SelectCard(CardId.GustoGulldo, CardId.PilicaDescendantOfGusto);
            this.AI.SelectCard(CardId.WindwitchSnowBell, CardId.DaigustoGulldos);
            this.AI.SelectCard(CardId.SpeedroidRedEyedDice, CardId.DaigustoGulldos);
            this.AI.SelectPosition(CardPosition.Attack);
            return true;
        }
        private bool DaigustoSphreezeff()
        {
            if (this.Summon_used == true)
            {
                this.AI.SelectCard(CardId.PilicaDescendantOfGusto, CardId.GustoGulldo, CardId.GustoEgul, CardId.WindaPriestessOfGusto);
                return true;
            }
            this.AI.SelectCard(CardId.GustoGulldo, CardId.PilicaDescendantOfGusto, CardId.GustoEgul, CardId.WindaPriestessOfGusto);
            return true;
        }
        private bool WindwitchWinterBelleff()
        {
            this.AI.SelectCard(CardId.WindwitchGlassBell);
            return true;
        }

        private bool WindwitchWinterBellsp()
        {
            if (this.Bot.HasInHandOrInSpellZone(CardId.SuperTeamBuddyForceUnite) ||
                this.Bot.HasInHandOrInSpellZone(CardId.MonsterReborn))
            {
                return false;
            }

            if (this.Bot.HasInMonstersZone(CardId.WindwitchIceBell) &&
                 this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell) &&
                 this.Bot.HasInMonstersZone(CardId.WindwitchSnowBell))
            {
                //AI.SelectPlace(Zones.z5, Zones.ExtraMonsterZones);
                this.AI.GetSelectedPosition();
                this.AI.SelectPosition(CardPosition.FaceUpAttack);
                this.AI.SelectCard(CardId.WindwitchIceBell, CardId.WindwitchGlassBell);
                return true;
            }

            return false;
        }

        private bool ClearWingSynchroDragonsp()
        {
            if (this.Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                return false;
            }

            this.AI.SelectPosition(CardPosition.Attack);
            return true;
        }

        private bool ClearWingSynchroDragoneff()
        {
            if (this.Duel.LastChainPlayer == 1)
            {
                return true;
            }
            return false;
        }
        private bool CrystalWingSynchroDragonsp()
        {
            if (this.Bot.HasInMonstersZone(CardId.WindwitchSnowBell) && this.Bot.HasInMonstersZone(CardId.WindwitchWinterBell))
            {
                //AI.SelectPlace(Zones.z5, Zones.ExtraMonsterZones);
                this.plan_A = true;
            }
            else if (this.Bot.HasInMonstersZone(CardId.WindwitchSnowBell) && this.Bot.HasInMonstersZone(CardId.ClearWingSynchroDragon))
            {
                //AI.SelectPlace(Zones.z5, Zones.ExtraMonsterZones);
                this.plan_A = true;
            }
            return true;
        }
        private bool ForbiddenChaliceeff()
        {
            if (this.Duel.LastChainPlayer == 1)
            {
                var target = this.Util.GetProblematicEnemyMonster(0, true);
                if (target != null && !target.IsShouldNotBeSpellTrapTarget() && this.Duel.CurrentChain.Contains(target))
                {
                    this.AI.SelectCard(target);
                    return true;
                }
            }
            return false;
        }
        private bool CosmicCycloneeff()
        {
            foreach (ClientCard card in this.Duel.CurrentChain)
            {
                if (card.IsCode(DefaultExecutor.CardId.CosmicCyclone))
                {
                    return false;
                }
            }

            if ((this.Enemy.HasInSpellZone(CardId.SkillDrain) ||
                this.Enemy.HasInSpellZone(CardId.Rivalry) ||
                this.Enemy.HasInSpellZone(CardId.OnlyOne)) &&
                (this.Bot.LifePoints > 1000))
            {
                this.AI.SelectCard(CardId.SkillDrain, CardId.SoulDrain, CardId.Rivalry, CardId.OnlyOne);
                return true;
            }
            if (this.Bot.HasInSpellZone(CardId.OldEntityHastorr) && (this.Bot.LifePoints > 1000))
            {
                this.AI.SelectCard(CardId.OldEntityHastorr);
                return true;
            }
            return (this.Bot.LifePoints > 1000) && this.DefaultMysticalSpaceTyphoon();
        }
        private bool CrystalWingSynchroDragoneff()
        {
            if (this.Duel.LastChainPlayer == 1)
            {
                return true;
            }
            return false;
        }
        private bool GustoGulldoeff()
        {
            if (this.Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                this.AI.SelectCard(CardId.GustoEgul, CardId.WindaPriestessOfGusto);
                this.AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            this.AI.SelectCard(CardId.GustoEgul, CardId.WindaPriestessOfGusto);
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
        private bool GustoEguleff()
        {
            if (this.Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                this.AI.SelectCard(CardId.WindaPriestessOfGusto, CardId.PilicaDescendantOfGusto);
                this.AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            this.AI.SelectCard(CardId.WindaPriestessOfGusto, CardId.PilicaDescendantOfGusto);
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
        private bool WindaPriestessOfGustoeff()
        {
            if (this.Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                this.AI.SelectCard(CardId.GustoGulldo, CardId.GustoEgul);
                this.AI.SelectPosition(CardPosition.Attack);
            }
            this.AI.SelectCard(CardId.GustoGulldo, CardId.GustoEgul);
            this.AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
        private bool WindwitchGlassBellsummonfirst()
        {
            if (this.Bot.HasInHand(CardId.PilicaDescendantOfGusto) &&
                (this.Bot.HasInGraveyard(CardId.GustoGulldo) ||
                this.Bot.HasInGraveyard(CardId.GustoEgul) ||
                this.Bot.HasInGraveyard(CardId.WindwitchGlassBell) ||
                this.Bot.HasInGraveyard(CardId.SpeedroidRedEyedDice)))
            {
                return false;
            }
            if (this.Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                return false;
            }
            else if (!this.Bot.HasInHand(CardId.WindwitchIceBell))
            {
                this.Summon_used = true;
                return true;
            }
            return false;
        }
        private bool WindwitchGlassBellsummon()
        {
            if (!this.plan_A && (this.Bot.HasInGraveyard(CardId.WindwitchGlassBell) || this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell)))
            {
                return false;
            }
            //AI.SelectPlace(Zones.z2, 1);
            if (this.Bot.HasInMonstersZone(CardId.WindwitchIceBell) &&
                !this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell))
            {
                this.Summon_used = true;
                return true;
            }
            if (this.WindwitchGlassBelleff_used)
            {
                return false;
            }

            return false;
        }

        public bool MonsterRepos()
        {
            if (this.Card.IsCode(CardId.CrystalWingSynchroDragon) || this.Card.IsCode(CardId.DaigustoSphreez))
            {
                return (!this.Card.HasPosition(CardPosition.Attack));
            }

            if (this.Card.IsCode(this.SynchroFull))
            {
                if (this.Card.IsFacedown() || this.Card.IsDefense())
                {
                    return true;
                }
            }     
            if (this.Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                if (this.Card.IsCode(this.gusto))
                {
                    if (this.Card.IsFacedown() || this.Card.IsDefense())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if ((this.Util.GetBotAvailZonesFromExtraDeck() >= 1) &&
                ((this.Bot.GetMonsterCount() - this.Bot.GetMonstersInExtraZone().Count) >= 2))
            {
                if (this.Bot.HasInMonstersZone(this.tuner) &&
                    (this.Bot.HasInMonstersZone(this.level3) ||
                    this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell)))
                {
                    if (this.Card.IsFacedown())
                    {
                        return true;
                    }
                }
                if (this.Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto) &&
                    (this.Bot.HasInMonstersZone(CardId.GustoGulldo) ||
                    this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell)))
                {
                    if (this.Card.IsFacedown())
                    {
                        return true;
                    }
                }
                if (((this.Bot.GetMonsterCount() - this.Bot.GetMonstersInExtraZone().Count) >= 3) &&
                    this.Bot.HasInMonstersZone(this.level1) &&
                    (this.Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto) ||
                    this.Bot.HasInMonstersZone(this.level3)))
                {
                    if (this.Card.IsFacedown())
                    {
                        return true;
                    }
                }
                if (((this.Bot.GetMonsterCount() - this.Bot.GetMonstersInExtraZone().Count) >= 2) &&
                    (this.Bot.HasInMonstersZone(CardId.GustoGulldo) || this.Bot.HasInMonstersZone(CardId.WindwitchGlassBell)) &&
                    this.Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto))
                {
                    if (this.Card.IsFacedown())
                    {
                        return true;
                    }
                }
            }
            
            if (this.Card.IsFacedown())
            {
                return false;
            }

            return base.DefaultMonsterRepos();
        }
        public override bool OnSelectHand()
        {
            // go first
            return true;
        }
        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (attacker.IsCode(CardId.CrystalWingSynchroDragon))
            {
                if (defender.Level >= 5)
                {
                    attacker.RealPower = attacker.Attack + defender.Attack;
                }

                return true;
            }
            else if (attacker.IsCode(CardId.DaigustoSphreez))
            {
                attacker.RealPower = attacker.Attack + defender.Attack + defender.Defense;
                return true;
            }
            else if (this.Bot.HasInMonstersZone(CardId.DaigustoSphreez) &&
                attacker.IsCode(CardId.DaigustoSphreez, CardId.GustoGulldo, CardId.GustoEgul, CardId.WindaPriestessOfGusto, CardId.PilicaDescendantOfGusto, CardId.DaigustoGulldos))
            {
                attacker.RealPower = attacker.Attack + defender.Attack + defender.Defense;
                return true;
            }
            return base.OnPreBattleBetween(attacker, defender);
        }

    }
}