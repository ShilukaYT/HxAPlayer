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
	// Token: 0x0200020F RID: 527
	public static class ServiceManager
	{
		// Token: 0x06001085 RID: 4229
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr OpenSCManager(string lpMachineName, string lpDatabaseName, ServiceManager.ServiceManagerRights dwDesiredAccess);

		// Token: 0x06001086 RID: 4230
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, ServiceManager.ServiceRights dwDesiredAccess);

		// Token: 0x06001087 RID: 4231
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, ServiceManager.ServiceRights dwDesiredAccess, int dwServiceType, ServiceManager.ServiceBootFlag dwStartType, ServiceManager.ServiceError dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, IntPtr lpdwTagId, string lpDependencies, string lp, string lpPassword);

		// Token: 0x06001088 RID: 4232
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int CloseServiceHandle(IntPtr hSCObject);

		// Token: 0x06001089 RID: 4233
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int QueryServiceStatus(IntPtr hService, ServiceManager.SERVICE_STATUS lpServiceStatus);

		// Token: 0x0600108A RID: 4234
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int DeleteService(IntPtr hService);

		// Token: 0x0600108B RID: 4235
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int ControlService(IntPtr hService, ServiceManager.ServiceControl dwControl, ServiceManager.SERVICE_STATUS lpServiceStatus);

		// Token: 0x0600108C RID: 4236
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int StartService(IntPtr hService, int dwNumServiceArgs, int lpServiceArgVectors);

		// Token: 0x0600108D RID: 4237
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool QueryServiceObjectSecurity(SafeHandle serviceHandle, SecurityInfos secInfo, byte[] lpSecDesrBuf, uint bufSize, out uint bufSizeNeeded);

		// Token: 0x0600108E RID: 4238
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetServiceObjectSecurity(SafeHandle serviceHandle, SecurityInfos secInfos, byte[] lpSecDesrBuf);

		// Token: 0x0600108F RID: 4239 RVA: 0x0003F0A0 File Offset: 0x0003D2A0
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
				if (intPtr2 == IntPtr.Zero)
				{
					Logger.Info("Service " + serviceName + " is not installed or inaccessible.");
					return true;
				}
				try
				{
					ServiceManager.StopService(intPtr2, isKernelDriverService);
					if (ServiceManager.DeleteService(intPtr2) == 0)
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

		// Token: 0x06001090 RID: 4240 RVA: 0x0003F194 File Offset: 0x0003D394
		public static bool ServiceIsInstalled(string serviceName)
		{
			IntPtr intPtr = ServiceManager.OpenSCManager(ServiceManager.ServiceManagerRights.Connect);
			bool result;
			try
			{
				IntPtr intPtr2 = ServiceManager.OpenService(intPtr, serviceName, ServiceManager.ServiceRights.QueryStatus);
				if (intPtr2 == IntPtr.Zero)
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

		// Token: 0x06001091 RID: 4241 RVA: 0x0003F1E8 File Offset: 0x0003D3E8
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
				if (ServiceManager.OpenService(intPtr, serviceName, ServiceManager.ServiceRights.QueryStatus | ServiceManager.ServiceRights.Start) != IntPtr.Zero)
				{
					Logger.Info("service is already installed...uninstalling it first");
					ServiceManager.UninstallService(serviceName, true);
				}
				IntPtr value = ServiceManager.CreateService(intPtr, serviceName, displayName, ServiceManager.ServiceRights.AllAccess, 1, ServiceManager.ServiceBootFlag.AutoStart, ServiceManager.ServiceError.Normal, fileName, null, IntPtr.Zero, null, null, null);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (value == IntPtr.Zero)
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

		// Token: 0x06001092 RID: 4242 RVA: 0x0003F2F8 File Offset: 0x0003D4F8
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
				if (ServiceManager.OpenService(intPtr, serviceName, ServiceManager.ServiceRights.QueryStatus | ServiceManager.ServiceRights.Start) != IntPtr.Zero)
				{
					Logger.Info("service is already installed...uninstalling it first");
					ServiceManager.UninstallService(serviceName, false);
				}
				IntPtr value = ServiceManager.CreateService(intPtr, serviceName, displayName, ServiceManager.ServiceRights.AllAccess, 16, ServiceManager.ServiceBootFlag.AutoStart, ServiceManager.ServiceError.Normal, fileName, null, IntPtr.Zero, null, null, null);
				Logger.Info("Successfully created service = " + serviceName + "...setting DACL now");
				ServiceManager.SetServicePermissions(serviceName);
				Logger.Info("Successfully set DACL");
				if (value == IntPtr.Zero)
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

		// Token: 0x06001093 RID: 4243 RVA: 0x0003F3EC File Offset: 0x0003D5EC
		public static void StartService(string name, bool isKernelDriverService = false)
		{
			IntPtr intPtr = ServiceManager.OpenSCManager(ServiceManager.ServiceManagerRights.Connect);
			try
			{
				IntPtr intPtr2 = ServiceManager.OpenService(intPtr, name, ServiceManager.ServiceRights.QueryStatus | ServiceManager.ServiceRights.Start);
				if (intPtr2 == IntPtr.Zero)
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

		// Token: 0x06001094 RID: 4244 RVA: 0x0003F458 File Offset: 0x0003D658
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
				if (!(intPtr2 == IntPtr.Zero))
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

		// Token: 0x06001095 RID: 4245 RVA: 0x0003F4DC File Offset: 0x0003D6DC
		private static void StartService(IntPtr hService, bool isKernelDriverService = false)
		{
			Logger.Info("{0} {1}", new object[]
			{
				MethodBase.GetCurrentMethod().Name,
				Convert.ToString(hService, CultureInfo.InvariantCulture)
			});
			int num = ServiceManager.StartService(hService, 0, 0);
			if (num == 0)
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

		// Token: 0x06001096 RID: 4246 RVA: 0x0003F560 File Offset: 0x0003D760
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

		// Token: 0x06001097 RID: 4247 RVA: 0x0003F5B8 File Offset: 0x0003D7B8
		private static bool WaitForServiceStatus(IntPtr hService, ServiceManager.ServiceState waitStatus, ServiceManager.ServiceState desiredStatus, ServiceManager.SERVICE_STATUS ssStatus)
		{
			ServiceManager.QueryServiceStatus(hService, ssStatus);
			if (ssStatus.dwCurrentState == desiredStatus)
			{
				return true;
			}
			int tickCount = Environment.TickCount;
			int dwCheckPoint = ssStatus.dwCheckPoint;
			while (ssStatus.dwCurrentState == waitStatus)
			{
				int num = ssStatus.dwWaitHint / 10;
				if (num < 1000)
				{
					num = 1000;
				}
				else if (num > 10000)
				{
					num = 10000;
				}
				Thread.Sleep(num);
				if (ServiceManager.QueryServiceStatus(hService, ssStatus) == 0)
				{
					break;
				}
				if (ssStatus.dwCheckPoint > dwCheckPoint)
				{
					tickCount = Environment.TickCount;
					dwCheckPoint = ssStatus.dwCheckPoint;
				}
				else if (Environment.TickCount - tickCount > ssStatus.dwWaitHint)
				{
					break;
				}
			}
			return ssStatus.dwCurrentState == desiredStatus;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0000E19D File Offset: 0x0000C39D
		private static IntPtr OpenSCManager(ServiceManager.ServiceManagerRights rights)
		{
			Logger.Info("{0}", new object[]
			{
				MethodBase.GetCurrentMethod().Name
			});
			IntPtr intPtr = ServiceManager.OpenSCManager(null, null, rights);
			if (intPtr == IntPtr.Zero)
			{
				throw new Exception("Could not connect to service control manager.");
			}
			return intPtr;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0003F658 File Offset: 0x0003D858
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
				if (!flag)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 122 && lastWin32Error != 0)
					{
						throw new Exception("error calling QueryServiceObjectSecurity() to get DACL : error code=" + lastWin32Error.ToString());
					}
					array = new byte[num];
					flag = ServiceManager.QueryServiceObjectSecurity(serviceController.ServiceHandle, SecurityInfos.DiscretionaryAcl, array, num, out num);
				}
				if (!flag)
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
				if (!ServiceManager.SetServiceObjectSecurity(serviceController.ServiceHandle, SecurityInfos.DiscretionaryAcl, array2))
				{
					throw new Exception("error calling SetServiceObjectSecurity(); error code=" + Marshal.GetLastWin32Error().ToString());
				}
			}
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0003F7DC File Offset: 0x0003D9DC
		public static int InstallBstkDrv(string installDir, string driverName = "")
		{
			Logger.Info("InstallService start");
			try
			{
				int num = ServiceManager.InstallPlusDriver(installDir, driverName);
				if (num != 0)
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

		// Token: 0x0600109B RID: 4251 RVA: 0x0003F834 File Offset: 0x0003DA34
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

		// Token: 0x0600109C RID: 4252 RVA: 0x0003F874 File Offset: 0x0003DA74
		private static int InstallPlusDriver(string installDir, string driverName = "")
		{
			Logger.Info("Installing driver");
			ServiceController[] devices = ServiceController.GetDevices();
			string text = Strings.BlueStacksDriverName;
			if (!string.IsNullOrEmpty(driverName))
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
			if (ServiceManager.IsServiceAlreadyExists(devices, text) && !ServiceManager.GetImagePathOfService(text).Equals(text2, StringComparison.InvariantCultureIgnoreCase))
			{
				Logger.Info("Image path of driver is not same");
				if (ServiceManager.QueryDriverStatus(text, true) != 1)
				{
					Logger.Info("InstallPlusDriver-> UNABLE_TO_STOP_SERVICE_BEFORE_UNINSTALLING");
					return -59;
				}
				int num;
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
							if (e.Data != null)
							{
								Logger.Info(string.Format("{0} {1}", p.Id, e.Data));
								return;
							}
							countdown.Signal();
						};
						p.ErrorDataReceived += delegate(object o, DataReceivedEventArgs e)
						{
							if (e.Data != null)
							{
								Logger.Error(string.Format("{0} {1}", p.Id, e.Data));
								return;
							}
							countdown.Signal();
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
						num = p.ExitCode;
						countdown.Wait();
						Logger.Info(string.Format("{0} {1} Exit Code:{2}", p.StartInfo.FileName, p.StartInfo.Arguments, num));
						if (num != 0)
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
				num = ServiceManager.CheckForBlueStacksServicesMarkForDeletion(new List<string>
				{
					Strings.BlueStacksDriverName
				});
				Logger.Info(string.Format("CheckForBlueStacksServicesMarkForDeletion Exit Code:{0}", num));
				if (num != 0)
				{
					return -58;
				}
			}
			if (!ServiceManager.InstallDriver(text, text2, blueStacksDriverDisplayName))
			{
				Logger.Error("Failed to install driver");
				return -40;
			}
			Logger.Info("Successfully Installed Driver");
			return 0;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0003FB9C File Offset: 0x0003DD9C
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
				if (intPtr2 == IntPtr.Zero)
				{
					Logger.Info("service handle not created");
					result = -1;
				}
				else
				{
					ServiceManager.SERVICE_STATUS service_STATUS = new ServiceManager.SERVICE_STATUS(isKernelDriverService);
					if (ServiceManager.QueryServiceStatus(intPtr2, service_STATUS) != 0)
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

		// Token: 0x0600109E RID: 4254 RVA: 0x0003FCA4 File Offset: 0x0003DEA4
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

		// Token: 0x0600109F RID: 4255 RVA: 0x0003FD30 File Offset: 0x0003DF30
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
				string name = Path.Combine("System\\CurrentControlSet\\Services", serviceName);
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
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return result;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0003FDD4 File Offset: 0x0003DFD4
		private static bool CheckIfInstalledServicePathAndInstallDirPathMatch(string serviceName, string installDir)
		{
			Logger.Info("Checking file path for {0}", new object[]
			{
				serviceName
			});
			string imagePathOfService = ServiceManager.GetImagePathOfService(serviceName);
			if (string.IsNullOrEmpty(imagePathOfService))
			{
				Logger.Error("The code checking image path of service returned null");
				return false;
			}
			string installDirOfService = ServiceManager.GetInstallDirOfService(imagePathOfService);
			if (installDirOfService != installDir)
			{
				Logger.Error("Service {0} is already installed but at incorrect path {1}, required path is {2}", new object[]
				{
					serviceName,
					installDirOfService,
					installDir
				});
				return false;
			}
			return true;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0003FE40 File Offset: 0x0003E040
		private static string GetInstallDirOfService(string servicePath)
		{
			int num = servicePath.IndexOf(".sys", StringComparison.OrdinalIgnoreCase);
			if (num != -1)
			{
				return Path.GetDirectoryName(servicePath.Substring(4, num));
			}
			num = servicePath.IndexOf(".exe", StringComparison.OrdinalIgnoreCase);
			if (num == -1)
			{
				return null;
			}
			return Path.GetDirectoryName(servicePath.Substring(0, num + 4).Replace("\"", ""));
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0003FEA0 File Offset: 0x0003E0A0
		private static bool IsServiceAlreadyExists(ServiceController[] services, string serviceName)
		{
			Logger.Info("Checking if service {0} exists on user's machine", new object[]
			{
				serviceName
			});
			try
			{
				for (int i = 0; i < services.Length; i++)
				{
					if (services[i].ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
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

		// Token: 0x060010A3 RID: 4259 RVA: 0x0003FF2C File Offset: 0x0003E12C
		public static int CheckForBlueStacksServicesMarkForDeletion(List<string> servicesName)
		{
			if (servicesName != null)
			{
				using (List<string>.Enumerator enumerator = servicesName.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (ServiceManager.CheckIfServiceHasBeenMarkedForDeletion(enumerator.Current))
						{
							return -30;
						}
					}
				}
				return 0;
			}
			return 0;
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0003FF84 File Offset: 0x0003E184
		private static bool CheckIfServiceHasBeenMarkedForDeletion(string serviceName)
		{
			try
			{
				Logger.Info("checking for marked for deletion flag in service {0}", new object[]
				{
					serviceName
				});
				string name = Path.Combine("system\\CurrentControlSet\\services", serviceName);
				int i = 10;
				while (i > 0)
				{
					int num = (int)Registry.LocalMachine.OpenSubKey(name).GetValue("DeleteFlag");
					i--;
					Logger.Info("delete flag : " + num.ToString() + " and retry number = " + (10 - i).ToString());
					if (num != 1)
					{
						break;
					}
					if (i == 0)
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

		// Token: 0x04000AE4 RID: 2788
		private const int SERVICE_WIN32_OWN_PROCESS = 16;

		// Token: 0x04000AE5 RID: 2789
		private const int SERVICE_KERNEL_DRIVER = 1;

		// Token: 0x02000210 RID: 528
		[Flags]
		public enum ServiceManagerRights
		{
			// Token: 0x04000AE7 RID: 2791
			Connect = 1,
			// Token: 0x04000AE8 RID: 2792
			CreateService = 2,
			// Token: 0x04000AE9 RID: 2793
			EnumerateService = 4,
			// Token: 0x04000AEA RID: 2794
			Lock = 8,
			// Token: 0x04000AEB RID: 2795
			QueryLockStatus = 16,
			// Token: 0x04000AEC RID: 2796
			ModifyBootConfig = 32,
			// Token: 0x04000AED RID: 2797
			StandardRightsRequired = 983040,
			// Token: 0x04000AEE RID: 2798
			AllAccess = 983103
		}

		// Token: 0x02000211 RID: 529
		[Flags]
		public enum ServiceRights
		{
			// Token: 0x04000AF0 RID: 2800
			QueryConfig = 1,
			// Token: 0x04000AF1 RID: 2801
			ChangeConfig = 2,
			// Token: 0x04000AF2 RID: 2802
			QueryStatus = 4,
			// Token: 0x04000AF3 RID: 2803
			EnumerateDependants = 8,
			// Token: 0x04000AF4 RID: 2804
			Start = 16,
			// Token: 0x04000AF5 RID: 2805
			Stop = 32,
			// Token: 0x04000AF6 RID: 2806
			PauseContinue = 64,
			// Token: 0x04000AF7 RID: 2807
			Interrogate = 128,
			// Token: 0x04000AF8 RID: 2808
			UserDefinedControl = 256,
			// Token: 0x04000AF9 RID: 2809
			Delete = 65536,
			// Token: 0x04000AFA RID: 2810
			StandardRightsRequired = 983040,
			// Token: 0x04000AFB RID: 2811
			ReadControl = 131072,
			// Token: 0x04000AFC RID: 2812
			AllAccess = 983551
		}

		// Token: 0x02000212 RID: 530
		public enum ServiceBootFlag
		{
			// Token: 0x04000AFE RID: 2814
			BootStart,
			// Token: 0x04000AFF RID: 2815
			SystemStart,
			// Token: 0x04000B00 RID: 2816
			AutoStart,
			// Token: 0x04000B01 RID: 2817
			DemandStart,
			// Token: 0x04000B02 RID: 2818
			Disabled
		}

		// Token: 0x02000213 RID: 531
		public enum ServiceControl
		{
			// Token: 0x04000B04 RID: 2820
			Stop = 1,
			// Token: 0x04000B05 RID: 2821
			Pause,
			// Token: 0x04000B06 RID: 2822
			Continue,
			// Token: 0x04000B07 RID: 2823
			Interrogate,
			// Token: 0x04000B08 RID: 2824
			Shutdown,
			// Token: 0x04000B09 RID: 2825
			ParamChange,
			// Token: 0x04000B0A RID: 2826
			NetBindAdd,
			// Token: 0x04000B0B RID: 2827
			NetBindRemove,
			// Token: 0x04000B0C RID: 2828
			NetBindEnable,
			// Token: 0x04000B0D RID: 2829
			NetBindDisable
		}

		// Token: 0x02000214 RID: 532
		public enum ServiceError
		{
			// Token: 0x04000B0F RID: 2831
			Ignore,
			// Token: 0x04000B10 RID: 2832
			Normal,
			// Token: 0x04000B11 RID: 2833
			Severe,
			// Token: 0x04000B12 RID: 2834
			Critical
		}

		// Token: 0x02000215 RID: 533
		public enum ServiceState
		{
			// Token: 0x04000B14 RID: 2836
			Unknown = -1,
			// Token: 0x04000B15 RID: 2837
			NotFound,
			// Token: 0x04000B16 RID: 2838
			Stopped,
			// Token: 0x04000B17 RID: 2839
			StartPending,
			// Token: 0x04000B18 RID: 2840
			StopPending,
			// Token: 0x04000B19 RID: 2841
			Running,
			// Token: 0x04000B1A RID: 2842
			ContinuePending,
			// Token: 0x04000B1B RID: 2843
			PausePending,
			// Token: 0x04000B1C RID: 2844
			Paused
		}

		// Token: 0x02000216 RID: 534
		[StructLayout(LayoutKind.Sequential)]
		private class SERVICE_STATUS
		{
			// Token: 0x060010A5 RID: 4261 RVA: 0x0000E1DC File Offset: 0x0000C3DC
			public SERVICE_STATUS(bool isKernelDriver)
			{
				if (isKernelDriver)
				{
					this.dwServiceType = 1;
				}
			}

			// Token: 0x04000B1D RID: 2845
			public int dwServiceType = 16;

			// Token: 0x04000B1E RID: 2846
			public ServiceManager.ServiceState dwCurrentState;

			// Token: 0x04000B1F RID: 2847
			public int dwControlsAccepted;

			// Token: 0x04000B20 RID: 2848
			public int dwWin32ExitCode;

			// Token: 0x04000B21 RID: 2849
			public int dwServiceSpecificExitCode;

			// Token: 0x04000B22 RID: 2850
			public int dwCheckPoint;

			// Token: 0x04000B23 RID: 2851
			public int dwWaitHint;
		}
	}
}
