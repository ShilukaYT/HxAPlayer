using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Interop;

namespace BlueStacks.Common
{
	// Token: 0x0200023D RID: 573
	public static class InteropWindow
	{
		// Token: 0x06001180 RID: 4480
		[DllImport("user32.dll")]
		public static extern uint MapVirtualKey(uint uCode, uint uMapType);

		// Token: 0x06001181 RID: 4481
		[DllImport("user32.dll")]
		public static extern IntPtr GetKeyboardLayout(uint idThread);

		// Token: 0x06001182 RID: 4482
		[DllImport("user32.dll")]
		public static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

		// Token: 0x06001183 RID: 4483 RVA: 0x0000EC21 File Offset: 0x0000CE21
		public static IntPtr GetHwnd(Popup popup)
		{
			return ((HwndSource)PresentationSource.FromVisual((popup != null) ? popup.Child : null)).Handle;
		}

		// Token: 0x06001184 RID: 4484
		[DllImport("user32.dll")]
		public static extern IntPtr SetFocus(IntPtr hWnd);

		// Token: 0x06001185 RID: 4485
		[DllImport("imm32.dll")]
		public static extern bool ImmSetOpenStatus(IntPtr hIMC, bool open);

		// Token: 0x06001186 RID: 4486
		[DllImport("Imm32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr ImmGetContext(IntPtr hWnd);

		// Token: 0x06001187 RID: 4487
		[DllImport("Imm32.dll")]
		public static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

		// Token: 0x06001188 RID: 4488
		[DllImport("Imm32.dll", CharSet = CharSet.Unicode)]
		private static extern int ImmGetCompositionStringW(IntPtr hIMC, int dwIndex, byte[] lpBuf, int dwBufLen);

		// Token: 0x06001189 RID: 4489
		[DllImport("imm32.dll")]
		public static extern bool ImmSetCompositionWindow(IntPtr hIMC, out COMPOSITIONFORM lpptPos);

		// Token: 0x0600118A RID: 4490
		[DllImport("user32.dll")]
		public static extern int GetSystemMetrics(int which);

		// Token: 0x0600118B RID: 4491
		[DllImport("user32.dll")]
		public static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int w, int h, uint flags);

		// Token: 0x0600118C RID: 4492
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool AdjustWindowRect(out RECT lpRect, int dwStyle, bool bMenu);

		// Token: 0x0600118D RID: 4493
		[DllImport("user32.dll")]
		public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		// Token: 0x0600118E RID: 4494
		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateDC(string driver, string name, string output, IntPtr mode);

		// Token: 0x0600118F RID: 4495
		[DllImport("gdi32.dll")]
		private static extern bool DeleteDC(IntPtr hdc);

		// Token: 0x06001190 RID: 4496
		[DllImport("gdi32.dll")]
		private static extern int GetDeviceCaps(IntPtr hdc, int index);

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0000EC3E File Offset: 0x0000CE3E
		public static int ScreenWidth
		{
			get
			{
				return InteropWindow.GetSystemMetrics(0);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0000EC46 File Offset: 0x0000CE46
		public static int ScreenHeight
		{
			get
			{
				return InteropWindow.GetSystemMetrics(1);
			}
		}

		// Token: 0x06001193 RID: 4499
		[DllImport("user32.dll")]
		public static extern bool HideCaret(IntPtr hWnd);

		// Token: 0x06001194 RID: 4500
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

		// Token: 0x06001195 RID: 4501
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06001196 RID: 4502
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYGAMEPADDATASTRUCT cds);

		// Token: 0x06001197 RID: 4503
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr FindWindow(string cls, string name);

		// Token: 0x06001198 RID: 4504
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetForegroundWindow(IntPtr handle);

		// Token: 0x06001199 RID: 4505
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr handle, int cmd);

		// Token: 0x0600119A RID: 4506
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestorFlags flags);

		// Token: 0x0600119B RID: 4507
		[DllImport("user32.dll")]
		public static extern IntPtr GetParent(IntPtr handle);

		// Token: 0x0600119C RID: 4508
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		// Token: 0x0600119D RID: 4509
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		// Token: 0x0600119E RID: 4510
		[DllImport("kernel32.dll")]
		public static extern bool FreeConsole();

		// Token: 0x0600119F RID: 4511
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		// Token: 0x060011A0 RID: 4512
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWindow(IntPtr hWnd);

		// Token: 0x060011A1 RID: 4513
		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		// Token: 0x060011A2 RID: 4514
		[DllImport("user32.dll")]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint ProcessId);

		// Token: 0x060011A3 RID: 4515
		[DllImport("kernel32.dll")]
		public static extern uint GetCurrentThreadId();

		// Token: 0x060011A4 RID: 4516
		[DllImport("user32.dll")]
		public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

		// Token: 0x060011A5 RID: 4517
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		// Token: 0x060011A6 RID: 4518
		[DllImport("user32.dll")]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		// Token: 0x060011A7 RID: 4519
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		// Token: 0x060011A8 RID: 4520
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x060011A9 RID: 4521
		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hwnd, ref RECT rect);

		// Token: 0x060011AA RID: 4522
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool EnableWindow(IntPtr hwnd, bool enable);

		// Token: 0x060011AB RID: 4523 RVA: 0x0000EC4E File Offset: 0x0000CE4E
		public static IntPtr MinimizeWindow(string name)
		{
			IntPtr intPtr = InteropWindow.FindWindow(null, name);
			if (intPtr == IntPtr.Zero)
			{
				throw new SystemException("Cannot find window '" + name + "'", new Win32Exception(Marshal.GetLastWin32Error()));
			}
			InteropWindow.ShowWindow(intPtr, 6);
			return intPtr;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00041938 File Offset: 0x0003FB38
		public static IntPtr BringWindowToFront(string name, bool _, bool isRetoreMinimizedWindow = false)
		{
			IntPtr intPtr = InteropWindow.FindWindow(null, name);
			if (intPtr == IntPtr.Zero)
			{
				throw new SystemException("Cannot find window '" + name + "'", new Win32Exception(Marshal.GetLastWin32Error()));
			}
			if (!InteropWindow.SetForegroundWindow(intPtr))
			{
				throw new SystemException("Cannot set foreground window", new Win32Exception(Marshal.GetLastWin32Error()));
			}
			if (isRetoreMinimizedWindow)
			{
				InteropWindow.ShowWindow(intPtr, 9);
			}
			else
			{
				InteropWindow.ShowWindow(intPtr, 5);
			}
			return intPtr;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x000419B0 File Offset: 0x0003FBB0
		public static void RemoveWindowFromAltTabUI(IntPtr handle)
		{
			int windowLong = InteropWindow.GetWindowLong(handle, -20);
			InteropWindow.SetWindowLong(handle, -20, windowLong | 128);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0000EC8C File Offset: 0x0000CE8C
		public static IntPtr GetWindowHandle(string name)
		{
			IntPtr intPtr = InteropWindow.FindWindow(null, name);
			if (intPtr == IntPtr.Zero)
			{
				throw new SystemException("Cannot find window '" + name + "'", new Win32Exception(Marshal.GetLastWin32Error()));
			}
			return intPtr;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000419D8 File Offset: 0x0003FBD8
		public static bool ForceSetForegroundWindow(IntPtr h)
		{
			if (h == IntPtr.Zero)
			{
				return false;
			}
			IntPtr foregroundWindow = InteropWindow.GetForegroundWindow();
			if (foregroundWindow == IntPtr.Zero)
			{
				return InteropWindow.SetForegroundWindow(h);
			}
			if (h == foregroundWindow)
			{
				return true;
			}
			uint num = 0U;
			uint windowThreadProcessId = InteropWindow.GetWindowThreadProcessId(foregroundWindow, ref num);
			uint currentThreadId = InteropWindow.GetCurrentThreadId();
			if (currentThreadId == windowThreadProcessId)
			{
				return InteropWindow.SetForegroundWindow(h);
			}
			if (windowThreadProcessId != 0U)
			{
				if (!InteropWindow.AttachThreadInput(currentThreadId, windowThreadProcessId, true))
				{
					return false;
				}
				if (!InteropWindow.SetForegroundWindow(h))
				{
					InteropWindow.AttachThreadInput(currentThreadId, windowThreadProcessId, false);
					return false;
				}
				InteropWindow.AttachThreadInput(currentThreadId, windowThreadProcessId, false);
			}
			return true;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00041A64 File Offset: 0x0003FC64
		public static int GetScreenDpi()
		{
			IntPtr intPtr = InteropWindow.CreateDC("DISPLAY", null, null, IntPtr.Zero);
			if (intPtr == IntPtr.Zero)
			{
				return -1;
			}
			int num = InteropWindow.GetDeviceCaps(intPtr, 88);
			if (num == 0)
			{
				num = 96;
			}
			InteropWindow.DeleteDC(intPtr);
			return num;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0000ECC2 File Offset: 0x0000CEC2
		public static void SetFullScreen(IntPtr hwnd)
		{
			InteropWindow.SetFullScreen(hwnd, 0, 0, InteropWindow.ScreenWidth, InteropWindow.ScreenHeight);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0000ECD6 File Offset: 0x0000CED6
		public static void SetFullScreen(IntPtr hwnd, int X, int Y, int cx, int cy)
		{
			if (!InteropWindow.SetWindowPos(hwnd, InteropWindow.HWND_TOP, X, Y, cx, cy, 64U))
			{
				throw new SystemException("Cannot call SetWindowPos()", new Win32Exception(Marshal.GetLastWin32Error()));
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00041AAC File Offset: 0x0003FCAC
		public static string CurrentCompStr(IntPtr handle)
		{
			int dwIndex = 8;
			IntPtr hIMC = InteropWindow.ImmGetContext(handle);
			string result;
			try
			{
				int num = InteropWindow.ImmGetCompositionStringW(hIMC, dwIndex, null, 0);
				if (num > 0)
				{
					byte[] array = new byte[num];
					InteropWindow.ImmGetCompositionStringW(hIMC, dwIndex, array, num);
					result = Encoding.Unicode.GetString(array);
				}
				else
				{
					result = string.Empty;
				}
			}
			finally
			{
				InteropWindow.ImmReleaseContext(handle, hIMC);
			}
			return result;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00041B14 File Offset: 0x0003FD14
		public static IntPtr GetWindowHandle(Window window)
		{
			HwndSource hwndSource = (HwndSource)PresentationSource.FromVisual(window);
			if (hwndSource != null)
			{
				return hwndSource.Handle;
			}
			return IntPtr.Zero;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0000ED01 File Offset: 0x0000CF01
		public static bool IsWindowTopMost(IntPtr hWnd)
		{
			return (InteropWindow.GetWindowLong(hWnd, -20) & 8) == 8;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0000ED10 File Offset: 0x0000CF10
		public static Window GetTopmostOwnerWindow(Window window)
		{
			while (window.Owner != null)
			{
				window = window.Owner;
			}
			return window;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00041B3C File Offset: 0x0003FD3C
		public static int GetAForegroundApplicationProcessId()
		{
			IntPtr foregroundWindow = InteropWindow.GetForegroundWindow();
			if (foregroundWindow == IntPtr.Zero)
			{
				return 0;
			}
			uint result = 0U;
			InteropWindow.GetWindowThreadProcessId(foregroundWindow, ref result);
			return (int)result;
		}

		// Token: 0x060011B8 RID: 4536
		[DllImport("user32.dll")]
		private static extern long GetKeyboardLayoutName(StringBuilder pwszKLID);

		// Token: 0x060011B9 RID: 4537 RVA: 0x0000ED25 File Offset: 0x0000CF25
		private static string GetLayoutCode()
		{
			StringBuilder stringBuilder = new StringBuilder(9);
			InteropWindow.GetKeyboardLayoutName(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00041B6C File Offset: 0x0003FD6C
		public static string MapLayoutName(string code = null)
		{
			if (code == null)
			{
				code = InteropWindow.GetLayoutCode();
			}
			if (code != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(code);
				if (num <= 2706750745U)
				{
					if (num <= 1667082820U)
					{
						if (num <= 712032952U)
						{
							if (num <= 231296871U)
							{
								if (num <= 99068648U)
								{
									if (num <= 15474743U)
									{
										if (num != 3522856U)
										{
											if (num == 15474743U)
											{
												if (code == "00010418")
												{
													return "Romanian (standard)";
												}
											}
										}
										else if (code == "00000C04")
										{
											return "Chinese (traditional, hong kong s.a.r.) - us keyboard";
										}
									}
									else if (num != 50162814U)
									{
										if (num != 65513410U)
										{
											if (num == 99068648U)
											{
												if (code == "00010437")
												{
													return "Georgian (qwerty)";
												}
											}
										}
										else if (code == "00010439")
										{
											return "Hindi traditional";
										}
									}
									else if (code == "00010480")
									{
										return "Uyghur";
									}
								}
								else if (num <= 110845946U)
								{
									if (num != 101545860U)
									{
										if (num == 110845946U)
										{
											if (code == "00050408")
											{
												return "Greek latin";
											}
										}
									}
									else if (code == "0000080C")
									{
										return "Belgian French";
									}
								}
								else if (num != 127623565U)
								{
									if (num != 135101098U)
									{
										if (num == 231296871U)
										{
											if (code == "00001809")
											{
												return "Irish";
											}
										}
									}
									else if (code == "0000080A")
									{
										return "Latin america";
									}
								}
								else if (code == "00050409")
								{
									return "United States - dvorak right hand";
								}
							}
							else if (num <= 503517168U)
							{
								if (num <= 389408093U)
								{
									if (num != 240518203U)
									{
										if (num == 389408093U)
										{
											if (code == "00000C0C")
											{
												return "Canada French (legacy)";
											}
										}
									}
									else if (code == "0001083B")
									{
										return "Finnish with sami";
									}
								}
								else if (num != 453037216U)
								{
									if (num != 471089250U)
									{
										if (num == 503517168U)
										{
											if (code == "00000843")
											{
												return "Uzbek cyrillic";
											}
										}
									}
									else if (code == "0002083B")
									{
										return "Sami extended finland-sweden";
									}
								}
								else if (code == "00000850")
								{
									return "Mongolian (mongolian script)";
								}
							}
							else if (num <= 631482070U)
							{
								if (num != 509862881U)
								{
									if (num == 631482070U)
									{
										if (code == "00004009")
										{
											return "United States - india";
										}
									}
								}
								else if (code == "0002041E")
								{
									return "Thai Kedmanee (non-shiftlock)";
								}
							}
							else if (num != 694269595U)
							{
								if (num != 694416690U)
								{
									if (num == 712032952U)
									{
										if (code == "00020445")
										{
											return "Bengali - inscript";
										}
									}
								}
								else if (code == "00020418")
								{
									return "Romanian (programmers)";
								}
							}
							else if (code == "00020422")
							{
								return "Ukrainian (enhanced)";
							}
						}
						else if (num <= 1465751392U)
						{
							if (num <= 1004864568U)
							{
								if (num <= 744602452U)
								{
									if (num != 744455357U)
									{
										if (num == 744602452U)
										{
											if (code == "00020427")
											{
												return "Lithuanian standard";
											}
										}
									}
									else if (code == "00020437")
									{
										return "Georgian (ergonomic)";
									}
								}
								else if (num != 803790914U)
								{
									if (num != 959994234U)
									{
										if (num == 1004864568U)
										{
											if (code == "00011009")
											{
												return "Canada Multilingual";
											}
										}
									}
									else if (code == "00000C1A")
									{
										return "Serbian (cyrillic)";
									}
								}
								else if (code == "0003041E")
								{
									return "Thai Pattachote (non-shiftlock)";
								}
							}
							else if (num <= 1163000852U)
							{
								if (num != 1117879927U)
								{
									if (num == 1163000852U)
									{
										if (code == "00001404")
										{
											return "Chinese (traditional, macao s.a.r.) - us keyboard";
										}
									}
								}
								else if (code == "00060408")
								{
									return "Greek polyonic";
								}
							}
							else if (num != 1335701245U)
							{
								if (num != 1432049059U)
								{
									if (num == 1465751392U)
									{
										if (code == "00000429")
										{
											return "Persian";
										}
									}
								}
								else if (code == "00000437")
								{
									return "Georgian";
								}
							}
							else if (code == "0000100C")
							{
								return "Swiss french";
							}
						}
						else if (num <= 1616749963U)
						{
							if (num <= 1482529011U)
							{
								if (num != 1482381916U)
								{
									if (num == 1482529011U)
									{
										if (code == "00000428")
										{
											return "Tajik";
										}
									}
								}
								else if (code == "00000432")
								{
									return "Setswana";
								}
							}
							else if (num != 1483703617U)
							{
								if (num != 1550625225U)
								{
									if (num == 1616749963U)
									{
										if (code == "00000420")
										{
											return "Urdu";
										}
									}
								}
								else if (code == "00000488")
								{
									return "Wolof";
								}
							}
							else if (code == "0000201A")
							{
								return "Bosnian (cyrillic)";
							}
						}
						else if (num <= 1650305201U)
						{
							if (num != 1633527582U)
							{
								if (num != 1650158106U)
								{
									if (num == 1650305201U)
									{
										if (code == "00000422")
										{
											return "Ukrainian";
										}
									}
								}
								else if (code == "00000438")
								{
									return "Faeroese";
								}
							}
							else if (code == "00000423")
							{
								return "Belarusian";
							}
						}
						else if (num != 1650452296U)
						{
							if (num != 1666935725U)
							{
								if (num == 1667082820U)
								{
									if (code == "00000425")
									{
										return "Estonian";
									}
								}
							}
							else if (code == "00000439")
							{
								return "Deanagari - inscript";
							}
						}
						else if (code == "00000454")
						{
							return "Lao";
						}
					}
					else if (num <= 1801156677U)
					{
						if (num <= 1717562772U)
						{
							if (num <= 1684154629U)
							{
								if (num <= 1668068558U)
								{
									if (num != 1667377010U)
									{
										if (num == 1668068558U)
										{
											if (code == "00000481")
											{
												return "Maroi";
											}
										}
									}
									else if (code == "00000449")
									{
										return "Tamil";
									}
								}
								else if (num != 1683713344U)
								{
									if (num != 1683860439U)
									{
										if (num == 1684154629U)
										{
											if (code == "00000448")
											{
												return "Oriya";
											}
										}
									}
									else if (code == "00000424")
									{
										return "Slovenian";
									}
								}
								else if (code == "0000043F")
								{
									return "Kazakh";
								}
							}
							else if (num <= 1700638058U)
							{
								if (num != 1684846177U)
								{
									if (num == 1700638058U)
									{
										if (code == "00000427")
										{
											return "Lithuanian ibm";
										}
									}
								}
								else if (code == "00000480")
								{
									return "Uyghur (legacy)";
								}
							}
							else if (num != 1700932248U)
							{
								if (num != 1717415677U)
								{
									if (num == 1717562772U)
									{
										if (code == "00000450")
										{
											return "Mongolian cyrillic";
										}
									}
								}
								else if (code == "00000426")
								{
									return "Latvian";
								}
							}
							else if (code == "00000447")
							{
								return "Gujarati";
							}
						}
						else if (num <= 1750823820U)
						{
							if (num <= 1734340391U)
							{
								if (num != 1717709867U)
								{
									if (num == 1734340391U)
									{
										if (code == "00000451")
										{
											return "Tibetan (prc)";
										}
									}
								}
								else if (code == "00000446")
								{
									return "Punjabi";
								}
							}
							else if (num != 1734487486U)
							{
								if (num != 1735179034U)
								{
									if (num == 1750823820U)
									{
										if (code == "0000043B")
										{
											return "Norwegian with sami";
										}
									}
								}
								else if (code == "00000485")
								{
									return "Yakut";
								}
							}
							else if (code == "00000445")
							{
								return "Bengali";
							}
						}
						else if (num <= 1751265105U)
						{
							if (num != 1751118010U)
							{
								if (num == 1751265105U)
								{
									if (code == "00000444")
									{
										return "Tatar";
									}
								}
							}
							else if (code == "00000452")
							{
								return "United Kingdom Extended";
							}
						}
						else if (num != 1767895629U)
						{
							if (num != 1784820343U)
							{
								if (num == 1801156677U)
								{
									if (code == "0000043A")
									{
										return "Maltese 47-key";
									}
								}
							}
							else if (code == "00000442")
							{
								return "Turkmen";
							}
						}
						else if (code == "00000453")
						{
							return "Khmer";
						}
					}
					else if (num <= 2472408532U)
					{
						if (num <= 2229575998U)
						{
							if (num <= 1973250767U)
							{
								if (num != 1818375581U)
								{
									if (num == 1973250767U)
									{
										if (code == "00001009")
										{
											return "Canada French";
										}
									}
								}
								else if (code == "00000440")
								{
									return "Kyrgyz cyrillic";
								}
							}
							else if (num != 2011172761U)
							{
								if (num != 2023583624U)
								{
									if (num == 2229575998U)
									{
										if (code == "00010426")
										{
											return "Latvian (qwerty)";
										}
									}
								}
								else if (code == "00001004")
								{
									return "Chinese (simplified, singapore) - us keyboard";
								}
							}
							else if (code == "00010445")
							{
								return "Bengali - inscript (legacy)";
							}
						}
						else if (num <= 2405298056U)
						{
							if (num != 2246353617U)
							{
								if (num == 2405298056U)
								{
									if (code == "0000042A")
									{
										return "Vietnamese";
									}
								}
							}
							else if (code == "00010427")
							{
								return "Lithuanian";
							}
						}
						else if (num != 2438853294U)
						{
							if (num != 2455630913U)
							{
								if (num == 2472408532U)
								{
									if (code == "0000042E")
									{
										return "Sorbian standard (legacy)";
									}
								}
							}
							else if (code == "0000042B")
							{
								return "Armenian eastern";
							}
						}
						else if (code == "0000042C")
						{
							return "Azeri Latin";
						}
					}
					else if (num <= 2598830711U)
					{
						if (num <= 2523324256U)
						{
							if (num != 2522741389U)
							{
								if (num == 2523324256U)
								{
									if (code == "00020401")
									{
										return "Arabic (102) Azerty";
									}
								}
							}
							else if (code == "0000042F")
							{
								return "Macedonian (fyrom)";
							}
						}
						else if (num != 2573657113U)
						{
							if (num != 2590434732U)
							{
								if (num == 2598830711U)
								{
									if (code == "0001043B")
									{
										return "Sami extended norway";
									}
								}
							}
							else if (code == "00020405")
							{
								return "Czech programmers";
							}
						}
						else if (code == "00020402")
						{
							return "Bulgarian (phonetic)";
						}
					}
					else if (num <= 2673195507U)
					{
						if (num != 2615608330U)
						{
							if (num != 2657545208U)
							{
								if (num == 2673195507U)
								{
									if (code == "0000041A")
									{
										return "Croatian";
									}
								}
							}
							else if (code == "00020409")
							{
								return "United States - international";
							}
						}
						else if (code == "0001043A")
						{
							return "Maltese 48-key";
						}
					}
					else if (num != 2674322827U)
					{
						if (num != 2689973126U)
						{
							if (num == 2706750745U)
							{
								if (code == "0000041C")
								{
									return "Albanian";
								}
							}
						}
						else if (code == "0000041B")
						{
							return "Slovak";
						}
					}
					else if (code == "00020408")
					{
						return "Greek (319)";
					}
				}
				else if (num <= 3546764528U)
				{
					if (num <= 3219517787U)
					{
						if (num <= 2953637922U)
						{
							if (num <= 2758216435U)
							{
								if (num <= 2740305983U)
								{
									if (num != 2723528364U)
									{
										if (num == 2740305983U)
										{
											if (code == "0000041E")
											{
												return "Thai Kedmanee";
											}
										}
									}
									else if (code == "0000041D")
									{
										return "Swedish";
									}
								}
								else if (num != 2741438816U)
								{
									if (num != 2757083602U)
									{
										if (num == 2758216435U)
										{
											if (code == "0000046D")
											{
												return "Bashkir";
											}
										}
									}
									else if (code == "0000041F")
									{
										return "Turkish Q";
									}
								}
								else if (code == "0000046E")
								{
									return "Luxembourgish";
								}
							}
							else if (num <= 2808549292U)
							{
								if (num != 2791771673U)
								{
									if (num == 2808549292U)
									{
										if (code == "0000046A")
										{
											return "Yoruba";
										}
									}
								}
								else if (code == "0000046F")
								{
									return "Greenlandic";
								}
							}
							else if (num != 2842104530U)
							{
								if (num != 2903452160U)
								{
									if (num == 2953637922U)
									{
										if (code == "00000809")
										{
											return "United Kingdom";
										}
									}
								}
								else if (code == "0000083B")
								{
									return "Swedish with sami";
								}
							}
							else if (code == "0000046C")
							{
								return "Sesotho sa Leboa";
							}
						}
						else if (num <= 3054156541U)
						{
							if (num <= 2987193160U)
							{
								if (num != 2970268446U)
								{
									if (num == 2987193160U)
									{
										if (code == "00000807")
										{
											return "Swiss german";
										}
									}
								}
								else if (code == "00000816")
								{
									return "Portuguese";
								}
							}
							else if (num != 3037526017U)
							{
								if (num != 3042527671U)
								{
									if (num == 3054156541U)
									{
										if (code == "00000813")
										{
											return "Belgian (period)";
										}
									}
								}
								else if (code == "0001080C")
								{
									return "Belgian (comma)";
								}
							}
							else if (code == "00000804")
							{
								return "Chinese (simplified) -us keyboard";
							}
						}
						else if (num <= 3093327530U)
						{
							if (num != 3076549911U)
							{
								if (num == 3093327530U)
								{
									if (code == "0000045B")
									{
										return "Sinhala";
									}
								}
							}
							else if (code == "0000045A")
							{
								return "Syriac";
							}
						}
						else if (num != 3102074454U)
						{
							if (num != 3202740168U)
							{
								if (num == 3219517787U)
								{
									if (code == "00030409")
									{
										return "United States - dvorak left hand";
									}
								}
							}
							else if (code == "00030408")
							{
								return "Greek (220) latin";
							}
						}
						else if (code == "00030402")
						{
							return "Bulgarian";
						}
					}
					else if (num <= 3472905468U)
					{
						if (num <= 3327669743U)
						{
							if (num <= 3260559267U)
							{
								if (num != 3243781648U)
								{
									if (num == 3260559267U)
									{
										if (code == "0000040B")
										{
											return "Finnish";
										}
									}
								}
								else if (code == "0000040C")
								{
									return "French";
								}
							}
							else if (num != 3277336886U)
							{
								if (num != 3286860185U)
								{
									if (num == 3327669743U)
									{
										if (code == "0000040F")
										{
											return "Icelandic";
										}
									}
								}
								else if (code == "0001040A")
								{
									return "Spanish variation";
								}
							}
							else if (code == "0000040A")
							{
								return "Spanish";
							}
						}
						else if (num <= 3353970661U)
						{
							if (num != 3344447362U)
							{
								if (num == 3353970661U)
								{
									if (code == "0001040E")
									{
										return "Hungarian 101 key";
									}
								}
							}
							else if (code == "0000040E")
							{
								return "Hungarian";
							}
						}
						else if (num != 3361224981U)
						{
							if (num != 3461743600U)
							{
								if (num == 3472905468U)
								{
									if (code == "0000085D")
									{
										return "Inuktitut - latin";
									}
								}
							}
							else if (code == "00000410")
							{
								return "Italian";
							}
						}
						else if (code == "0000040D")
						{
							return "Hebrew";
						}
					}
					else if (num <= 3528854076U)
					{
						if (num <= 3487750328U)
						{
							if (num != 3478521219U)
							{
								if (num == 3487750328U)
								{
									if (code == "0001045A")
									{
										return "Syriac phonetic";
									}
								}
							}
							else if (code == "00000411")
							{
								return "Japanese";
							}
						}
						else if (num != 3495298838U)
						{
							if (num != 3512076457U)
							{
								if (num == 3528854076U)
								{
									if (code == "00000414")
									{
										return "Norwegian";
									}
								}
							}
							else if (code == "00000413")
							{
								return "Dutch";
							}
						}
						else if (code == "00000412")
						{
							return "Korean";
						}
					}
					else if (num <= 3538083185U)
					{
						if (num != 3529001171U)
						{
							if (num != 3529839814U)
							{
								if (num == 3538083185U)
								{
									if (code == "0001045B")
									{
										return "Sinhala -Wij 9";
									}
								}
							}
							else if (code == "00000470")
							{
								return "Igbo";
							}
						}
						else if (code == "00000402")
						{
							return "Bulgarian(typewriter)";
						}
					}
					else if (num != 3545631695U)
					{
						if (num != 3545778790U)
						{
							if (num == 3546764528U)
							{
								if (code == "00000465")
								{
									return "Divehi phonetic";
								}
							}
						}
						else if (code == "00000401")
						{
							return "Arabic (101)";
						}
					}
					else if (code == "00000415")
					{
						return "Polish (programmers)";
					}
				}
				else if (num <= 3764873575U)
				{
					if (num <= 3613580814U)
					{
						if (num <= 3591481634U)
						{
							if (num <= 3571638423U)
							{
								if (num != 3562409314U)
								{
									if (num == 3571638423U)
									{
										if (code == "0001045D")
										{
											return "Inuktitut - naqittaut";
										}
									}
								}
								else if (code == "00000416")
								{
									return "Portuguese (brazillian abnt)";
								}
							}
							else if (num != 3573443440U)
							{
								if (num != 3579334028U)
								{
									if (num == 3591481634U)
									{
										if (code == "0000082C")
										{
											return "Azeri Cyrillic";
										}
									}
								}
								else if (code == "00000407")
								{
									return "German";
								}
							}
							else if (code == "00011809")
							{
								return "Gaelic";
							}
						}
						else if (num <= 3596111647U)
						{
							if (num != 3595964552U)
							{
								if (num == 3596111647U)
								{
									if (code == "00000406")
									{
										return "Danish";
									}
								}
							}
							else if (code == "00000418")
							{
								return "Romanian (legacy)";
							}
						}
						else if (num != 3612742171U)
						{
							if (num != 3612889266U)
							{
								if (num == 3613580814U)
								{
									if (code == "0000044E")
									{
										return "Marathi";
									}
								}
							}
							else if (code == "00000405")
							{
								return "Czech";
							}
						}
						else if (code == "00000419")
						{
							return "Russian";
						}
					}
					else if (num <= 3647430242U)
					{
						if (num <= 3629666885U)
						{
							if (num != 3613875004U)
							{
								if (num == 3629666885U)
								{
									if (code == "00000404")
									{
										return "Chinese (traditional) - us keyboard";
									}
								}
							}
							else if (code == "00000461")
							{
								return "Nepali";
							}
						}
						else if (num != 3630358433U)
						{
							if (num != 3647136052U)
							{
								if (num == 3647430242U)
								{
									if (code == "00000463")
									{
										return "Pashto (afghanistan)";
									}
								}
							}
							else if (code == "0000044C")
							{
								return "Malayalam";
							}
						}
						else if (code == "0000044D")
						{
							return "Assamese - inscript";
						}
					}
					else if (num <= 3679999742U)
					{
						if (num != 3663913671U)
						{
							if (num == 3679999742U)
							{
								if (code == "00000409")
								{
									return "United States";
								}
							}
						}
						else if (code == "0000044B")
						{
							return "Kannada";
						}
					}
					else if (num != 3680691290U)
					{
						if (num != 3696777361U)
						{
							if (num == 3764873575U)
							{
								if (code == "00000468")
								{
									return "Hausa";
								}
							}
						}
						else if (code == "00000408")
						{
							return "Greek";
						}
					}
					else if (code == "0000044A")
					{
						return "Telugu";
					}
				}
				else if (num <= 4101573375U)
				{
					if (num <= 4041853040U)
					{
						if (num <= 3825927015U)
						{
							if (num != 3823891088U)
							{
								if (num == 3825927015U)
								{
									if (code == "0000081A")
									{
										return "Serbian (latin)";
									}
								}
							}
							else if (code == "0001041E")
							{
								return "Thai Pattachote";
							}
						}
						else if (num != 3874223945U)
						{
							if (num != 3941334421U)
							{
								if (num == 4041853040U)
								{
									if (code == "00010402")
									{
										return "Bulgarian (latin)";
									}
								}
							}
							else if (code == "0001041B")
							{
								return "Slovak (qwerty)";
							}
						}
						else if (code == "0001041F")
						{
							return "Turkish F";
						}
					}
					else if (num <= 4091891707U)
					{
						if (num != 4091200159U)
						{
							if (num == 4091891707U)
							{
								if (code == "0001042E")
								{
									return "Sorbian extended";
								}
							}
						}
						else if (code == "00010465")
						{
							return "Divehi typewriter";
						}
					}
					else if (num != 4092185897U)
					{
						if (num != 4092332992U)
						{
							if (num == 4101573375U)
							{
								if (code == "00040402")
								{
									return "Bulgarian (phonetic traditional)";
								}
							}
						}
						else if (code == "00010415")
						{
							return "Polish (214)";
						}
					}
					else if (code == "00010401")
					{
						return "Arabic (102)";
					}
				}
				else if (num <= 4175779802U)
				{
					if (num <= 4125741135U)
					{
						if (num != 4108669326U)
						{
							if (num == 4125741135U)
							{
								if (code == "00010407")
								{
									return "German (ibm)";
								}
							}
						}
						else if (code == "0001042F")
						{
							return "Macedonian (fyrom) - standard";
						}
					}
					else if (num != 4142665849U)
					{
						if (num != 4159296373U)
						{
							if (num == 4175779802U)
							{
								if (code == "0001042B")
								{
									return "Armenian Western";
								}
							}
						}
						else if (code == "00010405")
						{
							return "Czech (qwerty)";
						}
					}
					else if (code == "00010416")
					{
						return "Portuguese (brazillian abnt2)";
					}
				}
				else if (num <= 4226406849U)
				{
					if (num != 4176221087U)
					{
						if (num != 4209629230U)
						{
							if (num == 4226406849U)
							{
								if (code == "00010409")
								{
									return "United States - dvorak";
								}
							}
						}
						else if (code == "00010408")
						{
							return "Greek (220)";
						}
					}
					else if (code == "00010410")
					{
						return "Italian (142)";
					}
				}
				else if (num != 4267799274U)
				{
					if (num != 4269349565U)
					{
						if (num == 4293664420U)
						{
							if (code == "00010419")
							{
								return "Russian (typewriter)";
							}
						}
					}
					else if (code == "00040408")
					{
						return "Greek (319) latin";
					}
				}
				else if (code == "0002042E")
				{
					return "Sorbian standard";
				}
			}
			return code;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0000ED3A File Offset: 0x0000CF3A
		public static WindowState FindMainWindowState(Window window)
		{
			if (((window != null) ? window.Owner : null) == null)
			{
				return window.WindowState;
			}
			return InteropWindow.FindMainWindowState(window.Owner);
		}

		// Token: 0x060011BC RID: 4540
		[DllImport("User32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
		public static extern IntPtr LoadCursorFromFile(string str);

		// Token: 0x060011BD RID: 4541
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

		// Token: 0x060011BE RID: 4542
		[DllImport("user32.dll")]
		public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

		// Token: 0x060011BF RID: 4543 RVA: 0x00043A54 File Offset: 0x00041C54
		public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot, float scalingFactor)
		{
			int num = (int)(32f * scalingFactor);
			int num2 = (int)(32f * scalingFactor);
			Cursor result;
			using (Bitmap bitmap = new Bitmap(bmp, num, num2))
			{
				IntPtr hicon = bitmap.GetHicon();
				IconInfo iconInfo = default(IconInfo);
				InteropWindow.GetIconInfo(hicon, ref iconInfo);
				iconInfo.xHotspot = (int)((float)xHotSpot * scalingFactor * ((float)num / (float)((bmp != null) ? new int?(bmp.Width) : null).Value));
				iconInfo.yHotspot = (int)((float)yHotSpot * scalingFactor * ((float)num2 / (float)((bmp != null) ? new int?(bmp.Height) : null).Value));
				iconInfo.fIcon = false;
				result = new Cursor(InteropWindow.CreateIconIndirect(ref iconInfo));
			}
			return result;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00043B30 File Offset: 0x00041D30
		public static Cursor LoadCustomCursor(string path)
		{
			IntPtr intPtr = InteropWindow.LoadCursorFromFile(path);
			if (intPtr == IntPtr.Zero)
			{
				throw new Win32Exception();
			}
			Cursor cursor = new Cursor(intPtr);
			typeof(Cursor).GetField("ownHandle", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(cursor, true);
			return cursor;
		}

		// Token: 0x04000DB4 RID: 3508
		public const int WM_CREATE = 1;

		// Token: 0x04000DB5 RID: 3509
		public const int WM_CLOSE = 16;

		// Token: 0x04000DB6 RID: 3510
		public const int WM_INPUT = 255;

		// Token: 0x04000DB7 RID: 3511
		public const int WM_USER = 1024;

		// Token: 0x04000DB8 RID: 3512
		public const int WM_USER_SHOW_WINDOW = 1025;

		// Token: 0x04000DB9 RID: 3513
		public const int WM_USER_SWITCH_TO_LAUNCHER = 1026;

		// Token: 0x04000DBA RID: 3514
		public const int WM_USER_RESIZE_WINDOW = 1027;

		// Token: 0x04000DBB RID: 3515
		public const int WM_USER_FE_STATE_CHANGE = 1028;

		// Token: 0x04000DBC RID: 3516
		public const int WM_USER_FE_APP_DISPLAYED = 1029;

		// Token: 0x04000DBD RID: 3517
		public const int WM_USER_FE_ORIENTATION_CHANGE = 1030;

		// Token: 0x04000DBE RID: 3518
		public const int WM_USER_FE_RESIZE = 1031;

		// Token: 0x04000DBF RID: 3519
		public const int WM_USER_INSTALL_COMPLETED = 1032;

		// Token: 0x04000DC0 RID: 3520
		public const int WM_USER_UNINSTALL_COMPLETED = 1033;

		// Token: 0x04000DC1 RID: 3521
		public const int WM_USER_APP_CRASHED = 1034;

		// Token: 0x04000DC2 RID: 3522
		public const int WM_USER_EXE_CRASHED = 1035;

		// Token: 0x04000DC3 RID: 3523
		public const int WM_USER_UPGRADE_FAILED = 1036;

		// Token: 0x04000DC4 RID: 3524
		public const int WM_USER_BOOT_FAILURE = 1037;

		// Token: 0x04000DC5 RID: 3525
		public const int WM_USER_FE_SHOOTMODE_STATE = 1038;

		// Token: 0x04000DC6 RID: 3526
		public const int WM_USER_TOGGLE_FULLSCREEN = 1056;

		// Token: 0x04000DC7 RID: 3527
		public const int WM_USER_GO_BACK = 1057;

		// Token: 0x04000DC8 RID: 3528
		public const int WM_USER_SHOW_GUIDANCE = 1058;

		// Token: 0x04000DC9 RID: 3529
		public const int WM_USER_AUDIO_MUTE = 1059;

		// Token: 0x04000DCA RID: 3530
		public const int WM_USER_AUDIO_UNMUTE = 1060;

		// Token: 0x04000DCB RID: 3531
		public const int WM_USER_AT_HOME = 1061;

		// Token: 0x04000DCC RID: 3532
		public const int WM_USER_ACTIVATE = 1062;

		// Token: 0x04000DCD RID: 3533
		public const int WM_USER_HIDE_WINDOW = 1063;

		// Token: 0x04000DCE RID: 3534
		public const int WM_USER_VMX_BIT_ON = 1064;

		// Token: 0x04000DCF RID: 3535
		public const int WM_USER_DEACTIVATE = 1065;

		// Token: 0x04000DD0 RID: 3536
		public const int WM_USER_LOGS_REPORTING = 1072;

		// Token: 0x04000DD1 RID: 3537
		public const int WM_NCHITTEST = 132;

		// Token: 0x04000DD2 RID: 3538
		public const int WM_MOUSEMOVE = 512;

		// Token: 0x04000DD3 RID: 3539
		public const int WM_MOUSEWHEEL = 522;

		// Token: 0x04000DD4 RID: 3540
		public const int WM_RBUTTONDOWN = 516;

		// Token: 0x04000DD5 RID: 3541
		public const int WM_RBUTTONUP = 517;

		// Token: 0x04000DD6 RID: 3542
		public const int WM_LBUTTONDOWN = 513;

		// Token: 0x04000DD7 RID: 3543
		public const int WM_LBUTTONUP = 514;

		// Token: 0x04000DD8 RID: 3544
		public const int WM_MBUTTONDOWN = 519;

		// Token: 0x04000DD9 RID: 3545
		public const int WM_MBUTTONUP = 520;

		// Token: 0x04000DDA RID: 3546
		public const int WM_XBUTTONDOWN = 523;

		// Token: 0x04000DDB RID: 3547
		public const int WM_XBUTTONUP = 524;

		// Token: 0x04000DDC RID: 3548
		public const int WM_LBUTTONDBLCLK = 515;

		// Token: 0x04000DDD RID: 3549
		public const int WM_DISPLAYCHANGE = 126;

		// Token: 0x04000DDE RID: 3550
		public const int WM_INPUTLANGCHANGEREQUEST = 80;

		// Token: 0x04000DDF RID: 3551
		public const int WM_IME_ENDCOMPOSITION = 270;

		// Token: 0x04000DE0 RID: 3552
		public const int WM_IME_COMPOSITION = 271;

		// Token: 0x04000DE1 RID: 3553
		public const int WM_IME_CHAR = 646;

		// Token: 0x04000DE2 RID: 3554
		public const int WM_CHAR = 258;

		// Token: 0x04000DE3 RID: 3555
		public const int WM_IME_NOTIFY = 642;

		// Token: 0x04000DE4 RID: 3556
		public const int WM_NCLBUTTONDOWN = 161;

		// Token: 0x04000DE5 RID: 3557
		public const int HT_CAPTION = 2;

		// Token: 0x04000DE6 RID: 3558
		public const int WM_IME_SETCONTEXT = 641;

		// Token: 0x04000DE7 RID: 3559
		public const int WM_USER_TROUBLESHOOT_STUCK_AT_LOADING = 1088;

		// Token: 0x04000DE8 RID: 3560
		public const int WM_USER_TROUBLESHOOT_BLACK_SCREEN = 1089;

		// Token: 0x04000DE9 RID: 3561
		public const int WM_USER_TROUBLESHOOT_RPC = 1090;

		// Token: 0x04000DEA RID: 3562
		public const int WM_SYSKEYDOWN = 260;

		// Token: 0x04000DEB RID: 3563
		public const int WM_SYSCHAR = 262;

		// Token: 0x04000DEC RID: 3564
		public const int VK_MENU = 18;

		// Token: 0x04000DED RID: 3565
		public const int VK_F10 = 121;

		// Token: 0x04000DEE RID: 3566
		public const int VK_SPACE = 32;

		// Token: 0x04000DEF RID: 3567
		public const int GWL_EXSTYLE = -20;

		// Token: 0x04000DF0 RID: 3568
		public const int WS_EX_TOOLWINDOW = 128;

		// Token: 0x04000DF1 RID: 3569
		public const int WS_EX_APPWINDOW = 262144;

		// Token: 0x04000DF2 RID: 3570
		public const int WS_EX_TOPMOST = 8;

		// Token: 0x04000DF3 RID: 3571
		public const int CHINESE_SIMPLIFIED_LANG_DECIMALVALUE = 2052;

		// Token: 0x04000DF4 RID: 3572
		private const int GCS_COMPSTR = 8;

		// Token: 0x04000DF5 RID: 3573
		public const int WM_COPYDATA = 74;

		// Token: 0x04000DF6 RID: 3574
		public const int SC_KEYMENU = 61696;

		// Token: 0x04000DF7 RID: 3575
		public const int SC_MAXIMIZE = 61488;

		// Token: 0x04000DF8 RID: 3576
		public const int SC_MAXIMIZE2 = 61490;

		// Token: 0x04000DF9 RID: 3577
		public const int SC_RESTORE = 61728;

		// Token: 0x04000DFA RID: 3578
		public const int SC_RESTORE2 = 61730;

		// Token: 0x04000DFB RID: 3579
		public const int WM_SYSCOMMAND = 274;

		// Token: 0x04000DFC RID: 3580
		public const int WM_ERASEBKGND = 20;

		// Token: 0x04000DFD RID: 3581
		public const int SM_CXSCREEN = 0;

		// Token: 0x04000DFE RID: 3582
		public const int SM_CYSCREEN = 1;

		// Token: 0x04000DFF RID: 3583
		public const int SWP_ASYNCWINDOWPOS = 16384;

		// Token: 0x04000E00 RID: 3584
		public const int SWP_DEFERERASE = 8192;

		// Token: 0x04000E01 RID: 3585
		public const int SWP_DRAWFRAME = 32;

		// Token: 0x04000E02 RID: 3586
		public const int SWP_FRAMECHANGED = 32;

		// Token: 0x04000E03 RID: 3587
		public const int SWP_HIDEWINDOW = 128;

		// Token: 0x04000E04 RID: 3588
		public const int SWP_NOACTIVATE = 16;

		// Token: 0x04000E05 RID: 3589
		public const int SWP_NOCOPYBITS = 256;

		// Token: 0x04000E06 RID: 3590
		public const int SWP_NOMOVE = 2;

		// Token: 0x04000E07 RID: 3591
		public const int SWP_NOOWNERZORDER = 512;

		// Token: 0x04000E08 RID: 3592
		public const int SWP_NOREDRAW = 8;

		// Token: 0x04000E09 RID: 3593
		public const int SWP_NOREPOSITION = 512;

		// Token: 0x04000E0A RID: 3594
		public const int SWP_NOSENDCHANGING = 1024;

		// Token: 0x04000E0B RID: 3595
		public const int SWP_NOSIZE = 1;

		// Token: 0x04000E0C RID: 3596
		public const int SWP_NOZORDER = 4;

		// Token: 0x04000E0D RID: 3597
		public const int SWP_SHOWWINDOW = 64;

		// Token: 0x04000E0E RID: 3598
		public const int WS_OVERLAPPED = 0;

		// Token: 0x04000E0F RID: 3599
		public const int WS_CAPTION = 12582912;

		// Token: 0x04000E10 RID: 3600
		public const int WS_SYSMENU = 524288;

		// Token: 0x04000E11 RID: 3601
		public const int WS_THICKFRAME = 262144;

		// Token: 0x04000E12 RID: 3602
		public const int WS_MINIMIZEBOX = 131072;

		// Token: 0x04000E13 RID: 3603
		public const int WS_MAXIMIZEBOX = 65536;

		// Token: 0x04000E14 RID: 3604
		public const int WS_OVERLAPPEDWINDOW = 13565952;

		// Token: 0x04000E15 RID: 3605
		public const int WS_EX_TRANSPARENT = 32;

		// Token: 0x04000E16 RID: 3606
		private const int LOGPIXELSX = 88;

		// Token: 0x04000E17 RID: 3607
		public const int WM_SETREDRAW = 11;

		// Token: 0x04000E18 RID: 3608
		public static readonly IntPtr HWND_TOP = IntPtr.Zero;

		// Token: 0x04000E19 RID: 3609
		public const int SW_HIDE = 0;

		// Token: 0x04000E1A RID: 3610
		public const int SW_SHOWMAXIMIZED = 3;

		// Token: 0x04000E1B RID: 3611
		public const int SW_SHOW = 5;

		// Token: 0x04000E1C RID: 3612
		public const int SW_MINIMIZE = 6;

		// Token: 0x04000E1D RID: 3613
		public const int SW_SHOWNA = 8;

		// Token: 0x04000E1E RID: 3614
		public const int SW_RESTORE = 9;

		// Token: 0x04000E1F RID: 3615
		public const int SW_SHOWNORMAL = 1;

		// Token: 0x04000E20 RID: 3616
		public const int GWL_STYLE = -16;

		// Token: 0x04000E21 RID: 3617
		public const uint WS_POPUP = 2147483648U;

		// Token: 0x04000E22 RID: 3618
		public const uint WS_CHILD = 1073741824U;

		// Token: 0x04000E23 RID: 3619
		public const uint WS_DISABLED = 134217728U;

		// Token: 0x04000E24 RID: 3620
		private const int KL_NAMELENGTH = 9;
	}
}
