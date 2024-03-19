using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Win32;

namespace BlueStacks.Common
{
	// Token: 0x02000067 RID: 103
	public static class ServiceManager
	{
		// Token: 0x06000148 RID: 328
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr OpenSCManager(string lpMachineName, string lpDatabaseName, ServiceManager.ServiceManagerRights dwDesiredAccess);

		// Token: 0x06000149 RID: 329
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, ServiceManager.ServiceRights dwDesiredAccess);

		// Token: 0x0600014A RID: 330
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, ServiceManager.ServiceRights dwDesiredAccess, int dwServiceType, ServiceManager.ServiceBootFlag dwStartType, ServiceManager.ServiceError dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, IntPtr lpdwTagId, string lpDependencies, string lp, string lpPassword);

		// Token: 0x0600014B RID: 331
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int CloseServiceHandle(IntPtr hSCObject);

		// Token: 0x0600014C RID: 332
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int QueryServiceStatus(IntPtr hService, ServiceManager.SERVICE_STATUS lpServiceStatus);

		// Token: 0x0600014D RID: 333
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int DeleteService(IntPtr hService);

		// Token: 0x0600014E RID: 334
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int ControlService(IntPtr hService, ServiceManager.ServiceControl dwControl, ServiceManager.SERVICE_STATUS lpServiceStatus);

		// Token: 0x0600014F RID: 335
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int StartService(IntPtr hService, int dwNumServiceArgs, int lpServiceArgVectors);

		// Token: 0x06000150 RID: 336
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool QueryServiceObjectSecurity(SafeHandle serviceHandle, SecurityInfos secInfo, byte[] lpSecDesrBuf, uint bufSize, out uint bufSizeNeeded);

		// Token: 0x06000151 RID: 337
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetServiceObjectSecurity(SafeHandle serviceHandle, SecurityInfos secInfos, byte[] lpSecDesrBuf);

		// Token: 0x06000152 RID: 338 RVA: 0x00007DBC File Offset: 0x00005FBC
		public static bool UninstallService(string serviceName, bool isKernelDriverService = false)
		{
			Logger.Info("{0} {1}", new object[]
			{
				MethodBase.GetCurrentMethod().Name,
				serviceName
			});
			IntPtr intPtr = ServiceManager.OpenSCManager(ServiceManager.ServiceManagerRights.Connect);
			bool result = true;
			try
			{
				IntPtr intPtr2 = ServiceManager.OpenService(intPtr, serviceName, (ServiceManager.ServiceRights)983076);
				bool flag = intPtr2 == IntPtr.Zero;
				if (flag)
				{
					Logger.Info("Service " + serviceName + " is not installed or inaccessible.");
					return true;
				}
				try
				{
					ServiceManager.StopService(intPtr2, isKernelDriverService);
					int num = ServiceManager.DeleteService(intPtr2);
					bool flag2 = num == 0;
					if (flag2)
					{
						throw new Exception("Could not delete service " + Marshal.GetLastWin32Error().ToString());
					}
				}
				catch (Exception ex)
				{
					Logger.Error("Failed to uninstall service... Err : " + ex.ToString());
					result = false;
				}
				finally
				{
					ServiceManager.CloseServiceHandle(intPtr2);
				}
			}
			finally
			{
				ServiceManager.CloseServiceHandle(intPtr);
			}
			return result;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007ED4 File Offset: 0x000060D4
		public static bool ServiceIsInstalled(string serviceName)
		{
			IntPtr intPtr = ServiceManager.OpenSCManager(ServiceManager.ServiceManagerRights.Connect);
			bool result;
			try
			{
				IntPtr intPtr2 = ServiceManager.OpenService(intPtr, serviceName, ServiceManager.ServiceRights.QueryStatus);
				bool flag = intPtr2 == IntPtr.Zero;
				if (flag)
				{
					result = false;
				}
				else
				{
					ServiceManager.CloseServiceHandle(intPtr2);
					result = true;
				}
			}
			finally
			{
				ServiceManager.CloseServiceHandle(intPtr);
			}
			return result;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007F30 File Offset: 0x00006130
		public static void InstallKernelDriver(string serviceName, string displayName, string fileName)
		{
			Logger.Info("{0} {1}", new object[]
			{
				MethodBase.GetCurrentMethod().Name,
				serviceName
			});
			IntPtr intPtr = ServiceManager.OpenSCManager(ServiceManager.ServiceManagerRights.AllAccess);
			try
			{
				IntPtr value = ServiceManager.OpenService(intPtr, serviceName, ServiceManager.ServiceRights.QueryStatus | ServiceManager.ServiceRights.Start);
				bool flag = value != IntPtr.Zero;
				if (flag)
				{
					Logger.Info("service is already installed...uninstalling it first");
					ServiceManager.UninstallService(serviceName, true);
				}
				value = ServiceManager.CreateService(intPtr, serviceName, displayName, ServiceManager.ServiceRights.AllAccess, 1, ServiceManager.ServiceBootFlag.AutoStart, ServiceManager.ServiceError.Normal, fileName, null, IntPtr.Zero, null, null, null);
				int lastWin32Error = Marshal.GetLastWin32Error();
				bool flag2 = value == IntPtr.Zero;
				if (flag2)
				{
					Logger.Info("Error in creating kernel driver service...last win32 error = " + lastWin32Error.ToString());
					throw new Exception("Failed to create service.");
				}
				Logger.Info("Successfully created service = " + serviceName + "...setting DACL now");
				ServiceManager.SetServicePermissions(serviceName);
				Logger.Info("Successfully set DACL");
			}
			catch (Exception ex)
			{
				ServiceManager.CloseServiceHandle(intPtr);
				Logger.Error("Failed to install kernel driver... Err : " + ex.ToString());
				throw new Exception(ex.Message);
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00008058 File Offset: 0x00006258
		public static void Install(string serviceName, string displayName, string fileName)
		{
			Logger.Info("{0} {1}", new object[]
			{
				MethodBase.GetCurrentMethod().Name,
				serviceName
			});
			IntPtr intPtr = ServiceManager.OpenSCManager(ServiceManager.ServiceManagerRights.AllAccess);
			try
			{
				IntPtr value = ServiceManager.OpenService(intPtr, serviceName, ServiceManager.ServiceRights.QueryStatus | ServiceManager.ServiceRights.Start);
				bool flag = value != IntPtr.Zero;
				if (flag)
				{
					Logger.Info("service is already installed...uninstalling it first");
					ServiceManager.UninstallService(serviceName, false);
				}
				value = ServiceManager.CreateService(intPtr, serviceName, displayName, ServiceManager.ServiceRights.AllAccess, 16, ServiceManager.ServiceBootFlag.AutoStart, ServiceManager.ServiceError.Normal, fileName, null, IntPtr.Zero, null, null, null);
				Logger.Info("Successfully created service = " + serviceName + "...setting DACL now");
				ServiceManager.SetServicePermissions(serviceName);
				Logger.Info("Successfully set DACL");
				bool flag2 = value == IntPtr.Zero;
				if (flag2)
				{
					throw new Exception("Failed to install service.");
				}
			}
			catch (Exception ex)
			{
				ServiceManager.CloseServiceHandle(intPtr);
				Logger.Error("Failed to install kernel driver... Err : " + ex.ToString());
				throw new Exception(ex.Message);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00008164 File Offset: 0x00006364
		public static void StartService(string name, bool isKernelDriverService = false)
		{
			IntPtr intPtr = ServiceManager.OpenSCManager(ServiceManager.ServiceManagerRights.Connect);
			try
			{
				IntPtr intPtr2 = ServiceManager.OpenService(intPtr, name, ServiceManager.ServiceRights.QueryStatus | ServiceManager.ServiceRights.Start);
				bool flag = intPtr2 == IntPtr.Zero;
				if (flag)
				{
					throw new Exception("Could not open service.");
				}
				try
				{
					ServiceManager.StartService(intPtr2, isKernelDriverService);
				}
				finally
				{
					ServiceManager.CloseServiceHandle(intPtr2);
				}
			}
			finally
			{
				ServiceManager.CloseServiceHandle(intPtr);
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000081E0 File Offset: 0x000063E0
		public static void StopService(string name, bool isKernelDriverService = false)
		{
			Logger.Info("{0} {1}", new object[]
			{
				MethodBase.GetCurrentMethod().Name,
				name
			});
			IntPtr intPtr = ServiceManager.OpenSCManager(ServiceManager.ServiceManagerRights.Connect);
			try
			{
				IntPtr intPtr2 = ServiceManager.OpenService(intPtr, name, ServiceManager.ServiceRights.QueryStatus | ServiceManager.ServiceRights.Stop);
				bool flag = intPtr2 == IntPtr.Zero;
				if (!flag)
				{
					try
					{
						ServiceManager.StopService(intPtr2, isKernelDriverService);
					}
					finally
					{
						ServiceManager.CloseServiceHandle(intPtr2);
					}
				}
			}
			finally
			{
				ServiceManager.CloseServiceHandle(intPtr);
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008274 File Offset: 0x00006474
		private static void StartService(IntPtr hService, bool isKernelDriverService = false)
		{
			Logger.Info("{0} {1}", new object[]
			{
				MethodBase.GetCurrentMethod().Name,
				Convert.ToString(hService, CultureInfo.InvariantCulture)
			});
			int num = ServiceManager.StartService(hService, 0, 0);
			bool flag = num == 0;
			if (flag)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				Logger.Warning("Error in starting service, StartService ret: {0}, Last win32 error: {1}", new object[]
				{
					num,
					lastWin32Error
				});
			}
			ServiceManager.SERVICE_STATUS ssStatus = new ServiceManager.SERVICE_STATUS(isKernelDriverService);
			ServiceManager.WaitForServiceStatus(hService, ServiceManager.ServiceState.StartPending, ServiceManager.ServiceState.Running, ssStatus);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008300 File Offset: 0x00006500
		private static void StopService(IntPtr hService, bool isKernelDriverService = false)
		{
			Logger.Info("{0} {1}", new object[]
			{
				MethodBase.GetCurrentMethod().Name,
				Convert.ToString(hService, CultureInfo.InvariantCulture)
			});
			ServiceManager.SERVICE_STATUS service_STATUS = new ServiceManager.SERVICE_STATUS(isKernelDriverService);
			ServiceManager.ControlService(hService, ServiceManager.ServiceControl.Stop, service_STATUS);
			ServiceManager.WaitForServiceStatus(hService, ServiceManager.ServiceState.StopPending, ServiceManager.ServiceState.Stopped, service_STATUS);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000835C File Offset: 0x0000655C
		private static bool WaitForServiceStatus(IntPtr hService, ServiceManager.ServiceState waitStatus, ServiceManager.ServiceState desiredStatus, ServiceManager.SERVICE_STATUS ssStatus)
		{
			ServiceManager.QueryServiceStatus(hService, ssStatus);
			bool flag = ssStatus.dwCurrentState == desiredStatus;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				int tickCount = Environment.TickCount;
				int dwCheckPoint = ssStatus.dwCheckPoint;
				while (ssStatus.dwCurrentState == waitStatus)
				{
					int num = ssStatus.dwWaitHint / 10;
					bool flag2 = num < 1000;
					if (flag2)
					{
						num = 1000;
					}
					else
					{
						bool flag3 = num > 10000;
						if (flag3)
						{
							num = 10000;
						}
					}
					Thread.Sleep(num);
					bool flag4 = ServiceManager.QueryServiceStatus(hService, ssStatus) == 0;
					if (flag4)
					{
						break;
					}
					bool flag5 = ssStatus.dwCheckPoint > dwCheckPoint;
					if (flag5)
					{
						tickCount = Environment.TickCount;
						dwCheckPoint = ssStatus.dwCheckPoint;
					}
					else
					{
						bool flag6 = Environment.TickCount - tickCount > ssStatus.dwWaitHint;
						if (flag6)
						{
							break;
						}
					}
				}
				result = (ssStatus.dwCurrentState == desiredStatus);
			}
			return result;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000844C File Offset: 0x0000664C
		private static IntPtr OpenSCManager(ServiceManager.ServiceManagerRights rights)
		{
			Logger.Info("{0}", new object[]
			{
				MethodBase.GetCurrentMethod().Name
			});
			IntPtr intPtr = ServiceManager.OpenSCManager(null, null, rights);
			bool flag = intPtr == IntPtr.Zero;
			if (flag)
			{
				throw new Exception("Could not connect to service control manager.");
			}
			return intPtr;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000084A4 File Offset: 0x000066A4
		public static void SetServicePermissions(string serviceName)
		{
			Logger.Info("{0} {1}", new object[]
			{
				MethodBase.GetCurrentMethod().Name,
				serviceName
			});
			using (ServiceController serviceController = new ServiceController(serviceName, "."))
			{
				ServiceControllerStatus status = serviceController.Status;
				byte[] array = new byte[0];
				uint num;
				bool flag = ServiceManager.QueryServiceObjectSecurity(serviceController.ServiceHandle, SecurityInfos.DiscretionaryAcl, array, 0U, out num);
				bool flag2 = !flag;
				if (flag2)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					bool flag3 = lastWin32Error == 122 || lastWin32Error == 0;
					if (!flag3)
					{
						throw new Exception("error calling QueryServiceObjectSecurity() to get DACL : error code=" + lastWin32Error.ToString());
					}
					array = new byte[num];
					flag = ServiceManager.QueryServiceObjectSecurity(serviceController.ServiceHandle, SecurityInfos.DiscretionaryAcl, array, num, out num);
				}
				bool flag4 = !flag;
				if (flag4)
				{
					throw new Exception("error calling QueryServiceObjectSecurity(2) to get DACL : error code=" + Marshal.GetLastWin32Error().ToString());
				}
				RawSecurityDescriptor rawSecurityDescriptor = new RawSecurityDescriptor(array, 0);
				RawAcl discretionaryAcl = rawSecurityDescriptor.DiscretionaryAcl;
				DiscretionaryAcl discretionaryAcl2 = new DiscretionaryAcl(false, false, discretionaryAcl);
				SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.InteractiveSid, null);
				discretionaryAcl2.AddAccess(AccessControlType.Allow, sid, 983551, InheritanceFlags.None, PropagationFlags.None);
				byte[] binaryForm = new byte[discretionaryAcl2.BinaryLength];
				discretionaryAcl2.GetBinaryForm(binaryForm, 0);
				rawSecurityDescriptor.DiscretionaryAcl = new RawAcl(binaryForm, 0);
				byte[] array2 = new byte[rawSecurityDescriptor.BinaryLength];
				rawSecurityDescriptor.GetBinaryForm(array2, 0);
				flag = ServiceManager.SetServiceObjectSecurity(serviceController.ServiceHandle, SecurityInfos.DiscretionaryAcl, array2);
				bool flag5 = !flag;
				if (flag5)
				{
					throw new Exception("error calling SetServiceObjectSecurity(); error code=" + Marshal.GetLastWin32Error().ToString());
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008664 File Offset: 0x00006864
		public static int InstallBstkDrv(string installDir, string driverName = "")
		{
			Logger.Info("InstallService start");
			try
			{
				int num = ServiceManager.InstallPlusDriver(installDir, driverName);
				bool flag = num != 0;
				if (flag)
				{
					return num;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error in installing BstkDrv, Err: " + ex.Message);
			}
			return 0;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000086C8 File Offset: 0x000068C8
		private static int CheckStatusAndReturnResult(int result)
		{
			try
			{
				Logger.Info("Install failed due to: {0}", new object[]
				{
					(InstallerCodes)result
				});
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008710 File Offset: 0x00006910
		private static int InstallPlusDriver(string installDir, string driverName = "")
		{
			Logger.Info("Installing driver");
			ServiceController[] devices = ServiceController.GetDevices();
			string text = Strings.BlueStacksDriverName;
			bool flag = !string.IsNullOrEmpty(driverName);
			if (flag)
			{
				text = driverName;
			}
			string blueStacksDriverDisplayName = Strings.BlueStacksDriverDisplayName;
			string text2 = Path.Combine(installDir, Strings.BlueStacksDriverFileName);
			Logger.Info("Registering driver with params: file path : {0}, DriverName {1}, DisplayName: {2}", new object[]
			{
				text2,
				text,
				blueStacksDriverDisplayName
			});
			bool flag2 = ServiceManager.IsServiceAlreadyExists(devices, text);
			if (flag2)
			{
				string imagePathOfService = ServiceManager.GetImagePathOfService(text);
				bool flag3 = !imagePathOfService.Equals(text2, StringComparison.InvariantCultureIgnoreCase);
				if (flag3)
				{
					Logger.Info("Image path of driver is not same");
					int num = ServiceManager.QueryDriverStatus(text, true);
					bool flag4 = num != 1;
					if (flag4)
					{
						Logger.Info("InstallPlusDriver-> UNABLE_TO_STOP_SERVICE_BEFORE_UNINSTALLING");
						return -59;
					}
					int num2;
					try
					{
						string str = "sc.exe delete " + text;
						using (Process p = new Process())
						{
							p.StartInfo.FileName = "cmd.exe";
							p.StartInfo.Arguments = "/c \"" + str + "\"";
							Countdown countdown = new Countdown(2);
							p.StartInfo.RedirectStandardInput = true;
							p.StartInfo.RedirectStandardOutput = true;
							p.StartInfo.RedirectStandardError = true;
							Logger.Info("Deleting service: {0}", new object[]
							{
								text
							});
							p.OutputDataReceived += delegate(object o, DataReceivedEventArgs e)
							{
								bool flag8 = e.Data != null;
								if (flag8)
								{
									Logger.Info(string.Format("{0} {1}", p.Id, e.Data));
								}
								else
								{
									countdown.Signal();
								}
							};
							p.ErrorDataReceived += delegate(object o, DataReceivedEventArgs e)
							{
								bool flag8 = e.Data != null;
								if (flag8)
								{
									Logger.Error(string.Format("{0} {1}", p.Id, e.Data));
								}
								else
								{
									countdown.Signal();
								}
							};
							Logger.Info("Calling {0} {1}", new object[]
							{
								p.StartInfo.FileName,
								p.StartInfo.Arguments
							});
							p.StartInfo.UseShellExecute = false;
							p.StartInfo.CreateNoWindow = true;
							p.Start();
							p.BeginOutputReadLine();
							p.BeginErrorReadLine();
							p.WaitForExit();
							num2 = p.ExitCode;
							countdown.Wait();
							Logger.Info(string.Format("{0} {1} Exit Code:{2}", p.StartInfo.FileName, p.StartInfo.Arguments, num2));
							bool flag5 = num2 != 0;
							if (flag5)
							{
								return -58;
							}
						}
					}
					catch (Exception ex)
					{
						Logger.Error("Some error while running sc delete. Ex: {0}", new object[]
						{
							ex
						});
					}
					num2 = ServiceManager.CheckForBlueStacksServicesMarkForDeletion(new List<string>
					{
						Strings.BlueStacksDriverName
					});
					Logger.Info(string.Format("CheckForBlueStacksServicesMarkForDeletion Exit Code:{0}", num2));
					bool flag6 = num2 != 0;
					if (flag6)
					{
						return -58;
					}
				}
			}
			bool flag7 = !ServiceManager.InstallDriver(text, text2, blueStacksDriverDisplayName);
			int result;
			if (flag7)
			{
				Logger.Error("Failed to install driver");
				result = -40;
			}
			else
			{
				Logger.Info("Successfully Installed Driver");
				result = 0;
			}
			return result;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008B14 File Offset: 0x00006D14
		public static int QueryDriverStatus(string name, bool isKernelDriverService = false)
		{
			Logger.Info("{0} {1}", new object[]
			{
				MethodBase.GetCurrentMethod().Name,
				name
			});
			IntPtr intPtr = ServiceManager.OpenSCManager(ServiceManager.ServiceManagerRights.Connect);
			IntPtr intPtr2 = IntPtr.Zero;
			int result;
			try
			{
				intPtr2 = ServiceManager.OpenService(intPtr, name, ServiceManager.ServiceRights.QueryStatus | ServiceManager.ServiceRights.Stop);
				bool flag = intPtr2 == IntPtr.Zero;
				if (flag)
				{
					Logger.Info("service handle not created");
					result = -1;
				}
				else
				{
					ServiceManager.SERVICE_STATUS service_STATUS = new ServiceManager.SERVICE_STATUS(isKernelDriverService);
					bool flag2 = ServiceManager.QueryServiceStatus(intPtr2, service_STATUS) != 0;
					if (flag2)
					{
						Logger.Info("current service state is: {0} for service: {1}", new object[]
						{
							service_STATUS.dwCurrentState,
							name
						});
						result = (int)service_STATUS.dwCurrentState;
					}
					else
					{
						Logger.Info("Error in getting service status.." + Marshal.GetLastWin32Error().ToString());
						result = -1;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error in querying driver status err: " + ex.ToString());
				result = -1;
			}
			finally
			{
				ServiceManager.CloseServiceHandle(intPtr2);
				ServiceManager.CloseServiceHandle(intPtr);
			}
			return result;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008C38 File Offset: 0x00006E38
		private static bool InstallDriver(string driverName, string driverPath, string driverDisplayName)
		{
			try
			{
				ServiceManager.StopService(driverName, false);
				ServiceManager.UninstallService(driverName, true);
			}
			catch (Exception ex)
			{
				Logger.Info("Ignore Error, when stopping and uninstalling driver ex : {0}", new object[]
				{
					ex.ToString()
				});
			}
			bool result;
			try
			{
				ServiceManager.InstallKernelDriver(driverName, driverDisplayName, driverPath);
				result = true;
			}
			catch (Exception ex2)
			{
				Logger.Error(string.Format(CultureInfo.InvariantCulture, "Error Occured, Err: {0}", new object[]
				{
					ex2.ToString()
				}));
				result = false;
			}
			return result;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008CD0 File Offset: 0x00006ED0
		private static string GetImagePathOfService(string serviceName)
		{
			RegistryKey registryKey = null;
			string result;
			try
			{
				Logger.Info("In GetImagePathOfService {0}", new object[]
				{
					serviceName
				});
				string path = "System\\CurrentControlSet\\Services";
				string name = Path.Combine(path, serviceName);
				registryKey = Registry.LocalMachine.OpenSubKey(name);
				string text = (string)registryKey.GetValue("ImagePath");
				registryKey.Close();
				result = text;
			}
			catch (Exception ex)
			{
				Logger.Error("Could not get the image path for service {0}, ex: {1}", new object[]
				{
					serviceName,
					ex.ToString()
				});
				result = null;
			}
			finally
			{
				bool flag = registryKey != null;
				if (flag)
				{
					registryKey.Close();
				}
			}
			return result;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00008D88 File Offset: 0x00006F88
		private static bool CheckIfInstalledServicePathAndInstallDirPathMatch(string serviceName, string installDir)
		{
			Logger.Info("Checking file path for {0}", new object[]
			{
				serviceName
			});
			string imagePathOfService = ServiceManager.GetImagePathOfService(serviceName);
			bool flag = string.IsNullOrEmpty(imagePathOfService);
			bool result;
			if (flag)
			{
				Logger.Error("The code checking image path of service returned null");
				result = false;
			}
			else
			{
				string installDirOfService = ServiceManager.GetInstallDirOfService(imagePathOfService);
				bool flag2 = installDirOfService != installDir;
				if (flag2)
				{
					Logger.Error("Service {0} is already installed but at incorrect path {1}, required path is {2}", new object[]
					{
						serviceName,
						installDirOfService,
						installDir
					});
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008E0C File Offset: 0x0000700C
		private static string GetInstallDirOfService(string servicePath)
		{
			int num = servicePath.IndexOf(".sys", StringComparison.OrdinalIgnoreCase);
			bool flag = num != -1;
			string result;
			if (flag)
			{
				string path = servicePath.Substring(4, num);
				string directoryName = Path.GetDirectoryName(path);
				result = directoryName;
			}
			else
			{
				num = servicePath.IndexOf(".exe", StringComparison.OrdinalIgnoreCase);
				bool flag2 = num == -1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					string path2 = servicePath.Substring(0, num + 4).Replace("\"", "");
					string directoryName2 = Path.GetDirectoryName(path2);
					result = directoryName2;
				}
			}
			return result;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008E94 File Offset: 0x00007094
		private static bool IsServiceAlreadyExists(ServiceController[] services, string serviceName)
		{
			Logger.Info("Checking if service {0} exists on user's machine", new object[]
			{
				serviceName
			});
			try
			{
				foreach (ServiceController serviceController in services)
				{
					bool flag = serviceController.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase);
					if (flag)
					{
						Logger.Info("Found service: " + serviceName);
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Exception in checking if service {0} is installed ex: {1}", new object[]
				{
					serviceName,
					ex.ToString()
				});
			}
			return false;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008F38 File Offset: 0x00007138
		public static int CheckForBlueStacksServicesMarkForDeletion(List<string> servicesName)
		{
			bool flag = servicesName != null;
			if (flag)
			{
				foreach (string serviceName in servicesName)
				{
					bool flag2 = ServiceManager.CheckIfServiceHasBeenMarkedForDeletion(serviceName);
					if (flag2)
					{
						return -30;
					}
				}
			}
			return 0;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008FA8 File Offset: 0x000071A8
		private static bool CheckIfServiceHasBeenMarkedForDeletion(string serviceName)
		{
			try
			{
				Logger.Info("checking for marked for deletion flag in service {0}", new object[]
				{
					serviceName
				});
				string path = "system\\CurrentControlSet\\services";
				string name = Path.Combine(path, serviceName);
				int i = 10;
				while (i > 0)
				{
					RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name);
					int num = (int)registryKey.GetValue("DeleteFlag");
					i--;
					Logger.Info("delete flag : " + num.ToString() + " and retry number = " + (10 - i).ToString());
					bool flag = num == 1;
					if (!flag)
					{
						break;
					}
					bool flag2 = i == 0;
					if (flag2)
					{
						Logger.Warning("the  service {0} has been marked for deletion.", new object[]
						{
							serviceName
						});
						return true;
					}
					Thread.Sleep(1000);
				}
			}
			catch (Exception)
			{
				Logger.Info("Could not check for service marked for deletion. should be safe to ignore in most cases.");
			}
			return false;
		}

		// Token: 0x040001E1 RID: 481
		private const int SERVICE_WIN32_OWN_PROCESS = 16;

		// Token: 0x040001E2 RID: 482
		private const int SERVICE_KERNEL_DRIVER = 1;

		// Token: 0x020000D1 RID: 209
		[Flags]
		public enum ServiceManagerRights
		{
			// Token: 0x040007A4 RID: 1956
			Connect = 1,
			// Token: 0x040007A5 RID: 1957
			CreateService = 2,
			// Token: 0x040007A6 RID: 1958
			EnumerateService = 4,
			// Token: 0x040007A7 RID: 1959
			Lock = 8,
			// Token: 0x040007A8 RID: 1960
			QueryLockStatus = 16,
			// Token: 0x040007A9 RID: 1961
			ModifyBootConfig = 32,
			// Token: 0x040007AA RID: 1962
			StandardRightsRequired = 983040,
			// Token: 0x040007AB RID: 1963
			AllAccess = 983103
		}

		// Token: 0x020000D2 RID: 210
		[Flags]
		public enum ServiceRights
		{
			// Token: 0x040007AD RID: 1965
			QueryConfig = 1,
			// Token: 0x040007AE RID: 1966
			ChangeConfig = 2,
			// Token: 0x040007AF RID: 1967
			QueryStatus = 4,
			// Token: 0x040007B0 RID: 1968
			EnumerateDependants = 8,
			// Token: 0x040007B1 RID: 1969
			Start = 16,
			// Token: 0x040007B2 RID: 1970
			Stop = 32,
			// Token: 0x040007B3 RID: 1971
			PauseContinue = 64,
			// Token: 0x040007B4 RID: 1972
			Interrogate = 128,
			// Token: 0x040007B5 RID: 1973
			UserDefinedControl = 256,
			// Token: 0x040007B6 RID: 1974
			Delete = 65536,
			// Token: 0x040007B7 RID: 1975
			StandardRightsRequired = 983040,
			// Token: 0x040007B8 RID: 1976
			ReadControl = 131072,
			// Token: 0x040007B9 RID: 1977
			AllAccess = 983551
		}

		// Token: 0x020000D3 RID: 211
		public enum ServiceBootFlag
		{
			// Token: 0x040007BB RID: 1979
			BootStart,
			// Token: 0x040007BC RID: 1980
			SystemStart,
			// Token: 0x040007BD RID: 1981
			AutoStart,
			// Token: 0x040007BE RID: 1982
			DemandStart,
			// Token: 0x040007BF RID: 1983
			Disabled
		}

		// Token: 0x020000D4 RID: 212
		public enum ServiceControl
		{
			// Token: 0x040007C1 RID: 1985
			Stop = 1,
			// Token: 0x040007C2 RID: 1986
			Pause,
			// Token: 0x040007C3 RID: 1987
			Continue,
			// Token: 0x040007C4 RID: 1988
			Interrogate,
			// Token: 0x040007C5 RID: 1989
			Shutdown,
			// Token: 0x040007C6 RID: 1990
			ParamChange,
			// Token: 0x040007C7 RID: 1991
			NetBindAdd,
			// Token: 0x040007C8 RID: 1992
			NetBindRemove,
			// Token: 0x040007C9 RID: 1993
			NetBindEnable,
			// Token: 0x040007CA RID: 1994
			NetBindDisable
		}

		// Token: 0x020000D5 RID: 213
		public enum ServiceError
		{
			// Token: 0x040007CC RID: 1996
			Ignore,
			// Token: 0x040007CD RID: 1997
			Normal,
			// Token: 0x040007CE RID: 1998
			Severe,
			// Token: 0x040007CF RID: 1999
			Critical
		}

		// Token: 0x020000D6 RID: 214
		public enum ServiceState
		{
			// Token: 0x040007D1 RID: 2001
			Unknown = -1,
			// Token: 0x040007D2 RID: 2002
			NotFound,
			// Token: 0x040007D3 RID: 2003
			Stopped,
			// Token: 0x040007D4 RID: 2004
			StartPending,
			// Token: 0x040007D5 RID: 2005
			StopPending,
			// Token: 0x040007D6 RID: 2006
			Running,
			// Token: 0x040007D7 RID: 2007
			ContinuePending,
			// Token: 0x040007D8 RID: 2008
			PausePending,
			// Token: 0x040007D9 RID: 2009
			Paused
		}

		// Token: 0x020000D7 RID: 215
		[StructLayout(LayoutKind.Sequential)]
		private class SERVICE_STATUS
		{
			// Token: 0x06000441 RID: 1089 RVA: 0x00018E80 File Offset: 0x00017080
			public SERVICE_STATUS(bool isKernelDriver)
			{
				if (isKernelDriver)
				{
					this.dwServiceType = 1;
				}
			}

			// Token: 0x040007DA RID: 2010
			public int dwServiceType = 16;

			// Token: 0x040007DB RID: 2011
			public ServiceManager.ServiceState dwCurrentState = ServiceManager.ServiceState.NotFound;

			// Token: 0x040007DC RID: 2012
			public int dwControlsAccepted = 0;

			// Token: 0x040007DD RID: 2013
			public int dwWin32ExitCode = 0;

			// Token: 0x040007DE RID: 2014
			public int dwServiceSpecificExitCode = 0;

			// Token: 0x040007DF RID: 2015
			public int dwCheckPoint = 0;

			// Token: 0x040007E0 RID: 2016
			public int dwWaitHint = 0;
		}
	}
}
