using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace BlueStacks.Common
{
	// Token: 0x02000178 RID: 376
	[Serializable]
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000E57 RID: 3671 RVA: 0x00038A10 File Offset: 0x00036C10
		// (remove) Token: 0x06000E58 RID: 3672 RVA: 0x00038A48 File Offset: 0x00036C48
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000E59 RID: 3673 RVA: 0x0000CEE6 File Offset: 0x0000B0E6
		protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
		{
			if (!EqualityComparer<T>.Default.Equals(field, newValue))
			{
				field = newValue;
				this.NotifyPropertyChanged(propertyName);
				return true;
			}
			return false;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0000CF0C File Offset: 0x0000B10C
		protected void NotifyPropertyChanged(string name)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(name));
			}
			CommandManager.InvalidateRequerySuggested();
		}
	}
}
