using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000098 RID: 152
	public static class Publisher
	{
		// Token: 0x06000320 RID: 800 RVA: 0x00014818 File Offset: 0x00012A18
		public static void PublishMessage(BrowserControlTags tag, string vmName = "", JObject extraData = null)
		{
			Logger.Info(string.Format("Install boot: PublishMessage {0}", tag));
			switch (tag)
			{
			case BrowserControlTags.bootComplete:
				EventAggregator.Publish<BootCompleteEventArgs>(new BootCompleteEventArgs(BrowserControlTags.bootComplete, vmName, extraData));
				return;
			case BrowserControlTags.googleSigninComplete:
				EventAggregator.Publish<GoogleSignInCompleteEventArgs>(new GoogleSignInCompleteEventArgs(BrowserControlTags.googleSigninComplete, vmName, extraData));
				return;
			case BrowserControlTags.appPlayerClosing:
				EventAggregator.Publish<AppPlayerClosingEventArgs>(new AppPlayerClosingEventArgs(BrowserControlTags.appPlayerClosing, vmName, extraData));
				return;
			case BrowserControlTags.tabClosing:
				EventAggregator.Publish<TabClosingEventArgs>(new TabClosingEventArgs(BrowserControlTags.tabClosing, vmName, extraData));
				return;
			case BrowserControlTags.tabSwitched:
				EventAggregator.Publish<TabSwitchedEventArgs>(new TabSwitchedEventArgs(BrowserControlTags.tabSwitched, vmName, extraData));
				return;
			case BrowserControlTags.appInstalled:
				EventAggregator.Publish<AppInstalledEventArgs>(new AppInstalledEventArgs(BrowserControlTags.appInstalled, vmName, extraData));
				return;
			case BrowserControlTags.appUninstalled:
				EventAggregator.Publish<AppUninstalledEventArgs>(new AppUninstalledEventArgs(BrowserControlTags.appUninstalled, vmName, extraData));
				return;
			case BrowserControlTags.grmAppListUpdate:
				EventAggregator.Publish<GrmAppListUpdateEventArgs>(new GrmAppListUpdateEventArgs(BrowserControlTags.grmAppListUpdate, vmName, extraData));
				return;
			case BrowserControlTags.apkDownloadStarted:
				EventAggregator.Publish<ApkDownloadStartedEventArgs>(new ApkDownloadStartedEventArgs(BrowserControlTags.apkDownloadStarted, vmName, extraData));
				return;
			case BrowserControlTags.apkDownloadFailed:
				EventAggregator.Publish<ApkDownloadFailedEventArgs>(new ApkDownloadFailedEventArgs(BrowserControlTags.apkDownloadFailed, vmName, extraData));
				return;
			case BrowserControlTags.apkDownloadCurrentProgress:
				EventAggregator.Publish<ApkDownloadCurrentProgressEventArgs>(new ApkDownloadCurrentProgressEventArgs(BrowserControlTags.apkDownloadCurrentProgress, vmName, extraData));
				return;
			case BrowserControlTags.apkDownloadCompleted:
				EventAggregator.Publish<ApkDownloadCompletedEventArgs>(new ApkDownloadCompletedEventArgs(BrowserControlTags.apkDownloadCompleted, vmName, extraData));
				return;
			case BrowserControlTags.apkInstallStarted:
				EventAggregator.Publish<ApkInstallStartedEventArgs>(new ApkInstallStartedEventArgs(BrowserControlTags.apkInstallStarted, vmName, extraData));
				return;
			case BrowserControlTags.apkInstallFailed:
				EventAggregator.Publish<ApkInstallFailedEventArgs>(new ApkInstallFailedEventArgs(BrowserControlTags.apkInstallFailed, vmName, extraData));
				return;
			case BrowserControlTags.apkInstallCompleted:
				EventAggregator.Publish<ApkInstallCompletedEventArgs>(new ApkInstallCompletedEventArgs(BrowserControlTags.apkInstallCompleted, vmName, extraData));
				return;
			case BrowserControlTags.getVmInfo:
				EventAggregator.Publish<GetVmInfoEventArgs>(new GetVmInfoEventArgs(BrowserControlTags.getVmInfo, vmName, extraData));
				return;
			case BrowserControlTags.userInfoUpdated:
				EventAggregator.Publish<UserInfoUpdatedEventArgs>(new UserInfoUpdatedEventArgs(BrowserControlTags.userInfoUpdated, vmName, extraData));
				return;
			case BrowserControlTags.themeChange:
				EventAggregator.Publish<ThemeChangeEventArgs>(new ThemeChangeEventArgs(BrowserControlTags.themeChange, vmName, extraData));
				return;
			case BrowserControlTags.oemDownloadStarted:
				EventAggregator.Publish<OemDownloadStartedEventArgs>(new OemDownloadStartedEventArgs(BrowserControlTags.oemDownloadStarted, vmName, extraData));
				return;
			case BrowserControlTags.oemDownloadFailed:
				EventAggregator.Publish<OemDownloadFailedEventArgs>(new OemDownloadFailedEventArgs(BrowserControlTags.oemDownloadFailed, vmName, extraData));
				return;
			case BrowserControlTags.oemDownloadCurrentProgress:
				EventAggregator.Publish<OemDownloadCurrentProgressEventArgs>(new OemDownloadCurrentProgressEventArgs(BrowserControlTags.oemDownloadCurrentProgress, vmName, extraData));
				return;
			case BrowserControlTags.oemDownloadCompleted:
				EventAggregator.Publish<OemDownloadCompletedEventArgs>(new OemDownloadCompletedEventArgs(BrowserControlTags.oemDownloadCompleted, vmName, extraData));
				return;
			case BrowserControlTags.oemInstallStarted:
				EventAggregator.Publish<OemInstallStartedEventArgs>(new OemInstallStartedEventArgs(BrowserControlTags.oemInstallStarted, vmName, extraData));
				return;
			case BrowserControlTags.oemInstallFailed:
				EventAggregator.Publish<OemInstallFailedEventArgs>(new OemInstallFailedEventArgs(BrowserControlTags.oemInstallFailed, vmName, extraData));
				return;
			case BrowserControlTags.oemInstallCompleted:
				EventAggregator.Publish<OemInstallCompletedEventArgs>(new OemInstallCompletedEventArgs(BrowserControlTags.oemInstallCompleted, vmName, extraData));
				return;
			case BrowserControlTags.showFlePopup:
				EventAggregator.Publish<ShowFlePopupEventArgs>(new ShowFlePopupEventArgs(BrowserControlTags.showFlePopup, vmName, extraData));
				return;
			case BrowserControlTags.currentlyRunningApps:
				EventAggregator.Publish<CurrentlyRunningAppsEventArgs>(new CurrentlyRunningAppsEventArgs(BrowserControlTags.currentlyRunningApps, vmName, extraData));
				return;
			case BrowserControlTags.handleFle:
				EventAggregator.Publish<HandleFleEventArgs>(new HandleFleEventArgs(BrowserControlTags.handleFle, vmName, extraData));
				return;
			case BrowserControlTags.getVolumeLevel:
				EventAggregator.Publish<GetVolumeLevelEventArgs>(new GetVolumeLevelEventArgs(BrowserControlTags.getVolumeLevel, vmName, extraData));
				return;
			case BrowserControlTags.toggleMicrophoneMuteState:
				EventAggregator.Publish<ToggleMicrophoneMuteStateEventArgs>(new ToggleMicrophoneMuteStateEventArgs(BrowserControlTags.toggleMicrophoneMuteState, vmName, extraData));
				return;
			default:
				return;
			}
		}
	}
}
