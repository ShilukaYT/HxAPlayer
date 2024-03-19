using System;

namespace BlueStacks.Common
{
	// Token: 0x020001EC RID: 492
	public static class HTTPRoutes
	{
		// Token: 0x020001ED RID: 493
		public static class Agent
		{
			// Token: 0x040008E4 RID: 2276
			public const string Ping = "ping";

			// Token: 0x040008E5 RID: 2277
			public const string GuestBootFailed = "guestBootFailed";

			// Token: 0x040008E6 RID: 2278
			public const string LaunchDefaultWebApp = "launchDefaultWebApp";

			// Token: 0x040008E7 RID: 2279
			public const string Installed = "installed";

			// Token: 0x040008E8 RID: 2280
			public const string Uninstalled = "uninstalled";

			// Token: 0x040008E9 RID: 2281
			public const string GetAppList = "getAppList";

			// Token: 0x040008EA RID: 2282
			public const string Install = "install";

			// Token: 0x040008EB RID: 2283
			public const string Uninstall = "uninstall";

			// Token: 0x040008EC RID: 2284
			public const string RunApp = "runApp";

			// Token: 0x040008ED RID: 2285
			public const string SetLocale = "setLocale";

			// Token: 0x040008EE RID: 2286
			public const string InstallAppByUrl = "installAppByUrl";

			// Token: 0x040008EF RID: 2287
			public const string AppCrashedInfo = "appCrashedInfo";

			// Token: 0x040008F0 RID: 2288
			public const string GetUserData = "getUserData";

			// Token: 0x040008F1 RID: 2289
			public const string ShowNotification = "showNotification";

			// Token: 0x040008F2 RID: 2290
			public const string AppDownloadStatus = "appDownloadStatus";

			// Token: 0x040008F3 RID: 2291
			public const string ShowFeNotification = "showFeNotification";

			// Token: 0x040008F4 RID: 2292
			public const string BindMount = "bindmount";

			// Token: 0x040008F5 RID: 2293
			public const string UnbindMount = "unbindmount";

			// Token: 0x040008F6 RID: 2294
			public const string QuitFrontend = "quitFrontend";

			// Token: 0x040008F7 RID: 2295
			public const string GetAppImage = "getAppImage";

			// Token: 0x040008F8 RID: 2296
			public const string ShowTrayNotification = "showTrayNotification";

			// Token: 0x040008F9 RID: 2297
			public const string Restart = "restart";

			// Token: 0x040008FA RID: 2298
			public const string Notification = "notification";

			// Token: 0x040008FB RID: 2299
			public const string Clipboard = "clipboard";

			// Token: 0x040008FC RID: 2300
			public const string IsAppInstalled = "isAppInstalled";

			// Token: 0x040008FD RID: 2301
			public const string TopActivityInfo = "topActivityInfo";

			// Token: 0x040008FE RID: 2302
			public const string SysTrayVisibility = "sysTrayVisibility";

			// Token: 0x040008FF RID: 2303
			public const string RestartAgent = "restartAgent";

			// Token: 0x04000900 RID: 2304
			public const string ShowTileInterface = "showTileInterface";

			// Token: 0x04000901 RID: 2305
			public const string SetNewLocation = "setNewLocation";

			// Token: 0x04000902 RID: 2306
			public const string AdEvents = "adEvents";

			// Token: 0x04000903 RID: 2307
			public const string ExitAgent = "exitAgent";

			// Token: 0x04000904 RID: 2308
			public const string StopApp = "stopApp";

			// Token: 0x04000905 RID: 2309
			public const string ReleaseApkInstallThread = "releaseApkInstallThread";

			// Token: 0x04000906 RID: 2310
			public const string ClearAppData = "clearAppData";

			// Token: 0x04000907 RID: 2311
			public const string RestartGameManager = "restartGameManager";

			// Token: 0x04000908 RID: 2312
			public const string PostHttpUrl = "postHttpUrl";

			// Token: 0x04000909 RID: 2313
			public const string InstanceExist = "instanceExist";

			// Token: 0x0400090A RID: 2314
			public const string QueryInstances = "queryInstances";

			// Token: 0x0400090B RID: 2315
			public const string CreateInstance = "createInstance";

			// Token: 0x0400090C RID: 2316
			public const string DeleteInstance = "deleteInstance";

			// Token: 0x0400090D RID: 2317
			public const string ResetSharedFolders = "resetSharedFolders";

			// Token: 0x0400090E RID: 2318
			public const string StartInstance = "startInstance";

			// Token: 0x0400090F RID: 2319
			public const string GetRunningInstances = "getRunningInstances";

			// Token: 0x04000910 RID: 2320
			public const string StopInstance = "stopInstance";

			// Token: 0x04000911 RID: 2321
			public const string SetVmConfig = "setVmConfig";

			// Token: 0x04000912 RID: 2322
			public const string IsMultiInstanceSupported = "isMultiInstanceSupported";

			// Token: 0x04000913 RID: 2323
			public const string SetCpu = "setCpu";

			// Token: 0x04000914 RID: 2324
			public const string SetDpi = "setDpi";

			// Token: 0x04000915 RID: 2325
			public const string SetRam = "setRam";

			// Token: 0x04000916 RID: 2326
			public const string SetResolution = "setResolution";

			// Token: 0x04000917 RID: 2327
			public const string GetGuid = "getGuid";

			// Token: 0x04000918 RID: 2328
			public const string Backup = "backup";

			// Token: 0x04000919 RID: 2329
			public const string Restore = "restore";

			// Token: 0x0400091A RID: 2330
			public const string AppJsonUpdatedForVideo = "appJsonUpdatedForVideo";

			// Token: 0x0400091B RID: 2331
			public const string DeviceProfileUpdated = "deviceProfileUpdated";

			// Token: 0x0400091C RID: 2332
			public const string InstanceStopped = "instanceStopped";

			// Token: 0x0400091D RID: 2333
			public const string GetInstanceStatus = "getInstanceStatus";

			// Token: 0x0400091E RID: 2334
			public const string IsEngineReady = "isEngineReady";

			// Token: 0x0400091F RID: 2335
			public const string FrontendStatusUpdate = "FrontendStatusUpdate";

			// Token: 0x04000920 RID: 2336
			public const string GuestStatusUpdate = "GuestStatusUpdate";

			// Token: 0x04000921 RID: 2337
			public const string CopyToAndroid = "copyToAndroid";

			// Token: 0x04000922 RID: 2338
			public const string CopyToWindows = "copyToWindows";

			// Token: 0x04000923 RID: 2339
			public const string SetCurrentVolume = "setCurrentVolume";

			// Token: 0x04000924 RID: 2340
			public const string DownloadInstalledAppsCfg = "downloadInstalledAppsCfg";

			// Token: 0x04000925 RID: 2341
			public const string SetVMDisplayName = "setVMDisplayName";

			// Token: 0x04000926 RID: 2342
			public const string SortWindows = "sortWindows";

			// Token: 0x04000927 RID: 2343
			public const string MaintenanceWarning = "maintenanceWarning";

			// Token: 0x04000928 RID: 2344
			public const string EnableDebugLogs = "enableDebugLogs";

			// Token: 0x04000929 RID: 2345
			public const string LogAppClick = "logAppClick";

			// Token: 0x0400092A RID: 2346
			public const string SetNCPlayerCharacterName = "setNCPlayerCharacterName";

			// Token: 0x0400092B RID: 2347
			public const string LaunchPlay = "launchPlay";

			// Token: 0x0400092C RID: 2348
			public const string RemoveAccount = "removeAccount";

			// Token: 0x0400092D RID: 2349
			public const string SetDeviceProfile = "setDeviceProfile";

			// Token: 0x0400092E RID: 2350
			public const string ScreenLock = "screenLock";

			// Token: 0x0400092F RID: 2351
			public const string MakeDir = "makeDir";

			// Token: 0x04000930 RID: 2352
			public const string GetHeightWidth = "getHeightWidth";

			// Token: 0x04000931 RID: 2353
			public const string SetStreamingStatus = "setStreamingStatus";

			// Token: 0x04000932 RID: 2354
			public const string GetShortcut = "getShortcut";

			// Token: 0x04000933 RID: 2355
			public const string SetShortcut = "setShortcut";

			// Token: 0x04000934 RID: 2356
			public const string SendEngineTimelineStats = "sendEngineTimelineStats";

			// Token: 0x04000935 RID: 2357
			public const string GrmAppLaunch = "grmAppLaunch";

			// Token: 0x04000936 RID: 2358
			public const string ReInitLocalization = "reinitlocalization";

			// Token: 0x04000937 RID: 2359
			public const string TestCloudAnnouncement = "testCloudAnnouncement";

			// Token: 0x04000938 RID: 2360
			public const string OverrideDesktopNotificationSettings = "overrideDesktopNotificationSettings";

			// Token: 0x04000939 RID: 2361
			public const string NotificationStatsOnClosing = "notificationStatsOnClosing";

			// Token: 0x0400093A RID: 2362
			public const string ConfigFileChanged = "configFileChanged";

			// Token: 0x0400093B RID: 2363
			public const string GetCallbackStatus = "getCallbackStatus";

			// Token: 0x0400093C RID: 2364
			public const string ShowClientNotification = "showClientNotification";
		}

		// Token: 0x020001EE RID: 494
		public static class Client
		{
			// Token: 0x0400093D RID: 2365
			public const string Ping = "ping";

			// Token: 0x0400093E RID: 2366
			public const string AppDisplayed = "appDisplayed";

			// Token: 0x0400093F RID: 2367
			public const string CloseCrashedAppTab = "closeCrashedAppTab";

			// Token: 0x04000940 RID: 2368
			public const string AppLaunched = "appLaunched";

			// Token: 0x04000941 RID: 2369
			public const string ShowApp = "showApp";

			// Token: 0x04000942 RID: 2370
			public const string ShowWindow = "showWindow";

			// Token: 0x04000943 RID: 2371
			public const string IsGMVisible = "isVisible";

			// Token: 0x04000944 RID: 2372
			public const string AppUninstalled = "appUninstalled";

			// Token: 0x04000945 RID: 2373
			public const string AppInstalled = "appInstalled";

			// Token: 0x04000946 RID: 2374
			public const string EnableWndProcLogging = "enableWndProcLogging";

			// Token: 0x04000947 RID: 2375
			public const string Quit = "quit";

			// Token: 0x04000948 RID: 2376
			public const string Google = "google";

			// Token: 0x04000949 RID: 2377
			public const string ShowWebPage = "showWebPage";

			// Token: 0x0400094A RID: 2378
			public const string ShowHomeTab = "showHomeTab";

			// Token: 0x0400094B RID: 2379
			public const string CloseTab = "closeTab";

			// Token: 0x0400094C RID: 2380
			public const string GooglePlayAppInstall = "googlePlayAppInstall";

			// Token: 0x0400094D RID: 2381
			public const string AppInstallStarted = "appInstallStarted";

			// Token: 0x0400094E RID: 2382
			public const string AppInstallFailed = "appInstallFailed";

			// Token: 0x0400094F RID: 2383
			public const string OTSCompleted = "oneTimeSetupCompleted";

			// Token: 0x04000950 RID: 2384
			public const string UpdateUserInfo = "updateUserInfo";

			// Token: 0x04000951 RID: 2385
			public const string BootFailedPopup = "bootFailedPopup";

			// Token: 0x04000952 RID: 2386
			public const string OpenPackage = "openPackage";

			// Token: 0x04000953 RID: 2387
			public const string DragDropInstall = "dragDropInstall";

			// Token: 0x04000954 RID: 2388
			public const string StopInstance = "stopInstance";

			// Token: 0x04000955 RID: 2389
			public const string MinimizeInstance = "minimizeInstance";

			// Token: 0x04000956 RID: 2390
			public const string StartInstance = "startInstance";

			// Token: 0x04000957 RID: 2391
			public const string HideBluestacks = "hideBluestacks";

			// Token: 0x04000958 RID: 2392
			public const string TileWindow = "tileWindow";

			// Token: 0x04000959 RID: 2393
			public const string CascadeWindow = "cascadeWindow";

			// Token: 0x0400095A RID: 2394
			public const string ToggleFarmMode = "toggleFarmMode";

			// Token: 0x0400095B RID: 2395
			public const string LaunchWebTab = "launchWebTab";

			// Token: 0x0400095C RID: 2396
			public const string OpenNotificationSettings = "openNotificationSettings";

			// Token: 0x0400095D RID: 2397
			public const string IsAnyAppRunning = "isAnyAppRunning";

			// Token: 0x0400095E RID: 2398
			public const string ChangeTextOTS = "changeTextOTS";

			// Token: 0x0400095F RID: 2399
			public const string ShowIMESwitchPrompt = "showIMESwitchPrompt";

			// Token: 0x04000960 RID: 2400
			public const string LaunchDefaultWebApp = "launchDefaultWebApp";

			// Token: 0x04000961 RID: 2401
			public const string MacroCompleted = "macroCompleted";

			// Token: 0x04000962 RID: 2402
			public const string AppInfoUpdated = "appInfoUpdated";

			// Token: 0x04000963 RID: 2403
			public const string SendAppDisplayed = "sendAppDisplayed";

			// Token: 0x04000964 RID: 2404
			public const string IsGmVisible = "static";

			// Token: 0x04000965 RID: 2405
			public const string RestartFrontend = "restartFrontend";

			// Token: 0x04000966 RID: 2406
			public const string GcCollect = "gcCollect";

			// Token: 0x04000967 RID: 2407
			public const string ShowWindowAndApp = "showWindowAndApp";

			// Token: 0x04000968 RID: 2408
			public const string UnsupportedCpuError = "unsupportedCpuError";

			// Token: 0x04000969 RID: 2409
			public const string ChangeOrientaion = "changeOrientaion";

			// Token: 0x0400096A RID: 2410
			public const string ShootingModeChanged = "shootingModeChanged";

			// Token: 0x0400096B RID: 2411
			public const string GuestBootCompleted = "guestBootCompleted";

			// Token: 0x0400096C RID: 2412
			public const string GetRunningInstances = "getRunningInstances";

			// Token: 0x0400096D RID: 2413
			public const string AppJsonChanged = "appJsonChanged";

			// Token: 0x0400096E RID: 2414
			public const string GetCurrentAppDetails = "getCurrentAppDetails";

			// Token: 0x0400096F RID: 2415
			public const string MaintenanceWarning = "maintenanceWarning";

			// Token: 0x04000970 RID: 2416
			public const string RequirementConfigUpdated = "requirementConfigUpdated";

			// Token: 0x04000971 RID: 2417
			public const string DeviceProfileUpdated = "deviceProfileUpdated";

			// Token: 0x04000972 RID: 2418
			public const string UpdateSizeOfOverlay = "updateSizeOfOverlay";

			// Token: 0x04000973 RID: 2419
			public const string AndroidLocaleChanged = "androidLocaleChanged";

			// Token: 0x04000974 RID: 2420
			public const string SaveComboEvents = "saveComboEvents";

			// Token: 0x04000975 RID: 2421
			public const string HandleClientOperation = "handleClientOperation";

			// Token: 0x04000976 RID: 2422
			public const string MacroPlaybackComplete = "macroPlaybackComplete";

			// Token: 0x04000977 RID: 2423
			public const string ObsStatus = "obsStatus";

			// Token: 0x04000978 RID: 2424
			public const string ReportObsError = "reportObsError";

			// Token: 0x04000979 RID: 2425
			public const string CapturingError = "capturingError";

			// Token: 0x0400097A RID: 2426
			public const string OpenGLCapturingError = "openGLCapturingError";

			// Token: 0x0400097B RID: 2427
			public const string ToggleStreamingMode = "toggleStreamingMode";

			// Token: 0x0400097C RID: 2428
			public const string HandleClientGamepadButton = "handleClientGamepadButton";

			// Token: 0x0400097D RID: 2429
			public const string HandleGamepadConnection = "handleGamepadConnection";

			// Token: 0x0400097E RID: 2430
			public const string HandleGamepadGuidanceButton = "handleGamepadGuidanceButton";

			// Token: 0x0400097F RID: 2431
			public const string DeviceProvisioned = "deviceProvisioned";

			// Token: 0x04000980 RID: 2432
			public const string GoogleSignin = "googleSignin";

			// Token: 0x04000981 RID: 2433
			public const string ShowFullscreenSidebar = "showFullscreenSidebar";

			// Token: 0x04000982 RID: 2434
			public const string HideTopSideBar = "hideTopSidebar";

			// Token: 0x04000983 RID: 2435
			public const string ShowFullscreenSidebarButton = "showFullscreenSidebarButton";

			// Token: 0x04000984 RID: 2436
			public const string ShowFullscreenTopbarButton = "showFullscreenTopbarButton";

			// Token: 0x04000985 RID: 2437
			public const string UpdateLocale = "updateLocale";

			// Token: 0x04000986 RID: 2438
			public const string ScreenshotCaptured = "screenshotCaptured";

			// Token: 0x04000987 RID: 2439
			public const string SetCurrentVolumeFromAndroid = "setCurrentVolumeFromAndroid";

			// Token: 0x04000988 RID: 2440
			public const string HotKeyEvents = "hotKeyEvents";

			// Token: 0x04000989 RID: 2441
			public const string SetLocale = "setLocale";

			// Token: 0x0400098A RID: 2442
			public const string EnableDebugLogs = "enableDebugLogs";

			// Token: 0x0400098B RID: 2443
			public const string SetDMMKeymapping = "setDMMKeymapping";

			// Token: 0x0400098C RID: 2444
			public const string NCSetGameInfoOnTopBar = "ncSetGameInfoOnTopBar";

			// Token: 0x0400098D RID: 2445
			public const string LaunchPlay = "launchPlay";

			// Token: 0x0400098E RID: 2446
			public const string EnableKeyboardHookLogging = "enableKeyboardHookLogging";

			// Token: 0x0400098F RID: 2447
			public const string MuteAllInstances = "muteAllInstances";

			// Token: 0x04000990 RID: 2448
			public const string ScreenLock = "screenLock";

			// Token: 0x04000991 RID: 2449
			public const string GetHeightWidth = "getHeightWidth";

			// Token: 0x04000992 RID: 2450
			public const string AccountSetupCompleted = "accountSetupCompleted";

			// Token: 0x04000993 RID: 2451
			public const string OpenThemeEditor = "openThemeEditor";

			// Token: 0x04000994 RID: 2452
			public const string SetStreamingStatus = "setStreamingStatus";

			// Token: 0x04000995 RID: 2453
			public const string PlayerScriptModifierClick = "playerScriptModifierClick";

			// Token: 0x04000996 RID: 2454
			public const string ReloadShortcuts = "reloadShortcuts";

			// Token: 0x04000997 RID: 2455
			public const string ShowFullscreenTopBar = "showFullscreenTopbar";

			// Token: 0x04000998 RID: 2456
			public const string ReloadPromotions = "reloadPromotions";

			// Token: 0x04000999 RID: 2457
			public const string HandleOverlayControlsVisibility = "overlayControlsVisibility";

			// Token: 0x0400099A RID: 2458
			public const string ShowGrmAndLaunchApp = "showGrmAndLaunchApp";

			// Token: 0x0400099B RID: 2459
			public const string ReinitRegistry = "reinitRegistry";

			// Token: 0x0400099C RID: 2460
			public const string OpenCFGReorderTool = "openCFGReorderTool";

			// Token: 0x0400099D RID: 2461
			public const string UpdateCrc = "updateCrc";

			// Token: 0x0400099E RID: 2462
			public const string ConfigFileChanged = "configFileChanged";

			// Token: 0x0400099F RID: 2463
			public const string AddNotificationInDrawer = "addNotificationInDrawer";

			// Token: 0x040009A0 RID: 2464
			public const string MarkNotificationInDrawer = "markNotificationInDrawer";

			// Token: 0x040009A1 RID: 2465
			public const string CheckCallbackEnabledStatus = "checkCallbackEnabledStatus";

			// Token: 0x040009A2 RID: 2466
			public const string HideOverlayWhenIMEActive = "hideOverlayWhenIMEActive";

			// Token: 0x040009A3 RID: 2467
			public const string ShowImageUploadedInfo = "showImageUploadedInfo";
		}

		// Token: 0x020001EF RID: 495
		public static class Cloud
		{
			// Token: 0x040009A4 RID: 2468
			public const string GetAnnouncement = "/getAnnouncement";

			// Token: 0x040009A5 RID: 2469
			public const string AppUsage = "/bs3/stats/v4/usage";

			// Token: 0x040009A6 RID: 2470
			public const string FrontendClickStats = "/bs3/stats/frontend_click_stats";

			// Token: 0x040009A7 RID: 2471
			public const string ScheduledPing = "/api/scheduledping";

			// Token: 0x040009A8 RID: 2472
			public const string ScheduledPingStats = "/stats/scheduledpingstats";

			// Token: 0x040009A9 RID: 2473
			public const string SecurityMetrics = "/bs4/security_metrics";

			// Token: 0x040009AA RID: 2474
			public const string UnifiedInstallStats = "/bs3/stats/unified_install_stats";

			// Token: 0x040009AB RID: 2475
			public const string UpdateLocale = "updateLocale";

			// Token: 0x040009AC RID: 2476
			public const string ProblemCategories = "/app_settings/problem_categories";

			// Token: 0x040009AD RID: 2477
			public const string Promotions = "promotions";

			// Token: 0x040009AE RID: 2478
			public const string ClientBootPromotionStats = "bs4/stats/client_boot_promotion_stats";

			// Token: 0x040009AF RID: 2479
			public const string GrmFetchUrl = "grm/files";

			// Token: 0x040009B0 RID: 2480
			public const string BtvFetchUrl = "bs4/btv/GetBTVFile";

			// Token: 0x040009B1 RID: 2481
			public const string HelpArticles = "help_articles";

			// Token: 0x040009B2 RID: 2482
			public const string GuidanceWindow = "guidance_window";

			// Token: 0x040009B3 RID: 2483
			public const string CalendarStats = "/bs4/stats/calendar_stats";

			// Token: 0x040009B4 RID: 2484
			public const string PostBootUrl = "/bs4/post_boot";

			// Token: 0x040009B5 RID: 2485
			public const string InstanceWisePostBootDataUrl = "/bs4/multi_instance";
		}

		// Token: 0x020001F0 RID: 496
		public static class HelpArticlesKeys
		{
			// Token: 0x040009B6 RID: 2486
			public const string KMScriptFAQ = "keymapping_script_faq";

			// Token: 0x040009B7 RID: 2487
			public const string BS4MinRequirements = "bs3_nougat_min_requirements";

			// Token: 0x040009B8 RID: 2488
			public const string BGPCompatKKVersion = "bgp_kk_compat_version";

			// Token: 0x040009B9 RID: 2489
			public const string EnableVirtualization = "enable_virtualization";

			// Token: 0x040009BA RID: 2490
			public const string VtxUnavailable = "vtx_unavailable";

			// Token: 0x040009BB RID: 2491
			public const string UpgradeSupportInfo = "upgrade_support_info";

			// Token: 0x040009BC RID: 2492
			public const string BS3MinRequirements = "bs3_min_requirements";

			// Token: 0x040009BD RID: 2493
			public const string DisableAntivirus = "disable_antivirus";

			// Token: 0x040009BE RID: 2494
			public const string ChangePowerPlan = "change_powerplan";

			// Token: 0x040009BF RID: 2495
			public const string FailedSslConnection = "failed_ssl_connection";

			// Token: 0x040009C0 RID: 2496
			public const string AudioServiceIssue = "audio_service_issue";

			// Token: 0x040009C1 RID: 2497
			public const string TermsOfUse = "terms_of_use";

			// Token: 0x040009C2 RID: 2498
			public const string DisableHypervisors = "disable_hypervisor";

			// Token: 0x040009C3 RID: 2499
			public const string HyperVComputePlatformEnableGuide = "hypervisor_compute_platform_guide";

			// Token: 0x040009C4 RID: 2500
			public const string ChangeGraphicsMode = "change_graphics_mode";

			// Token: 0x040009C5 RID: 2501
			public const string AdvancedGameControl = "advanced_game_control";

			// Token: 0x040009C6 RID: 2502
			public const string SmartControl = "smart_control";

			// Token: 0x040009C7 RID: 2503
			public const string GameSettingsKnowMorePubg = "game_settings_know_more_pubg";

			// Token: 0x040009C8 RID: 2504
			public const string GameSettingsKnowMoreFreefire = "game_settings_know_more_freefire";

			// Token: 0x040009C9 RID: 2505
			public const string GameSettingsKnowMoreCOD = "game_settings_know_more_callofduty";

			// Token: 0x040009CA RID: 2506
			public const string GameSettingsKnowMoreSevenDeadly = "game_settings_know_more_sevendeadly";

			// Token: 0x040009CB RID: 2507
			public const string ProfileSettingsWarningInPubg = "profile_settings_warning_pubg";

			// Token: 0x040009CC RID: 2508
			public const string FreeDiskSpaceUsingDiskCompactiontool = "free_disk_space_using_diskcompactiontool";

			// Token: 0x040009CD RID: 2509
			public const string GameGuideReadArticle = "game_guide_article";

			// Token: 0x040009CE RID: 2510
			public const string AbiHelpUrl = "ABI_Help";

			// Token: 0x040009CF RID: 2511
			public const string AstcHelpUrl = "ASTC_Help";

			// Token: 0x040009D0 RID: 2512
			public const string VsyncHelpUrl = "VSync_Help";

			// Token: 0x040009D1 RID: 2513
			public const string GpuSettingHelpUrl = "GPU_Setting_Help";

			// Token: 0x040009D2 RID: 2514
			public const string MergeMacroHelpUrl = "MergeMacro_Help";

			// Token: 0x040009D3 RID: 2515
			public const string NativeGamepadHelpUrl = "native_gamepad_help";

			// Token: 0x040009D4 RID: 2516
			public const string UnsureWhereStart = "unsure_start";

			// Token: 0x040009D5 RID: 2517
			public const string TroubleInstallingRunningGame = "trouble_installing_running_game";

			// Token: 0x040009D6 RID: 2518
			public const string StopMovementMOBAHelpUrl = "moba_stop_movement_help";

			// Token: 0x040009D7 RID: 2519
			public const string MOBASkillSettingsHelpUrl = "moba_skill_settings_help";

			// Token: 0x040009D8 RID: 2520
			public const string HowToUpdateHelpUrl = "how_to_update_help";

			// Token: 0x040009D9 RID: 2521
			public const string MIMHelpUrl = "MIM_help";

			// Token: 0x040009DA RID: 2522
			public const string EcoModeHelpUrl = "EcoMode_help";

			// Token: 0x040009DB RID: 2523
			public const string NotificationModeHelpUrl = "notification_mode_help";

			// Token: 0x040009DC RID: 2524
			public const string LogCollectorAllInstancesHelpUrl = "log_collector_all_instances_help";

			// Token: 0x040009DD RID: 2525
			public const string AndroidVersionHelpUrl = "android_version_help";

			// Token: 0x040009DE RID: 2526
			public const string GameControlsHelpUrl = "game_controls_help";

			// Token: 0x040009DF RID: 2527
			public const string GamepadConnectedNotifHelpUrl = "gamepad_connected_notif_help";

			// Token: 0x040009E0 RID: 2528
			public const string MacroTouchPointsHelpUrl = "macro_touch_points_help";

			// Token: 0x040009E1 RID: 2529
			public const string TouchSoundHelpUrl = "touch_sound_help";

			// Token: 0x040009E2 RID: 2530
			public const string BellNotificationsHelpUrl = "bell_notifications_help";

			// Token: 0x040009E3 RID: 2531
			public const string DesktopNotificationsHelpUrl = "desktop_notifications_help";

			// Token: 0x040009E4 RID: 2532
			public const string EnableHypervisor = "enable_hyperv";
		}

		// Token: 0x020001F1 RID: 497
		public static class BTv
		{
			// Token: 0x040009E5 RID: 2533
			public const string Ping = "ping";

			// Token: 0x040009E6 RID: 2534
			public const string ReceiveAppInstallStatus = "receiveAppInstallStatus";
		}

		// Token: 0x020001F2 RID: 498
		public static class Engine
		{
			// Token: 0x040009E7 RID: 2535
			public const string Ping = "ping";

			// Token: 0x040009E8 RID: 2536
			public const string RefreshKeymapUri = "refreshKeymap";

			// Token: 0x040009E9 RID: 2537
			public const string Shutdown = "shutdown";

			// Token: 0x040009EA RID: 2538
			public const string SwitchOrientation = "switchOrientation";

			// Token: 0x040009EB RID: 2539
			public const string ShowWindow = "showWindow";

			// Token: 0x040009EC RID: 2540
			public const string RefreshWindow = "refreshWindow";

			// Token: 0x040009ED RID: 2541
			public const string SetParent = "setParent";

			// Token: 0x040009EE RID: 2542
			public const string ShareScreenshot = "shareScreenshot";

			// Token: 0x040009EF RID: 2543
			public const string GoBack = "goBack";

			// Token: 0x040009F0 RID: 2544
			public const string CloseScreen = "closeScreen";

			// Token: 0x040009F1 RID: 2545
			public const string SoftControlBarEvent = "softControlBarEvent";

			// Token: 0x040009F2 RID: 2546
			public const string InputMapperFilesDownloaded = "inputMapperFilesDownloaded";

			// Token: 0x040009F3 RID: 2547
			public const string EnableWndProcLogging = "enableWndProcLogging";

			// Token: 0x040009F4 RID: 2548
			public const string PingVm = "pingVm";

			// Token: 0x040009F5 RID: 2549
			public const string CopyFiles = "copyFiles";

			// Token: 0x040009F6 RID: 2550
			public const string GetWindowsFiles = "getWindowsFiles";

			// Token: 0x040009F7 RID: 2551
			public const string GpsCoordinates = "gpsCoordinates";

			// Token: 0x040009F8 RID: 2552
			public const string InitGamepad = "initGamepad";

			// Token: 0x040009F9 RID: 2553
			public const string GetVolume = "getVolume";

			// Token: 0x040009FA RID: 2554
			public const string SetVolume = "setVolume";

			// Token: 0x040009FB RID: 2555
			public const string TopDisplayedActivityInfo = "topDisplayedActivityInfo";

			// Token: 0x040009FC RID: 2556
			public const string AppDisplayed = "appDisplayed";

			// Token: 0x040009FD RID: 2557
			public const string GoHome = "goHome";

			// Token: 0x040009FE RID: 2558
			public const string IsKeyboardEnabled = "isKeyboardEnabled";

			// Token: 0x040009FF RID: 2559
			public const string SetKeymappingState = "setKeymappingState";

			// Token: 0x04000A00 RID: 2560
			public const string Keymap = "keymap";

			// Token: 0x04000A01 RID: 2561
			public const string SetFrontendVisibility = "setFrontendVisibility";

			// Token: 0x04000A02 RID: 2562
			public const string GetFeSize = "getFeSize";

			// Token: 0x04000A03 RID: 2563
			public const string Mute = "mute";

			// Token: 0x04000A04 RID: 2564
			public const string Unmute = "unmute";

			// Token: 0x04000A05 RID: 2565
			public const string GetCurrentKeymappingStatus = "getCurrentKeymappingStatus";

			// Token: 0x04000A06 RID: 2566
			public const string Shake = "shake";

			// Token: 0x04000A07 RID: 2567
			public const string IsKeyNameFocussed = "isKeyNameFocussed";

			// Token: 0x04000A08 RID: 2568
			public const string AndroidImeSelected = "androidImeSelected";

			// Token: 0x04000A09 RID: 2569
			public const string IsGpsSupported = "isGpsSupported";

			// Token: 0x04000A0A RID: 2570
			public const string InstallApk = "installApk";

			// Token: 0x04000A0B RID: 2571
			public const string InjectCopy = "injectCopy";

			// Token: 0x04000A0C RID: 2572
			public const string InjectPaste = "injectPaste";

			// Token: 0x04000A0D RID: 2573
			public const string StopZygote = "stopZygote";

			// Token: 0x04000A0E RID: 2574
			public const string StartZygote = "startZygote";

			// Token: 0x04000A0F RID: 2575
			public const string GetKeyMappingParserVersion = "getKeyMappingParserVersion";

			// Token: 0x04000A10 RID: 2576
			public const string VibrateHostWindow = "vibrateHostWindow";

			// Token: 0x04000A11 RID: 2577
			public const string LocaleChanged = "localeChanged";

			// Token: 0x04000A12 RID: 2578
			public const string GetScreenshot = "getScreenshot";

			// Token: 0x04000A13 RID: 2579
			public const string SetPcImeWorkflow = "setPcImeWorkflow";

			// Token: 0x04000A14 RID: 2580
			public const string SetUserInfo = "setUserInfo";

			// Token: 0x04000A15 RID: 2581
			public const string GetUserInfo = "getUserInfo";

			// Token: 0x04000A16 RID: 2582
			public const string GetPremium = "getPremium";

			// Token: 0x04000A17 RID: 2583
			public const string SetCursorStyle = "setCursorStyle";

			// Token: 0x04000A18 RID: 2584
			public const string OpenMacroWindow = "openMacroWindow";

			// Token: 0x04000A19 RID: 2585
			public const string StartReroll = "startReroll";

			// Token: 0x04000A1A RID: 2586
			public const string AbortReroll = "abortReroll";

			// Token: 0x04000A1B RID: 2587
			public const string SetPackagesForInteraction = "setPackagesForInteraction";

			// Token: 0x04000A1C RID: 2588
			public const string GetInteractionForPackage = "getInteractionForPackage";

			// Token: 0x04000A1D RID: 2589
			public const string ToggleScreen = "toggleScreen";

			// Token: 0x04000A1E RID: 2590
			public const string SendGlWindowSize = "sendGlWindowSize";

			// Token: 0x04000A1F RID: 2591
			public const string DeactivateFrontend = "deactivateFrontend";

			// Token: 0x04000A20 RID: 2592
			public const string StartRecordingCombo = "startRecordingCombo";

			// Token: 0x04000A21 RID: 2593
			public const string StopRecordingCombo = "stopRecordingCombo";

			// Token: 0x04000A22 RID: 2594
			public const string HandleClientOperation = "handleClientOperation";

			// Token: 0x04000A23 RID: 2595
			public const string InitMacroPlayback = "initMacroPlayback";

			// Token: 0x04000A24 RID: 2596
			public const string StopMacroPlayback = "stopMacroPlayback";

			// Token: 0x04000A25 RID: 2597
			public const string FarmModeHandler = "farmModeHandler";

			// Token: 0x04000A26 RID: 2598
			public const string StartOperationsSync = "startOperationsSync";

			// Token: 0x04000A27 RID: 2599
			public const string StopOperationsSync = "stopOperationsSync";

			// Token: 0x04000A28 RID: 2600
			public const string StartSyncConsumer = "startSyncConsumer";

			// Token: 0x04000A29 RID: 2601
			public const string StopSyncConsumer = "stopSyncConsumer";

			// Token: 0x04000A2A RID: 2602
			public const string ShowFPS = "showFPS";

			// Token: 0x04000A2B RID: 2603
			public const string EnableVSync = "enableVSync";

			// Token: 0x04000A2C RID: 2604
			public const string CloseCrashedAppTab = "closeCrashedAppTab";

			// Token: 0x04000A2D RID: 2605
			public const string OTSCompleted = "oneTimeSetupCompleted";

			// Token: 0x04000A2E RID: 2606
			public const string VisibleChangedUri = "frontendVisibleChanged";

			// Token: 0x04000A2F RID: 2607
			public const string AppDataFEUrl = "appDataFeUrl";

			// Token: 0x04000A30 RID: 2608
			public const string RunAppInfo = "runAppInfo";

			// Token: 0x04000A31 RID: 2609
			public const string StopAppInfo = "stopAppInfo";

			// Token: 0x04000A32 RID: 2610
			public const string QuitFrontend = "quitFrontend";

			// Token: 0x04000A33 RID: 2611
			public const string ShowFeNotification = "showFeNotification";

			// Token: 0x04000A34 RID: 2612
			public const string ToggleGamepadButton = "toggleGamepadButton";

			// Token: 0x04000A35 RID: 2613
			public const string DeviceProvisioned = "deviceProvisioned";

			// Token: 0x04000A36 RID: 2614
			public const string DeviceProvisionedReceived = "deviceProvisionedReceived";

			// Token: 0x04000A37 RID: 2615
			public const string GoogleSignin = "googleSignin";

			// Token: 0x04000A38 RID: 2616
			public const string ShowFENotification = "showFENotification";

			// Token: 0x04000A39 RID: 2617
			public const string IsAppPlayerRooted = "isAppPlayerRooted";

			// Token: 0x04000A3A RID: 2618
			public const string SetIsFullscreen = "setIsFullscreen";

			// Token: 0x04000A3B RID: 2619
			public const string GetInteractionStats = "getInteractionStats";

			// Token: 0x04000A3C RID: 2620
			public const string EnableGamepad = "enableGamepad";

			// Token: 0x04000A3D RID: 2621
			public const string ExportCfgFile = "exportCfgFile";

			// Token: 0x04000A3E RID: 2622
			public const string ImportCfgFile = "importCfgFile";

			// Token: 0x04000A3F RID: 2623
			public const string EnableDebugLogs = "enableDebugLogs";

			// Token: 0x04000A40 RID: 2624
			public const string RunMacroUnit = "runMacroUnit";

			// Token: 0x04000A41 RID: 2625
			public const string PauseRecordingCombo = "pauseRecordingCombo";

			// Token: 0x04000A42 RID: 2626
			public const string ReloadShortcutsConfig = "reloadShortcutsConfig";

			// Token: 0x04000A43 RID: 2627
			public const string AccountSetupCompleted = "accountSetupCompleted";

			// Token: 0x04000A44 RID: 2628
			public const string ScriptEditingModeEntered = "scriptEditingModeEntered";

			// Token: 0x04000A45 RID: 2629
			public const string PlayPauseSync = "playPauseSync";

			// Token: 0x04000A46 RID: 2630
			public const string ReinitGuestRegistry = "reinitGuestRegistry";

			// Token: 0x04000A47 RID: 2631
			public const string UpdateMacroShortcutsDict = "updateMacroShortcutsDict";

			// Token: 0x04000A48 RID: 2632
			public const string IsAstcHardwareSupported = "IsAstcHardwareSupported";

			// Token: 0x04000A49 RID: 2633
			public const string SetAstcOption = "setAstcOption";

			// Token: 0x04000A4A RID: 2634
			public const string ValidateScriptCommands = "validateScriptCommands";

			// Token: 0x04000A4B RID: 2635
			public const string ChangeImei = "changeimei";

			// Token: 0x04000A4C RID: 2636
			public const string EnableNativeGamepad = "enableNativeGamepad";

			// Token: 0x04000A4D RID: 2637
			public const string SendImagePickerCoordinates = "sendImagePickerCoordinates";

			// Token: 0x04000A4E RID: 2638
			public const string ToggleImagePickerMode = "toggleImagePickerMode";

			// Token: 0x04000A4F RID: 2639
			public const string HandleLoadConfigOnTabSwitch = "handleLoadConfigOnTabSwitch";

			// Token: 0x04000A50 RID: 2640
			public const string SendCustomCursorEnabledApps = "sendCustomCursorEnabledApps";

			// Token: 0x04000A51 RID: 2641
			public const string ToggleScrollOnEdgeFeature = "toggleScrollOnEdgeFeature";

			// Token: 0x04000A52 RID: 2642
			public const string ForceShutdown = "forceShutdown";

			// Token: 0x04000A53 RID: 2643
			public const string BootCompleted = "bootcompleted";

			// Token: 0x04000A54 RID: 2644
			public const string EnableMemoryTrim = "enableMemoryTrim";

			// Token: 0x04000A55 RID: 2645
			public const string CheckIfGuestBooted = "checkIfGuestBooted";

			// Token: 0x04000A56 RID: 2646
			public const string ToggleIsMouseLocked = "toggleIsMouseLocked";
		}

		// Token: 0x020001F3 RID: 499
		public static class Guest
		{
			// Token: 0x04000A57 RID: 2647
			public const string Ping = "ping";

			// Token: 0x04000A58 RID: 2648
			public const string Install = "install";

			// Token: 0x04000A59 RID: 2649
			public const string Xinstall = "xinstall";

			// Token: 0x04000A5A RID: 2650
			public const string BrowserInstall = "browserInstall";

			// Token: 0x04000A5B RID: 2651
			public const string Uninstall = "uninstall";

			// Token: 0x04000A5C RID: 2652
			public const string InstalledPackages = "installedPackages";

			// Token: 0x04000A5D RID: 2653
			public const string Clipboard = "clipboard";

			// Token: 0x04000A5E RID: 2654
			public const string CustomStartActivity = "customStartActivity";

			// Token: 0x04000A5F RID: 2655
			public const string AmzInstall = "amzInstall";

			// Token: 0x04000A60 RID: 2656
			public const string ConnectHostTemp = "connectHost";

			// Token: 0x04000A61 RID: 2657
			public const string DisconnectHostTemp = "disconnectHost";

			// Token: 0x04000A62 RID: 2658
			public const string ConnectHostPermanently = "connectHost?d=permanent";

			// Token: 0x04000A63 RID: 2659
			public const string DisconnectHostPermanently = "disconnectHost?d=permanent";

			// Token: 0x04000A64 RID: 2660
			public const string CheckAdbStatus = "checkADBStatus";

			// Token: 0x04000A65 RID: 2661
			public const string CustomStartService = "customStartService";

			// Token: 0x04000A66 RID: 2662
			public const string SetNewLocation = "setNewLocation";

			// Token: 0x04000A67 RID: 2663
			public const string BindMount = "bindmount";

			// Token: 0x04000A68 RID: 2664
			public const string UnbindMount = "unbindmount";

			// Token: 0x04000A69 RID: 2665
			public const string CheckIfGuestReady = "checkIfGuestReady";

			// Token: 0x04000A6A RID: 2666
			public const string IsOTSCompleted = "isOTSCompleted";

			// Token: 0x04000A6B RID: 2667
			public const string GetDefaultLauncher = "getDefaultLauncher";

			// Token: 0x04000A6C RID: 2668
			public const string SetDefaultLauncher = "setDefaultLauncher";

			// Token: 0x04000A6D RID: 2669
			public const string Home = "home";

			// Token: 0x04000A6E RID: 2670
			public const string RemoveAccountsInfo = "removeAccountsInfo";

			// Token: 0x04000A6F RID: 2671
			public const string GetGoogleAdID = "getGoogleAdID";

			// Token: 0x04000A70 RID: 2672
			public const string CheckSSLConnection = "checkSSLConnection";

			// Token: 0x04000A71 RID: 2673
			public const string GetConfigList = "getConfigList";

			// Token: 0x04000A72 RID: 2674
			public const string GetVolume = "getVolume";

			// Token: 0x04000A73 RID: 2675
			public const string SetVolume = "setVolume";

			// Token: 0x04000A74 RID: 2676
			public const string ChangeDeviceProfile = "changeDeviceProfile";

			// Token: 0x04000A75 RID: 2677
			public const string FileDrop = "fileDrop";

			// Token: 0x04000A76 RID: 2678
			public const string GetCurrentIMEID = "getCurrentIMEID";

			// Token: 0x04000A77 RID: 2679
			public const string IsPackageInstalled = "isPackageInstalled";

			// Token: 0x04000A78 RID: 2680
			public const string GetPackageDetails = "getPackageDetails";

			// Token: 0x04000A79 RID: 2681
			public const string GetLaunchActivityName = "getLaunchActivityName";

			// Token: 0x04000A7A RID: 2682
			public const string GetAppName = "getAppName";

			// Token: 0x04000A7B RID: 2683
			public const string AppJSonChanged = "appJSonChanged";

			// Token: 0x04000A7C RID: 2684
			public const string SetWindowsAgentAddr = "setWindowsAgentAddr";

			// Token: 0x04000A7D RID: 2685
			public const string SetWindowsFrontendAddr = "setWindowsFrontendAddr";

			// Token: 0x04000A7E RID: 2686
			public const string SetGameManagerAddr = "setGameManagerAddr";

			// Token: 0x04000A7F RID: 2687
			public const string SetBlueStacksConfig = "setBlueStacksConfig";

			// Token: 0x04000A80 RID: 2688
			public const string ShowTrayNotification = "showTrayNotification";

			// Token: 0x04000A81 RID: 2689
			public const string MuteAppPlayer = "muteAppPlayer";

			// Token: 0x04000A82 RID: 2690
			public const string UnmuteAppPlayer = "unmuteAppPlayer";

			// Token: 0x04000A83 RID: 2691
			public const string HostOrientation = "hostOrientation";

			// Token: 0x04000A84 RID: 2692
			public const string GetProp = "getprop";

			// Token: 0x04000A85 RID: 2693
			public const string GetAndroidID = "getAndroidID";

			// Token: 0x04000A86 RID: 2694
			public const string GuestOrientation = "guestorientation";

			// Token: 0x04000A87 RID: 2695
			public const string IsSharedFolderMounted = "isSharedFolderMounted";

			// Token: 0x04000A88 RID: 2696
			public const string GameSettingsEnabled = "gameSettingsEnabled";

			// Token: 0x04000A89 RID: 2697
			public const string SwitchAbi = "switchAbi";

			// Token: 0x04000A8A RID: 2698
			public const string ChangeImei = "changeimei";

			// Token: 0x04000A8B RID: 2699
			public const string LaunchChrome = "launchchrome";

			// Token: 0x04000A8C RID: 2700
			public const string GrmPackages = "grmPackages";

			// Token: 0x04000A8D RID: 2701
			public const string SetApplicationState = "setapplicationstate";

			// Token: 0x04000A8E RID: 2702
			public const string SetLocale = "setLocale";

			// Token: 0x04000A8F RID: 2703
			public const string AddCalendarEvent = "addcalendarevent";

			// Token: 0x04000A90 RID: 2704
			public const string UpdateCalendarEvent = "updatecalendarevent";

			// Token: 0x04000A91 RID: 2705
			public const string DeleteCalendarEvent = "deletecalendarevent";

			// Token: 0x04000A92 RID: 2706
			public const string CheckAndroidTouchPointsState = "checkTouchPointState";

			// Token: 0x04000A93 RID: 2707
			public const string ShowTouchPoints = "showTouchPoints";

			// Token: 0x04000A94 RID: 2708
			public const string SetCustomAppSize = "setcustomappsize";

			// Token: 0x04000A95 RID: 2709
			public const string CheckNativeGamepadStatus = "checknativegamepadstatus";

			// Token: 0x04000A96 RID: 2710
			public const string GetGoogleAccounts = "getGoogleAccounts";

			// Token: 0x04000A97 RID: 2711
			public const string SetTouchSounds = "settouchsounds";

			// Token: 0x04000A98 RID: 2712
			public const string OpenUrl = "openurl";

			// Token: 0x04000A99 RID: 2713
			public const string ScreenCap = "screencap";
		}

		// Token: 0x020001F4 RID: 500
		public static class NCSoftAgent
		{
			// Token: 0x04000A9A RID: 2714
			public const string AccountGoogleLogin = "account/google/login";

			// Token: 0x04000A9B RID: 2715
			public const string ErrorCrash = "error/crash";

			// Token: 0x04000A9C RID: 2716
			public const string ActionButtonStreaming = "action/button/streaming";
		}

		// Token: 0x020001F5 RID: 501
		public static class MultiInstance
		{
			// Token: 0x04000A9D RID: 2717
			public const string Ping = "ping";

			// Token: 0x04000A9E RID: 2718
			public const string ToggleMIMFarmMode = "toggleMIMFarmMode";
		}
	}
}
