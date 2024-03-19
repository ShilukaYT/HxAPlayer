using System;

namespace BlueStacks.Common
{
	// Token: 0x0200005A RID: 90
	public static class HTTPRoutes
	{
		// Token: 0x020000BB RID: 187
		public static class Agent
		{
			// Token: 0x040005CB RID: 1483
			public const string Ping = "ping";

			// Token: 0x040005CC RID: 1484
			public const string GuestBootFailed = "guestBootFailed";

			// Token: 0x040005CD RID: 1485
			public const string LaunchDefaultWebApp = "launchDefaultWebApp";

			// Token: 0x040005CE RID: 1486
			public const string Installed = "installed";

			// Token: 0x040005CF RID: 1487
			public const string Uninstalled = "uninstalled";

			// Token: 0x040005D0 RID: 1488
			public const string GetAppList = "getAppList";

			// Token: 0x040005D1 RID: 1489
			public const string Install = "install";

			// Token: 0x040005D2 RID: 1490
			public const string Uninstall = "uninstall";

			// Token: 0x040005D3 RID: 1491
			public const string RunApp = "runApp";

			// Token: 0x040005D4 RID: 1492
			public const string SetLocale = "setLocale";

			// Token: 0x040005D5 RID: 1493
			public const string InstallAppByUrl = "installAppByUrl";

			// Token: 0x040005D6 RID: 1494
			public const string AppCrashedInfo = "appCrashedInfo";

			// Token: 0x040005D7 RID: 1495
			public const string GetUserData = "getUserData";

			// Token: 0x040005D8 RID: 1496
			public const string ShowNotification = "showNotification";

			// Token: 0x040005D9 RID: 1497
			public const string AppDownloadStatus = "appDownloadStatus";

			// Token: 0x040005DA RID: 1498
			public const string ShowFeNotification = "showFeNotification";

			// Token: 0x040005DB RID: 1499
			public const string BindMount = "bindmount";

			// Token: 0x040005DC RID: 1500
			public const string UnbindMount = "unbindmount";

			// Token: 0x040005DD RID: 1501
			public const string QuitFrontend = "quitFrontend";

			// Token: 0x040005DE RID: 1502
			public const string GetAppImage = "getAppImage";

			// Token: 0x040005DF RID: 1503
			public const string ShowTrayNotification = "showTrayNotification";

			// Token: 0x040005E0 RID: 1504
			public const string Restart = "restart";

			// Token: 0x040005E1 RID: 1505
			public const string Notification = "notification";

			// Token: 0x040005E2 RID: 1506
			public const string Clipboard = "clipboard";

			// Token: 0x040005E3 RID: 1507
			public const string IsAppInstalled = "isAppInstalled";

			// Token: 0x040005E4 RID: 1508
			public const string TopActivityInfo = "topActivityInfo";

			// Token: 0x040005E5 RID: 1509
			public const string SysTrayVisibility = "sysTrayVisibility";

			// Token: 0x040005E6 RID: 1510
			public const string RestartAgent = "restartAgent";

			// Token: 0x040005E7 RID: 1511
			public const string ShowTileInterface = "showTileInterface";

			// Token: 0x040005E8 RID: 1512
			public const string SetNewLocation = "setNewLocation";

			// Token: 0x040005E9 RID: 1513
			public const string AdEvents = "adEvents";

			// Token: 0x040005EA RID: 1514
			public const string ExitAgent = "exitAgent";

			// Token: 0x040005EB RID: 1515
			public const string StopApp = "stopApp";

			// Token: 0x040005EC RID: 1516
			public const string ReleaseApkInstallThread = "releaseApkInstallThread";

			// Token: 0x040005ED RID: 1517
			public const string ClearAppData = "clearAppData";

			// Token: 0x040005EE RID: 1518
			public const string RestartGameManager = "restartGameManager";

			// Token: 0x040005EF RID: 1519
			public const string PostHttpUrl = "postHttpUrl";

			// Token: 0x040005F0 RID: 1520
			public const string InstanceExist = "instanceExist";

			// Token: 0x040005F1 RID: 1521
			public const string QueryInstances = "queryInstances";

			// Token: 0x040005F2 RID: 1522
			public const string CreateInstance = "createInstance";

			// Token: 0x040005F3 RID: 1523
			public const string DeleteInstance = "deleteInstance";

			// Token: 0x040005F4 RID: 1524
			public const string ResetSharedFolders = "resetSharedFolders";

			// Token: 0x040005F5 RID: 1525
			public const string StartInstance = "startInstance";

			// Token: 0x040005F6 RID: 1526
			public const string GetRunningInstances = "getRunningInstances";

			// Token: 0x040005F7 RID: 1527
			public const string StopInstance = "stopInstance";

			// Token: 0x040005F8 RID: 1528
			public const string SetVmConfig = "setVmConfig";

			// Token: 0x040005F9 RID: 1529
			public const string IsMultiInstanceSupported = "isMultiInstanceSupported";

			// Token: 0x040005FA RID: 1530
			public const string SetCpu = "setCpu";

			// Token: 0x040005FB RID: 1531
			public const string SetDpi = "setDpi";

			// Token: 0x040005FC RID: 1532
			public const string SetRam = "setRam";

			// Token: 0x040005FD RID: 1533
			public const string SetResolution = "setResolution";

			// Token: 0x040005FE RID: 1534
			public const string GetGuid = "getGuid";

			// Token: 0x040005FF RID: 1535
			public const string Backup = "backup";

			// Token: 0x04000600 RID: 1536
			public const string Restore = "restore";

			// Token: 0x04000601 RID: 1537
			public const string AppJsonUpdatedForVideo = "appJsonUpdatedForVideo";

			// Token: 0x04000602 RID: 1538
			public const string DeviceProfileUpdated = "deviceProfileUpdated";

			// Token: 0x04000603 RID: 1539
			public const string InstanceStopped = "instanceStopped";

			// Token: 0x04000604 RID: 1540
			public const string GetInstanceStatus = "getInstanceStatus";

			// Token: 0x04000605 RID: 1541
			public const string IsEngineReady = "isEngineReady";

			// Token: 0x04000606 RID: 1542
			public const string FrontendStatusUpdate = "FrontendStatusUpdate";

			// Token: 0x04000607 RID: 1543
			public const string GuestStatusUpdate = "GuestStatusUpdate";

			// Token: 0x04000608 RID: 1544
			public const string CopyToAndroid = "copyToAndroid";

			// Token: 0x04000609 RID: 1545
			public const string CopyToWindows = "copyToWindows";

			// Token: 0x0400060A RID: 1546
			public const string SetCurrentVolume = "setCurrentVolume";

			// Token: 0x0400060B RID: 1547
			public const string DownloadInstalledAppsCfg = "downloadInstalledAppsCfg";

			// Token: 0x0400060C RID: 1548
			public const string SetVMDisplayName = "setVMDisplayName";

			// Token: 0x0400060D RID: 1549
			public const string SortWindows = "sortWindows";

			// Token: 0x0400060E RID: 1550
			public const string MaintenanceWarning = "maintenanceWarning";

			// Token: 0x0400060F RID: 1551
			public const string EnableDebugLogs = "enableDebugLogs";

			// Token: 0x04000610 RID: 1552
			public const string LogAppClick = "logAppClick";

			// Token: 0x04000611 RID: 1553
			public const string SetNCPlayerCharacterName = "setNCPlayerCharacterName";

			// Token: 0x04000612 RID: 1554
			public const string LaunchPlay = "launchPlay";

			// Token: 0x04000613 RID: 1555
			public const string RemoveAccount = "removeAccount";

			// Token: 0x04000614 RID: 1556
			public const string SetDeviceProfile = "setDeviceProfile";

			// Token: 0x04000615 RID: 1557
			public const string ScreenLock = "screenLock";

			// Token: 0x04000616 RID: 1558
			public const string MakeDir = "makeDir";

			// Token: 0x04000617 RID: 1559
			public const string GetHeightWidth = "getHeightWidth";

			// Token: 0x04000618 RID: 1560
			public const string SetStreamingStatus = "setStreamingStatus";

			// Token: 0x04000619 RID: 1561
			public const string GetShortcut = "getShortcut";

			// Token: 0x0400061A RID: 1562
			public const string SetShortcut = "setShortcut";

			// Token: 0x0400061B RID: 1563
			public const string SendEngineTimelineStats = "sendEngineTimelineStats";

			// Token: 0x0400061C RID: 1564
			public const string GrmAppLaunch = "grmAppLaunch";

			// Token: 0x0400061D RID: 1565
			public const string ReInitLocalization = "reinitlocalization";

			// Token: 0x0400061E RID: 1566
			public const string TestCloudAnnouncement = "testCloudAnnouncement";

			// Token: 0x0400061F RID: 1567
			public const string OverrideDesktopNotificationSettings = "overrideDesktopNotificationSettings";

			// Token: 0x04000620 RID: 1568
			public const string NotificationStatsOnClosing = "notificationStatsOnClosing";

			// Token: 0x04000621 RID: 1569
			public const string ConfigFileChanged = "configFileChanged";

			// Token: 0x04000622 RID: 1570
			public const string GetCallbackStatus = "getCallbackStatus";

			// Token: 0x04000623 RID: 1571
			public const string ShowClientNotification = "showClientNotification";
		}

		// Token: 0x020000BC RID: 188
		public static class Client
		{
			// Token: 0x04000624 RID: 1572
			public const string Ping = "ping";

			// Token: 0x04000625 RID: 1573
			public const string AppDisplayed = "appDisplayed";

			// Token: 0x04000626 RID: 1574
			public const string CloseCrashedAppTab = "closeCrashedAppTab";

			// Token: 0x04000627 RID: 1575
			public const string AppLaunched = "appLaunched";

			// Token: 0x04000628 RID: 1576
			public const string ShowApp = "showApp";

			// Token: 0x04000629 RID: 1577
			public const string ShowWindow = "showWindow";

			// Token: 0x0400062A RID: 1578
			public const string IsGMVisible = "isVisible";

			// Token: 0x0400062B RID: 1579
			public const string AppUninstalled = "appUninstalled";

			// Token: 0x0400062C RID: 1580
			public const string AppInstalled = "appInstalled";

			// Token: 0x0400062D RID: 1581
			public const string EnableWndProcLogging = "enableWndProcLogging";

			// Token: 0x0400062E RID: 1582
			public const string Quit = "quit";

			// Token: 0x0400062F RID: 1583
			public const string Google = "google";

			// Token: 0x04000630 RID: 1584
			public const string ShowWebPage = "showWebPage";

			// Token: 0x04000631 RID: 1585
			public const string ShowHomeTab = "showHomeTab";

			// Token: 0x04000632 RID: 1586
			public const string CloseTab = "closeTab";

			// Token: 0x04000633 RID: 1587
			public const string GooglePlayAppInstall = "googlePlayAppInstall";

			// Token: 0x04000634 RID: 1588
			public const string AppInstallStarted = "appInstallStarted";

			// Token: 0x04000635 RID: 1589
			public const string AppInstallFailed = "appInstallFailed";

			// Token: 0x04000636 RID: 1590
			public const string OTSCompleted = "oneTimeSetupCompleted";

			// Token: 0x04000637 RID: 1591
			public const string UpdateUserInfo = "updateUserInfo";

			// Token: 0x04000638 RID: 1592
			public const string BootFailedPopup = "bootFailedPopup";

			// Token: 0x04000639 RID: 1593
			public const string OpenPackage = "openPackage";

			// Token: 0x0400063A RID: 1594
			public const string DragDropInstall = "dragDropInstall";

			// Token: 0x0400063B RID: 1595
			public const string StopInstance = "stopInstance";

			// Token: 0x0400063C RID: 1596
			public const string MinimizeInstance = "minimizeInstance";

			// Token: 0x0400063D RID: 1597
			public const string StartInstance = "startInstance";

			// Token: 0x0400063E RID: 1598
			public const string HideBluestacks = "hideBluestacks";

			// Token: 0x0400063F RID: 1599
			public const string TileWindow = "tileWindow";

			// Token: 0x04000640 RID: 1600
			public const string CascadeWindow = "cascadeWindow";

			// Token: 0x04000641 RID: 1601
			public const string ToggleFarmMode = "toggleFarmMode";

			// Token: 0x04000642 RID: 1602
			public const string LaunchWebTab = "launchWebTab";

			// Token: 0x04000643 RID: 1603
			public const string OpenNotificationSettings = "openNotificationSettings";

			// Token: 0x04000644 RID: 1604
			public const string IsAnyAppRunning = "isAnyAppRunning";

			// Token: 0x04000645 RID: 1605
			public const string ChangeTextOTS = "changeTextOTS";

			// Token: 0x04000646 RID: 1606
			public const string ShowIMESwitchPrompt = "showIMESwitchPrompt";

			// Token: 0x04000647 RID: 1607
			public const string LaunchDefaultWebApp = "launchDefaultWebApp";

			// Token: 0x04000648 RID: 1608
			public const string MacroCompleted = "macroCompleted";

			// Token: 0x04000649 RID: 1609
			public const string AppInfoUpdated = "appInfoUpdated";

			// Token: 0x0400064A RID: 1610
			public const string SendAppDisplayed = "sendAppDisplayed";

			// Token: 0x0400064B RID: 1611
			public const string IsGmVisible = "static";

			// Token: 0x0400064C RID: 1612
			public const string RestartFrontend = "restartFrontend";

			// Token: 0x0400064D RID: 1613
			public const string GcCollect = "gcCollect";

			// Token: 0x0400064E RID: 1614
			public const string ShowWindowAndApp = "showWindowAndApp";

			// Token: 0x0400064F RID: 1615
			public const string UnsupportedCpuError = "unsupportedCpuError";

			// Token: 0x04000650 RID: 1616
			public const string ChangeOrientaion = "changeOrientaion";

			// Token: 0x04000651 RID: 1617
			public const string ShootingModeChanged = "shootingModeChanged";

			// Token: 0x04000652 RID: 1618
			public const string GuestBootCompleted = "guestBootCompleted";

			// Token: 0x04000653 RID: 1619
			public const string GetRunningInstances = "getRunningInstances";

			// Token: 0x04000654 RID: 1620
			public const string AppJsonChanged = "appJsonChanged";

			// Token: 0x04000655 RID: 1621
			public const string GetCurrentAppDetails = "getCurrentAppDetails";

			// Token: 0x04000656 RID: 1622
			public const string MaintenanceWarning = "maintenanceWarning";

			// Token: 0x04000657 RID: 1623
			public const string RequirementConfigUpdated = "requirementConfigUpdated";

			// Token: 0x04000658 RID: 1624
			public const string DeviceProfileUpdated = "deviceProfileUpdated";

			// Token: 0x04000659 RID: 1625
			public const string UpdateSizeOfOverlay = "updateSizeOfOverlay";

			// Token: 0x0400065A RID: 1626
			public const string AndroidLocaleChanged = "androidLocaleChanged";

			// Token: 0x0400065B RID: 1627
			public const string SaveComboEvents = "saveComboEvents";

			// Token: 0x0400065C RID: 1628
			public const string HandleClientOperation = "handleClientOperation";

			// Token: 0x0400065D RID: 1629
			public const string MacroPlaybackComplete = "macroPlaybackComplete";

			// Token: 0x0400065E RID: 1630
			public const string ObsStatus = "obsStatus";

			// Token: 0x0400065F RID: 1631
			public const string ReportObsError = "reportObsError";

			// Token: 0x04000660 RID: 1632
			public const string CapturingError = "capturingError";

			// Token: 0x04000661 RID: 1633
			public const string OpenGLCapturingError = "openGLCapturingError";

			// Token: 0x04000662 RID: 1634
			public const string ToggleStreamingMode = "toggleStreamingMode";

			// Token: 0x04000663 RID: 1635
			public const string HandleClientGamepadButton = "handleClientGamepadButton";

			// Token: 0x04000664 RID: 1636
			public const string HandleGamepadConnection = "handleGamepadConnection";

			// Token: 0x04000665 RID: 1637
			public const string HandleGamepadGuidanceButton = "handleGamepadGuidanceButton";

			// Token: 0x04000666 RID: 1638
			public const string DeviceProvisioned = "deviceProvisioned";

			// Token: 0x04000667 RID: 1639
			public const string GoogleSignin = "googleSignin";

			// Token: 0x04000668 RID: 1640
			public const string ShowFullscreenSidebar = "showFullscreenSidebar";

			// Token: 0x04000669 RID: 1641
			public const string HideTopSideBar = "hideTopSidebar";

			// Token: 0x0400066A RID: 1642
			public const string ShowFullscreenSidebarButton = "showFullscreenSidebarButton";

			// Token: 0x0400066B RID: 1643
			public const string ShowFullscreenTopbarButton = "showFullscreenTopbarButton";

			// Token: 0x0400066C RID: 1644
			public const string UpdateLocale = "updateLocale";

			// Token: 0x0400066D RID: 1645
			public const string ScreenshotCaptured = "screenshotCaptured";

			// Token: 0x0400066E RID: 1646
			public const string SetCurrentVolumeFromAndroid = "setCurrentVolumeFromAndroid";

			// Token: 0x0400066F RID: 1647
			public const string HotKeyEvents = "hotKeyEvents";

			// Token: 0x04000670 RID: 1648
			public const string SetLocale = "setLocale";

			// Token: 0x04000671 RID: 1649
			public const string EnableDebugLogs = "enableDebugLogs";

			// Token: 0x04000672 RID: 1650
			public const string SetDMMKeymapping = "setDMMKeymapping";

			// Token: 0x04000673 RID: 1651
			public const string NCSetGameInfoOnTopBar = "ncSetGameInfoOnTopBar";

			// Token: 0x04000674 RID: 1652
			public const string LaunchPlay = "launchPlay";

			// Token: 0x04000675 RID: 1653
			public const string EnableKeyboardHookLogging = "enableKeyboardHookLogging";

			// Token: 0x04000676 RID: 1654
			public const string MuteAllInstances = "muteAllInstances";

			// Token: 0x04000677 RID: 1655
			public const string ScreenLock = "screenLock";

			// Token: 0x04000678 RID: 1656
			public const string GetHeightWidth = "getHeightWidth";

			// Token: 0x04000679 RID: 1657
			public const string AccountSetupCompleted = "accountSetupCompleted";

			// Token: 0x0400067A RID: 1658
			public const string OpenThemeEditor = "openThemeEditor";

			// Token: 0x0400067B RID: 1659
			public const string SetStreamingStatus = "setStreamingStatus";

			// Token: 0x0400067C RID: 1660
			public const string PlayerScriptModifierClick = "playerScriptModifierClick";

			// Token: 0x0400067D RID: 1661
			public const string ReloadShortcuts = "reloadShortcuts";

			// Token: 0x0400067E RID: 1662
			public const string ShowFullscreenTopBar = "showFullscreenTopbar";

			// Token: 0x0400067F RID: 1663
			public const string ReloadPromotions = "reloadPromotions";

			// Token: 0x04000680 RID: 1664
			public const string HandleOverlayControlsVisibility = "overlayControlsVisibility";

			// Token: 0x04000681 RID: 1665
			public const string ShowGrmAndLaunchApp = "showGrmAndLaunchApp";

			// Token: 0x04000682 RID: 1666
			public const string ReinitRegistry = "reinitRegistry";

			// Token: 0x04000683 RID: 1667
			public const string OpenCFGReorderTool = "openCFGReorderTool";

			// Token: 0x04000684 RID: 1668
			public const string UpdateCrc = "updateCrc";

			// Token: 0x04000685 RID: 1669
			public const string ConfigFileChanged = "configFileChanged";

			// Token: 0x04000686 RID: 1670
			public const string AddNotificationInDrawer = "addNotificationInDrawer";

			// Token: 0x04000687 RID: 1671
			public const string MarkNotificationInDrawer = "markNotificationInDrawer";

			// Token: 0x04000688 RID: 1672
			public const string CheckCallbackEnabledStatus = "checkCallbackEnabledStatus";

			// Token: 0x04000689 RID: 1673
			public const string HideOverlayWhenIMEActive = "hideOverlayWhenIMEActive";

			// Token: 0x0400068A RID: 1674
			public const string ShowImageUploadedInfo = "showImageUploadedInfo";
		}

		// Token: 0x020000BD RID: 189
		public static class Cloud
		{
			// Token: 0x0400068B RID: 1675
			public const string GetAnnouncement = "/getAnnouncement";

			// Token: 0x0400068C RID: 1676
			public const string AppUsage = "/bs3/stats/v4/usage";

			// Token: 0x0400068D RID: 1677
			public const string FrontendClickStats = "/bs3/stats/frontend_click_stats";

			// Token: 0x0400068E RID: 1678
			public const string ScheduledPing = "/api/scheduledping";

			// Token: 0x0400068F RID: 1679
			public const string ScheduledPingStats = "/stats/scheduledpingstats";

			// Token: 0x04000690 RID: 1680
			public const string SecurityMetrics = "/bs4/security_metrics";

			// Token: 0x04000691 RID: 1681
			public const string UnifiedInstallStats = "/bs3/stats/unified_install_stats";

			// Token: 0x04000692 RID: 1682
			public const string UpdateLocale = "updateLocale";

			// Token: 0x04000693 RID: 1683
			public const string ProblemCategories = "/app_settings/problem_categories";

			// Token: 0x04000694 RID: 1684
			public const string Promotions = "promotions";

			// Token: 0x04000695 RID: 1685
			public const string ClientBootPromotionStats = "bs4/stats/client_boot_promotion_stats";

			// Token: 0x04000696 RID: 1686
			public const string GrmFetchUrl = "grm/files";

			// Token: 0x04000697 RID: 1687
			public const string BtvFetchUrl = "bs4/btv/GetBTVFile";

			// Token: 0x04000698 RID: 1688
			public const string HelpArticles = "help_articles";

			// Token: 0x04000699 RID: 1689
			public const string GuidanceWindow = "guidance_window";

			// Token: 0x0400069A RID: 1690
			public const string CalendarStats = "/bs4/stats/calendar_stats";

			// Token: 0x0400069B RID: 1691
			public const string PostBootUrl = "/bs4/post_boot";

			// Token: 0x0400069C RID: 1692
			public const string InstanceWisePostBootDataUrl = "/bs4/multi_instance";
		}

		// Token: 0x020000BE RID: 190
		public static class HelpArticlesKeys
		{
			// Token: 0x0400069D RID: 1693
			public const string KMScriptFAQ = "keymapping_script_faq";

			// Token: 0x0400069E RID: 1694
			public const string BS4MinRequirements = "bs3_nougat_min_requirements";

			// Token: 0x0400069F RID: 1695
			public const string BGPCompatKKVersion = "bgp_kk_compat_version";

			// Token: 0x040006A0 RID: 1696
			public const string EnableVirtualization = "enable_virtualization";

			// Token: 0x040006A1 RID: 1697
			public const string VtxUnavailable = "vtx_unavailable";

			// Token: 0x040006A2 RID: 1698
			public const string UpgradeSupportInfo = "upgrade_support_info";

			// Token: 0x040006A3 RID: 1699
			public const string BS3MinRequirements = "bs3_min_requirements";

			// Token: 0x040006A4 RID: 1700
			public const string DisableAntivirus = "disable_antivirus";

			// Token: 0x040006A5 RID: 1701
			public const string ChangePowerPlan = "change_powerplan";

			// Token: 0x040006A6 RID: 1702
			public const string FailedSslConnection = "failed_ssl_connection";

			// Token: 0x040006A7 RID: 1703
			public const string AudioServiceIssue = "audio_service_issue";

			// Token: 0x040006A8 RID: 1704
			public const string TermsOfUse = "terms_of_use";

			// Token: 0x040006A9 RID: 1705
			public const string DisableHypervisors = "disable_hypervisor";

			// Token: 0x040006AA RID: 1706
			public const string HyperVComputePlatformEnableGuide = "hypervisor_compute_platform_guide";

			// Token: 0x040006AB RID: 1707
			public const string ChangeGraphicsMode = "change_graphics_mode";

			// Token: 0x040006AC RID: 1708
			public const string AdvancedGameControl = "advanced_game_control";

			// Token: 0x040006AD RID: 1709
			public const string SmartControl = "smart_control";

			// Token: 0x040006AE RID: 1710
			public const string GameSettingsKnowMorePubg = "game_settings_know_more_pubg";

			// Token: 0x040006AF RID: 1711
			public const string GameSettingsKnowMoreFreefire = "game_settings_know_more_freefire";

			// Token: 0x040006B0 RID: 1712
			public const string GameSettingsKnowMoreCOD = "game_settings_know_more_callofduty";

			// Token: 0x040006B1 RID: 1713
			public const string GameSettingsKnowMoreSevenDeadly = "game_settings_know_more_sevendeadly";

			// Token: 0x040006B2 RID: 1714
			public const string ProfileSettingsWarningInPubg = "profile_settings_warning_pubg";

			// Token: 0x040006B3 RID: 1715
			public const string FreeDiskSpaceUsingDiskCompactiontool = "free_disk_space_using_diskcompactiontool";

			// Token: 0x040006B4 RID: 1716
			public const string GameGuideReadArticle = "game_guide_article";

			// Token: 0x040006B5 RID: 1717
			public const string AbiHelpUrl = "ABI_Help";

			// Token: 0x040006B6 RID: 1718
			public const string AstcHelpUrl = "ASTC_Help";

			// Token: 0x040006B7 RID: 1719
			public const string VsyncHelpUrl = "VSync_Help";

			// Token: 0x040006B8 RID: 1720
			public const string GpuSettingHelpUrl = "GPU_Setting_Help";

			// Token: 0x040006B9 RID: 1721
			public const string MergeMacroHelpUrl = "MergeMacro_Help";

			// Token: 0x040006BA RID: 1722
			public const string NativeGamepadHelpUrl = "native_gamepad_help";

			// Token: 0x040006BB RID: 1723
			public const string UnsureWhereStart = "unsure_start";

			// Token: 0x040006BC RID: 1724
			public const string TroubleInstallingRunningGame = "trouble_installing_running_game";

			// Token: 0x040006BD RID: 1725
			public const string StopMovementMOBAHelpUrl = "moba_stop_movement_help";

			// Token: 0x040006BE RID: 1726
			public const string MOBASkillSettingsHelpUrl = "moba_skill_settings_help";

			// Token: 0x040006BF RID: 1727
			public const string HowToUpdateHelpUrl = "how_to_update_help";

			// Token: 0x040006C0 RID: 1728
			public const string MIMHelpUrl = "MIM_help";

			// Token: 0x040006C1 RID: 1729
			public const string EcoModeHelpUrl = "EcoMode_help";

			// Token: 0x040006C2 RID: 1730
			public const string NotificationModeHelpUrl = "notification_mode_help";

			// Token: 0x040006C3 RID: 1731
			public const string LogCollectorAllInstancesHelpUrl = "log_collector_all_instances_help";

			// Token: 0x040006C4 RID: 1732
			public const string AndroidVersionHelpUrl = "android_version_help";

			// Token: 0x040006C5 RID: 1733
			public const string GameControlsHelpUrl = "game_controls_help";

			// Token: 0x040006C6 RID: 1734
			public const string GamepadConnectedNotifHelpUrl = "gamepad_connected_notif_help";

			// Token: 0x040006C7 RID: 1735
			public const string MacroTouchPointsHelpUrl = "macro_touch_points_help";

			// Token: 0x040006C8 RID: 1736
			public const string TouchSoundHelpUrl = "touch_sound_help";

			// Token: 0x040006C9 RID: 1737
			public const string BellNotificationsHelpUrl = "bell_notifications_help";

			// Token: 0x040006CA RID: 1738
			public const string DesktopNotificationsHelpUrl = "desktop_notifications_help";

			// Token: 0x040006CB RID: 1739
			public const string EnableHypervisor = "enable_hyperv";
		}

		// Token: 0x020000BF RID: 191
		public static class BTv
		{
			// Token: 0x040006CC RID: 1740
			public const string Ping = "ping";

			// Token: 0x040006CD RID: 1741
			public const string ReceiveAppInstallStatus = "receiveAppInstallStatus";
		}

		// Token: 0x020000C0 RID: 192
		public static class Engine
		{
			// Token: 0x040006CE RID: 1742
			public const string Ping = "ping";

			// Token: 0x040006CF RID: 1743
			public const string RefreshKeymapUri = "refreshKeymap";

			// Token: 0x040006D0 RID: 1744
			public const string Shutdown = "shutdown";

			// Token: 0x040006D1 RID: 1745
			public const string SwitchOrientation = "switchOrientation";

			// Token: 0x040006D2 RID: 1746
			public const string ShowWindow = "showWindow";

			// Token: 0x040006D3 RID: 1747
			public const string RefreshWindow = "refreshWindow";

			// Token: 0x040006D4 RID: 1748
			public const string SetParent = "setParent";

			// Token: 0x040006D5 RID: 1749
			public const string ShareScreenshot = "shareScreenshot";

			// Token: 0x040006D6 RID: 1750
			public const string GoBack = "goBack";

			// Token: 0x040006D7 RID: 1751
			public const string CloseScreen = "closeScreen";

			// Token: 0x040006D8 RID: 1752
			public const string SoftControlBarEvent = "softControlBarEvent";

			// Token: 0x040006D9 RID: 1753
			public const string InputMapperFilesDownloaded = "inputMapperFilesDownloaded";

			// Token: 0x040006DA RID: 1754
			public const string EnableWndProcLogging = "enableWndProcLogging";

			// Token: 0x040006DB RID: 1755
			public const string PingVm = "pingVm";

			// Token: 0x040006DC RID: 1756
			public const string CopyFiles = "copyFiles";

			// Token: 0x040006DD RID: 1757
			public const string GetWindowsFiles = "getWindowsFiles";

			// Token: 0x040006DE RID: 1758
			public const string GpsCoordinates = "gpsCoordinates";

			// Token: 0x040006DF RID: 1759
			public const string InitGamepad = "initGamepad";

			// Token: 0x040006E0 RID: 1760
			public const string GetVolume = "getVolume";

			// Token: 0x040006E1 RID: 1761
			public const string SetVolume = "setVolume";

			// Token: 0x040006E2 RID: 1762
			public const string TopDisplayedActivityInfo = "topDisplayedActivityInfo";

			// Token: 0x040006E3 RID: 1763
			public const string AppDisplayed = "appDisplayed";

			// Token: 0x040006E4 RID: 1764
			public const string GoHome = "goHome";

			// Token: 0x040006E5 RID: 1765
			public const string IsKeyboardEnabled = "isKeyboardEnabled";

			// Token: 0x040006E6 RID: 1766
			public const string SetKeymappingState = "setKeymappingState";

			// Token: 0x040006E7 RID: 1767
			public const string Keymap = "keymap";

			// Token: 0x040006E8 RID: 1768
			public const string SetFrontendVisibility = "setFrontendVisibility";

			// Token: 0x040006E9 RID: 1769
			public const string GetFeSize = "getFeSize";

			// Token: 0x040006EA RID: 1770
			public const string Mute = "mute";

			// Token: 0x040006EB RID: 1771
			public const string Unmute = "unmute";

			// Token: 0x040006EC RID: 1772
			public const string GetCurrentKeymappingStatus = "getCurrentKeymappingStatus";

			// Token: 0x040006ED RID: 1773
			public const string Shake = "shake";

			// Token: 0x040006EE RID: 1774
			public const string IsKeyNameFocussed = "isKeyNameFocussed";

			// Token: 0x040006EF RID: 1775
			public const string AndroidImeSelected = "androidImeSelected";

			// Token: 0x040006F0 RID: 1776
			public const string IsGpsSupported = "isGpsSupported";

			// Token: 0x040006F1 RID: 1777
			public const string InstallApk = "installApk";

			// Token: 0x040006F2 RID: 1778
			public const string InjectCopy = "injectCopy";

			// Token: 0x040006F3 RID: 1779
			public const string InjectPaste = "injectPaste";

			// Token: 0x040006F4 RID: 1780
			public const string StopZygote = "stopZygote";

			// Token: 0x040006F5 RID: 1781
			public const string StartZygote = "startZygote";

			// Token: 0x040006F6 RID: 1782
			public const string GetKeyMappingParserVersion = "getKeyMappingParserVersion";

			// Token: 0x040006F7 RID: 1783
			public const string VibrateHostWindow = "vibrateHostWindow";

			// Token: 0x040006F8 RID: 1784
			public const string LocaleChanged = "localeChanged";

			// Token: 0x040006F9 RID: 1785
			public const string GetScreenshot = "getScreenshot";

			// Token: 0x040006FA RID: 1786
			public const string SetPcImeWorkflow = "setPcImeWorkflow";

			// Token: 0x040006FB RID: 1787
			public const string SetUserInfo = "setUserInfo";

			// Token: 0x040006FC RID: 1788
			public const string GetUserInfo = "getUserInfo";

			// Token: 0x040006FD RID: 1789
			public const string GetPremium = "getPremium";

			// Token: 0x040006FE RID: 1790
			public const string SetCursorStyle = "setCursorStyle";

			// Token: 0x040006FF RID: 1791
			public const string OpenMacroWindow = "openMacroWindow";

			// Token: 0x04000700 RID: 1792
			public const string StartReroll = "startReroll";

			// Token: 0x04000701 RID: 1793
			public const string AbortReroll = "abortReroll";

			// Token: 0x04000702 RID: 1794
			public const string SetPackagesForInteraction = "setPackagesForInteraction";

			// Token: 0x04000703 RID: 1795
			public const string GetInteractionForPackage = "getInteractionForPackage";

			// Token: 0x04000704 RID: 1796
			public const string ToggleScreen = "toggleScreen";

			// Token: 0x04000705 RID: 1797
			public const string SendGlWindowSize = "sendGlWindowSize";

			// Token: 0x04000706 RID: 1798
			public const string DeactivateFrontend = "deactivateFrontend";

			// Token: 0x04000707 RID: 1799
			public const string StartRecordingCombo = "startRecordingCombo";

			// Token: 0x04000708 RID: 1800
			public const string StopRecordingCombo = "stopRecordingCombo";

			// Token: 0x04000709 RID: 1801
			public const string HandleClientOperation = "handleClientOperation";

			// Token: 0x0400070A RID: 1802
			public const string InitMacroPlayback = "initMacroPlayback";

			// Token: 0x0400070B RID: 1803
			public const string StopMacroPlayback = "stopMacroPlayback";

			// Token: 0x0400070C RID: 1804
			public const string FarmModeHandler = "farmModeHandler";

			// Token: 0x0400070D RID: 1805
			public const string StartOperationsSync = "startOperationsSync";

			// Token: 0x0400070E RID: 1806
			public const string StopOperationsSync = "stopOperationsSync";

			// Token: 0x0400070F RID: 1807
			public const string StartSyncConsumer = "startSyncConsumer";

			// Token: 0x04000710 RID: 1808
			public const string StopSyncConsumer = "stopSyncConsumer";

			// Token: 0x04000711 RID: 1809
			public const string ShowFPS = "showFPS";

			// Token: 0x04000712 RID: 1810
			public const string EnableVSync = "enableVSync";

			// Token: 0x04000713 RID: 1811
			public const string CloseCrashedAppTab = "closeCrashedAppTab";

			// Token: 0x04000714 RID: 1812
			public const string OTSCompleted = "oneTimeSetupCompleted";

			// Token: 0x04000715 RID: 1813
			public const string VisibleChangedUri = "frontendVisibleChanged";

			// Token: 0x04000716 RID: 1814
			public const string AppDataFEUrl = "appDataFeUrl";

			// Token: 0x04000717 RID: 1815
			public const string RunAppInfo = "runAppInfo";

			// Token: 0x04000718 RID: 1816
			public const string StopAppInfo = "stopAppInfo";

			// Token: 0x04000719 RID: 1817
			public const string QuitFrontend = "quitFrontend";

			// Token: 0x0400071A RID: 1818
			public const string ShowFeNotification = "showFeNotification";

			// Token: 0x0400071B RID: 1819
			public const string ToggleGamepadButton = "toggleGamepadButton";

			// Token: 0x0400071C RID: 1820
			public const string DeviceProvisioned = "deviceProvisioned";

			// Token: 0x0400071D RID: 1821
			public const string DeviceProvisionedReceived = "deviceProvisionedReceived";

			// Token: 0x0400071E RID: 1822
			public const string GoogleSignin = "googleSignin";

			// Token: 0x0400071F RID: 1823
			public const string ShowFENotification = "showFENotification";

			// Token: 0x04000720 RID: 1824
			public const string IsAppPlayerRooted = "isAppPlayerRooted";

			// Token: 0x04000721 RID: 1825
			public const string SetIsFullscreen = "setIsFullscreen";

			// Token: 0x04000722 RID: 1826
			public const string GetInteractionStats = "getInteractionStats";

			// Token: 0x04000723 RID: 1827
			public const string EnableGamepad = "enableGamepad";

			// Token: 0x04000724 RID: 1828
			public const string ExportCfgFile = "exportCfgFile";

			// Token: 0x04000725 RID: 1829
			public const string ImportCfgFile = "importCfgFile";

			// Token: 0x04000726 RID: 1830
			public const string EnableDebugLogs = "enableDebugLogs";

			// Token: 0x04000727 RID: 1831
			public const string RunMacroUnit = "runMacroUnit";

			// Token: 0x04000728 RID: 1832
			public const string PauseRecordingCombo = "pauseRecordingCombo";

			// Token: 0x04000729 RID: 1833
			public const string ReloadShortcutsConfig = "reloadShortcutsConfig";

			// Token: 0x0400072A RID: 1834
			public const string AccountSetupCompleted = "accountSetupCompleted";

			// Token: 0x0400072B RID: 1835
			public const string ScriptEditingModeEntered = "scriptEditingModeEntered";

			// Token: 0x0400072C RID: 1836
			public const string PlayPauseSync = "playPauseSync";

			// Token: 0x0400072D RID: 1837
			public const string ReinitGuestRegistry = "reinitGuestRegistry";

			// Token: 0x0400072E RID: 1838
			public const string UpdateMacroShortcutsDict = "updateMacroShortcutsDict";

			// Token: 0x0400072F RID: 1839
			public const string IsAstcHardwareSupported = "IsAstcHardwareSupported";

			// Token: 0x04000730 RID: 1840
			public const string SetAstcOption = "setAstcOption";

			// Token: 0x04000731 RID: 1841
			public const string ValidateScriptCommands = "validateScriptCommands";

			// Token: 0x04000732 RID: 1842
			public const string ChangeImei = "changeimei";

			// Token: 0x04000733 RID: 1843
			public const string EnableNativeGamepad = "enableNativeGamepad";

			// Token: 0x04000734 RID: 1844
			public const string SendImagePickerCoordinates = "sendImagePickerCoordinates";

			// Token: 0x04000735 RID: 1845
			public const string ToggleImagePickerMode = "toggleImagePickerMode";

			// Token: 0x04000736 RID: 1846
			public const string HandleLoadConfigOnTabSwitch = "handleLoadConfigOnTabSwitch";

			// Token: 0x04000737 RID: 1847
			public const string SendCustomCursorEnabledApps = "sendCustomCursorEnabledApps";

			// Token: 0x04000738 RID: 1848
			public const string ToggleScrollOnEdgeFeature = "toggleScrollOnEdgeFeature";

			// Token: 0x04000739 RID: 1849
			public const string ForceShutdown = "forceShutdown";

			// Token: 0x0400073A RID: 1850
			public const string BootCompleted = "bootcompleted";

			// Token: 0x0400073B RID: 1851
			public const string EnableMemoryTrim = "enableMemoryTrim";

			// Token: 0x0400073C RID: 1852
			public const string CheckIfGuestBooted = "checkIfGuestBooted";

			// Token: 0x0400073D RID: 1853
			public const string ToggleIsMouseLocked = "toggleIsMouseLocked";
		}

		// Token: 0x020000C1 RID: 193
		public static class Guest
		{
			// Token: 0x0400073E RID: 1854
			public const string Ping = "ping";

			// Token: 0x0400073F RID: 1855
			public const string Install = "install";

			// Token: 0x04000740 RID: 1856
			public const string Xinstall = "xinstall";

			// Token: 0x04000741 RID: 1857
			public const string BrowserInstall = "browserInstall";

			// Token: 0x04000742 RID: 1858
			public const string Uninstall = "uninstall";

			// Token: 0x04000743 RID: 1859
			public const string InstalledPackages = "installedPackages";

			// Token: 0x04000744 RID: 1860
			public const string Clipboard = "clipboard";

			// Token: 0x04000745 RID: 1861
			public const string CustomStartActivity = "customStartActivity";

			// Token: 0x04000746 RID: 1862
			public const string AmzInstall = "amzInstall";

			// Token: 0x04000747 RID: 1863
			public const string ConnectHostTemp = "connectHost";

			// Token: 0x04000748 RID: 1864
			public const string DisconnectHostTemp = "disconnectHost";

			// Token: 0x04000749 RID: 1865
			public const string ConnectHostPermanently = "connectHost?d=permanent";

			// Token: 0x0400074A RID: 1866
			public const string DisconnectHostPermanently = "disconnectHost?d=permanent";

			// Token: 0x0400074B RID: 1867
			public const string CheckAdbStatus = "checkADBStatus";

			// Token: 0x0400074C RID: 1868
			public const string CustomStartService = "customStartService";

			// Token: 0x0400074D RID: 1869
			public const string SetNewLocation = "setNewLocation";

			// Token: 0x0400074E RID: 1870
			public const string BindMount = "bindmount";

			// Token: 0x0400074F RID: 1871
			public const string UnbindMount = "unbindmount";

			// Token: 0x04000750 RID: 1872
			public const string CheckIfGuestReady = "checkIfGuestReady";

			// Token: 0x04000751 RID: 1873
			public const string IsOTSCompleted = "isOTSCompleted";

			// Token: 0x04000752 RID: 1874
			public const string GetDefaultLauncher = "getDefaultLauncher";

			// Token: 0x04000753 RID: 1875
			public const string SetDefaultLauncher = "setDefaultLauncher";

			// Token: 0x04000754 RID: 1876
			public const string Home = "home";

			// Token: 0x04000755 RID: 1877
			public const string RemoveAccountsInfo = "removeAccountsInfo";

			// Token: 0x04000756 RID: 1878
			public const string GetGoogleAdID = "getGoogleAdID";

			// Token: 0x04000757 RID: 1879
			public const string CheckSSLConnection = "checkSSLConnection";

			// Token: 0x04000758 RID: 1880
			public const string GetConfigList = "getConfigList";

			// Token: 0x04000759 RID: 1881
			public const string GetVolume = "getVolume";

			// Token: 0x0400075A RID: 1882
			public const string SetVolume = "setVolume";

			// Token: 0x0400075B RID: 1883
			public const string ChangeDeviceProfile = "changeDeviceProfile";

			// Token: 0x0400075C RID: 1884
			public const string FileDrop = "fileDrop";

			// Token: 0x0400075D RID: 1885
			public const string GetCurrentIMEID = "getCurrentIMEID";

			// Token: 0x0400075E RID: 1886
			public const string IsPackageInstalled = "isPackageInstalled";

			// Token: 0x0400075F RID: 1887
			public const string GetPackageDetails = "getPackageDetails";

			// Token: 0x04000760 RID: 1888
			public const string GetLaunchActivityName = "getLaunchActivityName";

			// Token: 0x04000761 RID: 1889
			public const string GetAppName = "getAppName";

			// Token: 0x04000762 RID: 1890
			public const string AppJSonChanged = "appJSonChanged";

			// Token: 0x04000763 RID: 1891
			public const string SetWindowsAgentAddr = "setWindowsAgentAddr";

			// Token: 0x04000764 RID: 1892
			public const string SetWindowsFrontendAddr = "setWindowsFrontendAddr";

			// Token: 0x04000765 RID: 1893
			public const string SetGameManagerAddr = "setGameManagerAddr";

			// Token: 0x04000766 RID: 1894
			public const string SetBlueStacksConfig = "setBlueStacksConfig";

			// Token: 0x04000767 RID: 1895
			public const string ShowTrayNotification = "showTrayNotification";

			// Token: 0x04000768 RID: 1896
			public const string MuteAppPlayer = "muteAppPlayer";

			// Token: 0x04000769 RID: 1897
			public const string UnmuteAppPlayer = "unmuteAppPlayer";

			// Token: 0x0400076A RID: 1898
			public const string HostOrientation = "hostOrientation";

			// Token: 0x0400076B RID: 1899
			public const string GetProp = "getprop";

			// Token: 0x0400076C RID: 1900
			public const string GetAndroidID = "getAndroidID";

			// Token: 0x0400076D RID: 1901
			public const string GuestOrientation = "guestorientation";

			// Token: 0x0400076E RID: 1902
			public const string IsSharedFolderMounted = "isSharedFolderMounted";

			// Token: 0x0400076F RID: 1903
			public const string GameSettingsEnabled = "gameSettingsEnabled";

			// Token: 0x04000770 RID: 1904
			public const string SwitchAbi = "switchAbi";

			// Token: 0x04000771 RID: 1905
			public const string ChangeImei = "changeimei";

			// Token: 0x04000772 RID: 1906
			public const string LaunchChrome = "launchchrome";

			// Token: 0x04000773 RID: 1907
			public const string GrmPackages = "grmPackages";

			// Token: 0x04000774 RID: 1908
			public const string SetApplicationState = "setapplicationstate";

			// Token: 0x04000775 RID: 1909
			public const string SetLocale = "setLocale";

			// Token: 0x04000776 RID: 1910
			public const string AddCalendarEvent = "addcalendarevent";

			// Token: 0x04000777 RID: 1911
			public const string UpdateCalendarEvent = "updatecalendarevent";

			// Token: 0x04000778 RID: 1912
			public const string DeleteCalendarEvent = "deletecalendarevent";

			// Token: 0x04000779 RID: 1913
			public const string CheckAndroidTouchPointsState = "checkTouchPointState";

			// Token: 0x0400077A RID: 1914
			public const string ShowTouchPoints = "showTouchPoints";

			// Token: 0x0400077B RID: 1915
			public const string SetCustomAppSize = "setcustomappsize";

			// Token: 0x0400077C RID: 1916
			public const string CheckNativeGamepadStatus = "checknativegamepadstatus";

			// Token: 0x0400077D RID: 1917
			public const string GetGoogleAccounts = "getGoogleAccounts";

			// Token: 0x0400077E RID: 1918
			public const string SetTouchSounds = "settouchsounds";

			// Token: 0x0400077F RID: 1919
			public const string OpenUrl = "openurl";

			// Token: 0x04000780 RID: 1920
			public const string ScreenCap = "screencap";
		}

		// Token: 0x020000C2 RID: 194
		public static class NCSoftAgent
		{
			// Token: 0x04000781 RID: 1921
			public const string AccountGoogleLogin = "account/google/login";

			// Token: 0x04000782 RID: 1922
			public const string ErrorCrash = "error/crash";

			// Token: 0x04000783 RID: 1923
			public const string ActionButtonStreaming = "action/button/streaming";
		}

		// Token: 0x020000C3 RID: 195
		public static class MultiInstance
		{
			// Token: 0x04000784 RID: 1924
			public const string Ping = "ping";

			// Token: 0x04000785 RID: 1925
			public const string ToggleMIMFarmMode = "toggleMIMFarmMode";
		}
	}
}
