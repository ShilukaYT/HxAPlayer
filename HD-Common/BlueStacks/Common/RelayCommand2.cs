using System;
using System.Windows.Input;

namespace BlueStacks.Common
{
	// Token: 0x0200006B RID: 107
	public class RelayCommand2 : ICommand
	{
		// Token: 0x06000262 RID: 610 RVA: 0x000033DE File Offset: 0x000015DE
		public RelayCommand2(Action<object> execute)
		{
			this.canExecute = ((object obj) => true);
			this.execute = execute;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00003412 File Offset: 0x00001612
		public RelayCommand2(Func<object, bool> canExecute, Action<object> execute)
		{
			this.canExecute = canExecute;
			this.execute = execute;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00003428 File Offset: 0x00001628
		public bool CanExecute(object parameter)
		{
			return this.canExecute != null && this.canExecute(parameter);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00003440 File Offset: 0x00001640
		public void Execute(object parameter)
		{
			Action<object> action = this.execute;
			if (action == null)
			{
				return;
			}
			action(parameter);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000266 RID: 614 RVA: 0x00003453 File Offset: 0x00001653
		// (remove) Token: 0x06000267 RID: 615 RVA: 0x0000345B File Offset: 0x0000165B
		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		// Token: 0x0400011B RID: 283
		private readonly Func<object, bool> canExecute;

		// Token: 0x0400011C RID: 284
		private readonly Action<object> execute;
	}
}
