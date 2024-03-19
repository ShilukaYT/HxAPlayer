using System;

namespace BlueStacks.Common
{
	// Token: 0x0200007B RID: 123
	public enum MultiInstanceErrorCodesEnum
	{
		// Token: 0x040003F9 RID: 1017
		ReachedMaxLimit = -1,
		// Token: 0x040003FA RID: 1018
		CloneVmFailure = -2,
		// Token: 0x040003FB RID: 1019
		RegistryCopyFailure = -3,
		// Token: 0x040003FC RID: 1020
		CreateServiceFailure = -4,
		// Token: 0x040003FD RID: 1021
		UnknownException = -5,
		// Token: 0x040003FE RID: 1022
		CommandNotFound = -6,
		// Token: 0x040003FF RID: 1023
		VmNameNotValid = -7,
		// Token: 0x04000400 RID: 1024
		VmNotExist = -8,
		// Token: 0x04000401 RID: 1025
		VmNotRunning = -9,
		// Token: 0x04000402 RID: 1026
		CannotDeleteDefaultVm = -10,
		// Token: 0x04000403 RID: 1027
		NotSupportedInLegacyAndRawMode = -11,
		// Token: 0x04000404 RID: 1028
		VirtualBoxInitFailed = -12,
		// Token: 0x04000405 RID: 1029
		NotSupportedInLegacyMode = -13,
		// Token: 0x04000406 RID: 1030
		WrongValue = -14,
		// Token: 0x04000407 RID: 1031
		DeviceCapsNotPresent = -15,
		// Token: 0x04000408 RID: 1032
		FactoryResetUnHandledException = -16,
		// Token: 0x04000409 RID: 1033
		ProcessAlreadyRunning = -17,
		// Token: 0x0400040A RID: 1034
		InvalidVmType = -18,
		// Token: 0x0400040B RID: 1035
		CannotCloneRunningVm = -19,
		// Token: 0x0400040C RID: 1036
		ErrorInRemovingDisk = -20,
		// Token: 0x0400040D RID: 1037
		ErrorInResettingSharedFolders = -21
	}
}
