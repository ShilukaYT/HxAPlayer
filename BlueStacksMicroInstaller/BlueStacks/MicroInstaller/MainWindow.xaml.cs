using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using BlueStacks.Common;
using CodeTitans.JSon;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x0200009D RID: 157
	public partial class MainWindow : Window
	{
		// Token: 0x0600035F RID: 863 RVA: 0x00015CD8 File Offset: 0x00013ED8
		public MainWindow()
		{
			base.Title = "BlueStacks Installer";
			base.Loaded += this.MainWindow_Loaded;
			this.InitTimers();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00015DC4 File Offset: 0x00013FC4
		private void ShowInsufficientRAMPopup()
		{
			StrippedMessageWindow strippedMessageWindow = new StrippedMessageWindow();
			bool isLoaded = base.IsLoaded;
			if (isLoaded)
			{
				strippedMessageWindow.Owner = this;
			}
			strippedMessageWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			strippedMessageWindow.TitleTextBlock.Text = Globalization.GetLocalizedString("STRING_INSUFFICIENT_MEMORY");
			string text = string.Format(Globalization.GetLocalizedString("STRING_PC_HAS_LESS_MEMORY"), "2");
			strippedMessageWindow.BodyTextBlock.Text = text;
			bool isLoaded2 = base.IsLoaded;
			if (isLoaded2)
			{
				strippedMessageWindow.Owner = this;
			}
			strippedMessageWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			strippedMessageWindow.AddButton(ButtonColors.Blue, Globalization.GetLocalizedString("STRING_CLOSE"), delegate(object s, EventArgs ev)
			{
				base.Hide();
				DownloaderStats.SendStats("mi_failed", "insufficient_physical_memory", "", "");
				App.ExitApplication(-1);
			}, null, false, null);
			strippedMessageWindow.ShowDialog();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00015E70 File Offset: 0x00014070
		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			bool flag = File.Exists(Path.Combine(App.sFLEAssetsDir, "installer_bg_1.png"));
			if (flag)
			{
				this.mInstallerBackgroundImage.Source = ImageUtils.BitmapFromPath(Path.Combine(App.sFLEAssetsDir, "installer_bg_1.png"));
			}
			else
			{
				this.mInstallerBackgroundImage.Source = ImageUtils.BitmapFromPath(Path.Combine("Assets", "installer_flash_background.jpg"));
			}
			string campaignAppName = this.GetCampaignAppName();
			bool flag2 = !string.IsNullOrEmpty(campaignAppName);
			if (flag2)
			{
				this.mAppNameInstallingText.Text = campaignAppName;
				this.mAppNameInstallingText.Visibility = Visibility.Visible;
			}
			this.mInstallerText.Text = Globalization.GetLocalizedString("STRING_INSTALLER");
			this.mInstallProgress.mInstallTips.Content = "";
			this.mInstallProgress.mInstallProgressStatus.Content = Globalization.GetLocalizedString("STRING_PRE_INSTALL_CHECKS");
			this.mChooseInstallFolder.mBluestacksDataLocation.Text = MainWindow.sPDDir;
			this.SetupEventHandlers();
			this.FetchImageForCampaign();
			bool flag3 = DownloaderStats.sCurrentInstallMode == InstallationModes.Upgrade;
			if (flag3)
			{
				this.ChangeUIForUpgrade();
			}
			base.Topmost = true;
			ThreadPool.QueueUserWorkItem(delegate(object obj)
			{
				base.Dispatcher.Invoke(new Action(delegate()
				{
					base.Topmost = false;
				}), new object[0]);
			});
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00015FB0 File Offset: 0x000141B0
		private string GetCampaignAppName()
		{
			string result = "";
			try
			{
				result = Encoding.UTF8.GetString(Convert.FromBase64String(App.sAppNameBase64ByteString.TrimEnd(new char[]
				{
					'"'
				})));
			}
			catch (Exception ex)
			{
				Logger.Warning("Exception while parsing app name for installer: " + ex.Message);
			}
			return result;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00016020 File Offset: 0x00014220
		private void MainWindow_UrlNavigationHandler(object sender, RequestNavigateEventArgs e)
		{
			try
			{
				Process.Start(new ProcessStartInfo(e.Uri.OriginalString));
			}
			catch (Exception ex)
			{
				Logger.Error("Couldn't open URL {0}, Ex: {1}", new object[]
				{
					e.Uri.OriginalString,
					ex.Message
				});
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00016088 File Offset: 0x00014288
		private void FetchImageForCampaign()
		{
			BackgroundWorker backgroundWorker = new BackgroundWorker();
			backgroundWorker.DoWork += this.MCampaignImageDownloader_DoWork;
			backgroundWorker.RunWorkerCompleted += this.MCampaignImageDownloader_RunWorkerCompleted;
			backgroundWorker.RunWorkerAsync();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000160CC File Offset: 0x000142CC
		private void MCampaignImageDownloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				this.mBackgroundImages = Directory.GetFiles(App.sFLEAssetsDir, "installer_bg_*.png", SearchOption.TopDirectoryOnly);
				bool flag = this.mBackgroundImages.Count<string>() < 1;
				if (!flag)
				{
					bool flag2 = this.mBackgroundImages.Count<string>() == 1;
					if (flag2)
					{
						this.mInstallerBackgroundImage.Source = ImageUtils.BitmapFromPath(this.mBackgroundImages[0]);
					}
					else
					{
						this.mBackgroundChangeTimer = new System.Timers.Timer(4000.0);
						this.mBackgroundChangeTimer.Elapsed += this.MBackgroundChangeTimer_Elapsed;
						this.mBackgroundChangeTimer.Enabled = true;
						this.MBackgroundChangeTimer_Elapsed(this, null);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Exception while setting image as background " + ex.ToString());
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000161AC File Offset: 0x000143AC
		private void MBackgroundChangeTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			bool flag = this.mBackgroundImages.Count<string>() <= this.mBackgroundSelectedIndex + 1;
			if (flag)
			{
				this.mBackgroundSelectedIndex = -1;
			}
			bool flag2 = this.mBackgroundImages.Count<string>() > this.mBackgroundSelectedIndex + 1;
			if (flag2)
			{
				this.mBackgroundSelectedIndex++;
				base.Dispatcher.Invoke(new Action(delegate()
				{
					DoubleAnimation fadeInAnimation = new DoubleAnimation(1.0, new TimeSpan(0, 0, 0, 0, 200));
					try
					{
						bool flag3 = this.mInstallerBackgroundImage.Source != null;
						if (flag3)
						{
							DoubleAnimation doubleAnimation = new DoubleAnimation(0.0, new TimeSpan(0, 0, 0, 0, 300));
							doubleAnimation.Completed += delegate(object o, EventArgs er)
							{
								this.mInstallerBackgroundImage.Source = ImageUtils.BitmapFromPath(this.mBackgroundImages[this.mBackgroundSelectedIndex]);
								this.mInstallerBackgroundImage.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
							};
							this.mInstallerBackgroundImage.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
						}
						else
						{
							this.mInstallerBackgroundImage.Opacity = 0.0;
							this.mInstallerBackgroundImage.Source = (new ImageSourceConverter().ConvertFromString(this.mBackgroundImages[this.mBackgroundSelectedIndex]) as ImageSource);
							this.mInstallerBackgroundImage.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
						}
					}
					catch
					{
						Logger.Error("Couldn't set animation for Background images");
					}
				}), new object[0]);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00016228 File Offset: 0x00014428
		private void MCampaignImageDownloader_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				bool flag = Directory.Exists(App.sFLEAssetsDir);
				if (flag)
				{
					Directory.Delete(App.sFLEAssetsDir, true);
				}
				string url = string.Format("{0}/bs3/get_installer_images?md5_hash={1}&prod_ver={2}&oem={3}&locale={4}", new object[]
				{
					DownloaderUtils.Host,
					App.sCampaignHash,
					"4.250.0.1070",
					"bgp",
					Thread.CurrentThread.CurrentCulture.Name
				});
				string text = DownloaderUtils.GetWebResponse(url, null, 5000);
				Logger.Info("The json response for get_installer_images {0} is {1}", new object[]
				{
					App.sCampaignHash,
					text
				});
				bool flag2 = string.IsNullOrEmpty(text);
				if (flag2)
				{
					text = "[]";
				}
				IJSonReader ijsonReader = new JSonReader();
				IJSonObject ijsonObject = ijsonReader.ReadAsJSonObject(text);
				int num = 1;
				foreach (IJSonObject ijsonObject2 in ijsonObject["installer_images"].ArrayItems)
				{
					DownloaderUtils.DownloadImage(ijsonObject2.ToString().Trim(), "installer_bg_" + num.ToString() + ".png", App.sFLEAssetsDir);
					num++;
				}
				bool flag3 = File.Exists(Path.Combine(App.sFLEAssetsDir, "installer_bg_1.png"));
				if (flag3)
				{
					base.Dispatcher.Invoke(new Action(delegate()
					{
						this.mInstallerBackgroundImage.Source = ImageUtils.BitmapFromPath(Path.Combine(App.sFLEAssetsDir, "installer_bg_1.png"));
					}), new object[0]);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error while downloading images for installer. " + ex.ToString());
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000163E8 File Offset: 0x000145E8
		private void ShowTakeBackupPopup()
		{
			StrippedMessageWindow strippedMessageWindow = new StrippedMessageWindow();
			strippedMessageWindow.TitleTextBlock.Text = Globalization.GetLocalizedString("STRING_BEFORE_YOU_UPDATE");
			strippedMessageWindow.BodyTextBlock.Text = Globalization.GetLocalizedString("STRING_PRECAUTION_BEFORE_UPGRADE");
			strippedMessageWindow.AddButton(ButtonColors.Blue, Globalization.GetLocalizedString("STRING_CONTINUE"), delegate(object s, EventArgs ev)
			{
				DownloaderStats.SendStats("mi_backup_continue", "", "", "");
			}, null, false, null);
			strippedMessageWindow.AddButton(ButtonColors.White, Globalization.GetLocalizedString("STRING_CANCEL"), delegate(object s, EventArgs ev)
			{
				DownloaderStats.SendStats("mi_backup_cancel", "", "", "");
				App.ExitApplication(-1);
			}, null, false, null);
			string uriString = string.Format("{0}/bs3/{1}&article={2}", DownloaderUtils.Host, "help_articles", "Backup_and_Restore");
			strippedMessageWindow.mUrlTextBlock.Visibility = Visibility.Visible;
			strippedMessageWindow.mUrlLink.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#047CD2"));
			strippedMessageWindow.mUrlLink.Inlines.Add(Globalization.GetLocalizedString("STRING_HOW_TO_TAKE_BACKUP"));
			strippedMessageWindow.mUrlLink.NavigateUri = new Uri(uriString);
			strippedMessageWindow.mUrlLink.RequestNavigate += this.MainWindow_UrlNavigationHandler;
			strippedMessageWindow.CloseButtonHandle(delegate(object s, EventArgs ev)
			{
				DownloaderStats.SendStats("mi_backup_cross", "", "", "");
				App.ExitApplication(-1);
			}, null);
			strippedMessageWindow.Owner = this;
			strippedMessageWindow.ShowDialog();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00016552 File Offset: 0x00014752
		private void ChangeUIForUpgrade()
		{
			this.mStartInstall.mCustomInstallLicensePanel.Visibility = Visibility.Hidden;
			this.mStartInstall.mInstallNowButton.Content = Globalization.GetLocalizedString("STRING_UPGRADE_NOW");
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00016584 File Offset: 0x00014784
		private void ShowInsufficientDiskSpacePopup(bool isSystemDrive)
		{
			StrippedMessageWindow strippedMessageWindow = new StrippedMessageWindow
			{
				Owner = this,
				WindowStartupLocation = WindowStartupLocation.CenterOwner
			};
			strippedMessageWindow.TitleTextBlock.Text = Globalization.GetLocalizedString("STRING_INSUFFICIENT_SPACE");
			string text = string.Format(Globalization.GetLocalizedString("STRING_BS_REQUIRES_DISK_INSTALL"), 5L);
			strippedMessageWindow.BodyTextBlock.Text = text;
			strippedMessageWindow.IsWindowClosable = false;
			strippedMessageWindow.AddButton(ButtonColors.Blue, Globalization.GetLocalizedString("STRING_CHOOSE_AGAIN"), new EventHandler(this.RetryDiskSpaceCheckHandler), null, false, null);
			strippedMessageWindow.ShowDialog();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00016612 File Offset: 0x00014812
		private void RetryDiskSpaceCheckHandler(object sender, EventArgs e)
		{
			this.mDownloadedBytes = 0L;
			DownloaderStats.SendStatsAsync("mi_low_disk_space_retried", "", "", "");
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00016638 File Offset: 0x00014838
		private void SetupEventHandlers()
		{
			this.mInstallProgress.MouseLeftButtonDown += this.HandleMouseDrag;
			this.mInstallerBackgroundImage.MouseLeftButtonDown += this.HandleMouseDrag;
			this.mStartInstall.MouseLeftButtonDown += this.HandleMouseDrag;
			this.mStartInstall.mCustomInstallImage.MouseDown += this.ShowCustomInstallUserControl;
			this.mStartInstall.mCustomInstallLabel.MouseDown += this.ShowCustomInstallUserControl;
			this.mStartInstall.mInstallNowButton.Click += this.InstallNowButton_Click;
			this.mInstallProgress.MouseLeftButtonDown += this.HandleMouseDrag;
			this.mChooseInstallFolder.MouseLeftButtonDown += this.HandleMouseDrag;
			this.mChooseInstallFolder.mBackButton.MouseDown += this.BackButton_MouseDown;
			this.mChooseInstallFolder.mBackButtonLabel.MouseDown += this.BackButton_MouseDown;
			this.mChooseInstallFolder.mInstallNowLabel.MouseDown += new MouseButtonEventHandler(this.InstallNowButton_Click);
			this.mChooseInstallFolder.mChooseFolderLabel.MouseDown += this.ChooseFolder_MouseDown;
			this.mChooseInstallFolder.mChooseFolderButton.MouseDown += this.ChooseFolder_MouseDown;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000167A8 File Offset: 0x000149A8
		private void ChooseFolder_MouseDown(object sender, MouseButtonEventArgs e)
		{
			using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
			{
				DialogResult dialogResult = folderBrowserDialog.ShowDialog();
				bool flag = dialogResult == System.Windows.Forms.DialogResult.OK;
				if (flag)
				{
					string text = folderBrowserDialog.SelectedPath.ToString();
					text = Path.Combine(text, "BlueStacks" + App.sRegistrySuffix);
					text = text.TrimEnd(new char[]
					{
						'\\'
					});
					bool flag2 = IOUtils.IsDirectoryEmpty(text);
					string value = new DirectoryInfo(MainWindow.sPFDir).ToString().TrimEnd(new char[]
					{
						'\\'
					});
					int num = text.IndexOfAny(IOUtils.DisallowedCharsInDirs);
					bool flag3 = text.Equals(value, StringComparison.InvariantCultureIgnoreCase) || !flag2;
					if (flag3)
					{
						StrippedMessageWindow strippedMessageWindow = new StrippedMessageWindow();
						strippedMessageWindow.TitleTextBlock.Text = Globalization.GetLocalizedString("STRING_CANNOT_USE_FOLDER");
						strippedMessageWindow.BodyTextBlock.Text = string.Format("{0}. {1}.", Globalization.GetLocalizedString("STRING_CANT_USE_THIS_FOLDER"), Globalization.GetLocalizedString("STRING_SELECT_FOLDER"));
						strippedMessageWindow.AddButton(ButtonColors.Blue, Globalization.GetLocalizedString("STRING_SELECT_AGAIN"), null, null, false, null);
						strippedMessageWindow.Owner = this;
						strippedMessageWindow.ShowDialog();
					}
					else
					{
						bool flag4 = -1 != num;
						if (flag4)
						{
							string text2 = string.Empty;
							for (int i = 0; i < IOUtils.DisallowedCharsInDirs.Length; i++)
							{
								text2 = text2 + IOUtils.DisallowedCharsInDirs[i].ToString() + "  ";
							}
							text2.TrimEnd(new char[]
							{
								' '
							});
							StrippedMessageWindow strippedMessageWindow2 = new StrippedMessageWindow();
							strippedMessageWindow2.TitleTextBlock.Text = Globalization.GetLocalizedString("STRING_CANNOT_USE_FOLDER");
							strippedMessageWindow2.BodyTextBlock.Text = string.Format("{0}\n\t{1}", Globalization.GetLocalizedString("STRING_FOLDER_CANNOT_CONTAIN_FOLLOWING_SPECIAL_CHARS"), text2);
							strippedMessageWindow2.AddButton(ButtonColors.Blue, Globalization.GetLocalizedString("STRING_SELECT_AGAIN"), null, null, false, null);
							strippedMessageWindow2.Owner = this;
							strippedMessageWindow2.ShowDialog();
						}
						else
						{
							this.CheckDiskSpaceAndUpdateLabel(text);
							this.mChooseInstallFolder.mBluestacksDataLocation.Text = text;
							MainWindow.sPDDir = text;
							Logger.Info("Updating PDDir to {0}", new object[]
							{
								MainWindow.sPDDir
							});
						}
					}
				}
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00016A04 File Offset: 0x00014C04
		private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.mChooseInstallFolder.Visibility = Visibility.Hidden;
			this.mStartInstall.Visibility = Visibility.Visible;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00016A24 File Offset: 0x00014C24
		private void InstallNowButton_Click(object sender, RoutedEventArgs e)
		{
			bool flag = (string)this.mStartInstall.mCheckboxImage.Tag != "checked_gray";
			if (flag)
			{
				StrippedMessageWindow strippedMessageWindow = new StrippedMessageWindow();
				strippedMessageWindow.TitleTextBlock.Text = Globalization.GetLocalizedString("STIRNG_ACCEPT_TNC");
				strippedMessageWindow.BodyTextBlock.Text = Globalization.GetLocalizedString("STRING_AGREE_WITH_LICENSE");
				strippedMessageWindow.AddButton(ButtonColors.Blue, Globalization.GetLocalizedString("STRING_OK"), null, null, false, null);
				strippedMessageWindow.Owner = this;
				strippedMessageWindow.ShowDialog();
			}
			else
			{
				bool flag2 = !this.mLicenceAgreedStatsSent;
				if (flag2)
				{
					DownloaderStats.SendStatsAsync("mi_license_agreed", "", "", "");
					this.mLicenceAgreedStatsSent = true;
				}
				bool flag3 = DownloaderUtils.IsPhysicalMemoryAvailable();
				bool flag4 = !flag3;
				if (flag4)
				{
					this.ShowInsufficientRAMPopup();
				}
				else
				{
					bool isSystemDrive;
					bool flag5 = !DownloaderUtils.IsDiskSpaceAvailable(MainWindow.sPDDir, out isSystemDrive);
					if (flag5)
					{
						this.ShowInsufficientDiskSpacePopup(isSystemDrive);
					}
					else
					{
						DownloaderStats.SendStatsAsync("mi_checks_passed", "", "", "");
						this.mChooseInstallFolder.Visibility = Visibility.Hidden;
						this.mStartInstall.Visibility = Visibility.Hidden;
						this.mInstallProgress.Visibility = Visibility.Visible;
						this.mInstallProgress.mInstallTips.Content = Globalization.GetLocalizedString("STRING_INSTALLATION_START_WHEN_DOWNLOAD_FINISHES");
						this.mInstallProgress.mInstallProgressStatus.Content = Globalization.GetLocalizedString("STRING_PRE_INSTALL_CHECKS");
						this.InitAndStartDownload();
					}
				}
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00016B9F File Offset: 0x00014D9F
		private void ShowCustomInstallUserControl(object sender, MouseButtonEventArgs e)
		{
			this.mStartInstall.Visibility = Visibility.Hidden;
			this.mChooseInstallFolder.Visibility = Visibility.Visible;
			this.CheckDiskSpaceAndUpdateLabel(MainWindow.sPDDir);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00016BC8 File Offset: 0x00014DC8
		private void CheckDiskSpaceAndUpdateLabel(string newPath)
		{
			DriveInfo driveInfo = new DriveInfo(newPath);
			long availableFreeSpace = driveInfo.AvailableFreeSpace;
			long num = availableFreeSpace / 1048576L;
			bool flag = num < 5120L;
			if (flag)
			{
				StrippedMessageWindow strippedMessageWindow = new StrippedMessageWindow();
				strippedMessageWindow.TitleTextBlock.Text = Globalization.GetLocalizedString("STRING_INSUFFICIENT_SPACE");
				strippedMessageWindow.BodyTextBlock.Text = Globalization.GetLocalizedString("STRING_SELECTED_DRIVE_INSUFFICIENT_DISKSPACE");
				strippedMessageWindow.AddButton(ButtonColors.Blue, Globalization.GetLocalizedString("STRING_CHOOSE_AGAIN"), null, null, false, null);
				strippedMessageWindow.Owner = this;
				strippedMessageWindow.ShowDialog();
			}
			this.mChooseInstallFolder.mSpaceAvailable.Content = string.Format("{0}MB", num);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00016C7C File Offset: 0x00014E7C
		private void HandleMouseDrag(object sender, MouseButtonEventArgs e)
		{
			try
			{
				base.DragMove();
			}
			catch
			{
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00016CAC File Offset: 0x00014EAC
		private void InitTimers()
		{
			this.mDownloadStatusTimer = new System.Threading.Timer(delegate(object x)
			{
				this.DownloadStatusTimerTick();
			}, null, 5000, 5000);
			this.mSpeedCalculatorTimer = new System.Threading.Timer(delegate(object x)
			{
				this.MSpeedCalculatorTimer_Tick();
			}, null, 5000, 5000);
			this.mPercentUpdateTimer = new System.Threading.Timer(delegate(object x)
			{
				this.MProgressUpdateTimer_Tick();
			}, null, 200, 200);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00016D20 File Offset: 0x00014F20
		private void MProgressUpdateTimer_Tick()
		{
			this.mUpdatePercent = true;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00016D2C File Offset: 0x00014F2C
		private void MSpeedCalculatorTimer_Tick()
		{
			bool flag = this.mLastRecordedSpeedCalculationBytes != this.mDownloadedBytes;
			if (flag)
			{
				long num = this.mDownloadedBytes - this.mLastRecordedSpeedCalculationBytes;
				double num2 = (double)num / 5.0;
				bool flag2 = num2 >= 1.0;
				if (flag2)
				{
					long num3 = this.mInstallerSizeInBytes - this.mDownloadedBytes;
					long secondsToDownload = (long)((double)num3 / num2);
					this.mLastRecordedSpeedCalculationBytes = this.mDownloadedBytes;
					this.UpdateInstallerProgressSpeed(secondsToDownload);
				}
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00016DB0 File Offset: 0x00014FB0
		private void DownloadStatusTimerTick()
		{
			Logger.Info("Status timer tick, mIsDownloading: {0}", new object[]
			{
				this.mIsDownloading
			});
			bool flag = !this.mIsDownloading;
			if (flag)
			{
				Logger.Info("Not downloading");
			}
			else
			{
				Logger.Info("LastRecorded: {0}, Downloaded: {1}", new object[]
				{
					this.mLastRecordedDownloadedBytes,
					this.mDownloadedBytes
				});
				bool flag2 = this.mLastRecordedDownloadedBytes != this.mDownloadedBytes;
				if (flag2)
				{
					this.mLastRecordedDownloadedBytes = this.mDownloadedBytes;
					this.mSecondsSinceNoDownload = 0;
					bool flag3 = this.mWasNetworkConnectivityLost;
					if (flag3)
					{
						base.Dispatcher.Invoke(new Action(delegate()
						{
							this.mInstallProgress.mInstallTips.Content = Globalization.GetLocalizedString("STRING_INSTALLATION_START_WHEN_DOWNLOAD_FINISHES");
						}), new object[0]);
						this.mWasNetworkConnectivityLost = false;
					}
				}
				else
				{
					this.mWasNetworkConnectivityLost = true;
					this.mSecondsSinceNoDownload += 5;
					base.Dispatcher.Invoke(new Action(delegate()
					{
						this.mInstallProgress.mInstallProgressStatus.Content = Globalization.GetLocalizedString("STRING_WAITING_FOR_INTERNET");
					}), new object[0]);
				}
				bool flag4 = this.mSecondsSinceNoDownload >= 120;
				if (flag4)
				{
				}
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00016ED0 File Offset: 0x000150D0
		private void InitAndStartDownload()
		{
			this.mSecondsSinceNoDownload = 0;
			this.SetFinalInstallerLocation();
			this.mTempDownloadPath = Path.Combine(Path.GetTempPath(), App.sParentFileName);
			Logger.Info("Temp download path: " + this.mTempDownloadPath);
			Logger.Info("Installer file path: " + this.mInstallerFilePath);
			bool flag = (string)this.mStartInstall.mPromotionCheckboxImage.Tag == "checked_gray";
			if (flag)
			{
				DownloaderStats.sReceiveEmailNotification = true.ToString();
			}
			else
			{
				DownloaderStats.sReceiveEmailNotification = false.ToString();
			}
			this.StartDownloadAsync();
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00016F7C File Offset: 0x0001517C
		private void SetFinalInstallerLocation()
		{
			this.mInstallerFilePath = Path.Combine(Strings.BlueStacksSetupFolder, App.sParentFileName);
			while (File.Exists(this.mInstallerFilePath))
			{
				Random random = new Random();
				this.mInstallerFilePath = Path.Combine(Strings.BlueStacksSetupFolder, string.Format("{0}{1}", random.Next(), App.sParentFileName));
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00016FE4 File Offset: 0x000151E4
		private void RetryDownloadHandler(object sender, EventArgs e)
		{
			bool flag = this.mDeleteFileAndRetry;
			if (flag)
			{
				File.Delete(this.mTempDownloadPath);
				File.Delete(this.mTempDownloadPath + ".tmp");
				this.mDeleteFileAndRetry = false;
			}
			this.mDownloadedBytes = 0L;
			DownloaderStats.SendStatsAsync("mi_download_retried", "", this.mDownloadErrorMessage, this.mDownloadedBytes.ToString());
			this.InitAndStartDownload();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00017058 File Offset: 0x00015258
		private static string GetSpecialFolderName(string version)
		{
			_MD5 md = new _MD5
			{
				Value = version + "-samyak"
			};
			return md.FingerPrint.ToLower();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00017090 File Offset: 0x00015290
		private static Uri GetDownloadUri()
		{
			bool flag = MicroInstallerProperties.BuildPlatformToDownload == DownloadBuildType.Default;
			string arg;
			if (flag)
			{
				bool flag2 = SystemUtils.IsOs64Bit();
				if (flag2)
				{
					arg = string.Format("http://cdn3.bluestacks.com/downloads/windows/{0}/{1}/{4}/{2}/BlueStacks-Installer_{1}_{3}_native.exe", new object[]
					{
						"bgp",
						"4.250.0.1070",
						"x64",
						"amd64",
						MainWindow.GetSpecialFolderName("4.250.0.1070")
					});
				}
				else
				{
					arg = string.Format("http://cdn3.bluestacks.com/downloads/windows/{0}/{1}/{4}/{2}/BlueStacks-Installer_{1}_{3}_native.exe", new object[]
					{
						"bgp",
						"4.250.0.1070",
						"x86",
						"x86",
						MainWindow.GetSpecialFolderName("4.250.0.1070")
					});
				}
			}
			else
			{
				arg = string.Format("https://cdn3.bluestacks.com/downloads/windows/bgp64_hyperv/{0}/{1}/x64/BlueStacks-Installer_{0}_amd64_native.exe", "4.240.15.4204", MainWindow.GetSpecialFolderName("4.240.15.4204"));
			}
			string text = string.Format("{0}?filename={1}", arg, App.sParentFileName);
			Logger.Info("Download url: " + text);
			return new Uri(text);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00017188 File Offset: 0x00015388
		public void StartDownloadAsync()
		{
			Thread thread = new Thread(delegate()
			{
				this.mWasDownloadStatSent = false;
				Downloader downloader = new Downloader();
				downloader.DownloadException += this.Downloader_DownloadException;
				downloader.DownloadProgressChanged += this.Downloader_DownloadProgressChanged;
				downloader.DownloadFileCompleted += this.Downloader_DownloadFileCompleted;
				downloader.FilePayloadInfoReceived += this.Downloader_FilePayloadInfoReceived;
				downloader.DownloadRetry += this.Downloader_DownloadRetry;
				downloader.UnsupportedResume += this.Downloader_UnsupportedResume;
				this.mIsDownloading = true;
				downloader.DownloadFile(MainWindow.GetDownloadUri().AbsoluteUri, this.mTempDownloadPath);
			});
			thread.Start();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000171AF File Offset: 0x000153AF
		private void Downloader_DownloadRetry(object sender, EventArgs args)
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				this.mInstallProgress.mInstallProgressStatus.Content = Globalization.GetLocalizedString("STRING_WAITING_FOR_INTERNET");
			}), new object[0]);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000171D0 File Offset: 0x000153D0
		private void Downloader_UnsupportedResume(HttpStatusCode sc)
		{
			this.mDeleteFileAndRetry = true;
			Logger.Warning("The remote server does not support resume: {0}", new object[]
			{
				sc
			});
			string text = "HTTP response: " + sc.ToString();
			DownloaderStats.SendStatsAsync("mi_download_failed", "", text, this.mDownloadedBytes.ToString());
			this.HandleDownloadError(text);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001723C File Offset: 0x0001543C
		private void Downloader_FilePayloadInfoReceived(long fileSize)
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				this.mInstallProgress.mInstallTips.Content = Globalization.GetLocalizedString("STRING_INSTALLATION_START_WHEN_DOWNLOAD_FINISHES");
				this.mInstallerSizeInBytes = fileSize;
				this.mInstallerSizeInMegaBytes = this.mInstallerSizeInBytes / 1048576L;
				this.mInstallerSizeInMBString = string.Format("{0} MB", this.mInstallerSizeInMegaBytes);
				this.mInstallProgress.mInstallProgressStatus.Content = Globalization.GetLocalizedString("STRING_DOWNLOADING");
				this.UpdateInstallerProgressSpeed(240L);
			}), new object[0]);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001727C File Offset: 0x0001547C
		private void Downloader_DownloadException(Exception e)
		{
			DownloaderStats.SendStatsAsync("mi_download_failed", "", e.Message, this.mDownloadedBytes.ToString());
			this.HandleDownloadError(e.Message);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000172B0 File Offset: 0x000154B0
		private void Downloader_DownloadProgressChanged(long size)
		{
			base.Dispatcher.Invoke(new Action(delegate()
			{
				this.mInstallProgress.mInstallProgressStatus.Content = Globalization.GetLocalizedString("STRING_DOWNLOADING");
				bool flag = !this.mWasDownloadStatSent;
				if (flag)
				{
					DownloaderStats.SendStatsAsync("mi_download_started", "", "", "");
					this.mWasDownloadStatSent = true;
					string mbString = string.Format(Globalization.GetLocalizedString("STRING_0_OF_1"), "0", this.mInstallerSizeInMBString);
					this.UpdateInstallerProgressSize(mbString);
					this.UpdateInstallerProgressSpeed(240L);
				}
				this.mDownloadedBytes = size;
				this.mDownloadedMegaBytes = this.mDownloadedBytes / 1048576L;
				bool flag2 = this.mLastRecordedDownloadedMegaBytes != this.mDownloadedMegaBytes;
				if (flag2)
				{
					this.mLastRecordedDownloadedMegaBytes = this.mDownloadedMegaBytes;
					string arg = string.Format("{0} MB", this.mDownloadedMegaBytes);
					string mbString2 = string.Format(Globalization.GetLocalizedString("STRING_0_OF_1"), arg, this.mInstallerSizeInMBString);
					this.UpdateInstallerProgressSize(mbString2);
				}
				bool flag3 = this.mUpdatePercent;
				if (flag3)
				{
					decimal num = decimal.Divide(this.mDownloadedBytes, this.mInstallerSizeInBytes) * 100m;
					this.mInstallProgress.mInstallProgressPercentage.Content = string.Format("{0:N2}%", num);
					this.mInstallProgress.mInstallProgressBar.Value = (double)num;
					this.mUpdatePercent = false;
				}
			}), new object[0]);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000172F0 File Offset: 0x000154F0
		private void Downloader_DownloadFileCompleted(object sender, EventArgs args)
		{
			Logger.Info("MIsDownloading false");
			this.mIsDownloading = false;
			this.mDownloadStatusTimer.Change(-1, -1);
			DownloaderStats.SendStatsAsync("mi_download_completed", "", "", "");
			base.Dispatcher.Invoke(new Action(delegate()
			{
				this.UpdateUiAndLaunchInstallerAsync();
			}), new object[0]);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00017358 File Offset: 0x00015558
		private void UpdateInstallerProgressSize(string mbString)
		{
			this.mDownloadedString = mbString;
			this.mInstallProgress.mInstallProgressStatus.Content = Globalization.GetLocalizedString("STRING_DOWNLOADING");
			bool flag = string.IsNullOrEmpty(this.mDownloadedString);
			if (flag)
			{
				this.mInstallProgress.mInstallTips.Content = this.mETAString;
			}
			else
			{
				this.mInstallProgress.mInstallTips.Content = string.Format(Globalization.GetLocalizedString("STRING_0_COMMA_1"), this.mDownloadedString, this.mETAString);
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000173E0 File Offset: 0x000155E0
		private void UpdateInstallerProgressSpeed(long secondsToDownload)
		{
			bool flag = secondsToDownload >= 60L;
			string text;
			if (flag)
			{
				text = string.Format(Globalization.GetLocalizedString("STRING_0_MINS_LEFT"), secondsToDownload / 60L);
			}
			else
			{
				text = string.Format(Globalization.GetLocalizedString("STRING_0_MINS_LEFT"), 1);
			}
			this.mETAString = text;
			base.Dispatcher.Invoke(new Action(delegate()
			{
				bool flag2 = string.IsNullOrEmpty(this.mDownloadedString);
				if (flag2)
				{
					this.mInstallProgress.mInstallTips.Content = this.mETAString;
				}
				else
				{
					this.mInstallProgress.mInstallTips.Content = string.Format(Globalization.GetLocalizedString("STRING_0_COMMA_1"), this.mDownloadedString, this.mETAString);
				}
			}), new object[0]);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00017458 File Offset: 0x00015658
		private void HandleDownloadError(string errorMessage)
		{
			this.mDownloadErrorMessage = errorMessage;
			EventHandler <>9__1;
			base.Dispatcher.Invoke(new Action(delegate()
			{
				StrippedMessageWindow strippedMessageWindow = new StrippedMessageWindow();
				bool isLoaded = this.IsLoaded;
				if (isLoaded)
				{
					strippedMessageWindow.Owner = this;
				}
				strippedMessageWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				strippedMessageWindow.TitleTextBlock.Text = Globalization.GetLocalizedString("STRING_DOWNLOAD_FAILED");
				string text = string.Format("{0} {1}", Globalization.GetLocalizedString("STRING_ERROR_WHILE_DOWNLOADING_BS"), Globalization.GetLocalizedString("STRING_POST_OTS_FAILED_WARNING_MESSAGE"));
				strippedMessageWindow.BodyTextBlock.Text = text;
				strippedMessageWindow.IsWindowClosable = false;
				strippedMessageWindow.AddButton(ButtonColors.Blue, Globalization.GetLocalizedString("STRING_RETRY_CONNECTION_ISSUE_TEXT1"), new EventHandler(this.RetryDownloadHandler), null, false, null);
				StrippedMessageWindow strippedMessageWindow2 = strippedMessageWindow;
				ButtonColors color = ButtonColors.White;
				string localizedString = Globalization.GetLocalizedString("STRING_CANCEL");
				EventHandler handle;
				if ((handle = <>9__1) == null)
				{
					handle = (<>9__1 = delegate(object s, EventArgs ev)
					{
						this.Hide();
						DownloaderStats.SendStats("mi_closed", "downloading_failed", errorMessage, this.mDownloadedBytes.ToString());
						App.ExitApplication(-1);
					});
				}
				strippedMessageWindow2.AddButton(color, localizedString, handle, null, false, null);
				strippedMessageWindow.ShowDialog();
			}), new object[0]);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000174A4 File Offset: 0x000156A4
		private void Installer_Closing(object sender, CancelEventArgs e)
		{
			bool flag = this.mIsDownloading;
			if (flag)
			{
				this.ShowCloseDuringDownloadPopup();
			}
			e.Cancel = true;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000174D0 File Offset: 0x000156D0
		private void ShowCloseDuringDownloadPopup()
		{
			bool flag = this.mDownloadedBytes != 0L;
			if (flag)
			{
				StrippedMessageWindow window = new StrippedMessageWindow();
				bool isLoaded = base.IsLoaded;
				if (isLoaded)
				{
					window.Owner = this;
				}
				window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
				window.TitleTextBlock.Text = Globalization.GetLocalizedString("STRING_MINIMIZE_INSTALLER");
				window.BodyTextBlock.Text = Globalization.GetLocalizedString("STRING_CONTINUE_IN_BACKGROUND");
				window.AddButton(ButtonColors.Blue, Globalization.GetLocalizedString("STRING_YES"), delegate(object s, EventArgs ev)
				{
					DownloaderStats.SendStatsAsync("mi_minimizepopup_yes", "", "", this.mDownloadedBytes.ToString());
					base.WindowState = WindowState.Minimized;
				}, null, false, null);
				window.AddButton(ButtonColors.White, Globalization.GetLocalizedString("STRING_NO"), delegate(object s, EventArgs ev)
				{
					window.Hide();
					this.Hide();
					DownloaderStats.SendStats("mi_minimizepopup_no", "", "", this.mDownloadedBytes.ToString());
					App.ExitApplication(-1);
				}, null, false, null);
				DownloaderStats.SendStatsAsync("mi_minimizepopup_init", "", "", this.mDownloadedBytes.ToString());
				window.ShowDialog();
			}
			else
			{
				base.Hide();
				DownloaderStats.SendStats("mi_closed", "", "", this.mDownloadedBytes.ToString());
				App.ExitApplication(-1);
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00017610 File Offset: 0x00015810
		private void UpdateUiAndLaunchInstallerAsync()
		{
			Thread thread = new Thread(delegate()
			{
				bool flag = File.Exists(this.mTempDownloadPath);
				if (flag)
				{
					bool flag2 = File.Exists(this.mInstallerFilePath);
					if (flag2)
					{
						File.Delete(this.mInstallerFilePath);
					}
					Directory.CreateDirectory(Path.GetDirectoryName(this.mInstallerFilePath));
					File.Move(this.mTempDownloadPath, this.mInstallerFilePath);
					Logger.Info("Installer file moved from " + this.mTempDownloadPath + " to " + this.mInstallerFilePath);
				}
				Process process = new Process();
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.UseShellExecute = true;
				process.StartInfo.FileName = this.mInstallerFilePath;
				process.StartInfo.Arguments = string.Format("-versionMachineID={0} -machineID={1} -pddir=\"{2}\"", App.sBluestacksVersionId, App.sBlueStacksMachineId, MainWindow.sPDDir);
				bool flag3 = DownloaderStats.sReceiveEmailNotification == true.ToString();
				if (flag3)
				{
					ProcessStartInfo startInfo = process.StartInfo;
					startInfo.Arguments += " -receiveEmailNotification";
				}
				try
				{
					process.Start();
					System.Windows.Application.Current.Dispatcher.Invoke(new Action(delegate()
					{
						base.Hide();
					}), new object[0]);
				}
				catch (Exception ex)
				{
					Logger.Info("Failed to launch full installer. Error: " + ex.Message);
					DownloaderStats.SendStats("mi_failed", "full_installer_launch_failed", ex.Message, "");
					App.ExitApplication(-1);
				}
				DownloaderStats.SendStatsAsync("mi_full_installer_launched", "", "", "");
				process.WaitForExit();
				int exitCode = process.ExitCode;
				Logger.Info("Full installer process finished with ExitCode: {0}", new object[]
				{
					exitCode
				});
				App.ExitApplication(exitCode);
			})
			{
				IsBackground = true
			};
			thread.Start();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001763F File Offset: 0x0001583F
		private void MCloseButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.ShowCloseDuringDownloadPopup();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00017649 File Offset: 0x00015849
		private void MMinimizeButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x0400053C RID: 1340
		private string mTempDownloadPath = "";

		// Token: 0x0400053D RID: 1341
		private string mInstallerFilePath = "";

		// Token: 0x0400053E RID: 1342
		private string mInstallerSizeInMBString = "0 MB";

		// Token: 0x0400053F RID: 1343
		private string mETAString = "";

		// Token: 0x04000540 RID: 1344
		private string mDownloadedString = "";

		// Token: 0x04000541 RID: 1345
		private bool mIsDownloading = false;

		// Token: 0x04000542 RID: 1346
		private long mDownloadedBytes = 0L;

		// Token: 0x04000543 RID: 1347
		private int mSecondsSinceNoDownload = 0;

		// Token: 0x04000544 RID: 1348
		private bool mWasDownloadStatSent = false;

		// Token: 0x04000545 RID: 1349
		private bool mWasNetworkConnectivityLost = false;

		// Token: 0x04000546 RID: 1350
		private long mInstallerSizeInBytes = 0L;

		// Token: 0x04000547 RID: 1351
		private long mInstallerSizeInMegaBytes = 0L;

		// Token: 0x04000548 RID: 1352
		private long mDownloadedMegaBytes = 0L;

		// Token: 0x04000549 RID: 1353
		private long mLastRecordedDownloadedBytes = 0L;

		// Token: 0x0400054A RID: 1354
		private long mLastRecordedSpeedCalculationBytes;

		// Token: 0x0400054B RID: 1355
		private long mLastRecordedDownloadedMegaBytes;

		// Token: 0x0400054C RID: 1356
		private System.Threading.Timer mDownloadStatusTimer;

		// Token: 0x0400054D RID: 1357
		private System.Threading.Timer mSpeedCalculatorTimer;

		// Token: 0x0400054E RID: 1358
		private System.Threading.Timer mPercentUpdateTimer;

		// Token: 0x0400054F RID: 1359
		private System.Timers.Timer mBackgroundChangeTimer;

		// Token: 0x04000550 RID: 1360
		private const long MB_FACTOR = 1048576L;

		// Token: 0x04000551 RID: 1361
		private static string sPDDir = DownloaderUtils.GetPDDir();

		// Token: 0x04000552 RID: 1362
		private static string sPFDir = DownloaderUtils.GetPFDir();

		// Token: 0x04000553 RID: 1363
		private string mDownloadErrorMessage = string.Empty;

		// Token: 0x04000554 RID: 1364
		private bool mLicenceAgreedStatsSent = false;

		// Token: 0x04000555 RID: 1365
		private bool mDeleteFileAndRetry = false;

		// Token: 0x04000556 RID: 1366
		private const int DownloadSpeedAverageCalculationTimeSeconds = 5;

		// Token: 0x04000557 RID: 1367
		private bool mUpdatePercent = true;

		// Token: 0x04000558 RID: 1368
		private const string DOWNLOAD_URL_BASE = "http://cdn3.bluestacks.com/downloads/windows/{0}/{1}/{4}/{2}/BlueStacks-Installer_{1}_{3}_native.exe";

		// Token: 0x04000559 RID: 1369
		private const string HYPERV_BUILD_URL_FORMAT = "https://cdn3.bluestacks.com/downloads/windows/bgp64_hyperv/{0}/{1}/x64/BlueStacks-Installer_{0}_amd64_native.exe";

		// Token: 0x0400055A RID: 1370
		private string[] mBackgroundImages = new string[0];

		// Token: 0x0400055B RID: 1371
		private int mBackgroundSelectedIndex = -1;
	}
}
