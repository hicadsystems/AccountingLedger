using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NavyAccountCore.Core.Entities
{
    public class npf_navip
    {
    [Key]
	public int  Id { get; set; }
	[StringLength(30)]
	public string Batch { get; set; }
	[StringLength(30)]
	public string svcno   {get; set;}
	public int rank  {get; set;}
	public DateTime reviewdate1  {get; set;}
	public DateTime reviewdate2  {get; set;}
	public decimal surval  {get; set;}
	public int smonth {get; set;}
	public DateTime calcdate1  {get; set;}
	public DateTime caldate2 {get; set;}
	public string beneficiary { get; set; }
	public int bank { get; set; }
	public int acctno { get; set; }
	public int title { get; set; }
	public DateTime datecreated  {get; set;}
	public string createdby { get; set; }

	}
}
