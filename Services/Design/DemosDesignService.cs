﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Core.Models.Events;
using Core.Models.Source;
using DemoInfo;
using Services.Interfaces;
using Demo = Core.Models.Demo;
using Player = Core.Models.Player;
using Round = Core.Models.Round;
using Team = Core.Models.Team;
using Side = DemoInfo.Team;

namespace Services.Design
{
	public class DemosDesignService : IDemosService
	{
		public string DownloadFolderPath { get; set; }
		public long SelectedStatsAccountSteamId { get; set; }
		public bool IgnoreLaterBan { get; set; }

		public Task<List<Demo>> GetDemosHeader(List<string> folders, List<Demo> currentDemos = null, bool limit = false, long accountSteamId = 0)
		{
			List<Demo> demos = new List<Demo>();

			for (int i = 0; i < 20; i++)
			{
				ObservableCollection<Player> players = new ObservableCollection<Player>();
				Random random = new Random();

				ObservableCollection<EntryKillEvent> entryKills = new ObservableCollection<EntryKillEvent>();
				for (int indexEntryKill = 0; indexEntryKill < random.Next(5); indexEntryKill++)
				{
					Player killer = players.ElementAt(random.Next(0, 9));
					Player killed = players.ElementAt(random.Next(0, 9));
					EntryKillEvent entryKill = new EntryKillEvent(random.Next(7000, 100000), random.Next(1, 50000))
					{
						KilledSteamId = killed.SteamId,
						KilledName = killed.Name,
						KilledSide = killed.Side,
						KillerSteamId = killer.SteamId,
						KillerName = killer.Name,
						KillerSide = killer.Side
					};
					entryKills.Add(entryKill);
				}

				for (int j = 0; j < 10; j++)
				{
					Player player = new Player
					{
						Name = "player" + (j + 1),
						HeadshotCount = random.Next(14),
						OneKillCount = random.Next(10, 30),
						TwoKillCount = random.Next(10, 20),
						ThreeKillCount = random.Next(0, 10),
						FourKillCount = random.Next(0, 5),
						FiveKillCount = random.Next(0, 2),
						BombDefusedCount = random.Next(0, 2),
						BombPlantedCount = random.Next(0, 2),
						EntryKills = entryKills,
						DeathCount = random.Next(0, 32),
						KillCount = random.Next(30),
						AssistCount = random.Next(15),
						Score = random.Next(10, 80),
						RoundMvpCount = random.Next(6)
					};

					players.Add(player);
				}

				ObservableCollection<Round> rounds = new ObservableCollection<Round>();
				for (int k = 0; k < 32; k++)
				{
					Round round = new Round
					{
						Number = k + 1,
						OneKillCount = random.Next(5),
						TwoKillCount = random.Next(2),
						ThreeKillCount = random.Next(1),
						FourKillCount = random.Next(1),
						FiveKillCount = random.Next(1),
						EquipementValueTeam1 = random.Next(4200, 30000),
						EquipementValueTeam2 = random.Next(4200, 30000),
						StartMoneyTeam1 = random.Next(4200, 50000),
						StartMoneyTeam2 = random.Next(4200, 50000),
						Tick = random.Next(7000, 100000)
					};

					rounds.Add(round);
				}

				Demo demo = new Demo
				{
					Id = "de_dust25445648778447878",
					Name = "mydemo" + (i + 1) + ".dem",
					Tickrate = 128,
					MapName = "de_dust2",
					ClientName = "localhost",
					Hostname = "local",
					OneKillCount = random.Next(50, 90),
					TwoKillCount = random.Next(20, 50),
					ThreeKillCount = random.Next(10),
					FourKillCount = random.Next(3),
					FiveKillCount = random.Next(1),
					Path = "C:\\mydemo.dem",
					ScoreTeam1 = 16,
					ScoreTeam2 = 6,
					Type = "GOTV",
					Comment = "comment",
					ScoreFirstHalfTeam1 = 10,
					ScoreFirstHalfTeam2 = 5,
					ScoreSecondHalfTeam1 = 6,
					ScoreSecondHalfTeam2 = 1,
					Players = players,
					MostBombPlantedPlayer = players.ElementAt(random.Next(10)),
					MostHeadshotPlayer = players.ElementAt(random.Next(10)),
					Rounds = rounds
				};

				demos.Add(demo);
			}

			return Task.FromResult(demos);
		}

		public Task<Demo> GetDemoDataAsync(Demo demo)
		{
			Demo newDemo = new Demo();
			return Task.FromResult(newDemo);
		}

		public Task<Demo> AnalyzeDemo(Demo demo, CancellationToken token)
		{
			Random random = new Random();

			ObservableCollection<Player> players = new ObservableCollection<Player>();
			for (int i = 0; i < 10; i++)
			{
				Player player = new Player
				{
					Name = "player" + (i + 1),
					HeadshotCount = random.Next(14),
					OneKillCount = random.Next(10, 30),
					TwoKillCount = random.Next(10, 20),
					ThreeKillCount = random.Next(0, 10),
					FourKillCount = random.Next(0, 5),
					FiveKillCount = random.Next(0, 2),
					BombDefusedCount = random.Next(0, 2),
					BombPlantedCount = random.Next(0, 2),
					DeathCount = random.Next(0, 32),
					KillCount = random.Next(30),
					AssistCount = random.Next(15),
					Score = random.Next(10, 80),
					RoundMvpCount = random.Next(6),
					RankNumberNew = 5,
					RankNumberOld = 4,
					RatingHltv = (float)random.NextDouble(),
					SteamId = random.Next(1000, 800000),
					IsOverwatchBanned = random.Next(100) < 40,
					IsVacBanned = random.Next(100) < 40,
					TeamKillCount = random.Next(0, 1),
					WinCount = random.Next(10, 687),
					MolotovThrownCount = random.Next(0, 10),
					DecoyThrownCount = random.Next(0, 10),
					IncendiaryThrownCount = random.Next(20),
					SmokeThrownCount = random.Next(20),
					FlashbangThrownCount = random.Next(20),
					HeGrenadeThrownCount = random.Next(20),
					BombExplodedCount = random.Next(5),
					AvatarUrl = string.Empty,
					KillDeathRatio = (decimal)random.NextDouble(),
					MatchCount = random.Next(100),
					RoundPlayedCount = random.Next(100)
				};

				players.Add(player);
			}
			Team teamT = new Team
			{
				Name = "Team 1",
				Players = new ObservableCollection<Player>(players.Take(5))
			};
			Team teamCt = new Team
			{
				Name = "Team 2",
				Players = new ObservableCollection<Player>(players.Skip(5).Take(5))
			};

			ObservableCollection<Round> rounds = new ObservableCollection<Round>();
			for (int i = 0; i < 32; i++)
			{
				ObservableCollection<KillEvent> kills = new ObservableCollection<KillEvent>();
				for (int j = 0; j < random.Next(1, 9); j++)
				{
					Player killer = players.ElementAt(random.Next(9));
					Player killed = players.ElementAt(random.Next(9));
					kills.Add(new KillEvent(random.Next(1, 10000), random.Next(1, 100))
					{
						KillerName = killer.Name,
						KillerSteamId = killer.SteamId,
						KillerSide = killer.Side,
						KilledName = killed.Name,
						KilledSteamId = killed.SteamId,
						KilledSide = killed.Side,
						RoundNumber = i,
						Weapon = Weapon.WeaponList.ElementAt(random.Next(44))
					});
				}

				// generate open / entry kills for this round
				Round round = new Round
				{
					Number = i + 1,
					OneKillCount = random.Next(5),
					TwoKillCount = random.Next(2),
					ThreeKillCount = random.Next(1),
					FourKillCount = random.Next(1),
					FiveKillCount = random.Next(1),
					EquipementValueTeam1 = random.Next(4200, 30000),
					EquipementValueTeam2 = random.Next(4200, 30000),
					StartMoneyTeam1 = random.Next(4200, 50000),
					StartMoneyTeam2 = random.Next(4200, 50000),
					Tick = random.Next(7000, 100000),
					WinnerName = teamCt.Name,
					WinnerSide = Side.CounterTerrorist,
					StartTimeSeconds = random.Next(1),
					BombDefused = null,
					EndTimeSeconds = random.Next(100),
					BombPlanted = null,
					BombExploded = null,
					Type = RoundType.NORMAL,
					EndReason = RoundEndReason.CTWin,
					EntryKillEvent = new EntryKillEvent(random.Next(1, 10000), random.Next(1, 100))
					{
						HasWon = random.Next(100) < 50,
						KillerSteamId = players.ElementAt(random.Next(9)).SteamId,
						KillerName = players.ElementAt(random.Next(9)).Name,
						KilledSteamId = players.ElementAt(random.Next(9)).SteamId,
						KilledName = players.ElementAt(random.Next(9)).Name,
						Weapon = Weapon.WeaponList.ElementAt(random.Next(44)),
						KilledSide = Side.CounterTerrorist,
						KillerSide = Side.Terrorist
					},
					SideTrouble = Side.Spectate,
					Kills = kills
				};
				rounds.Add(round);
			}

			demo.Id = "de_dust25445648778447878";
			demo.Source = new Valve();
			demo.Name = "esea_nip_vs_titan.dem";
			demo.Tickrate = 15;
			demo.ServerTickrate = 128;
			demo.MapName = "de_dust2";
			demo.ClientName = "localhost";
			demo.Hostname = "local";
			demo.OneKillCount = 90;
			demo.TwoKillCount = 30;
			demo.ThreeKillCount = 25;
			demo.FourKillCount = 3;
			demo.FiveKillCount = 1;
			demo.Path = "C:\\mydemo.dem";
			demo.ScoreTeam1 = 16;
			demo.ScoreTeam2 = 6;
			demo.Type = "GOTV";
			demo.Comment = "comment";
			demo.ScoreFirstHalfTeam1 = 10;
			demo.ScoreFirstHalfTeam2 = 5;
			demo.ScoreSecondHalfTeam1 = 6;
			demo.ScoreSecondHalfTeam2 = 1;
			demo.TeamCT = teamCt;
			demo.TeamT = teamT;
			demo.Players = players;
			demo.Rounds = rounds;
			demo.MostKillingWeapon = Weapon.WeaponList[random.Next(44)];
			foreach (KillEvent e in rounds.SelectMany(round => round.Kills)) demo.Kills.Add(e);

			return Task.FromResult(demo);
		}

		public Task SaveComment(Demo demo, string comment)
		{
			demo.Comment = comment;
			return Task.FromResult(demo);
		}

		public Task SaveStatus(Demo demo, string status)
		{
			demo.Status = status;
			return Task.FromResult(demo);
		}

		public Task SetSource(ObservableCollection<Demo> demos, string source)
		{
			return Task.FromResult(true);
		}

		public Task<Demo> AnalyzePlayersPosition(Demo demo, CancellationToken token)
		{
			return Task.FromResult(demo);
		}

		public Task<List<Demo>> GetDemosFromBackup(string jsonFile)
		{
			return Task.FromResult(new List<Demo>());
		}

		public Task<Demo> AnalyzeBannedPlayersAsync(Demo demo)
		{
			return Task.FromResult(demo);
		}

		public Task<Rank> GetLastRankAccountStatsAsync(long steamId)
		{
			return Task.FromResult(AppSettings.RankList[0]);
		}

		public Task<List<Demo>> GetDemosPlayer(string steamId)
		{
			return Task.FromResult(new List<Demo>());
		}

		public Task<bool> DeleteDemo(Demo demo)
		{
			return Task.FromResult(true);
		}

		public Task<Dictionary<string, string>> GetDemoListUrl()
		{
			return Task.FromResult(new Dictionary<string, string>());
		}

		public Task<bool> DownloadDemo(string url, string location)
		{
			return Task.FromResult(true);
		}

		public Task<bool> DecompressDemoArchive(string demoName)
		{
			return Task.FromResult(true);
		}
	}
}
