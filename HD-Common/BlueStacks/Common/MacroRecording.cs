using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x020000E0 RID: 224
	[Serializable]
	public class MacroRecording : BiDirectionalVertex<MacroRecording>, INotifyPropertyChanged, IEquatable<MacroRecording>
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600059B RID: 1435 RVA: 0x0001BF30 File Offset: 0x0001A130
		// (remove) Token: 0x0600059C RID: 1436 RVA: 0x0001BF68 File Offset: 0x0001A168
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600059D RID: 1437 RVA: 0x00005314 File Offset: 0x00003514
		protected void OnPropertyChanged(string property)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(property));
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000532D File Offset: 0x0000352D
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x00005335 File Offset: 0x00003535
		[JsonProperty("TimeCreated")]
		public string TimeCreated { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000533E File Offset: 0x0000353E
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x00005346 File Offset: 0x00003546
		[JsonProperty("FileName")]
		public string FileName { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000534F File Offset: 0x0000354F
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x00005357 File Offset: 0x00003557
		[JsonProperty("Name")]
		public string Name { get; set; } = string.Empty;

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00005360 File Offset: 0x00003560
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x00005368 File Offset: 0x00003568
		[JsonProperty("Events", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public List<MacroEvents> Events { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00005371 File Offset: 0x00003571
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x00005379 File Offset: 0x00003579
		[JsonProperty("SourceRecordings", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> SourceRecordings { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00005382 File Offset: 0x00003582
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0000538A File Offset: 0x0000358A
		[JsonProperty("MergedMacroConfigurations", NullValueHandling = NullValueHandling.Ignore)]
		public ObservableCollection<MergedMacroConfiguration> MergedMacroConfigurations { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00005393 File Offset: 0x00003593
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0000539B File Offset: 0x0000359B
		[JsonProperty("LoopType")]
		public OperationsLoopType LoopType { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x000053A4 File Offset: 0x000035A4
		[JsonIgnore]
		public RecordingTypes RecordingType
		{
			get
			{
				if (this.Events != null)
				{
					return RecordingTypes.SingleRecording;
				}
				return RecordingTypes.MultiRecording;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x000053B1 File Offset: 0x000035B1
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x000053B9 File Offset: 0x000035B9
		[JsonProperty("LoopNumber")]
		public int LoopNumber { get; set; } = 1;

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x000053C2 File Offset: 0x000035C2
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x000053CA File Offset: 0x000035CA
		[JsonProperty("LoopTime")]
		public int LoopTime { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x000053D3 File Offset: 0x000035D3
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x000053DB File Offset: 0x000035DB
		[JsonProperty("LoopInterval")]
		public int LoopInterval { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000053E4 File Offset: 0x000035E4
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x000053EC File Offset: 0x000035EC
		[JsonProperty("Acceleration")]
		public double Acceleration { get; set; } = 1.0;

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x000053F5 File Offset: 0x000035F5
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x000053FD File Offset: 0x000035FD
		[JsonProperty("PlayOnStart")]
		public bool PlayOnStart { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00005406 File Offset: 0x00003606
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0000540E File Offset: 0x0000360E
		[JsonProperty("DonotShowWindowOnFinish")]
		public bool DonotShowWindowOnFinish { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00005417 File Offset: 0x00003617
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x0000541F File Offset: 0x0000361F
		[JsonProperty("RestartPlayer")]
		public bool RestartPlayer { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00005428 File Offset: 0x00003628
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x00005430 File Offset: 0x00003630
		[JsonProperty("RestartPlayerAfterMinutes")]
		public int RestartPlayerAfterMinutes { get; set; } = 60;

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00005439 File Offset: 0x00003639
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x00005441 File Offset: 0x00003641
		[JsonProperty("ShortCut")]
		public string Shortcut { get; set; } = string.Empty;

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000544A File Offset: 0x0000364A
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x00005452 File Offset: 0x00003652
		[JsonProperty("UserName", NullValueHandling = NullValueHandling.Ignore)]
		public string User { get; set; } = string.Empty;

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000545B File Offset: 0x0000365B
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x00005463 File Offset: 0x00003663
		[JsonProperty("AuthorPageUrl", NullValueHandling = NullValueHandling.Ignore)]
		public Uri AuthorPageUrl { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000546C File Offset: 0x0000366C
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x00005474 File Offset: 0x00003674
		[JsonProperty("MacroId", NullValueHandling = NullValueHandling.Ignore)]
		public string MacroId { get; set; } = string.Empty;

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000547D File Offset: 0x0000367D
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x00005485 File Offset: 0x00003685
		[JsonProperty("MacroPageUrl", NullValueHandling = NullValueHandling.Ignore)]
		public Uri MacroPageUrl { get; set; }

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000548E File Offset: 0x0000368E
		public bool Equals(MacroRecording other)
		{
			return other != null && string.Equals(this.Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000054A7 File Offset: 0x000036A7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MacroRecording);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000054B5 File Offset: 0x000036B5
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001BFA0 File Offset: 0x0001A1A0
		public void CopyFrom(MacroRecording previous)
		{
			if (previous != null)
			{
				this.TimeCreated = previous.TimeCreated;
				this.Name = previous.Name;
				List<MacroEvents> events = previous.Events;
				this.Events = ((events != null) ? events.DeepCopy<List<MacroEvents>>() : null);
				List<string> sourceRecordings = previous.SourceRecordings;
				this.SourceRecordings = ((sourceRecordings != null) ? sourceRecordings.DeepCopy<List<string>>() : null);
				this.MergedMacroConfigurations = JsonConvert.DeserializeObject<ObservableCollection<MergedMacroConfiguration>>(JsonConvert.SerializeObject(previous.MergedMacroConfigurations, Utils.GetSerializerSettings()), Utils.GetSerializerSettings());
				this.LoopType = previous.LoopType;
				this.LoopNumber = previous.LoopNumber;
				this.LoopTime = previous.LoopTime;
				this.LoopInterval = previous.LoopInterval;
				this.Acceleration = previous.Acceleration;
				this.PlayOnStart = previous.PlayOnStart;
				this.RestartPlayer = previous.RestartPlayer;
				this.RestartPlayerAfterMinutes = previous.RestartPlayerAfterMinutes;
				this.DonotShowWindowOnFinish = previous.DonotShowWindowOnFinish;
				this.Shortcut = previous.Shortcut;
				MacroGraph.Instance.DeLinkMacroChild(this);
			}
		}
	}
}
