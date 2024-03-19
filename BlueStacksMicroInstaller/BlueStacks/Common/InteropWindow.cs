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
	// Token: 0x02000088 RID: 136
	public static class InteropWindow
	{
		// Token: 0x06000233 RID: 563
		[DllImport("user32.dll")]
		public static extern uint MapVirtualKey(uint uCode, uint uMapType);

		// Token: 0x06000234 RID: 564
		[DllImport("user32.dll")]
		public static extern IntPtr GetKeyboardLayout(uint idThread);

		// Token: 0x06000235 RID: 565
		[DllImport("user32.dll")]
		public static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);

		// Token: 0x06000236 RID: 566 RVA: 0x0000B99C File Offset: 0x00009B9C
		public static IntPtr GetHwnd(Popup popup)
		{
			HwndSource hwndSource = (HwndSource)PresentationSource.FromVisual((popup != null) ? popup.Child : null);
			return hwndSource.Handle;
		}

		// Token: 0x06000237 RID: 567
		[DllImport("user32.dll")]
		public static extern IntPtr SetFocus(IntPtr hWnd);

		// Token: 0x06000238 RID: 568
		[DllImport("imm32.dll")]
		public static extern bool ImmSetOpenStatus(IntPtr hIMC, bool open);

		// Token: 0x06000239 RID: 569
		[DllImport("Imm32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr ImmGetContext(IntPtr hWnd);

		// Token: 0x0600023A RID: 570
		[DllImport("Imm32.dll")]
		public static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

		// Token: 0x0600023B RID: 571
		[DllImport("Imm32.dll", CharSet = CharSet.Unicode)]
		private static extern int ImmGetCompositionStringW(IntPtr hIMC, int dwIndex, byte[] lpBuf, int dwBufLen);

		// Token: 0x0600023C RID: 572
		[DllImport("imm32.dll")]
		public static extern bool ImmSetCompositionWindow(IntPtr hIMC, out COMPOSITIONFORM lpptPos);

		// Token: 0x0600023D RID: 573
		[DllImport("user32.dll")]
		public static extern int GetSystemMetrics(int which);

		// Token: 0x0600023E RID: 574
		[DllImport("user32.dll")]
		public static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int w, int h, uint flags);

		// Token: 0x0600023F RID: 575
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool AdjustWindowRect(out RECT lpRect, int dwStyle, bool bMenu);

		// Token: 0x06000240 RID: 576
		[DllImport("user32.dll")]
		public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		// Token: 0x06000241 RID: 577
		[DllImport("gdi32.dll")]
		private static extern IntPtr CreateDC(string driver, string name, string output, IntPtr mode);

		// Token: 0x06000242 RID: 578
		[DllImport("gdi32.dll")]
		private static extern bool DeleteDC(IntPtr hdc);

		// Token: 0x06000243 RID: 579
		[DllImport("gdi32.dll")]
		private static extern int GetDeviceCaps(IntPtr hdc, int index);

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000B9CB File Offset: 0x00009BCB
		public static int ScreenWidth
		{
			get
			{
				return InteropWindow.GetSystemMetrics(0);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000B9D3 File Offset: 0x00009BD3
		public static int ScreenHeight
		{
			get
			{
				return InteropWindow.GetSystemMetrics(1);
			}
		}

		// Token: 0x06000246 RID: 582
		[DllImport("user32.dll")]
		public static extern bool HideCaret(IntPtr hWnd);

		// Token: 0x06000247 RID: 583
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

		// Token: 0x06000248 RID: 584
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000249 RID: 585
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYGAMEPADDATASTRUCT cds);

		// Token: 0x0600024A RID: 586
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr FindWindow(string cls, string name);

		// Token: 0x0600024B RID: 587
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetForegroundWindow(IntPtr handle);

		// Token: 0x0600024C RID: 588
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr handle, int cmd);

		// Token: 0x0600024D RID: 589
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestorFlags flags);

		// Token: 0x0600024E RID: 590
		[DllImport("user32.dll")]
		public static extern IntPtr GetParent(IntPtr handle);

		// Token: 0x0600024F RID: 591
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		// Token: 0x06000250 RID: 592
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		// Token: 0x06000251 RID: 593
		[DllImport("kernel32.dll")]
		public static extern bool FreeConsole();

		// Token: 0x06000252 RID: 594
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		// Token: 0x06000253 RID: 595
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWindow(IntPtr hWnd);

		// Token: 0x06000254 RID: 596
		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		// Token: 0x06000255 RID: 597
		[DllImport("user32.dll")]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint ProcessId);

		// Token: 0x06000256 RID: 598
		[DllImport("kernel32.dll")]
		public static extern uint GetCurrentThreadId();

		// Token: 0x06000257 RID: 599
		[DllImport("user32.dll")]
		public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

		// Token: 0x06000258 RID: 600
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		// Token: 0x06000259 RID: 601
		[DllImport("user32.dll")]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		// Token: 0x0600025A RID: 602
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		// Token: 0x0600025B RID: 603
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x0600025C RID: 604
		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hwnd, ref RECT rect);

		// Token: 0x0600025D RID: 605
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool EnableWindow(IntPtr hwnd, bool enable);

		// Token: 0x0600025E RID: 606 RVA: 0x0000B9DC File Offset: 0x00009BDC
		public static IntPtr MinimizeWindow(string name)
		{
			IntPtr intPtr = InteropWindow.FindWindow(null, name);
			bool flag = intPtr == IntPtr.Zero;
			if (flag)
			{
				throw new SystemException("Cannot find window '" + name + "'", new Win32Exception(Marshal.GetLastWin32Error()));
			}
			InteropWindow.ShowWindow(intPtr, 6);
			return intPtr;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000BA30 File Offset: 0x00009C30
		public static IntPtr BringWindowToFront(string name, bool _, bool isRetoreMinimizedWindow = false)
		{
			IntPtr intPtr = InteropWindow.FindWindow(null, name);
			bool flag = intPtr == IntPtr.Zero;
			if (flag)
			{
				throw new SystemException("Cannot find window '" + name + "'", new Win32Exception(Marshal.GetLastWin32Error()));
			}
			bool flag2 = !InteropWindow.SetForegroundWindow(intPtr);
			if (flag2)
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

		// Token: 0x06000260 RID: 608 RVA: 0x0000BABC File Offset: 0x00009CBC
		public static void RemoveWindowFromAltTabUI(IntPtr handle)
		{
			int windowLong = InteropWindow.GetWindowLong(handle, -20);
			InteropWindow.SetWindowLong(handle, -20, windowLong | 128);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		public static IntPtr GetWindowHandle(string name)
		{
			IntPtr intPtr = InteropWindow.FindWindow(null, name);
			bool flag = intPtr == IntPtr.Zero;
			if (flag)
			{
				throw new SystemException("Cannot find window '" + name + "'", new Win32Exception(Marshal.GetLastWin32Error()));
			}
			return intPtr;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000BB30 File Offset: 0x00009D30
		public static bool ForceSetForegroundWindow(IntPtr h)
		{
			bool flag = h == IntPtr.Zero;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				IntPtr foregroundWindow = InteropWindow.GetForegroundWindow();
				bool flag2 = foregroundWindow == IntPtr.Zero;
				if (flag2)
				{
					result = InteropWindow.SetForegroundWindow(h);
				}
				else
				{
					bool flag3 = h == foregroundWindow;
					if (flag3)
					{
						result = true;
					}
					else
					{
						uint num = 0U;
						uint windowThreadProcessId = InteropWindow.GetWindowThreadProcessId(foregroundWindow, ref num);
						uint currentThreadId = InteropWindow.GetCurrentThreadId();
						bool flag4 = currentThreadId == windowThreadProcessId;
						if (flag4)
						{
							result = InteropWindow.SetForegroundWindow(h);
						}
						else
						{
							bool flag5 = windowThreadProcessId > 0U;
							if (flag5)
							{
								bool flag6 = !InteropWindow.AttachThreadInput(currentThreadId, windowThreadProcessId, true);
								if (flag6)
								{
									return false;
								}
								bool flag7 = !InteropWindow.SetForegroundWindow(h);
								if (flag7)
								{
									InteropWindow.AttachThreadInput(currentThreadId, windowThreadProcessId, false);
									return false;
								}
								InteropWindow.AttachThreadInput(currentThreadId, windowThreadProcessId, false);
							}
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000BC08 File Offset: 0x00009E08
		public static int GetScreenDpi()
		{
			IntPtr intPtr = InteropWindow.CreateDC("DISPLAY", null, null, IntPtr.Zero);
			bool flag = intPtr == IntPtr.Zero;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				int num = InteropWindow.GetDeviceCaps(intPtr, 88);
				bool flag2 = num == 0;
				if (flag2)
				{
					num = 96;
				}
				InteropWindow.DeleteDC(intPtr);
				result = num;
			}
			return result;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000BC60 File Offset: 0x00009E60
		public static void SetFullScreen(IntPtr hwnd)
		{
			InteropWindow.SetFullScreen(hwnd, 0, 0, InteropWindow.ScreenWidth, InteropWindow.ScreenHeight);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000BC78 File Offset: 0x00009E78
		public static void SetFullScreen(IntPtr hwnd, int X, int Y, int cx, int cy)
		{
			bool flag = !InteropWindow.SetWindowPos(hwnd, InteropWindow.HWND_TOP, X, Y, cx, cy, 64U);
			if (flag)
			{
				throw new SystemException("Cannot call SetWindowPos()", new Win32Exception(Marshal.GetLastWin32Error()));
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000BCB8 File Offset: 0x00009EB8
		public static string CurrentCompStr(IntPtr handle)
		{
			int dwIndex = 8;
			IntPtr hIMC = InteropWindow.ImmGetContext(handle);
			string result;
			try
			{
				int num = InteropWindow.ImmGetCompositionStringW(hIMC, dwIndex, null, 0);
				bool flag = num > 0;
				if (flag)
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

		// Token: 0x06000267 RID: 615 RVA: 0x0000BD30 File Offset: 0x00009F30
		public static IntPtr GetWindowHandle(Window window)
		{
			HwndSource hwndSource = (HwndSource)PresentationSource.FromVisual(window);
			bool flag = hwndSource != null;
			IntPtr result;
			if (flag)
			{
				result = hwndSource.Handle;
			}
			else
			{
				result = IntPtr.Zero;
			}
			return result;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000BD68 File Offset: 0x00009F68
		public static bool IsWindowTopMost(IntPtr hWnd)
		{
			int windowLong = InteropWindow.GetWindowLong(hWnd, -20);
			return (windowLong & 8) == 8;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000BD8C File Offset: 0x00009F8C
		public static Window GetTopmostOwnerWindow(Window window)
		{
			while (window.Owner != null)
			{
				window = window.Owner;
			}
			return window;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		public static int GetAForegroundApplicationProcessId()
		{
			IntPtr foregroundWindow = InteropWindow.GetForegroundWindow();
			bool flag = foregroundWindow == IntPtr.Zero;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				uint num = 0U;
				InteropWindow.GetWindowThreadProcessId(foregroundWindow, ref num);
				result = (int)num;
			}
			return result;
		}

		// Token: 0x0600026B RID: 619
		[DllImport("user32.dll")]
		private static extern long GetKeyboardLayoutName(StringBuilder pwszKLID);

		// Token: 0x0600026C RID: 620 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		private static string GetLayoutCode()
		{
			StringBuilder stringBuilder = new StringBuilder(9);
			InteropWindow.GetKeyboardLayoutName(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000BE18 File Offset: 0x0000A018
		public static string MapLayoutName(string code = null)
		{
			bool flag = code == null;
			if (flag)
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

		// Token: 0x0600026E RID: 622 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		public static WindowState FindMainWindowState(Window window)
		{
			bool flag = ((window != null) ? window.Owner : null) == null;
			WindowState result;
			if (flag)
			{
				result = window.WindowState;
			}
			else
			{
				result = InteropWindow.FindMainWindowState(window.Owner);
			}
			return result;
		}

		// Token: 0x0600026F RID: 623
		[DllImport("User32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
		public static extern IntPtr LoadCursorFromFile(string str);

		// Token: 0x06000270 RID: 624
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

		// Token: 0x06000271 RID: 625
		[DllImport("user32.dll")]
		public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

		// Token: 0x06000272 RID: 626 RVA: 0x0000DE18 File Offset: 0x0000C018
		public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot, float scalingFactor)
		{
			int num = (int)(32f * scalingFactor);
			int num2 = (int)(32f * scalingFactor);
			Cursor result;
			using (Bitmap bitmap = new Bitmap(bmp, num, num2))
			{
				IntPtr intPtr = bitmap.GetHicon();
				IconInfo iconInfo = default(IconInfo);
				InteropWindow.GetIconInfo(intPtr, ref iconInfo);
				iconInfo.xHotspot = (int)((float)xHotSpot * scalingFactor * ((float)num / (float)((bmp != null) ? new int?(bmp.Width) : null).Value));
				iconInfo.yHotspot = (int)((float)yHotSpot * scalingFactor * ((float)num2 / (float)((bmp != null) ? new int?(bmp.Height) : null).Value));
				iconInfo.fIcon = false;
				intPtr = InteropWindow.CreateIconIndirect(ref iconInfo);
				result = new Cursor(intPtr);
			}
			return result;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000DEFC File Offset: 0x0000C0FC
		public static Cursor LoadCustomCursor(string path)
		{
			IntPtr intPtr = InteropWindow.LoadCursorFromFile(path);
			bool flag = intPtr == IntPtr.Zero;
			if (flag)
			{
				throw new Win32Exception();
			}
			Cursor cursor = new Cursor(intPtr);
			FieldInfo field = typeof(Cursor).GetField("ownHandle", BindingFlags.Instance | BindingFlags.NonPublic);
			field.SetValue(cursor, true);
			return cursor;
		}

		// Token: 0x04000460 RID: 1120
		public const int WM_CREATE = 1;

		// Token: 0x04000461 RID: 1121
		public const int WM_CLOSE = 16;

		// Token: 0x04000462 RID: 1122
		public const int WM_INPUT = 255;

		// Token: 0x04000463 RID: 1123
		public const int WM_USER = 1024;

		// Token: 0x04000464 RID: 1124
		public const int WM_USER_SHOW_WINDOW = 1025;

		// Token: 0x04000465 RID: 1125
		public const int WM_USER_SWITCH_TO_LAUNCHER = 1026;

		// Token: 0x04000466 RID: 1126
		public const int WM_USER_RESIZE_WINDOW = 1027;

		// Token: 0x04000467 RID: 1127
		public const int WM_USER_FE_STATE_CHANGE = 1028;

		// Token: 0x04000468 RID: 1128
		public const int WM_USER_FE_APP_DISPLAYED = 1029;

		// Token: 0x04000469 RID: 1129
		public const int WM_USER_FE_ORIENTATION_CHANGE = 1030;

		// Token: 0x0400046A RID: 1130
		public const int WM_USER_FE_RESIZE = 1031;

		// Token: 0x0400046B RID: 1131
		public const int WM_USER_INSTALL_COMPLETED = 1032;

		// Token: 0x0400046C RID: 1132
		public const int WM_USER_UNINSTALL_COMPLETED = 1033;

		// Token: 0x0400046D RID: 1133
		public const int WM_USER_APP_CRASHED = 1034;

		// Token: 0x0400046E RID: 1134
		public const int WM_USER_EXE_CRASHED = 1035;

		// Token: 0x0400046F RID: 1135
		public const int WM_USER_UPGRADE_FAILED = 1036;

		// Token: 0x04000470 RID: 1136
		public const int WM_USER_BOOT_FAILURE = 1037;

		// Token: 0x04000471 RID: 1137
		public const int WM_USER_FE_SHOOTMODE_STATE = 1038;

		// Token: 0x04000472 RID: 1138
		public const int WM_USER_TOGGLE_FULLSCREEN = 1056;

		// Token: 0x04000473 RID: 1139
		public const int WM_USER_GO_BACK = 1057;

		// Token: 0x04000474 RID: 1140
		public const int WM_USER_SHOW_GUIDANCE = 1058;

		// Token: 0x04000475 RID: 1141
		public const int WM_USER_AUDIO_MUTE = 1059;

		// Token: 0x04000476 RID: 1142
		public const int WM_USER_AUDIO_UNMUTE = 1060;

		// Token: 0x04000477 RID: 1143
		public const int WM_USER_AT_HOME = 1061;

		// Token: 0x04000478 RID: 1144
		public const int WM_USER_ACTIVATE = 1062;

		// Token: 0x04000479 RID: 1145
		public const int WM_USER_HIDE_WINDOW = 1063;

		// Token: 0x0400047A RID: 1146
		public const int WM_USER_VMX_BIT_ON = 1064;

		// Token: 0x0400047B RID: 1147
		public const int WM_USER_DEACTIVATE = 1065;

		// Token: 0x0400047C RID: 1148
		public const int WM_USER_LOGS_REPORTING = 1072;

		// Token: 0x0400047D RID: 1149
		public const int WM_NCHITTEST = 132;

		// Token: 0x0400047E RID: 1150
		public const int WM_MOUSEMOVE = 512;

		// Token: 0x0400047F RID: 1151
		public const int WM_MOUSEWHEEL = 522;

		// Token: 0x04000480 RID: 1152
		public const int WM_RBUTTONDOWN = 516;

		// Token: 0x04000481 RID: 1153
		public const int WM_RBUTTONUP = 517;

		// Token: 0x04000482 RID: 1154
		public const int WM_LBUTTONDOWN = 513;

		// Token: 0x04000483 RID: 1155
		public const int WM_LBUTTONUP = 514;

		// Token: 0x04000484 RID: 1156
		public const int WM_MBUTTONDOWN = 519;

		// Token: 0x04000485 RID: 1157
		public const int WM_MBUTTONUP = 520;

		// Token: 0x04000486 RID: 1158
		public const int WM_XBUTTONDOWN = 523;

		// Token: 0x04000487 RID: 1159
		public const int WM_XBUTTONUP = 524;

		// Token: 0x04000488 RID: 1160
		public const int WM_LBUTTONDBLCLK = 515;

		// Token: 0x04000489 RID: 1161
		public const int WM_DISPLAYCHANGE = 126;

		// Token: 0x0400048A RID: 1162
		public const int WM_INPUTLANGCHANGEREQUEST = 80;

		// Token: 0x0400048B RID: 1163
		public const int WM_IME_ENDCOMPOSITION = 270;

		// Token: 0x0400048C RID: 1164
		public const int WM_IME_COMPOSITION = 271;

		// Token: 0x0400048D RID: 1165
		public const int WM_IME_CHAR = 646;

		// Token: 0x0400048E RID: 1166
		public const int WM_CHAR = 258;

		// Token: 0x0400048F RID: 1167
		public const int WM_IME_NOTIFY = 642;

		// Token: 0x04000490 RID: 1168
		public const int WM_NCLBUTTONDOWN = 161;

		// Token: 0x04000491 RID: 1169
		public const int HT_CAPTION = 2;

		// Token: 0x04000492 RID: 1170
		public const int WM_IME_SETCONTEXT = 641;

		// Token: 0x04000493 RID: 1171
		public const int WM_USER_TROUBLESHOOT_STUCK_AT_LOADING = 1088;

		// Token: 0x04000494 RID: 1172
		public const int WM_USER_TROUBLESHOOT_BLACK_SCREEN = 1089;

		// Token: 0x04000495 RID: 1173
		public const int WM_USER_TROUBLESHOOT_RPC = 1090;

		// Token: 0x04000496 RID: 1174
		public const int WM_SYSKEYDOWN = 260;

		// Token: 0x04000497 RID: 1175
		public const int WM_SYSCHAR = 262;

		// Token: 0x04000498 RID: 1176
		public const int VK_MENU = 18;

		// Token: 0x04000499 RID: 1177
		public const int VK_F10 = 121;

		// Token: 0x0400049A RID: 1178
		public const int VK_SPACE = 32;

		// Token: 0x0400049B RID: 1179
		public const int GWL_EXSTYLE = -20;

		// Token: 0x0400049C RID: 1180
		public const int WS_EX_TOOLWINDOW = 128;

		// Token: 0x0400049D RID: 1181
		public const int WS_EX_APPWINDOW = 262144;

		// Token: 0x0400049E RID: 1182
		public const int WS_EX_TOPMOST = 8;

		// Token: 0x0400049F RID: 1183
		public const int CHINESE_SIMPLIFIED_LANG_DECIMALVALUE = 2052;

		// Token: 0x040004A0 RID: 1184
		private const int GCS_COMPSTR = 8;

		// Token: 0x040004A1 RID: 1185
		public const int WM_COPYDATA = 74;

		// Token: 0x040004A2 RID: 1186
		public const int SC_KEYMENU = 61696;

		// Token: 0x040004A3 RID: 1187
		public const int SC_MAXIMIZE = 61488;

		// Token: 0x040004A4 RID: 1188
		public const int SC_MAXIMIZE2 = 61490;

		// Token: 0x040004A5 RID: 1189
		public const int SC_RESTORE = 61728;

		// Token: 0x040004A6 RID: 1190
		public const int SC_RESTORE2 = 61730;

		// Token: 0x040004A7 RID: 1191
		public const int WM_SYSCOMMAND = 274;

		// Token: 0x040004A8 RID: 1192
		public const int WM_ERASEBKGND = 20;

		// Token: 0x040004A9 RID: 1193
		public const int SM_CXSCREEN = 0;

		// Token: 0x040004AA RID: 1194
		public const int SM_CYSCREEN = 1;

		// Token: 0x040004AB RID: 1195
		public const int SWP_ASYNCWINDOWPOS = 16384;

		// Token: 0x040004AC RID: 1196
		public const int SWP_DEFERERASE = 8192;

		// Token: 0x040004AD RID: 1197
		public const int SWP_DRAWFRAME = 32;

		// Token: 0x040004AE RID: 1198
		public const int SWP_FRAMECHANGED = 32;

		// Token: 0x040004AF RID: 1199
		public const int SWP_HIDEWINDOW = 128;

		// Token: 0x040004B0 RID: 1200
		public const int SWP_NOACTIVATE = 16;

		// Token: 0x040004B1 RID: 1201
		public const int SWP_NOCOPYBITS = 256;

		// Token: 0x040004B2 RID: 1202
		public const int SWP_NOMOVE = 2;

		// Token: 0x040004B3 RID: 1203
		public const int SWP_NOOWNERZORDER = 512;

		// Token: 0x040004B4 RID: 1204
		public const int SWP_NOREDRAW = 8;

		// Token: 0x040004B5 RID: 1205
		public const int SWP_NOREPOSITION = 512;

		// Token: 0x040004B6 RID: 1206
		public const int SWP_NOSENDCHANGING = 1024;

		// Token: 0x040004B7 RID: 1207
		public const int SWP_NOSIZE = 1;

		// Token: 0x040004B8 RID: 1208
		public const int SWP_NOZORDER = 4;

		// Token: 0x040004B9 RID: 1209
		public const int SWP_SHOWWINDOW = 64;

		// Token: 0x040004BA RID: 1210
		public const int WS_OVERLAPPED = 0;

		// Token: 0x040004BB RID: 1211
		public const int WS_CAPTION = 12582912;

		// Token: 0x040004BC RID: 1212
		public const int WS_SYSMENU = 524288;

		// Token: 0x040004BD RID: 1213
		public const int WS_THICKFRAME = 262144;

		// Token: 0x040004BE RID: 1214
		public const int WS_MINIMIZEBOX = 131072;

		// Token: 0x040004BF RID: 1215
		public const int WS_MAXIMIZEBOX = 65536;

		// Token: 0x040004C0 RID: 1216
		public const int WS_OVERLAPPEDWINDOW = 13565952;

		// Token: 0x040004C1 RID: 1217
		public const int WS_EX_TRANSPARENT = 32;

		// Token: 0x040004C2 RID: 1218
		private const int LOGPIXELSX = 88;

		// Token: 0x040004C3 RID: 1219
		public const int WM_SETREDRAW = 11;

		// Token: 0x040004C4 RID: 1220
		public static readonly IntPtr HWND_TOP = IntPtr.Zero;

		// Token: 0x040004C5 RID: 1221
		public const int SW_HIDE = 0;

		// Token: 0x040004C6 RID: 1222
		public const int SW_SHOWMAXIMIZED = 3;

		// Token: 0x040004C7 RID: 1223
		public const int SW_SHOW = 5;

		// Token: 0x040004C8 RID: 1224
		public const int SW_MINIMIZE = 6;

		// Token: 0x040004C9 RID: 1225
		public const int SW_SHOWNA = 8;

		// Token: 0x040004CA RID: 1226
		public const int SW_RESTORE = 9;

		// Token: 0x040004CB RID: 1227
		public const int SW_SHOWNORMAL = 1;

		// Token: 0x040004CC RID: 1228
		public const int GWL_STYLE = -16;

		// Token: 0x040004CD RID: 1229
		public const uint WS_POPUP = 2147483648U;

		// Token: 0x040004CE RID: 1230
		public const uint WS_CHILD = 1073741824U;

		// Token: 0x040004CF RID: 1231
		public const uint WS_DISABLED = 134217728U;

		// Token: 0x040004D0 RID: 1232
		private const int KL_NAMELENGTH = 9;
	}
}
