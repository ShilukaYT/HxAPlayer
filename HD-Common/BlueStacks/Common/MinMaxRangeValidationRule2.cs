using System;
using System.Globalization;
using System.Windows.Controls;

namespace BlueStacks.Common
{
	// Token: 0x02000069 RID: 105
	public class MinMaxRangeValidationRule2 : ValidationRule
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00003311 File Offset: 0x00001511
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00003319 File Offset: 0x00001519
		public Wrapper Wrapper { get; set; }

		// Token: 0x06000259 RID: 601 RVA: 0x00013374 File Offset: 0x00011574
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (value == null)
			{
				return new ValidationResult(false, "Illegal characters");
			}
			if (string.IsNullOrEmpty(value.ToString()) || int.Parse(value.ToString(), CultureInfo.InvariantCulture) < this.Wrapper.Min || int.Parse(value.ToString(), CultureInfo.InvariantCulture) > this.Wrapper.Max)
			{
				return new ValidationResult(false, this.Wrapper.ErrorMessage);
			}
			return ValidationResult.ValidResult;
		}
	}
}
